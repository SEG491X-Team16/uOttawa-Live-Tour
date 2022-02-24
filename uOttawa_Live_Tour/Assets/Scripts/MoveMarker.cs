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

    public float lat = 45.423284f;
    public float lon = -75.684895f;

    public float x;
    public float z;    
    private float radius = 6378.1f ;

    private float topLon = -75.688351f;
    private float topLat = 45.425846f;
    private float bottomLon = -75.675846f;
    private float bottomLat = 45.418926f;
    private float centerLon = -75.682059f;
    private float centerLat = 45.422425f;

    public bool rotate;


    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(x, 1.5f, z);
    }

    // Update is called once per frame
    void Update()
    {
        //set lat and lon to current GPS coordinates here

        //convert lat/lon to position on map
        LonToX(lon);
        LatToZ(lat);

        //apply position
        transform.localPosition = new Vector3(x, 1.5f, z);

        //slowly rotates location marker
        if (rotate){
            transform.Rotate(Vector3.up * 25f* Time.deltaTime);
        }

        //will add onto this later to account for the person location marker so it can rotate with your magnetic direction

    }


    //Tried a bunch of ways to project coordinates using different projecting methods, but they always gave odd numbers
    //I found simply mapping them worked the best since the map is pretty small in scale and doesnt really have to account for Earth's curvature
    void LonToX (float lon){
        //calculate difference between points on map as a percentage and multiply by maps's dimension
        float deltaX = (lon - centerLon)/(topLon - bottomLon);
        x = 6.92f * deltaX * 2;

    }

    void LatToZ (float lat){
         //calculate difference between points on map as a percentage and multiply by maps's dimension
        float deltaZ = (lat - centerLat)/(bottomLat - topLat);
        z = 5.35f * deltaZ * 2;
    }
}
