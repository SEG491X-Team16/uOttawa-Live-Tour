using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapScript2D : MonoBehaviour
{
    public GameObject camera;
    public float playerLat;
    public float playerLon;

    private const float radius = 6371000f;
    private float latMax = 45.425846f;
    private float lonMax = -75.675846f;
    private float latMin = 45.418926f;
    private float lonMin = -75.688351f;
    
    private float startLat = 45.42466f;
    private float startLon = -75.68608f;
    
    public bool onCampus = false;

    public float yOffset;
    public float forwardOffset;

    [SerializeField] TextMeshProUGUI notOnCampusError;
    [SerializeField] TextMeshProUGUI atDestination;
    void Start (){
        //move map to position infront of cameraYRot so it is always a fixed position in front of the camera
        transform.position = camera.transform.position + camera.transform.forward * forwardOffset + camera.transform.up;
        Vector3 rotation = transform.eulerAngles;
        rotation.y += yOffset;
        transform.eulerAngles = rotation; 

    }
    
    void Update()
    {
        if((playerLat <= latMax && playerLat >= latMin) && (playerLon <= lonMax && playerLon >= lonMin)){
            //Debug.Log("on campus");
            onCampus = true;
            float distance = CalculateDistace(playerLat, playerLon, startLat, startLon);
            notOnCampusError.SetText("ON CAMPUS!!!!" + distance.ToString());

        }else {
            onCampus = false;
            //Debug.Log( ""+ (playerLat <= latMax) + "" + (playerLat >= latMin) + "" + (playerLon <= lonMax) +""+ (playerLon >= lonMin));
            notOnCampusError.SetText("NOT ON CAMPUS >:(");

        }

        if(!onCampus){

        }

    }

    float CalculateDistace(float lat1, float lon1, float lat2, float lon2){
        // x = Δλ * cos φm
        // y = Δφ
        // d = R * √x² + y²

        float avgLat = (lat1 + lat2)/2;
        float x = (lon2 - lon1) * Mathf.Cos(avgLat);
        float y = (lat2 - lat1);

        float distance = Mathf.Sqrt(x*x + y*y) * radius;

        return distance;
    }
}
