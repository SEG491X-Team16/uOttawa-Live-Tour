using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GPSConsumer : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI locationUpdate;

    private int i = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GPSSingleton.Instance.IsDataValid())
        {
            locationUpdate.text =  i + ":" + GPSSingleton.Instance.GetLastUpdate() + " : "+ GPSSingleton.Instance.GetCurrentCoordinates().Latitude + " : " + GPSSingleton.Instance.GetCurrentCoordinates().Longitude;
            i++;
        }
    }
}
