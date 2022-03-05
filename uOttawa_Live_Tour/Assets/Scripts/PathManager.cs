using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Based on example from: https://blog.anarks2.com/Geolocated-AR-In-Unity-ARFoundation/

public class PathManager : MonoBehaviour
{

    private bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");   
    }

    // Update is called once per frame
    void Update()
    {
        if (GPSSingleton.Instance.isDataValid() && !flag) {
            Debug.Log("placing waypoint");
            GPSCoords pos = GPSSingleton.Instance.getCurrentCoordinates();
            pos.Latitude = pos.Latitude + 6;
            pos.Longitude = pos.Longitude + 6;

            Waypoint way = new Waypoint();
            way.Coordinates = pos;

            placeWaypoint(way);
            flag = true;
        }
    }

    void placeWaypoint(Waypoint waypoint) {
        GPSCoords userPos = GPSSingleton.Instance.getCurrentCoordinates();

        double latOffset = userPos.Latitude - waypoint.Coordinates.Latitude;
        double lonOffset = userPos.Longitude - waypoint.Coordinates.Longitude;

        // Instantiate(PrimitiveType.Cylinder, new Vector3((float)latOffset, 0, (float)lonOffset), Quaternion.identity);
        var obj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        obj.transform.position = new Vector3((float)latOffset, 0, (float)lonOffset);
        Debug.Log("object placed at: "+latOffset+":"+lonOffset);
        obj.transform.localScale = new Vector3(4, 4, 4);
    }
}
