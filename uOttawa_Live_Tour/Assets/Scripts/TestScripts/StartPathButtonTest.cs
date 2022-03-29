using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPathButtonTest : MonoBehaviour
{
    public Button startBtn;

    public Button nextBtn;

    public PathManager pathManager;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = startBtn.GetComponent<Button>();
        btn.onClick.AddListener(onClickTask);

        Button btnN = nextBtn.GetComponent<Button>();
        btnN.onClick.AddListener(onClickNext);
        nextBtn.enabled = false;
    }

    void onClickTask() {
        Debug.Log("Start path clicked");

        Path path = new Path();
        PathSegment seg1 = new PathSegment();

        Waypoint way1 = new Waypoint();
        GPSCoords gps1 = new GPSCoords(45.054411f, -75.640017f);//(45.05434f, -75.63994f);
        way1.Coordinates = gps1;
        way1.ID = 1;

        // Waypoint way1 = new Waypoint();
        // GPSCoords gps1 = new GPSCoords(45.054439, -75.639727);
        // way1.Coordinates = gps1;

        Waypoint way2 = new Waypoint();
        GPSCoords gps2 = new GPSCoords(45.054515f, -75.640025f);//(45.054436f, -75.639698f);
        way2.Coordinates = gps2;
        way2.ID = 2;

        Waypoint way3 = new Waypoint();
        GPSCoords gps3 = new GPSCoords(45.054614f, -75.640073f);//(45.054449f, -75.639620f);
        way3.Coordinates = gps3;
        way3.ID = 3;

        Waypoint[] ways1 = new Waypoint[] {way1, way2, way3 };
        seg1.Waypoints = ways1;

        PathSegment seg2 = new PathSegment();

        Waypoint way4 = new Waypoint();
        GPSCoords gps4 = new GPSCoords(45.054439f, -75.639727f);
        way4.Coordinates = gps4;
        way4.ID = 4;

        Waypoint way5 = new Waypoint();
        GPSCoords gps5 = new GPSCoords(45.054436f, -75.639698f);
        way5.Coordinates = gps5;
        way5.ID = 5;

        Waypoint[] ways2 = new Waypoint[] {way4, way5};
        seg2.Waypoints = ways2;

        PathSegment[] segments = new PathSegment[] {seg1, seg2};
        path.Segments = segments;

        Debug.Log("path created");

        pathManager.SetCurrentPath(path);

        // Debug.Log("path set up");
        // pathManager.StartNextPathSegment();
        Debug.Log("path started");
    }

    public void onPathSegmentFinished() {
        nextBtn.enabled = true;
        Debug.Log("path segment done");
    }

    public void onClickNext() {
        nextBtn.enabled = false;
        Debug.Log("continuing to next segement");
        pathManager.StartNextPathSegment();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
