using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Google.XR.ARCoreExtensions;
using UnityEngine.XR.ARSubsystems;

/**
 * This manager handles the placement of waypoints and any other specifics of following the path.
 *
 * Waypoint placing based on theory/quasi-example from: https://blog.anarks2.com/Geolocated-AR-In-Unity-ARFoundation/
 */
public class PathManager : MonoBehaviour
{
    //Max distance user can be from waypoint for them to be visible
    private const float MaxDistFromWayPointToDisplay = 500.0f; //meters

    //Max distance user can be from the last waypoint for the system to register as
    //the user having reached the end of the segment
    private const float MaxDistFromEnd = 500.0f; //meters

    //minimum heading error before arrows will be placed
    private const float MinHeadingAccuracy = 4.0f; //degrees 

    //invoked when navigation of a path segment is finished
    //all callbacks for this event are attached via the GUI

    //The controller than handles the geospatial api calls
    public GeospatialController geospatialController;

    public GameObject userCamera;

    public UnityEvent pathSegmentFinished;

    //the prefab used for instantiating the waypoint arrows
    public GameObject arrowPrefab;

    
    public GameObject posCalMsg;

    //Two models used for debugging
    public GameObject userPosPrefab;
    private GameObject userPosInstance;

    //flag that is true if the pathManager should display waypoints
    private bool guidanceEnabled = false;

    //the Path that is currently being followed
    private Path currentPath;

    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        if(this.geospatialController.getEarthTrackingState() != TrackingState.Tracking)
        {
            Debug.Log("waiting for heading");
        }

        Debug.Log("heading accuracy: "+this.geospatialController.getCameraGeospatialPose().HeadingAccuracy+":"+this.geospatialController.getCameraGeospatialPose().Heading);

        //if we have a path
        if ((this.currentPath != null) && guidanceEnabled && (this.geospatialController.getEarthTrackingState() == TrackingState.Tracking) && (this.geospatialController.getCameraGeospatialPose().HeadingAccuracy <= MinHeadingAccuracy)) {
            this.posCalMsg.SetActive(false);
            GPSCoords userPos = GPSSingleton.Instance.GetCurrentCoordinates();
            PathSegment currSegment = this.currentPath.GetCurrentSegment();

            if(time > 0) {
                time -= Time.deltaTime;
 
                if(time < 0) time = 0;
            }
     
            if(time <= 0){
                //Do stuff here
                time = 5;
                Debug.Log("updating waypoints");

                //place the waypoints
                this.currentPath.ClearVisiblePath();
                for (int i = 0; i < currSegment.Waypoints.Length; i++)
                {
                    // currSegment.IncrementVisibleStart();
                    // Waypoint next = currSegment.Waypoints[i + 1];//currSegment.GetNextVisibleStart();

                    //point to POI if no more waypoints
                    GPSCoords pointTo;
                    if (i == currSegment.Waypoints.Length - 1) {//(next == null) {
                        pointTo = currentPath.GetCurrentPOI().Coordinates;
                    } else {
                        pointTo = currSegment.Waypoints[i + 1].Coordinates;//next.Coordinates;
                    }
                    
                    placeWaypoint(currSegment.Waypoints[i], pointTo);//currSegment.GetVisibleStart(), pointTo);
                }
            }

            

            //check if current visible ends need to be added or removed
            // while (((currSegment.GetVisibleStart() == null) || (currSegment.GetLastWaypoint() != currSegment.GetVisibleStart())) 
            //             && (Math.Abs(userPos.GetDistance(currSegment.GetNextVisibleStart().Coordinates)) < MaxDistFromWayPointToDisplay)) {
            //     //make the next waypoint visible
            //     if (currSegment.IncrementVisibleStart()) {
            //         Waypoint next = currSegment.GetNextVisibleStart();

            //         //point to POI if no more waypoints
            //         GPSCoords pointTo;
            //         if (next == null) {
            //             pointTo = currentPath.GetCurrentPOI().Coordinates;
            //         } else {
            //             pointTo = next.Coordinates;
            //         }
                    
            //         placeWaypoint(currSegment.GetVisibleStart(), pointTo);
            //     }
            // }

            // while ((currSegment.GetVisibleEnd() != null) && Math.Abs(userPos.GetDistance(currSegment.GetVisibleEnd().Coordinates)) > MaxDistFromWayPointToDisplay) {
            //     //TODO: make the last waypoint NOT visible
            // }

            //if we have access to knowing cloud anchors loads we might want to use that instead
            //check if in range of the end
            // if ((currSegment.GetVisibleStart() != null) && (currSegment.GetLastWaypoint() == currSegment.GetVisibleStart())
            //         && (Math.Abs(userPos.GetDistance(currSegment.GetLastWaypoint().Coordinates)) < MaxDistFromEnd)) {
            if (Math.Abs(userPos.GetDistance(currSegment.GetLastWaypoint().Coordinates)) < MaxDistFromEnd) {
                //trigger pathsegment end
                Debug.Log("reached end");
                guidanceEnabled = false;
                // cleanup();

                //trigger event callback
                this.pathSegmentFinished.Invoke();
            }
        }
        else
        {
            this.posCalMsg.SetActive(true);
        }

