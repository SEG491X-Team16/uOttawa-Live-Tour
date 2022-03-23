using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using TMPro;

//Based on example from: https://blog.anarks2.com/Geolocated-AR-In-Unity-ARFoundation/

// //TODO: move this???
public struct UnityCoords {
    public UnityCoords(double x, double y, double z) {
        X = x;
        Y = y;
        Z = z;
    }

    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
}

public class PathManager : MonoBehaviour
{
    private const float MaxDistFromWayPointToDisplay = 40.0f; //meters
    private const float MaxDistFromEnd = 5.0f; //meters

    //invoked when navigation of a path segment is finished
    //all callbacks for this event are attached via the GUI
    public UnityEvent pathSegmentFinished;

    //the prefab used for arrows
    public GameObject arrowPrefab;

    public GameObject userPosPrefab;
    private GameObject userPosInstance;

    private bool guidanceEnabled = false;

    private bool flag = false;

    private Path currentPath;

    public Transform target;
    public GameObject cube;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");  

        Vector3 relativePos = cube.transform.position - target.position;

        cube.transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up) * Quaternion.AngleAxis(90, Vector3.up);

    }

    // Update is called once per frame
    void Update()
    {
        // if (GPSSingleton.Instance.isDataValid() && !flag) {
        //     Debug.Log("placing waypoint");
        //     GPSCoords pos = GPSSingleton.Instance.getCurrentCoordinates();
        //     pos.Latitude = pos.Latitude + 6;
        //     pos.Longitude = pos.Longitude + 6;

        //     Waypoint way = new Waypoint();
        //     way.Coordinates = pos;

        //     placeWaypoint(way);
        //     flag = true;
        // }

        //if we have a path
        if ((this.currentPath != null) && guidanceEnabled) {
            // Debug.Log("updating");
            GPSCoords userPos = GPSSingleton.Instance.GetCurrentCoordinates();

            PathSegment currSegment = this.currentPath.GetCurrentSegment();

            // Debug.Log(currSegment == null);

            // Debug.Log(Math.Abs(userPos.GetDistance(currSegment.GetNextVisibleStart().Coordinates)));

            // Debug.Log("user lat: "+userPos.Latitude+" lon: "+userPos.Longitude+", vis id: "+currSegment.GetNextVisibleStart().ID+" lat: "+currSegment.GetNextVisibleStart().Coordinates.Latitude+" lon: "+currSegment.GetNextVisibleStart().Coordinates.Longitude);
            //check if current visible ends need to be added or removed
            while (((currSegment.GetVisibleStart() == null) || (currSegment.GetLastWaypoint() != currSegment.GetVisibleStart())) 
                        && (Math.Abs(userPos.GetDistance(currSegment.GetNextVisibleStart().Coordinates)) < MaxDistFromWayPointToDisplay)) {
                //make the next waypoint visible
                if (currSegment.IncrementVisibleStart()) {
                    Waypoint next = currSegment.GetNextVisibleStart();

                    //use current waypoint if no more waypoints
                    if (next == null) {
                        next = currSegment.GetVisibleStart();
                    }
                    
                    placeWaypoint(currSegment.GetVisibleStart(), userPos, next);
                }
            }

            while ((currSegment.GetVisibleEnd() != null) && Math.Abs(userPos.GetDistance(currSegment.GetVisibleEnd().Coordinates)) > MaxDistFromWayPointToDisplay) {
                //make the last waypoint NOT visible
            }

            //if we have access to knowing cloud anchors loads we might want to use that instead
            //check if in range of the end
            if ((currSegment.GetVisibleStart() != null) && (currSegment.GetLastWaypoint() == currSegment.GetVisibleStart())
                    && (Math.Abs(userPos.GetDistance(currSegment.GetLastWaypoint().Coordinates)) < MaxDistFromEnd)) {
                //trigger pathsegment end
                guidanceEnabled = false;

                //trigger event callback
                this.pathSegmentFinished.Invoke();
            }
        }

        if (userPosPrefab != null) {
            if (userPosInstance != null) {
                Destroy(userPosInstance);
            }

            Waypoint way = new Waypoint();
            way.Coordinates = GPSSingleton.Instance.GetCurrentCoordinates();
            Vector3 waypointPos = getWaypointUnityPos(way);

            userPosInstance = Instantiate(userPosPrefab, waypointPos, Quaternion.identity);
        }
    }

    //called when the tour starts
    //get path from database?
    public void SetCurrentPath(Path newPath) {
        cleanup();
        this.currentPath = newPath;
        guidanceEnabled = true;
    }

    //this is called when the user clicks the Continue button after a stop on the tour
    public void StartNextPathSegment() {
        if (this.currentPath.HasNextSegment()) {
            this.currentPath.GetNextSegment();
            guidanceEnabled = true;
        }
    }

    private void placeWaypoint(Waypoint waypoint, GPSCoords userPos, Waypoint nextWaypoint) {
        // GPSCoords origin = GPSSingleton.Instance.GetUserOrigin();

        // double latOffset = userPos.Latitude - waypoint.Coordinates.Latitude;
        // double lonOffset = userPos.Longitude - waypoint.Coordinates.Longitude;

        //get position of this waypoint
        Vector3 waypointPos = getWaypointUnityPos(waypoint);

        //get direction to point to
        Vector3 nextWaypointPos = getWaypointUnityPos(nextWaypoint);

        //TODO: what about at the ends? point to cloud anchor?

        Vector3 relativePos = waypointPos - nextWaypointPos;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

        //create the waypoint in the unity world
        Instantiate(arrowPrefab, waypointPos, rotation);//Quaternion.identity);

        // Instantiate(prefab, new Vector3((float)latOffset, 0, (float)lonOffset), Quaternion.identity);
        // var obj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        //obj.transform.position = new Vector3((float)latOffset, 0, (float)lonOffset);
        // obj.transform.position = new Vector3((float)waypointPos.Y, (float)waypointPos.Z, (float)waypointPos.X);
        // Debug.Log("object placed at: "+latOffset+":"+lonOffset);
        // Debug.Log("object placed at: "+waypointPos.x+":"+waypointPos.z);
        // obj.transform.localScale = new Vector3(1,1,1);
    }

    //(x, y, z)
    private Vector3 getWaypointUnityPos(Waypoint waypoint) {
        GPSCoords origin = GPSSingleton.Instance.GetUserOrigin();

        double x = origin.GetDistance(new GPSCoords(origin.Latitude, waypoint.Coordinates.Longitude));
        double z = origin.GetDistance(new GPSCoords(waypoint.Coordinates.Latitude, origin.Longitude));

        //correct the positive vs negative axis
        if (origin.Latitude > waypoint.Coordinates.Latitude) {
            z = -1 * z;
        }

        if (origin.Longitude > waypoint.Coordinates.Longitude) {
            x = -1 * x;
        }

        // Debug.Log("x: "+x+", y: "+z);

        Debug.Log("heading: "+ GPSSingleton.Instance.GetUserOriginHeading());

        Vector3 dir = new Vector3((float)x, 0, (float)z);
        Vector3 heading = new Vector3(0, -1*(float)GPSSingleton.Instance.GetUserOriginHeading(), 0);
        // Vector3 heading = new Vector3(0, 270f, 0);
        dir = Quaternion.Euler(heading) * dir;

        // Debug.Log("dir x: "+dir.x+", y: "+dir.y+", z: "+dir.z);

        //TODO: determine positive vs negative
        return dir;
    }

    private void removeWaypoint(Waypoint waypoint) {
        waypoint.ClearInGameInstance();
    }

    private void cleanup() {
        // do clean up here
        //make sure that all waypoints are removed and not visible
        if (this.currentPath != null) {
            this.currentPath.ClearVisiblePath();
        }

        guidanceEnabled = false;
    }
}
