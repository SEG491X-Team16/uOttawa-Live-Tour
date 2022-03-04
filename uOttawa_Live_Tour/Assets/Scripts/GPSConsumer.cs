using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GPSConsumer : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI locationUpdate;

    GPSSingleton gpsSingleton;

    private int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gpsObject = GameObject.Find("GPS");
        gpsSingleton = gpsObject.GetComponent<GPSSingleton>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gpsSingleton.isDataValid())
        {
            locationUpdate.text =  i + ":" + gpsSingleton.getLastUpdate() + " : "+ gpsSingleton.getLatitude() + " : " + gpsSingleton.getLongitude();
            i++;
        }
    }
}