        //show current user pos for debugging
        //TODO: remove this when done
        // if (userPosPrefab != null) {
        //     if (userPosInstance != null) {
        //         Destroy(userPosInstance);
        //     }

        //     Waypoint way = new Waypoint(GPSSingleton.Instance.GetCurrentCoordinates(), 0);
        //     Vector3 waypointPos = getWaypointUnityPos(way.Coordinates);

        //     userPosInstance = Instantiate(userPosPrefab, waypointPos, Quaternion.identity);
        // }
    }

    //called when the tour starts
    //get path from database?
    public void SetCurrentPath(Path newPath) {
        cleanup();
        this.currentPath = newPath;
        guidanceEnabled = true;
    }

    //this is called when the user clicks the Continue button after a stop on the tour
    public bool StartNextPathSegment() {
        cleanup();
        if (this.currentPath.HasNextSegment()) {
            this.currentPath.GetNextSegment();
            guidanceEnabled = true;
            return true;
        }
        return false;
    }

    public void CleanupCurrentSegment()
    {
        cleanup();
    }

    //place the waypoint and have it point to the next waypoint
    private void placeWaypoint(Waypoint waypoint, GPSCoords pointTo) {
        Debug.Log("rendering waypoint");

        //get position of this waypoint
        Debug.Log("waypoint");
        Vector3 waypointPos = getWaypointUnityPos(waypoint.Coordinates);

        //get direction to point to
        Debug.Log("points to");
        Vector3 nextWaypointPos = getWaypointUnityPos(pointTo);

        Vector3 relativePos = waypointPos - nextWaypointPos;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up) * Quaternion.Euler(0, -90, 0); 
        //rotate 90 degrees to compensate for the prefab beign 90 degrees off 

        //create the waypoint in the unity world
        waypoint.SetInGameInstance(Instantiate(arrowPrefab, waypointPos, rotation));//Quaternion.identity);
        Debug.Log("set waypoint at "+waypointPos+":"+rotation);
    }

    //get the waypoint position in the unity world from the waypoint GPS point
    private Vector3 getWaypointUnityPos(GPSCoords waypoint) {
        // GPSCoords origin = GPSSingleton.Instance.GetUserOrigin();
        GPSCoords origin = new GPSCoords((float)this.geospatialController.getCameraGeospatialPose().Latitude, (float)this.geospatialController.getCameraGeospatialPose().Longitude);

        double x = origin.GetDistance(new GPSCoords(origin.Latitude, waypoint.Longitude));
        double z = origin.GetDistance(new GPSCoords(waypoint.Latitude, origin.Longitude));

        //correct the positive vs negative axis
        if (origin.Latitude > waypoint.Latitude) {
            z = -1 * z;
        }

        if (origin.Longitude > waypoint.Longitude) {
            x = -1 * x;
        }
        Debug.Log("lat, lon: "+waypoint.Latitude+","+waypoint.Longitude+"x, z: "+x+","+z);

        // Debug.Log("heading: "+ GPSSingleton.Instance.GetUserOriginHeading());

        //vector3 = (x, y, z)
        Vector3 dir = new Vector3((float)x, 0, (float)z) + this.userCamera.transform.position;
        Vector3 heading;
        if (this.geospatialController.getEarthTrackingState() == TrackingState.Tracking)
        {
            Debug.Log("using the VPS headings"+(this.geospatialController.getCameraGeospatialPose().Heading - this.userCamera.transform.rotation.y)+":"+this.geospatialController.getCameraGeospatialPose().Heading+":"+this.userCamera.transform.rotation.y+":"+this.geospatialController.getCameraGeospatialPose().HeadingAccuracy);
            heading = new Vector3(0, (float)(this.geospatialController.getCameraGeospatialPose().Heading - this.userCamera.transform.rotation.y), 0);
        }
        else
        {
            Debug.Log("defaulting to GPS heading");
            heading = new Vector3(0, -1*(float)GPSSingleton.Instance.GetUserOriginHeading(), 0);
        }
        dir = Quaternion.Euler(heading) * dir;

        return dir;
    }

    private void removeWaypoint(Waypoint waypoint) {
        Debug.Log("removing waypoints");
        waypoint.ClearInGameInstance();
    }

    //clean up the path, and move all visible waypoints
    private void cleanup() {
        //make sure that all waypoints are removed and not visible
        if (this.currentPath != null) {
            this.currentPath.ClearVisiblePath();
        }

        guidanceEnabled = false;
    }
}
