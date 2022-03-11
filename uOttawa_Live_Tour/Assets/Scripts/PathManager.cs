using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

//Based on example from: https://blog.anarks2.com/Geolocated-AR-In-Unity-ARFoundation/

public class PathManager : MonoBehaviour
{
    private const float MaxDistFromWayPointToDisplay = 20.0f; //meters
    private const float MaxDistFromEnd = 5.0f; //meters

    //invoked when navigation of a path segment is finished
    //all callbacks for this event are attached via the GUI
    public UnityEvent pathSegmentFinished;

    private bool guidanceEnabled = false;

    private bool flag = false;

    private Path currentPath;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");   
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
            GPSCoords userPos = GPSSingleton.Instance.GetCurrentCoordinates();

            PathSegment currSegment = this.currentPath.GetCurrentSegment();

            //check if current visible ends need to be added or removed
            while ((currSegment.GetLastWaypoint() != currSegment.GetVisibleStart()) 
                        && (Math.Abs(userPos.GetDistance(currSegment.GetVisibleStart().Coordinates)) < MaxDistFromWayPointToDisplay)) {
                //make the next waypoint visible
                //placeWaypoint(getNext(), userPos);
            }

            while (Math.Abs(userPos.GetDistance(currSegment.GetVisibleEnd().Coordinates)) > MaxDistFromWayPointToDisplay) {
                //make the last waypoint NOT visible
            }

            //if we have access to knowing cloud anchors loads we might want to use that instead
            //check if in range of the end
            if ((currSegment.GetLastWaypoint() == currSegment.GetVisibleStart())
                    && (Math.Abs(userPos.GetDistance(currSegment.GetLastWaypoint().Coordinates)) < MaxDistFromEnd)) {
                //trigger pathsegment end
                guidanceEnabled = false;

                //trigger event callback
                this.pathSegmentFinished.Invoke();
            }
        }
    }

    //called when the tour starts
    //get path from database?
    public void SetCurrentPath(Path newPath) {
        cleanup();
        this.currentPath = newPath;
    }

    //this is called when the user clicks the Continue button after a stop on the tour
    public void StartNextPathSegment() {
        if (this.currentPath.HasNextSegment()) {
            this.currentPath.GetNextSegment();
            guidanceEnabled = true;
        }
    }

    private void placeWaypoint(Waypoint waypoint, GPSCoords userPos) {
        GPSCoords origin = GPSSingleton.Instance.GetUserOrigin();

        double latOffset = userPos.Latitude - waypoint.Coordinates.Latitude;
        double lonOffset = userPos.Longitude - waypoint.Coordinates.Longitude;

        // Instantiate(prefab, new Vector3((float)latOffset, 0, (float)lonOffset), Quaternion.identity);
        var obj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        obj.transform.position = new Vector3((float)latOffset, 0, (float)lonOffset);
        Debug.Log("object placed at: "+latOffset+":"+lonOffset);
        obj.transform.localScale = new Vector3(4, 4, 4);
    }

    private void removeWaypoint(Waypoint waypoint) {

    }

    private void cleanup() {
        // do clean up here
        //make sure that all waypoints are removed and not visible

        guidanceEnabled = false;
    }
}
