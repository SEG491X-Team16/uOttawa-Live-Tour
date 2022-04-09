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

        //segment 1
        Waypoint way1 = new Waypoint(new GPSCoords(45.054411f, -75.640017f), 1);
        Waypoint way2 = new Waypoint(new GPSCoords(45.054515f, -75.640025f), 2);
        Waypoint way3 = new Waypoint(new GPSCoords(45.054614f, -75.640073f), 3);

        Waypoint[] ways1 = new Waypoint[] {way1, way2, way3 };
        PathSegment seg1 = new PathSegment(ways1);

        //segment 2
        Waypoint way4 = new Waypoint(new GPSCoords(45.054439f, -75.639727f), 4);
        Waypoint way5 = new Waypoint(new GPSCoords(45.054436f, -75.639698f), 5);

        Waypoint[] ways2 = new Waypoint[] {way4, way5};
        PathSegment seg2 = new PathSegment(ways2);

        PathSegment[] segments = new PathSegment[] {seg1, seg2};

        //POIs
        GPSCoords arcPos = new GPSCoords(45.420713f, -75.678542f);
        GPSCoords crxPos = new GPSCoords(45.421709f, -75.681234f);

        Dialogue dia1 = new Dialogue();
        dia1.buildingName = "Tabaret Sign";
        dia1.informations = new string[] {"Welcome to the uOttawa Live Tour!", "This is Tabaret the first building ever built on campus. It's also on the uOttawa logo."};

        Dialogue dia2 = new Dialogue();
        dia2.buildingName = "She Dances with the Earth, Water and Sky";
        dia2.informations = new string[] {"This piece of artwork recognizes, and is dedicated to, the relationship between the University of Ottawa and the Omamìwìnini Anishinàbeg as well as all Indigenous people in the National Capital Region."};


        PointOfInterest pOI1 = new PointOfInterest("ua-3466088c3f3206288d98e66062cf15c5", "ARC", arcPos, dia1);
        PointOfInterest pOI2 = new PointOfInterest("ua-3466088c3f3206288d98e66062cf15c5", "CBY", crxPos, dia2);

        PointOfInterest[] pois = new PointOfInterest[] {pOI1, pOI2};

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
