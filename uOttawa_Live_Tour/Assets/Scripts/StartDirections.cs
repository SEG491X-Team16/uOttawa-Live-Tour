using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class StartDirections : MonoBehaviour
{

    [SerializeField] GameObject playerMarker;
    [SerializeField] GameObject tourStartPinMarker;
    [SerializeField] GameObject notOnCampusError;
    [SerializeField] GameObject map;
    [SerializeField] Camera camera;
    [SerializeField] TextMeshProUGUI locationText;

    private const double radius = 6371000d;
    private double latMax = 45.425846d;
    private double lonMax = -75.675846d;
    private double latMin = 45.418926d;
    private double lonMin = -75.688351d;
    private double playerLat;
    private double playerLon;
    
    private double startLat = 45.42466d;
    private double startLon = -75.68608d;
    private double minDistance = 3d;
    float distance;
    
    private int interval = 1; 
    private int nextTime = 0;
    
    bool onCampus = false;
    bool zoomedIn = false;
    float minOrthographicSize = 1f;
    float maxOrthographicSize = 5f;
    public float smoothTime = 0.1F;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

        if (GPSSingleton.Instance.isDataValid()) {
            playerLat = GPSSingleton.Instance.getCurrentCoordinates().Latitude;
            playerLon = GPSSingleton.Instance.getCurrentCoordinates().Longitude;
        } else{
            playerLat = playerMarker.GetComponent<MoveMarker>().lat;
            playerLon = playerMarker.GetComponent<MoveMarker>().lon;
        }

        onCampus = isOnCampus();

        if(!onCampus){
            //not on campus error
            notOnCampusError.SetActive(true);
            map.SetActive(false);
            locationText.SetText("");
        } else{
            //distance = CalculateDistace(playerLat, playerLon, startLat, startLon);
            //MoveCamera(false);
            //ZoomCamera(false);
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
                notOnCampusError.SetActive(true);
                map.SetActive(false);
                locationText.SetText("");
                //not on campus error
            } else {
                notOnCampusError.SetActive(false);
                map.SetActive(true);

                distance = CalculateDistace(playerLat, playerLon, startLat, startLon);

                locationText.SetText("" + playerLat + ", "+ playerLon + " Distance: " + distance);

                if(distance <= minDistance){
                    //display success, move to next scene
                }
            }  
        }
        MoveCamera(true);
        ZoomCamera(true);
    }

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

    void ZoomCamera(bool easeTransition){
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
        bounds.Encapsulate(tourStartPinMarker.transform.position);
        bounds.Encapsulate(playerMarker.transform.position);
        float boundingDistance = bounds.size.x;
        //Debug.Log(boundingDistance + ", " + distance/700);

        float zoom = Mathf.Lerp(minOrthographicSize, maxOrthographicSize, distance/700);
        if (easeTransition){
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, zoom, Time.deltaTime);
        }else{
            camera.orthographicSize = zoom;
        }
    }

    void MoveCamera(bool easeTransition){
        Vector3 midpoint = (tourStartPinMarker.transform.position + playerMarker.transform.position)/2;
        midpoint.y += 10f;
        if (easeTransition){
            camera.transform.position = Vector3.SmoothDamp(camera.transform.position, midpoint, ref velocity, smoothTime);
        } else{
            camera.transform.position = midpoint;
        }
    }

    void GetLocation(){
        if (GPSSingleton.Instance.isDataValid()){
            playerLat = GPSSingleton.Instance.getCurrentCoordinates().Latitude;
            playerLon = GPSSingleton.Instance.getCurrentCoordinates().Longitude;
        } else{
            playerLat = playerMarker.GetComponent<MoveMarker>().lat;
            playerLon = playerMarker.GetComponent<MoveMarker>().lon;
        }
    }
}
