using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using TMPro;

/**
* This class is created once per scene and it provides the location to all assets/scripts that require it.
* It is added to an Empty. This way all classes will have access to the same GPS coordinates.
* 
* Adapted from code at: https://nosuchstudio.medium.com/how-to-access-gps-location-in-unity-521f1371a7e3
*/

public class GPSSingleton : MonoBehaviour
{

    //The latitude and Longitude in Decimal Degrees
    private double latitude = 0.0;
    private double longitude =  0.0;

    //The timestamp of the last location update
    private double lastUpdate = -1.0;

    //The instance of the class
    public static GPSSingleton Instance { get; private set; }

    //makes sure no more than one instance of the class exists at once
    private void Awake() 
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    //connect to the devices location services and update the stored variables
    IEnumerator GPSCoroutine() {

        //Persmissions
        #if UNITY_ANDROID
        
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }

        if (!UnityEngine.Input.location.isEnabledByUser) {
            Debug.Log("Android Location not enabled");
            yield break;
            //we need to do something here to let user know
        }

        #elif UNITY_IOS

        if (!UnityEngine.Input.location.isEnabledByUser) {
            Debug.Log("IOS Location not enabled");
            yield break;
            //we need to do something here to let user know
        }

        #endif

        UnityEngine.Input.location.Start(500f, 500f);
                
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
            yield break;
        }

        // Connection has failed
        if (UnityEngine.Input.location.status != LocationServiceStatus.Running) {
            Debug.LogFormat("Unable to determine device location. Failed with status {0}", UnityEngine.Input.location.status);
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
        }

        //update values until we become dissconnected
        while (UnityEngine.Input.location.status == LocationServiceStatus.Running) {
            this.latitude = UnityEngine.Input.location.lastData.latitude;
            this.longitude = UnityEngine.Input.location.lastData.longitude;
            this.lastUpdate = UnityEngine.Input.location.lastData.timestamp;

            Debug.Log("Location: " 
                + UnityEngine.Input.location.lastData.latitude + " " 
                + UnityEngine.Input.location.lastData.longitude + " " 
                + UnityEngine.Input.location.lastData.timestamp);

            yield return new WaitForSeconds(1f);
        }

        // Stop service when done
        UnityEngine.Input.location.Stop();
    }

    //returns true if the singleton has valid GPS coordinates
    public bool isDataValid()
    {
        return this.lastUpdate != -1;
    }

    //returns the timestamp of the last coordinates update
    public double getLastUpdate()
    {
        return this.lastUpdate;
    }

    //returns the latitude in decimal degrees
    public double getLatitude()
    {
        return this.latitude;
    }

    //returns the longitude in decimal degrees
    public double getLongitude()
    {
        return this.longitude;
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
