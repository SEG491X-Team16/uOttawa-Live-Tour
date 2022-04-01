using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using TMPro;
using System;

public class EnableGPSNotification : MonoBehaviour
{
    [SerializeField] GameObject allowGPS;
    [SerializeField] GameObject enableGPSAndroid;
    [SerializeField] GameObject enableGPSIOS;

    private bool gpsActive = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation) && !GPSSingleton.Instance.IsRunning()){
            allowGPS.SetActive(true);
            enableGPSAndroid.SetActive(false);
            enableGPSIOS.SetActive(false);
            gpsActive = false;
            
        } else if(!UnityEngine.Input.location.isEnabledByUser && !GPSSingleton.Instance.IsRunning()){

            #if UNITY_ANDROID

            allowGPS.SetActive(false);
            enableGPSAndroid.SetActive(true);
        
            #elif UNITY_IOS

            allowGPS.SetActive(false);
            enableGPSIOS.SetActive(true);

            #endif

            gpsActive = false;
        }  else {
            if (!gpsActive){
                gpsActive = true;
                GPSSingleton.Instance.RestartGPS();
            }
            allowGPS.SetActive(false);
            enableGPSAndroid.SetActive(false);
            enableGPSIOS.SetActive(false);
            
        }
    }
}
