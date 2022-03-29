using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class StartDirections : MonoBehaviour
{
    //gameObjects in scene used in script
    [SerializeField] GameObject playerMarker;
    [SerializeField] GameObject tourStartPinMarker;
    [SerializeField] GameObject notOnCampusError;
    [SerializeField] GameObject map;
    [SerializeField] new Camera camera;
    [SerializeField] TextMeshProUGUI locationText;

    private const double radius = 6371000d; //radius of earth

    //lat & lon bounds for campus
    private double latMax = 45.425846d;
    private double lonMax = -75.675846d;
    private double latMin = 45.418926d;
    private double lonMin = -75.688351d;

    private double playerLat;
    private double playerLon;
    
    private double startLat = 45.42466d;
    private double startLon = -75.68608d;
    private float minDistance = 5f;
    float distance;
    
    //used so distance is updated 1 time/sec
    private int interval = 1; 
    private int nextTime = 0;
    
    bool onCampus = false;

    //used for scaling markers and camera size
    float minOrthographicSize = 1f;
    float maxOrthographicSize = 5f;
    float minScale = 0.0625f;
    float maxScale = 0.125f;
    public float smoothTime = 0.1F;
    private Vector3 velocityCam = Vector3.zero;
    private Vector3 velocityPlayerScale = Vector3.zero;
    private Vector3 velocityStartScale = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        //get GPS data
        if (GPSSingleton.Instance.IsDataValid()) {
            playerLat = GPSSingleton.Instance.GetCurrentCoordinates().Latitude;
            playerLon = GPSSingleton.Instance.GetCurrentCoordinates().Longitude;
        } else{
            //placeholder values when not enabled
            playerLat = playerMarker.GetComponent<MoveMarker>().lat;
            playerLon = playerMarker.GetComponent<MoveMarker>().lon;
        }

        //get tour starting position lat/lon
        startLat = tourStartPinMarker.GetComponent<MoveMarker>().lat;
        startLon = tourStartPinMarker.GetComponent<MoveMarker>().lon;

        onCampus = isOnCampus();

        if(!onCampus){
            //display not on campus error
            notOnCampusError.SetActive(true);
            map.SetActive(false);
            locationText.SetText("");
        } 
    }

    // Update is called once per frame
    void Update()
    { 
        GetLocation();
        
        if (Time.time >= nextTime) {
            nextTime += interval; //only check once a second

            onCampus = isOnCampus();
            if(!onCampus){
                //display not on campus error
                notOnCampusError.SetActive(true);
                map.SetActive(false);
                locationText.SetText("");

            } else {
                notOnCampusError.SetActive(false);
                map.SetActive(true);

                distance = CalculateDistace(playerLat, playerLon, startLat, startLon);
                locationText.SetText(" Distance to Start: " + Mathf.Round(distance) + "m");

                if(distance <= minDistance){
                    //display success, move to next scene
                    SceneSwitch.loadTourPath();
                }
            }  
        }
        
        MoveCamera(true);
        ZoomCamera(true);
        ScaleMarkers(true);
    }

    //checks if player is within bounds
    bool isOnCampus(){
        return (playerLat <= latMax && playerLat >= latMin) && (playerLon <= lonMax && playerLon >= lonMin);
    }


    //Method uses the Haversine formula to calculate the distance between the two GPS points
    //formula for calclating distance can be found at http://www.movable-type.co.uk/scripts/latlong.html
    float CalculateDistace(double lat1, double lon1, double lat2, double lon2){
        double radians = Mathf.PI/180;

        int r = 6371000;
        float l1 = (float)(lat1 * radians);
        float l2 = (float)(lat2 * radians);
        float dla = (float)((lat2-lat1) * radians);
        float dlo = (float)((lon2-lon1) * radians);

        float a = Mathf.Sin(dla/2) * Mathf.Sin(dla/2) + Mathf.Cos(l1) * Mathf.Cos(l2) * Mathf.Sin(dlo/2) * Mathf.Sin(dlo/2);
        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1-a));

        float distance = r * c;
        return distance;
    }

    //zooms camera based on position relative to tour starting position
    void ZoomCamera(bool easeTransition){
        //create bounds
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

        //add both markers' positions to bounds
        bounds.Encapsulate(tourStartPinMarker.transform.position);
        bounds.Encapsulate(playerMarker.transform.position);

        //get distance between points
        float boundingDistance = Mathf.Sqrt(bounds.size.x*bounds.size.x + bounds.size.y*bounds.size.y);

        float zoom = Mathf.Lerp(minOrthographicSize, maxOrthographicSize, distance/700);

        if (easeTransition){
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, zoom, Time.deltaTime);
        }else{
            camera.orthographicSize = zoom;
        }
    }

    //moves camera to center of player and tour starting position
    void MoveCamera(bool easeTransition){
        Vector3 midpoint = (tourStartPinMarker.transform.position + playerMarker.transform.position)/2;
        midpoint.y = Mathf.Max(tourStartPinMarker.transform.position.y, playerMarker.transform.position.y) + 1f;

        if (easeTransition){
            camera.transform.position = Vector3.SmoothDamp(camera.transform.position, midpoint, ref velocityCam, smoothTime);
        } else{
            camera.transform.position = midpoint;
        }
    }

    //scales markers' size based on position relative to eachother
    void ScaleMarkers(bool easeTransition){

        float scale = Mathf.Lerp(minScale, maxScale, distance/200);
        Vector3 scaleVector = new Vector3(scale,scale,scale);
        playerMarker.transform.localScale = Vector3.SmoothDamp(playerMarker.transform.localScale, scaleVector, ref velocityPlayerScale, smoothTime);
        tourStartPinMarker.transform.localScale = Vector3.SmoothDamp(tourStartPinMarker.transform.localScale, scaleVector, ref velocityStartScale, smoothTime);
    }

    void GetLocation(){
        if (GPSSingleton.Instance.IsDataValid()){
            playerLat = GPSSingleton.Instance.GetCurrentCoordinates().Latitude;
            playerLon = GPSSingleton.Instance.GetCurrentCoordinates().Longitude;
        } else{
            playerLat = playerMarker.GetComponent<MoveMarker>().lat;
            playerLon = playerMarker.GetComponent<MoveMarker>().lon;
        }
    }
}
