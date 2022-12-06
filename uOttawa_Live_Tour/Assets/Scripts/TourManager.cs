using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using TMPro;

/**
 * This manager coordinates the tour and the other managers for the tour.
 * It controls the entire TourPath scene
 */
public class TourManager : MonoBehaviour
{

    //reference to the Path Manager in the scene
    public PathManager pathManager;

    //reference to the POI Manager in the scene
    public POIManager poiManager;

    //reference to the Dialog Manager in the scene
    public DialogueManager dialogueManager;

    //reference to the Building Highlighter on the 3D map in the scene
    public BuildingHighlight buildingHighlighter;

    //reference to the Destination Pin on the 3D map in the scene
    public MoveMarker destinationMarker;
    
    //reference to the controller for the waypoints on the 3D map in the scene
    public MapDirections mapDirections;

    //reference to the object that handles switching scenes
    public SceneSwitch sceneSwitch;

    //the tour path to be used for the tour
    private Path _path;

    //time interval and next time to display distance updates
    private int interval = 1; 
    private int nextTime = 0;

    //reference to distance/next building heading text
    public Text nextBuildingText;
    public Text distanceText;

    public Image nextBuildingImage;
    public TextMeshProUGUI nextBuildingName;

    // Start is called before the first frame update
    void Start()
    {
        //prevent the screen from going to sleep
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //set up the tour
        if (LocalizationSettings.SelectedLocale.LocaleName == "Français (fr)")
        {
            this._path = TourPathGenerator.GetFrenchPath();
        }
        else
        {
            this._path = TourPathGenerator.GetEnglishPath();
            // this._path = getAppartmentPath();
        }
        pathManager.SetCurrentPath(this._path);

        // UpdateNextSegementOn3DMap();
        Waypoint[] start = { new Waypoint(new GPSCoords(45.42456813170738f, -75.6859576372278f), 1), new Waypoint(new GPSCoords(45.42454778216218f, -75.68598234644377f), 2) };
        buildingHighlighter.SetBuildingHighlight(this._path.GetCurrentPOI().BuildingHighlight);
        destinationMarker.lat = this._path.GetCurrentPOI().Coordinates.Latitude;
        destinationMarker.lon = this._path.GetCurrentPOI().Coordinates.Longitude;
        
        string nextStopText = "Next Stop - ";
        if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.GetLocale("fr")){
            nextStopText = "Prochain Arrêt - ";
        } 
        
        
        mapDirections.SetDirections(start);
        UpdateUIElements();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDistanceText();      
    }

    //callback when the user reaches the end of the path segment
    //tells the POI Manager to start looking for the POI
    public void OnPathSegmentFinished() {
        Debug.Log("path segment finished callback");
        poiManager.AddPOI(this._path.GetCurrentPOI());
        mapDirections.ClearDirections();
    }

    //callback when the POI Manager finds the POI
    //tells the DialogManager to show the Dialog
    public void OnPOIAchieved() {
        Debug.Log("POI achieved callback");
        pathManager.CleanupCurrentSegment();

        dialogueManager.StartDialogue(this._path.GetCurrentPOI().Dialogue);
    }

    //callback when the DialogManager signals the dialog as complete and
    //the user is ready for the next path segement
    public void OnContinueTour() {
        Debug.Log("continue tour callback");
        //cleanup
        poiManager.RemovePOI(this._path.GetCurrentPOI());
        buildingHighlighter.ClearBuildingHighlight(this._path.GetCurrentPOI().BuildingHighlight);

        //if we've reached the end of the tour, go back to the main menu
        if (!pathManager.StartNextPathSegment()) {
            sceneSwitch.loadEnding();
        }

        //start the next segment
        UpdateNextSegementOn3DMap();
        UpdateUIElements();
    }

    private void UpdateUIElements(){
        nextBuildingText.text = ("Next Stop - " + this._path.GetCurrentPOI().BuildingHighlight);
        nextBuildingImage.sprite = Resources.Load<Sprite>(this._path.GetCurrentPOI().BuildingImagePath);
        nextBuildingName.SetText(this._path.GetCurrentPOI().BuildingName);

    }

    //called when starting the next segment to update the 3D map in the scenee
    private void UpdateNextSegementOn3DMap()
    {
        buildingHighlighter.SetBuildingHighlight(this._path.GetCurrentPOI().BuildingHighlight);
        destinationMarker.lat = this._path.GetCurrentPOI().Coordinates.Latitude;
        destinationMarker.lon = this._path.GetCurrentPOI().Coordinates.Longitude;
        nextBuildingText.text = ("Next Stop - " + this._path.GetCurrentPOI().BuildingHighlight);
        mapDirections.SetDirections(this._path.GetCurrentSegment().Waypoints);
    }

    private Path getPath() {
        Path path = new Path();

        //segment 1: 
        Waypoint way1 = new Waypoint(new GPSCoords(0,0), 1);

        Waypoint[] waypoints1 = new Waypoint[] {way1};
        PathSegment seg1 = new PathSegment(waypoints1);

        Dialogue dia1 = new Dialogue();
        dia1.buildingName = "name1";
        dia1.informations = new string[] {"info1", "info2", "info3"};
        dia1.audioClips = new AudioClip[] { Resources.Load<AudioClip>("Audio/90U/90U_is_one_of_uOttaw_426"), Resources.Load<AudioClip>("Audio/90U/90U_is_one_of_uOttaw_426"), Resources.Load<AudioClip>("Audio/90U/90U_is_one_of_uOttaw_426")};
        
        PointOfInterest poi1 = new PointOfInterest("ua-b2bb7bb87eca47a53fd6591fceb0601e", "ARC", new GPSCoords(0,0), dia1);

        //segment 2:

        //segment 3:

        //assemble path
        PathSegment[] segments = {seg1};
        PointOfInterest[] pois = {poi1};

        path.SetSegmentsAndPOIs(segments, pois);

        return path;
    }

    private Path getAppartmentPath() {
        //TODO: get the path from the database

        Path path = new Path();
        
        //sgement 1
        Waypoint way1 = new Waypoint(new GPSCoords(45.42508482415584f, -75.6792733837361f), 1);
        Waypoint way2 = new Waypoint(new GPSCoords(45.42510082527518f, -75.67928075981074f), 2);
        Waypoint way3 = new Waypoint(new GPSCoords(45.42513423936271f, -75.67930288803466f), 3);

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
        dia1.informations = new string[] {"Welcome to the uOttawa Live Tour!", "This is Tabaret, the first building ever built on campus. It's also on the uOttawa logo."};

        Dialogue dia2 = new Dialogue();
        dia2.buildingName = "She Dances with the Earth, Water and Sky";
        dia2.informations = new string[] {"She Dances with the Earth, Water and Sky: This piece of artwork recognizes, and is dedicated to, the relationship between the University of Ottawa and the Omamìwìnini Anishinàbeg as well as all Indigenous people in the National Capital Region."};


        PointOfInterest pOI1 = new PointOfInterest("ua-3466088c3f3206288d98e66062cf15c5", "ARC", arcPos, dia1);
        PointOfInterest pOI2 = new PointOfInterest("ua-3466088c3f3206288d98e66062cf15c5", "CRX", crxPos, dia2);

        PointOfInterest[] pois = new PointOfInterest[] {pOI1, pOI2};

        path.SetSegmentsAndPOIs(segments, pois);

        Debug.Log("path created");

        return path;
    }

    private Path getTestPath() {
        //TODO: get the path from the database

        Path path = new Path();
        
        //sgement 1
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
        dia1.informations = new string[] {"Welcome to the uOttawa Live Tour!", "This is Tabaret, the first building ever built on campus. It's also on the uOttawa logo."};

        Dialogue dia2 = new Dialogue();
        dia2.buildingName = "She Dances with the Earth, Water and Sky";
        dia2.informations = new string[] {"She Dances with the Earth, Water and Sky: This piece of artwork recognizes, and is dedicated to, the relationship between the University of Ottawa and the Omamìwìnini Anishinàbeg as well as all Indigenous people in the National Capital Region."};


        PointOfInterest pOI1 = new PointOfInterest("ua-3466088c3f3206288d98e66062cf15c5", "ARC", arcPos, dia1);
        PointOfInterest pOI2 = new PointOfInterest("ua-3466088c3f3206288d98e66062cf15c5", "CRX", crxPos, dia2);

        PointOfInterest[] pois = new PointOfInterest[] {pOI1, pOI2};

        path.SetSegmentsAndPOIs(segments, pois);

        Debug.Log("path created");

        return path;
    }

    //this is the current demo path
    private Path getTabaretPath() {
        //TODO: get the path from the database

        Path path = new Path();

        // Path Segment #1
        Waypoint way1 = new Waypoint(new GPSCoords(45.424638f, -75.685985f), 1);
        
        Waypoint[] ways1 = new Waypoint[] {way1};
        PathSegment seg1 = new PathSegment(ways1);
        
        // Path,Segment #2
        Waypoint way4 = new Waypoint(new GPSCoords(45.424620f, -75.685984f), 4);
        Waypoint way5 = new Waypoint(new GPSCoords(45.424701f, -75.686042f), 5);
        Waypoint way2 = new Waypoint(new GPSCoords(45.424810f, -75.686134f), 2);
        Waypoint way3 = new Waypoint(new GPSCoords(45.424906f, -75.686210f), 3);

        Waypoint[] ways2 = new Waypoint[] {way4, way5, way2, way3};
        PathSegment seg2 = new PathSegment(ways2);

        //path segment #3
        Waypoint way6 = new Waypoint(new GPSCoords(45.425017f, -75.686314f), 6);
        Waypoint way7 = new Waypoint(new GPSCoords(45.425115f, -75.686395f), 7);
        Waypoint way8 = new Waypoint(new GPSCoords(45.425041f, -75.686601f), 8);
        Waypoint way9 = new Waypoint(new GPSCoords(45.424991f, -75.686748f), 9);

        Waypoint[] ways3 = new Waypoint[] {way6, way7, way8, way9};
        PathSegment seg3 = new PathSegment(ways3);

        PathSegment[] segments = new PathSegment[] {seg1, seg2, seg3};

        //Points of Interest
        GPSCoords signPos = new GPSCoords(45.424552f, -75.685973f);
        GPSCoords statuePos = new GPSCoords(45.425004f, -75.686192f);
        GPSCoords haganPos = new GPSCoords(45.425038f, -75.686832f);

        Dialogue dia1 = new Dialogue();
        dia1.buildingName = "Tabaret Sign";
        dia1.informations = new string[] {"Welcome to the uOttawa Live Tour!", "This is Tabaret, the first building ever built on campus. It's also on the uOttawa logo."};

        Dialogue dia2 = new Dialogue();
        dia2.buildingName = "She Dances with the Earth, Water and Sky";
        dia2.informations = new string[] {"She Dances with the Earth, Water and Sky: This piece of artwork recognizes, and is dedicated to, the relationship between the University of Ottawa and the Omamìwìnini Anishinàbeg as well as all Indigenous people in the National Capital Region."};

        Dialogue dia3 = new Dialogue();
        dia3.buildingName = "Hagan Hall";
        dia3.informations = new string[] {"This is Hagan Hall, one of the buildings on campus frequently used for classes. Hint: look for the abbrevation 'HGN' in front of the room name for classes here."};

        PointOfInterest pOI1 = new PointOfInterest("ua-6a4d616b518ee56e51b2dd0f354abba4", "TBT", signPos, dia1);
        PointOfInterest pOI2 = new PointOfInterest("ua-8b8ebc2d7f303d5e749506701d75d6da", "TBT", statuePos, dia2);
        PointOfInterest pOI3 = new PointOfInterest("ua-3ed9aac7f115ccc7560ff188518fc26a", "HGN", haganPos, dia3);

        PointOfInterest[] pois = new PointOfInterest[] {pOI1, pOI2, pOI3};

        path.SetSegmentsAndPOIs(segments, pois);

        Debug.Log("path created");

        return path;
    }

    private void UpdateDistanceText(){
        if (Time.time >= nextTime) {
            nextTime += interval; 

            GPSCoords userPos = GPSSingleton.Instance.GetCurrentCoordinates();
            double distance = userPos.GetDistance(this._path.GetCurrentPOI().Coordinates);
            distanceText.text = ("Distance - " + Mathf.Round((float)distance) + "m");
        }
    }
}
