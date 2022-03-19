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
    using UnityEngine;
    using UnityEngine.XR.ARFoundation;

    public class PersistentCloudAnchorsController : MonoBehaviour
    {
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
            HomePage.SetActive(true);
            ARView.SetActive(true);
            SetPlatformActive(true);
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