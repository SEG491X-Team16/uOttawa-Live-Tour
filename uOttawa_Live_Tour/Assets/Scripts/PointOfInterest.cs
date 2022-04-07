using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterest : MonoBehaviour
{

    public PointOfInterest(string cloudAnchorId, string buildingHighlight, GPSCoords coordinates) {
        this._cloudAnchorId = cloudAnchorId;
        this._buildingHighlight = buildingHighlight;
        this._coordinates = coordinates;
    }

    //ID of the cloud anchor for the POI
    private string _cloudAnchorId;

    public string CloudAnchorId 
    {
        get => _cloudAnchorId;
        set => _cloudAnchorId = value;
    }

    //building ID passed to the AR map for hightlighting
    private string _buildingHighlight;

    public string BuildingHighlight
    {
        get => _buildingHighlight;
        set => _buildingHighlight = value;
    }

    //GPS coordinates of the cloud anchor
    private GPSCoords _coordinates;

    public GPSCoords Coordinates
    {
        get => _coordinates;
        set => _coordinates = value;
    }

    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
