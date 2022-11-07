using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDirections : MonoBehaviour
{
    private LineRenderer lineRenderer;

    public Waypoint[] waypoints;
    public GameObject[] go;
    public GameObject map;

    [SerializeField]
    public float y;

    [SerializeField]
    public Texture[] frames;

    [SerializeField]
    public float fps = 30;

    [SerializeField]
    public float counter;

    public int frame;
    private MoveMarker moveMarker;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        map = GameObject.Find("uOttawaMap");
        moveMarker = this.GetComponent<MoveMarker>();

        // Example directions
        // GPSCoords c1 = new GPSCoords(45.423370f, -75.683817f); first point
        // GPSCoords c2 = new GPSCoords(45.422591f, -75.682041f);
        // GPSCoords c3 = new GPSCoords(45.422207f, -75.683533f); last point
        // waypoints = new Waypoint[3];
        // waypoints[0] = new Waypoint(c1,1);
        // waypoints[1] = new Waypoint(c2,2);
        // waypoints[2] = new Waypoint(c3,3);
        // setDirections(waypoints);

    }

    //Clears current reference GOs, creates a new array of GOs, and set waypoints to new values
    public void setDirections(Waypoint[] waypoints){
        clearDirections();

        lineRenderer.positionCount = waypoints.Length;
        this.waypoints = waypoints;

        go = new GameObject[this.waypoints.Length];

        for(int i = 0; i < this.waypoints.Length; i++){
            go[i] = new GameObject("MapDirectionsWayPoint");
            go[i].transform.SetParent(map.transform);
        }
    }

    //clears all current GOs used as reference
    public void clearDirections(){
        for(int i = 0; i < this.go.Length; i++){
            GameObject.Destroy(go[i]);
        }
        this.waypoints = null;
        lineRenderer.positionCount = 0;
    }

    //Updates the correct frame of the direction arrow's animation     
    //Array of GOs are transformed within map's localspace and their global positioning is used as endpoints for the linerenderer
    void LateUpdate()
    {
        counter += Time.deltaTime;
        if (counter >= 1f/fps){
            frame = ++frame % frames.Length;
            lineRenderer.material.SetTexture("_MainTex", frames[frame]);
            counter = 0f;
        }

        for(int i = 0; i < waypoints.Length; i++){
            go[i].transform.localPosition = new Vector3(moveMarker.GetLonToX(waypoints[i].Coordinates.Longitude), y , moveMarker.GetLatToZ(waypoints[i].Coordinates.Latitude));
            lineRenderer.SetPosition(i, go[i].transform.position);
        }

    }
}
