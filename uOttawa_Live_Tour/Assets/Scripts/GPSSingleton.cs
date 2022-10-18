using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using TMPro;
using System;

/**
* This class is created once per scene and it provides the location to all assets/scripts that require it.
* It is added to an Empty. This way all classes will have access to the same GPS coordinates.
* 
* Adapted from code at: https://nosuchstudio.medium.com/how-to-access-gps-location-in-unity-521f1371a7e3
*/

//This struct is used to hold latitude and longitude for coordinates
public struct GPSCoords
{
    public GPSCoords(float lat, float lon) {
        Latitude = lat;
        Longitude = lon;
    }

    public float Latitude { get; set; }
    public float Longitude { get; set; }

    //returns distance from other in metres
    //based on code from: https://www.movable-type.co.uk/scripts/latlong.html
    public double GetDistance(GPSCoords other) {
        const int R = 6371000; //earth's mean radius in metres

        double radianLat1 = this.Latitude * (Math.PI /180);
        double radianLat2 = other.Latitude * (Math.PI /180);

        double radianDeltaLat = (other.Latitude - this.Latitude) * (Math.PI /180);
        double radianDeltaLon = (other.Longitude - this.Longitude) * (Math.PI /180);

        double a = Math.Sin(radianDeltaLat / 2) * Math.Sin(radianDeltaLat / 2) +
                    Math.Cos(radianLat1) * Math.Cos(radianLat2) * 
                    Math.Sin(radianDeltaLon) * Math.Sin(radianDeltaLon);

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return (R * c);
    }
}

public class GPSSingleton : MonoBehaviour
{

    //Minimum accepted GPS accuracy in metres
    private const int MinGPSAccuracy = 100; 

    //max time waiting for GPS to give good accuracy in seconds
    private const int GPSWaitTimeout = 5;

    //The latitude and Longitude in Decimal Degrees
    private GPSCoords coordinates = new GPSCoords(0, 0);

    //The first recorded GPS point for the user
    private GPSCoords userOrigin = new GPSCoords(0, 0);

    //the first recorded heading of the user
    private float userOriginHeading = 0;

    //The timestamp of the last location update
    private double lastUpdate = -1.0;

    private bool running = false;

    //The instance of the class
    public static GPSSingleton Instance { get; private set; }

    //makes sure no more than one instance of the class exists at once
    private void Awake() 
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("multiple instances");
            Destroy(this);
            return;
        }

        Instance = this;
    }

    //connect to the devices location services and update the stored variables
    IEnumerator GPSCoroutine() {

        running = true;

        //Persmissions
        #if UNITY_ANDROID
        
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }

        if (!UnityEngine.Input.location.isEnabledByUser) {
            Debug.Log("Android Location not enabled");
            running = false;
            yield break;
            //we need to do something here to let user know
        }

        #elif UNITY_IOS

        if (!UnityEngine.Input.location.isEnabledByUser) {
            Debug.Log("IOS Location not enabled");
            running = false;
            yield break;
            //we need to do something here to let user know
        }

        #endif

        UnityEngine.Input.location.Start(5f, 5f);

        //enable the compass, otherwise we get 0
        UnityEngine.Input.compass.enabled = true;
                
        // Wait until service initializes
        int maxWait = 15;
        while (UnityEngine.Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
            yield return new WaitForSecondsRealtime(1);
            maxWait--;
        }

        // Editor has a bug which doesn't set the service status to Initializing. So extra wait in Editor.
        #if UNITY_EDITOR

        int editorMaxWait = 15;
        while (UnityEngine.Input.location.status == LocationServiceStatus.Stopped && editorMaxWait > 0) {
            yield return new WaitForSecondsRealtime(1);
            editorMaxWait--;
        }

        #endif

        // Service didn't initialize in 15 seconds
        if (maxWait < 1) {
            Debug.Log("Timed out");
            running = false;
            yield break;
        }

        // Connection has failed
        if (UnityEngine.Input.location.status != LocationServiceStatus.Running) {
            Debug.LogFormat("Unable to determine device location. Failed with status {0}", UnityEngine.Input.location.status);
            running = false;
            yield break;
        } else {
            Debug.LogFormat("Location service live. status {0}", UnityEngine.Input.location.status);
            // Access granted and location value could be retrieved
            Debug.Log("Location: " 
                + UnityEngine.Input.location.lastData.latitude + " " 
                + UnityEngine.Input.location.lastData.longitude + " " 
                + UnityEngine.Input.location.lastData.altitude + " " 
                + UnityEngine.Input.location.lastData.horizontalAccuracy + " " 
                + UnityEngine.Input.location.lastData.timestamp);

            bool originRecorded = false;
            DateTime start = DateTime.Now;
            while ((UnityEngine.Input.location.status == LocationServiceStatus.Running) && !originRecorded)  { 
                //record the user's original position
                if ((UnityEngine.Input.location.lastData.horizontalAccuracy <= MinGPSAccuracy) || ((DateTime.Now.Second - start.Second) > GPSWaitTimeout)) {
                    this.userOrigin = new GPSCoords(UnityEngine.Input.location.lastData.latitude, UnityEngine.Input.location.lastData.longitude);
                    this.userOriginHeading = UnityEngine.Input.compass.trueHeading;

                    originRecorded = true;
                }
                yield return 0;
            }
        }

        //update values until we become dissconnected
        while (UnityEngine.Input.location.status == LocationServiceStatus.Running) {
            this.coordinates = new GPSCoords(UnityEngine.Input.location.lastData.latitude, UnityEngine.Input.location.lastData.longitude);
            this.lastUpdate = UnityEngine.Input.location.lastData.timestamp;

            Debug.Log("Location: " 
                + UnityEngine.Input.location.lastData.latitude + " " 
                + UnityEngine.Input.location.lastData.longitude + " " 
                + UnityEngine.Input.location.lastData.horizontalAccuracy + " " 
                + UnityEngine.Input.compass.trueHeading + " "
                + UnityEngine.Input.location.lastData.timestamp);

            yield return new WaitForSeconds(1f);
        }

        // Stop service when done
        UnityEngine.Input.location.Stop();
        running = false;
    }

    //returns true if the singleton has valid GPS coordinates
    public bool IsDataValid()
    {
        return this.lastUpdate != -1;
    }

    //returns the timestamp of the last coordinates update
    public double GetLastUpdate()
    {
        return this.lastUpdate;
    }

    /*Returns the most recent latitude and longitude. Returns 0, 0 as the coordinates
      if no data has been read yet.
    */
    public GPSCoords GetCurrentCoordinates()
    {
        return this.coordinates;
    }

    /*Returns the first recorded GPS coordinates for the user, this cooresponds 
      to (0, 0) in the unity world. Returns 0, 0 as the coordinates if no data has 
      been read yet.
    */
    public GPSCoords GetUserOrigin()
    {
        // return new GPSCoords(45.424622f, -75.685978f);
        return (this.lastUpdate == -1) ? (new GPSCoords(0, 0)) : (this.userOrigin);
    }

    public float GetUserOriginHeading()
    {
        // return 0;
        return this.userOriginHeading;
    }

    public bool IsRunning(){
        return running;
    }

    public void RestartGPS()
    {
        if(!running){
            StartCoroutine(GPSCoroutine());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GPSCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
