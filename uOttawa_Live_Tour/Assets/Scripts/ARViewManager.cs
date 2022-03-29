//-----------------------------------------------------------------------
// <copyright file="ARViewManager.cs" company="Google LLC">
//
// Copyright 2020 Google LLC
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
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    using UnityEngine.XR.ARFoundation;
    using UnityEngine.XR.ARSubsystems;

    /// <summary>
    /// A manager component that helps with resolving Cloud Anchors.
    /// </summary>
    public class ARViewManager : MonoBehaviour
    {

        /// <summary>
        /// The main controller for Persistent Cloud Anchors sample.
        /// </summary>
        public PersistentCloudAnchorsController Controller;

        /// <summary>
        /// The 3D object that represents a Cloud Anchor.
        /// </summary>
        public GameObject CloudAnchorPrefab;

        /// <summary>
        /// The UI element that displays the instructions to guide resolving experience.
        /// </summary>
        public GameObject InstructionBar;

        /// <summary>
        /// The instruction text in the top instruction bar.
        /// </summary>
        public Text InstructionText;

        /// <summary>
        /// Display the tracking helper text when the session in not tracking.
        /// </summary>
        public Text TrackingHelperText;

        /// <summary>
        /// The debug text in bottom snack bar.
        /// </summary>
        public Text DebugText;

        /// <summary>
        /// Helper message for <see cref="NotTrackingReason.Initializing">.</see>
        /// </summary>
        private const string _initializingMessage = "Tracking is being initialized.";

        /// <summary>
        /// Helper message for <see cref="NotTrackingReason.Relocalizing">.</see>
        /// </summary>
        private const string _relocalizingMessage = "Tracking is resuming after an interruption.";

        /// <summary>
        /// Helper message for <see cref="NotTrackingReason.InsufficientLight">.</see>
        /// </summary>
        private const string _insufficientLightMessage = "Too dark. Try moving to a well-lit area.";

        /// <summary>
        /// Helper message for <see cref="NotTrackingReason.InsufficientLight">
        /// in Android S or above.</see>
        /// </summary>
        private const string _insufficientLightMessageAndroidS =
            "Too dark. Try moving to a well-lit area. " +
            "Also, make sure the Block Camera is set to off in system settings.";

        /// <summary>
        /// Helper message for <see cref="NotTrackingReason.InsufficientFeatures">.</see>
        /// </summary>
        private const string _insufficientFeatureMessage =
            "Can't find anything. Aim device at a surface with more texture or color.";

        /// <summary>
        /// Helper message for <see cref="NotTrackingReason.ExcessiveMotion">.</see>
        /// </summary>
        private const string _excessiveMotionMessage = "Moving too fast. Slow down.";

        /// <summary>
        /// Helper message for <see cref="NotTrackingReason.Unsupported">.</see>
        /// </summary>
        private const string _unsupportedMessage = "Tracking lost reason is not supported.";

        /// <summary>
        /// The time between enters AR View and ARCore session starts to host or resolve.
        /// </summary>
        private const float _startPrepareTime = 3.0f;

        /// <summary>
        /// Android 12 (S) SDK version.
        /// </summary>
        private const int _androidSSDKVesion = 31;

        /// <summary>
        /// Pixel Model keyword.
        /// </summary>
        private const string _pixelModel = "pixel";

        /// <summary>
        /// The timer to indicate whether the AR View has passed the start prepare time.
        /// </summary>
        private float _timeSinceStart;

        /// <summary>
        /// True if the app is in the process of returning to home page due to an invalid state,
        /// otherwise false.
        /// </summary>
        private bool _isReturning;

        /// <summary>
        /// Cached Cloud Anchor history data used.
        /// </summary>
        private CloudAnchorHistoryCollection _history = new CloudAnchorHistoryCollection();
        /// <summary>
        /// The history data that represents the current hosted Cloud Anchor.
        /// </summary>
        private CloudAnchorHistory _hostedCloudAnchor;

        /// <summary>
        /// A list of Cloud Anchors that have been created but are not yet ready to use.
        /// </summary>
        private List<ARCloudAnchor> _pendingCloudAnchors = new List<ARCloudAnchor>();

        /// <summary>
        /// A list for caching all Cloud Anchors.
        /// </summary>
        private List<ARCloudAnchor> _cachedCloudAnchors = new List<ARCloudAnchor>();

        private Color _activeColor;
        private AndroidJavaClass _versionInfo;

        /// <summary>
        /// The Unity Awake() method.
        /// </summary>
        public void Awake()
        {
            _versionInfo = new AndroidJavaClass("android.os.Build$VERSION");
        }


        /// <summary>
        /// The Unity OnEnable() method.
        /// </summary>
        public async void OnEnable()
        {
            _timeSinceStart = 0.0f;
            _isReturning = false;
            _pendingCloudAnchors.Clear();
            _cachedCloudAnchors.Clear();

            InstructionBar.SetActive(true);

            // Manually pre-load cloud anchor in resolving set
            await Controller.LoadCloudAnchorHistory();
            _history = Controller.getCloudAnchorHistory();
            Controller.ResolvingSet.Add(_history.Collection[0].Id);

            switch (Controller.Mode)
            {
                case PersistentCloudAnchorsController.ApplicationMode.Ready:
                    ReturnToHomePage("Invalid application mode, returning to home page...");
                    break;
                case PersistentCloudAnchorsController.ApplicationMode.Resolving:
                    InstructionText.text = "Detecting flat surface...";
                    DebugText.text = "ARCore is preparing for " + Controller.Mode;
                    break;
            }


        }


        /// <summary>
        /// The Unity OnDisable() method.
        /// </summary>
        public void OnDisable()
        {
            if (_pendingCloudAnchors.Count > 0)
            {
                foreach (var anchor in _pendingCloudAnchors)
                {
                    Destroy(anchor.gameObject);
                }

                _pendingCloudAnchors.Clear();
            }

            if (_cachedCloudAnchors.Count > 0)
            {
                foreach (var anchor in _cachedCloudAnchors)
                {
                    Destroy(anchor.gameObject);
                }

                _cachedCloudAnchors.Clear();
            }
        }

        /// <summary>
        /// The Unity Update() method.
        //  Update is called once per frame
        /// </summary>  
        public void Update()
        {
            // Give ARCore some time to prepare for hosting or resolving.
            if (_timeSinceStart < _startPrepareTime)
            {
                _timeSinceStart += Time.deltaTime;
                if (_timeSinceStart >= _startPrepareTime)
                {
                    UpdateInitialInstruction();
                }

                return;
            }

            ARCoreLifecycleUpdate();
            if (_isReturning)
            {
                return;
            }

            if (_timeSinceStart >= _startPrepareTime)
            {
                DisplayTrackingHelperMessage();
            }

            if (Controller.Mode == PersistentCloudAnchorsController.ApplicationMode.Resolving)
            {
                ResolvingCloudAnchors();
            }

            UpdatePendingCloudAnchors();
            
        }

        private void ResolvingCloudAnchors()
        {
            // No Cloud Anchor for resolving.
            if (Controller.ResolvingSet.Count == 0)
            {
                return;
            }

            // There are pending or finished resolving tasks.
            if (_pendingCloudAnchors.Count > 0 || _cachedCloudAnchors.Count > 0)
            {
                return;
            }

            // ARCore session is not ready for resolving.
            if (ARSession.state != ARSessionState.SessionTracking)
            {
                return;
            }

            // Manually pre-load cloud anchor in resolving set
            Controller.ResolvingSet.Add(_history.Collection[0].Id);

            Debug.LogFormat("Attempting to resolve {0} Cloud Anchor(s): {1}",
                Controller.ResolvingSet.Count,
                string.Join(",", new List<string>(Controller.ResolvingSet).ToArray()));
            foreach (string cloudId in Controller.ResolvingSet)
            {
                ARCloudAnchor cloudAnchor =
                    Controller.AnchorManager.ResolveCloudAnchorId(cloudId);
                if (cloudAnchor == null)
                {
                    Debug.LogFormat("Faild to resolve Cloud Anchor " + cloudId);
                    OnAnchorResolvedFinished(false, cloudId);
                }
                else
                {
                    _pendingCloudAnchors.Add(cloudAnchor);
                }
            }

            Controller.ResolvingSet.Clear();
        }

        private void UpdatePendingCloudAnchors()
        {
            foreach (var cloudAnchor in _pendingCloudAnchors)
            {
                if (cloudAnchor.cloudAnchorState == CloudAnchorState.Success)
                {
                    if (Controller.Mode ==
                        PersistentCloudAnchorsController.ApplicationMode.Resolving)
                    {
                        Debug.LogFormat("Succeed to resolve the Cloud Anchor: {0}",
                            cloudAnchor.cloudAnchorId);
                        OnAnchorResolvedFinished(true, cloudAnchor.cloudAnchorId);
                        Instantiate(CloudAnchorPrefab, cloudAnchor.transform);
                    }

                    _cachedCloudAnchors.Add(cloudAnchor);
                }
                else if (cloudAnchor.cloudAnchorState != CloudAnchorState.TaskInProgress)
                {
                    if (Controller.Mode ==
                        PersistentCloudAnchorsController.ApplicationMode.Resolving)
                    {
                        Debug.LogFormat("Failed to resolve the Cloud Anchor {0} with error {1}.",
                            cloudAnchor.cloudAnchorId, cloudAnchor.cloudAnchorState);
                        OnAnchorResolvedFinished(false, cloudAnchor.cloudAnchorId,
                            cloudAnchor.cloudAnchorState.ToString());
                    }

                    _cachedCloudAnchors.Add(cloudAnchor);
                }
            }

            _pendingCloudAnchors.RemoveAll(
                x => x.cloudAnchorState != CloudAnchorState.TaskInProgress);
        }

        private void OnAnchorResolvedFinished(bool success, string cloudId, string response = null)
        {
            if (success)
            {
                InstructionText.text = "Resolve success!";
                DebugText.text =
                    string.Format("Succeed to resolve the Cloud Anchor: {0}.", cloudId);
            }
            else
            {
                InstructionText.text = "Resolve failed.";
                DebugText.text = "Failed to resolve Cloud Anchor: " + cloudId +
                    (response == null ? "." : "with error " + response + ".");
            }
        }


        private void DisplayTrackingHelperMessage()
        {
            if (_isReturning || ARSession.notTrackingReason == NotTrackingReason.None)
            {
                TrackingHelperText.gameObject.SetActive(false);
            }
            else
            {
                TrackingHelperText.gameObject.SetActive(true);
                switch (ARSession.notTrackingReason)
                {
                    case NotTrackingReason.Initializing:
                        TrackingHelperText.text = _initializingMessage;
                        return;
                    case NotTrackingReason.Relocalizing:
                        TrackingHelperText.text = _relocalizingMessage;
                        return;
                    case NotTrackingReason.InsufficientLight:
                        if (_versionInfo.GetStatic<int>("SDK_INT") < _androidSSDKVesion)
                        {
                            TrackingHelperText.text = _insufficientLightMessage;
                        }
                        else
                        {
                            TrackingHelperText.text = _insufficientLightMessageAndroidS;
                        }

                        return;
                    case NotTrackingReason.InsufficientFeatures:
                        TrackingHelperText.text = _insufficientFeatureMessage;
                        return;
                    case NotTrackingReason.ExcessiveMotion:
                        TrackingHelperText.text = _excessiveMotionMessage;
                        return;
                    case NotTrackingReason.Unsupported:
                        TrackingHelperText.text = _unsupportedMessage;
                        return;
                    default:
                        TrackingHelperText.text =
                            string.Format("Not tracking reason: {0}", ARSession.notTrackingReason);
                        return;
                }
            }
        }
        private void ReturnToHomePage(string reason)
        {
            Debug.Log("Returning home for reason: " + reason);
            if (_isReturning)
            {
                return;
            }

            _isReturning = true;
            DebugText.text = reason;
            Invoke("DoReturnToHomePage", 3.0f);
        }

        private void DoReturnToHomePage()
        {
            Controller.SwitchToHomePage();
        }
        private void UpdateInitialInstruction()
        {
            switch (Controller.Mode)
            {
                case PersistentCloudAnchorsController.ApplicationMode.Resolving:
                    // Initial instruction for resolving flow:
                    InstructionText.text =
                        "Look at the location you expect to see the AR experience appear.";
                    DebugText.text = string.Format("Attempting to resolve {0} anchors...",
                        Controller.ResolvingSet.Count);
                    return;
                default:
                    return;
            }
        }

        private void ARCoreLifecycleUpdate()
        {
            // Only allow the screen to sleep when not tracking.
            var sleepTimeout = SleepTimeout.NeverSleep;
            if (ARSession.state != ARSessionState.SessionTracking)
            {
                sleepTimeout = SleepTimeout.SystemSetting;
            }

            Screen.sleepTimeout = sleepTimeout;

            if (_isReturning)
            {
                return;
            }

            // Return to home page if ARSession is in error status.
            if (ARSession.state != ARSessionState.Ready &&
                ARSession.state != ARSessionState.SessionInitializing &&
                ARSession.state != ARSessionState.SessionTracking)
            {
                ReturnToHomePage(string.Format(
                    "ARCore encountered an error state {0}. Please start the app again.",
                    ARSession.state));
            }
        }
        private void DoHideInstructionBar()
        {
            InstructionBar.SetActive(false);
        }

    }
}
