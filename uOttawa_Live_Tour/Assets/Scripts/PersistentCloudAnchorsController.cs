//-----------------------------------------------------------------------
// <copyright file="PersistentCloudAnchorsController.cs" company="Google LLC">
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
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.XR.ARFoundation;

    using Firebase;
    using Firebase.Database;

    public class PersistentCloudAnchorsController : MonoBehaviour
    {
        [Header("Firebase")]
        public Firebase.FirebaseApp app; 
        public DatabaseReference DBreference;

        [Header("AR Foundation")]

        /// <summary>
        /// The active ARSessionOrigin used in the scene.
        /// </summary>
        public ARSessionOrigin SessionOrigin;

        /// <summary>
        /// The ARSession used in the scene.
        /// </summary>
        public ARSession SessionCore;

        /// <summary>
        /// The ARCoreExtensions used in the scene.
        /// </summary>
        public ARCoreExtensions Extensions;

        /// <summary>
        /// The active ARAnchorManager used in the scene.
        /// </summary>
        public ARAnchorManager AnchorManager;

        /// <summary>
        /// The active ARPlaneManager used in the scene.
        /// </summary>
        public ARPlaneManager PlaneManager;

        /// <summary>
        /// The active ARRaycastManager used in the scene.
        /// </summary>
        public ARRaycastManager RaycastManager;

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
        /// The current application mode.
        /// </summary>
        [HideInInspector]
        public ApplicationMode Mode = ApplicationMode.Ready;

        /// <summary>
        /// A list of Cloud Anchors that will be used in resolving.
        /// </summary>
        public HashSet<string> ResolvingSet = new HashSet<string>();

        /// <summary>
        /// The collection of cloud anchors saved in memory
        /// at least one time.
        /// </summary>
        private CloudAnchorHistoryCollection _history = new CloudAnchorHistoryCollection();


        /// <summary>
        /// The key name used in PlayerPrefs which indicates whether the start info has displayed
        /// at least one time.
        /// </summary>
        private const string _hasDisplayedStartInfoKey = "HasDisplayedStartInfo";

        /// <summary>
        ///  Application modes.
        /// </summary>
        public enum ApplicationMode
        {
            /// <summary>
            /// Ready to host or resolve.
            /// </summary>
            Ready,

            /// <summary>
            /// Resolving Cloud Anchors.
            /// </summary>
            Resolving,
        }

        /// <summary>
        /// Gets the current main camera.
        /// </summary>
        public Camera MainCamera
        {
            get
            {
                return SessionOrigin.camera;
            }
        }

        /// <summary>
        /// Callback handling "Begin to resolve" button click event in Home Page.
        /// </summary>
        public void OnResolveButtonClicked()
        {
            Mode = ApplicationMode.Resolving;
            SwitchToARView();        
        }

        /// <summary>
        /// Switch to home page, and disable all other screens.
        /// </summary>
        public void SwitchToHomePage()
        {
            ResetAllViews();
            Mode = ApplicationMode.Ready;
            ResolvingSet.Clear();
            HomePage.SetActive(true);
        }

        /// <summary>
        /// Switch to resolve menu, and disable all other screens.
        /// </summary>
        public void SwitchToARView()
        {
            ResetAllViews();
            ARView.SetActive(true);
            SetPlatformActive(true);
        }

        /// <summary>
        /// Retrieve persistent Cloud Anchors history data from memory
        /// </summary>
        /// <returns>A collection of persistent Cloud Anchors history data.</returns>
        public CloudAnchorHistoryCollection getCloudAnchorHistory()
        {
            return _history;
        }

        /// <summary>
        /// Load the persistent Cloud Anchors history from the database into memory
        /// </summary>
        public async Task LoadCloudAnchorHistory()
        {
            // Retrieve anchor data from database
            await DBreference.Child("anchors").GetValueAsync().ContinueWith(task => {
                if (task.Exception != null)
                {
                    Debug.LogWarning(message: $"Failed to register task with {task.Exception}");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    // Load anchor data into history collection
                    foreach (DataSnapshot anchor in snapshot.Children) {
                        var data = JsonUtility.FromJson<CloudAnchorHistory>(anchor.GetRawJsonValue());
                        _history.Collection.Add(data);
                        _history.Collection.Sort((left, right) => right.CreatedTime.CompareTo(left.CreatedTime));
                    }                   
                }
            });

            // For testing purposes only:
            // Manually set _history since database isn't yet integrated in project
            // (Set to Id property to that of a cloud anchor hosted in the past 24h )
            var anchorJson = "{\"Id\":\"ua-b08c5ba41df5001f6a272bc53dac73ea\",\"Name\":\"NotRelevantNorIsTime\",\"SerializedTime\":\"2022-03-16 2:13:33 PM\"}";
            var data = JsonUtility.FromJson<CloudAnchorHistory>(anchorJson);
            _history.Collection.Insert(0, data);

            return;
        }


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

            // Enable Persistent Cloud Anchors scene to target 60fps camera capture frame rate
            // on supported devices.
            // Note, Application.targetFrameRate is ignored when QualitySettings.vSyncCount != 0.
            Application.targetFrameRate = 60;
            SwitchToHomePage();

            Debug.Log("Hello World");

            // Check and optionally update Firebase Google Play services dependencies
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
                var dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available) {
                    // If they are available, initialise Firebase
                    InitializeFirebase();

                    // Set a flag here to indicate whether Firebase is ready to use by your app.
                } else {
                    Debug.LogError(System.String.Format(
                    "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                    // Firebase Unity SDK is not safe to use here.
                }
            });
        }

        /// <summary>
        /// The Unity Update() method.
        // Update is called once per frame.
        /// </summary>
        public void Update()
        {
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
                }
            }            
        }

        private void InitializeFirebase()
        {
            Debug.Log("Setting up database...");
            DBreference = FirebaseDatabase.DefaultInstance.RootReference;
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
            SessionCore.gameObject.SetActive(active);
            Extensions.gameObject.SetActive(active);
        }
    }
}