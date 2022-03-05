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
        if (GPSSingleton.Instance.isDataValid())
        {
            locationUpdate.text =  i + ":" + GPSSingleton.Instance.getLastUpdate() + " : "+ GPSSingleton.Instance.getLatitude() + " : " + GPSSingleton.Instance.getLongitude();
            i++;
        }
    }
}
