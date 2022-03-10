using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMarker : MonoBehaviour
{
    //Unity campus map scale
    //6.92, -5.35     
    //            0.0,0.0
    //                   -6.92, 5.35

    //lat, lon -> coresponding point on map
    //45.422425, -75.682059 -> 0,0 (center)
    //45.425846, -75.688351 -> 6.92, -5.35 (top left) 
    //45.418926, -75.675846 -> -6.92, 5.35 (bottom right)

    public double lat;
    public double lon;

    float x;
    float z;    

    private double topLon = -75.688351d;
    private double topLat = 45.425846d;
    private double bottomLon = -75.675846d;
    private double bottomLat = 45.418926d;
    private double centerLon = -75.682059d;
    private double centerLat = 45.422425d;

    public bool rotate;
    public bool followPlayer;
    public float yOffset;


    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(x, 1.5f + yOffset, z);
    }

    // Update is called once per frame
    void Update()
    {
        if(followPlayer){
            if (GPSSingleton.Instance.isDataValid())
            {
                lat = GPSSingleton.Instance.getCurrentCoordinates().Latitude;
                lon = GPSSingleton.Instance.getCurrentCoordinates().Longitude;
            }
        }

        //convert lat/lon to position on map
        LonToX(lon);
        LatToZ(lat);

        //apply position
        transform.localPosition = new Vector3(x, 1.5f + yOffset, z);

        //slowly rotates location marker
        if (rotate){
            transform.Rotate(Vector3.up * 25f* Time.deltaTime);
        }

        //will add onto this later to account for the person location marker so it can rotate with your magnetic direction

    }


    //Tried a bunch of ways to project coordinates using different projecting methods, but they always gave odd numbers
    //I found simply mapping them worked the best since the map is pretty small in scale and doesnt really have to account for Earth's curvature
    void LonToX (double lon){
        //calculate difference between points on map as a percentage and multiply by maps's dimension
        double deltaX = (lon - centerLon)/(topLon - bottomLon);
        x = (float)(6.92f * deltaX * 2);

    }

    void LatToZ (double lat){
         //calculate difference between points on map as a percentage and multiply by maps's dimension
        double deltaZ = (lat - centerLat)/(bottomLat - topLat);
        z = (float)(5.35f * deltaZ * 2);
    }
}
