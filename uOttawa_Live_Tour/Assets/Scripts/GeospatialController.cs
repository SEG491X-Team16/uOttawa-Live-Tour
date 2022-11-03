//-----------------------------------------------------------------------
// Adapted from Google's Geospatial API ARCore Extensions sample
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace Google.XR.ARCoreExtensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.Android;
    using UnityEngine.XR.ARFoundation;
    using UnityEngine.XR.ARSubsystems;


    public class GeospatialController : MonoBehaviour
    {
        [Header("AR Components")]

        /// <summary>
        /// The active ARSessionOrigin used in the scene.
        /// </summary>
        public ARSessionOrigin SessionOrigin;

        /// <summary>
        /// The ARSession used in the scene.
        /// </summary>
        public ARSession Session;

        /// <summary>
        /// The active ARRaycastManager used in the scene.
        /// </summary>
        public ARRaycastManager RaycastManager;

        /// <summary>
        /// The AREarthManager used in the scene.
        /// </summary>
        public AREarthManager EarthManager;

        /// <summary>
        /// The ARCoreExtensions used in the scene.
        /// </summary>
        public ARCoreExtensions ARCoreExtensions;

        [Header("UI")]

        /// <summary>
        /// The home page to choose entering hosting or resolving work flow.
        /// </summary>
        public GameObject HomePage;

        /// <summary>
        /// The AR screen which displays the AR view, hosts or resolves cloud anchors,
        /// and returns to home page.
        /// </summary>
        public GameObject ARView;

        /// <summary>
        /// The timeout period waiting for localization to be completed.
        /// </summary>
        private const float _timeoutSeconds = 180;

        /// <summary>
        /// Accuracy threshold for heading degree that can be treated as localization completed.
        /// </summary>
        private const double _headingAccuracyThreshold = 25;

        /// <summary>
        /// Accuracy threshold for altitude and longitude that can be treated as localization
        /// completed.
        /// </summary>
        private const double _horizontalAccuracyThreshold = 20;        

        private bool _isReturning = false;
        private bool _isInARView = false;
        private bool _isLocalizing = false;
        private bool _waitingForLocationService = false;
        private bool _enablingGeospatial = false;
        private float _localizationPassedTime = 0f;
        private float _configurePrepareTime = 3f;
    
        private IEnumerator _startLocationService = null;
        private IEnumerator _asyncCheck = null;


        /// <summary>
        /// The Unity Awake() method.     
        //  Awake is called before the first frame update
        /// </summary>
        public void Awake()
        {
            // Lock screen to portrait.
            Screen.autorotateToLandscapeLeft = false;
            Screen.autorotateToLandscapeRight = false;
            Screen.autorotateToPortraitUpsideDown = false;
            Screen.orientation = ScreenOrientation.Portrait;

            // Target 60 fps camera capture on supported devices
            Application.targetFrameRate = 60;

            // Check AR Configuration
            if (SessionOrigin == null)
            {
                Debug.LogError("Cannot find ARSessionOrigin.");
            }

            if (Session == null)
            {
                Debug.LogError("Cannot find ARSession.");
            }

            if (ARCoreExtensions == null)
            {
                Debug.LogError("Cannot find ARCoreExtensions.");
            }

            Debug.Log("Geospatial controller awake!");
        }

        /// <summary>
        /// Unity's OnEnable() method.
        /// </summary>
        public void OnEnable()
        {
            _startLocationService = StartLocationService();
            StartCoroutine(_startLocationService);
            _isReturning = false;
            _enablingGeospatial = false;
            _localizationPassedTime = 0f;
            _isLocalizing = true;

            SwitchToHomePage();
        }

        /// <summary>
        /// Unity's OnDisable() method.
        /// </summary>
        public void OnDisable()
        {
            StopCoroutine(_asyncCheck);
            _asyncCheck = null;
            StopCoroutine(_startLocationService);
            _startLocationService = null;
            Debug.Log("Stop location services.");
            Input.location.Stop();
        }


        /// <summary>
        /// The Unity Update() method.
        // Update is called once per frame.
        /// </summary>
        public void Update()
        {

            // No need for geospatial api on the home page
            if (!_isInARView)
            {
                return;
            }

            // On home page, pressing 'back' button quits the app.
            // Otherwise, returns to home page.
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (HomePage.activeSelf)
                {
                    Application.Quit();
                }
                else
                {
                    SwitchToHomePage();
                    _isInARView = false;
                }
            }

            // Check AR session error status
            // LifecycleUpdate();
            if (_isReturning)
            {
                return;
            }

            if (ARSession.state != ARSessionState.SessionInitializing &&
                ARSession.state != ARSessionState.SessionTracking)
            {
                return;
            }

            // Enable Geospatial API
            // Warning: we skips device support check for simplicity
            var featureSupport = EarthManager.IsGeospatialModeSupported(GeospatialMode.Enabled);
            if (ARCoreExtensions.ARCoreExtensionsConfig.GeospatialMode ==
                GeospatialMode.Disabled)
            {
                Debug.Log("Geospatial sample switched to GeospatialMode.Enabled.");
                ARCoreExtensions.ARCoreExtensionsConfig.GeospatialMode =
                    GeospatialMode.Enabled;
                _configurePrepareTime = 3.0f;
                _enablingGeospatial = true;
                return;
            }

            // Wait for geospatial configuration to take effect
            if (_enablingGeospatial)
            {
                _configurePrepareTime -= Time.deltaTime;
                if (_configurePrepareTime < 0)
                {
                    _enablingGeospatial = false;
                }
                else
                {
                    return;
                }
            }

            // Ensure Earth state is valid            
            var earthState = EarthManager.EarthState;
            if (earthState == EarthState.ErrorEarthNotReady)
            {
                return;
            }
            else if (earthState != EarthState.Enabled)
            {
                string errorMessage =
                    "Geospatial sample encountered an EarthState error: " + earthState;
                Debug.LogWarning(errorMessage);
                return;
            }

            // Check earth localisation
            bool isSessionReady = ARSession.state == ARSessionState.SessionTracking &&
                Input.location.status == LocationServiceStatus.Running;
            var earthTrackingState = EarthManager.EarthTrackingState;
            var pose = earthTrackingState == TrackingState.Tracking ?
                EarthManager.CameraGeospatialPose : new GeospatialPose();
            if (!isSessionReady || earthTrackingState != TrackingState.Tracking ||
                pose.HeadingAccuracy > _headingAccuracyThreshold ||
                pose.HorizontalAccuracy > _horizontalAccuracyThreshold)
            {
                // Lost localization during the session.
                if (!_isLocalizing)
                {
                    _isLocalizing = true;
                    _localizationPassedTime = 0f;
                }

                if (_localizationPassedTime > _timeoutSeconds)
                {
                    Debug.LogError("Geospatial sample localization passed timeout.");
                    _isReturning = true;
                    Application.Quit();                }
                else
                {
                    _localizationPassedTime += Time.deltaTime;
                }
            }
            else if (_isLocalizing)
            {
                // Finished localization.
                _isLocalizing = false;
                _localizationPassedTime = 0f;
            }

        }

        /// <summary> 
        /// Gets the GeospatialPose for the camera in the latest frame, describing the geodedic 
        /// position of the device (location, elevation, and compass heading relative to Earth).
        /// The position of the pose is located at the device's camera, while the orientation closely 
        /// approximates the orientation of the display.
        /// </summary>
        public GeospatialPose getCameraGeospatialPose() {
            var earthTrackingState = EarthManager.EarthTrackingState;
            var pose = earthTrackingState == TrackingState.Tracking ?
                EarthManager.CameraGeospatialPose : new GeospatialPose();
            return pose;
        }

        /// <summary>
        /// Gets the tracking state of Earth for the latest frame (i.e how well the device is 
        /// tracling its position). Can be Limited, None, or Tracking.
        /// </summary>
        public TrackingState getEarthTrackingState() {
            var earthTrackingState = EarthManager.EarthTrackingState;
            return earthTrackingState;
        }
        
        /// <summary>
        /// Switch to home page, and disable all other screens.
        /// </summary>
        public void SwitchToHomePage()
        {
            ResetAllViews();

            HomePage.SetActive(true);
        }

        /// <summary>
        /// Callback handling "Begin to resolve" button click event in Home Page.
        /// </summary>
        public void OnBeginButtonClicked()
        {
            SwitchToARView();        
        }

        /// <summary>
        /// Switch to AR view and disable all other screens.
        /// </summary>
        public void SwitchToARView()
        {            
            _isInARView = true;
    
            ResetAllViews();
            ARView.SetActive(true);
            SetPlatformActive(true);

            _asyncCheck = AvailabilityCheck();
            StartCoroutine(_asyncCheck);  
        }

        private void ResetAllViews()
        {
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
            SetPlatformActive(false);
            ARView.SetActive(false);
            HomePage.SetActive(false);
        }

        private void SetPlatformActive(bool active)
        {
            SessionOrigin.gameObject.SetActive(active);
            Session.gameObject.SetActive(active);
            ARCoreExtensions.gameObject.SetActive(active);
        }

        /// <summary>
        /// Monitor ARSession state and quit the app when there are errors
        /// </summary>
        private void LifecycleUpdate()
        {
            // Pressing 'back' button quits the app (phone's back not app's)
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Application.Quit();
            }

            if (_isReturning)
            {
                return;
            }

            // Only allow the screen to sleep when not tracking.
            var sleepTimeout = SleepTimeout.NeverSleep;
            if (ARSession.state != ARSessionState.SessionTracking)
            {
                sleepTimeout = SleepTimeout.SystemSetting;
            }

            Screen.sleepTimeout = sleepTimeout;

            // Quit the app if ARSession is in an error status.
            string returningReason = string.Empty;
            if (ARSession.state != ARSessionState.CheckingAvailability &&
                ARSession.state != ARSessionState.Ready &&
                ARSession.state != ARSessionState.SessionInitializing &&
                ARSession.state != ARSessionState.SessionTracking)
            {
                returningReason = string.Format(
                    "Geospatial sample encountered an ARSession error state {0}.\n" +
                    "Please start the app again.",
                    ARSession.state);
            }
            else if (Input.location.status == LocationServiceStatus.Failed)
            {
                returningReason =
                    "Geospatial sample failed to start location service.\n" +
                    "Please start the app again and grant precise location permission.";
            }
            else if (SessionOrigin == null || Session == null || ARCoreExtensions == null)
            {
                returningReason = string.Format(
                    "Geospatial sample failed with missing AR Components.");
            }

            if (!string.IsNullOrEmpty(returningReason)) {
                Debug.LogError(returningReason);
                _isReturning = true;
                Application.Quit();
            }
        }

        private IEnumerator StartLocationService()
        {
            _waitingForLocationService = true;

            #if UNITY_ANDROID
            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                Debug.Log("Requesting fine location permission.");
                Permission.RequestUserPermission(Permission.FineLocation);
                yield return new WaitForSeconds(3.0f);
            }
            #endif

            if (!Input.location.isEnabledByUser)
            {
                Debug.Log("Location service is disabled by User.");
                _waitingForLocationService = false;
                yield break;
            }

            Debug.Log("Start location service.");
            Input.location.Start();

            while (Input.location.status == LocationServiceStatus.Initializing)
            {
                yield return null;
            }

            _waitingForLocationService = false;
            if (Input.location.status != LocationServiceStatus.Running)
            {
                Debug.LogWarningFormat(
                    "Location service ends with {0} status.", Input.location.status);
                Input.location.Stop();
            }
        } 

        /// <summary>
        /// Check availability of positioning systems (the camera and Google's VPS)
        /// </summary>
        private IEnumerator AvailabilityCheck()
        {
            if (ARSession.state == ARSessionState.None)
            {
                yield return ARSession.CheckAvailability();
            }

            // Waiting for ARSessionState.CheckingAvailability.
            yield return null;

            if (ARSession.state == ARSessionState.NeedsInstall)
            {
                yield return ARSession.Install();
            }

            // Waiting for ARSessionState.Installing.
            yield return null;

            #if UNITY_ANDROID
            if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
            {
                Debug.Log("Requesting camera permission.");
                Permission.RequestUserPermission(Permission.Camera);
                yield return new WaitForSeconds(3.0f);
            }

            if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
            {
                // User has denied the request.
                Debug.LogWarning(
                    "Failed to get camera permission. VPS availability check is not available.");
                yield break;
            }
            #endif

            while (_waitingForLocationService)
            {
                yield return null;
            }

            if (Input.location.status != LocationServiceStatus.Running)
            {
                Debug.LogWarning(
                    "Location service is not running. VPS availability check is not available.");
                yield break;
            }

            // Update event is executed before coroutines so it checks the latest error states.
            if (_isReturning)
            {
                yield break;
            }

            var location = Input.location.lastData;
            var vpsAvailabilityPromise =
                AREarthManager.CheckVpsAvailability(location.latitude, location.longitude);
            yield return vpsAvailabilityPromise;

            Debug.LogFormat("VPS Availability at ({0}, {1}): {2}",
                location.latitude, location.longitude, vpsAvailabilityPromise.Result);
        }               
    }
}