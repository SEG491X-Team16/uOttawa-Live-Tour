using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using TMPro;

// Adapted from code at: https://nosuchstudio.medium.com/how-to-access-gps-location-in-unity-521f1371a7e3

public class GPSSingleton : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI location;

    private double latitude = 0.0;
    private double longitude =  0.0;

    private double lastUpdate = -1.0;

    // public static GPSSingleton Instance { get; private set; }

    // private void Awake() 
    // {
    //     if (Instance != null && Instance != this)
    //     {
    //         Destroy(this);
    //         return;
    //     }

    //     Instance = this;
    // }

    IEnumerator GPSCoroutine() {

        location.text = "start coroutine";

        //Persmissions
        #if UNITY_EDITOR
            //no permissions in editor

        yield return new WaitWhile(() => !UnityEditor.EditorApplication.isRemoteConnected);
        yield return new WaitForSecondsRealtime(5f);
        #elif UNITY_ANDROID
        
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }

        if (!UnityEngine.Input.location.isEnabledByUser) {
            location.text = "location no go";
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

        location.text = "starting";

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
            // TODO Failure
            Debug.Log("Timed out");
            yield break;
        }

        location.text = "checking connection";

        // Connection has failed
        if (UnityEngine.Input.location.status != LocationServiceStatus.Running) {
            Debug.LogFormat("Unable to determine device location. Failed with status {0}", UnityEngine.Input.location.status);
            location.text = "unable to determine location";
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

            location.text = "Location: " 
                + UnityEngine.Input.location.lastData.latitude + " " 
                + UnityEngine.Input.location.lastData.longitude + " " 
                + UnityEngine.Input.location.lastData.altitude + " " 
                + UnityEngine.Input.location.lastData.horizontalAccuracy + " " 
                + UnityEngine.Input.location.lastData.timestamp;

            var _latitude = UnityEngine.Input.location.lastData.latitude;
            var _longitude = UnityEngine.Input.location.lastData.longitude;
        }

        int i = 0;
        while (UnityEngine.Input.location.status == LocationServiceStatus.Running) {
            this.latitude = UnityEngine.Input.location.lastData.latitude;
            this.longitude = UnityEngine.Input.location.lastData.longitude;
            this.lastUpdate = UnityEngine.Input.location.lastData.timestamp;

            location.text = "Location: " 
                + i + " : " 
                + UnityEngine.Input.location.lastData.timestamp;

            yield return new WaitForSeconds(1f);
            i++;
        }

        // Stop service if there is no need to query location updates continuously
        UnityEngine.Input.location.Stop();
        location.text = "stop";
    }

    public bool isDataValid()
    {
        return this.lastUpdate != -1;
    }

    public double getLastUpdate()
    {
        return this.lastUpdate;
    }

    public double getLatitude()
    {
        return this.latitude;
    }

    public double getLongitude()
    {
        return this.longitude;
    }

    // Start is called before the first frame update
    void Start()
    {
        location.text = "start function";
        StartCoroutine(GPSCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
