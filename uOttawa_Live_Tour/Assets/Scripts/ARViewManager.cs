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
    using System;
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
        /// The main controller for Geospatial API.
        /// </summary>
        public GeospatialController Controller;

        /// <summary>
        /// The location of the camera in Earth-relative coordinates
        /// </summary>
        public GeospatialPose CameraPose;

        /// <summary>
        /// The UI element that displays the instructions to guide resolving experience.
        /// </summary>
        public GameObject InstructionBar;

        /// <summary>
        /// The debug text in bottom snack bar.
        /// </summary>
        public Text DebugText;

        /// <summary>
        /// UI element to display information at runtime.
        /// </summary>
        public GameObject InfoPanel;

        /// <summary>
        /// Text displaying <see cref="GeospatialPose"/> information at runtime.
        /// </summary>
        public Text InfoText;

        /// <summary>
        /// True if the app is in the process of returning to home page due to an invalid state,
        /// otherwise false.
        /// </summary>
        private bool _isReturning;

        /// <summary>
        /// The Unity Awake() method.
        /// </summary>
        public void Awake()
        {

        }

        /// <summary>
        /// The Unity OnEnable() method.
        /// </summary>
        public void OnEnable()
        {
            _isReturning = false;
            InfoPanel.SetActive(true);
        }

        /// <summary>
        /// The Unity OnDisable() method.
        /// </summary>
        public void OnDisable()
        {

        }

        /// <summary>
        /// The Unity Update() method.
        //  Update is called once per frame
        /// </summary>  
        public void Update()
        {   
            if (!Controller.isGeospatialModeSupported()) 
            {
                DebugText.text = "Geospatial API not supported by this device.";
            }

            if (Controller.getEarthTrackingState() == TrackingState.Tracking) {
                var pose = Controller.getCameraGeospatialPose();
                InfoText.text = string.Format(
                    "Latitude/Longitude: {1}°, {2}°{0}" +
                    "Horizontal Accuracy: {3}m{0}" +
                    "Altitude: {4}m{0}" +
                    "Vertical Accuracy: {5}m{0}" +
                    "Heading: {6}°{0}" +
                    "Heading Accuracy: {7}°",
                    Environment.NewLine,
                    pose.Latitude.ToString("F6"),
                    pose.Longitude.ToString("F6"),
                    pose.HorizontalAccuracy.ToString("F6"),
                    pose.Altitude.ToString("F2"),
                    pose.VerticalAccuracy.ToString("F2"),
                    pose.Heading.ToString("F1"),
                    pose.HeadingAccuracy.ToString("F1")
                );                
            } else {
                InfoText.text = "GEOSPATIAL POSE: not tracking \n Point camera to nearby buildings or check error logs";
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

        private void ARCoreLifecycleUpdate()
        {
            // Return to home page if ARSession is in error status.
            if (ARSession.state != ARSessionState.Ready &&
                ARSession.state != ARSessionState.SessionInitializing &&
                ARSession.state != ARSessionState.SessionTracking)
            {
                ReturnToHomePage("");
            }
        }

        private void DoReturnToHomePage()
        {
            Controller.SwitchToHomePage();
        }
    }
}
