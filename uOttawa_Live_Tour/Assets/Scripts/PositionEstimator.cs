using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionEstimator : MonoBehaviour
{
    //Unity Reference
    private Transform currentGameReferencePoint;

    //Real World Reference
    private GPSCoords currentPositionEstimate = new GPSCoords(0, 0);
    private float currentHeadingEstimate;

    public void Initialize(GPSCoords realPos1, Transform unityPos1, GPSCoords realPos2, Transform unityPos2)
    {
        /*
        GPSCoords origin = GPSSingleton.Instance.GetUserOrigin();

        double x = origin.GetDistance(new GPSCoords(origin.Latitude, waypoint.Longitude));
        double z = origin.GetDistance(new GPSCoords(waypoint.Latitude, origin.Longitude));

        //correct the positive vs negative axis
        if (origin.Latitude > waypoint.Latitude) {
            z = -1 * z;
        }

        if (origin.Longitude > waypoint.Longitude) {
            x = -1 * x;
        }
        // Debug.Log("lat, lon: "+waypoint.Latitude+","+waypoint.Longitude+"x, z: "+x+","+z);

        // Debug.Log("heading: "+ GPSSingleton.Instance.GetUserOriginHeading());

        //vector3 = (x, y, z)
        Vector3 dir = new Vector3((float)x, 0, (float)z);
        Vector3 heading = new Vector3(0, -1*(float)GPSSingleton.Instance.GetUserOriginHeading(), 0);
        dir = Quaternion.Euler(heading) * dir;

        // return dir;
        */


        GPSCoords estimate1 = this.getOriginEstimate(realPos1, unityPos1.position);
        GPSCoords estimate2 = this.getOriginEstimate(realPos2, unityPos2.position);

        //combine the two estimates, with equal weight
        this.currentPositionEstimate.Latitude = ((0.5f * estimate1.Latitude) + (0.5f * estimate2.Latitude));
        this.currentPositionEstimate.Longitude = ((0.5f * estimate1.Longitude) + (0.5f * estimate2.Longitude));

        //get the heading estimate
    }

    public void UpdateEstimate(GPSCoords realPos, Transform unityPos)
    {

    }

    public GPSCoords GetUserOriginPosition()
    {
        return this.currentPositionEstimate;
    }

    public float GetUserOriginHeading()
    {
        return this.currentHeadingEstimate;
    }


    //based on re-arranged equations from: https://www.movable-type.co.uk/scripts/latlong.html
    //Using Equirectangular Approximation Equations
    private GPSCoords getOriginEstimate(GPSCoords realPos, Vector3 unityPos)
    {
        const int R = 6371000; //earth's mean radius in metres

        float radianLat = realPos.Latitude * (Mathf.PI / 180);
        float radianLon = realPos.Longitude * (Mathf.PI / 180);

        //get Latitude
        float latEst = radianLat - (unityPos.y / R);

        //get Logitude
        float lonEst = radianLon - (unityPos.x / (R * Mathf.Cos(radianLat)));

        //convert back to decimal degrees an return
        return new GPSCoords(latEst * (180 / Mathf.PI), lonEst * (180 / Mathf.PI));;
    }

    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
