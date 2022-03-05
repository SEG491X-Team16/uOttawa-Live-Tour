using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint //: MonoBehaviour
{
    private GPSCoords _coordinates = new GPSCoords(0, 0);

    public GPSCoords Coordinates
    {
        get => _coordinates;
        set => _coordinates = value;
    }

    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
