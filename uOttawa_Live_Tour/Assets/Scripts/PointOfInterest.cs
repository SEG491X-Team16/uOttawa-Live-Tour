using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class represents a tour stop on the tour.
 */
public class PointOfInterest
{

    public PointOfInterest(string cloudAnchorId, string buildingHighlight, GPSCoords coordinates, Dialogue dialogue) {
        this._cloudAnchorId = cloudAnchorId;
        this._buildingHighlight = buildingHighlight;
        this._coordinates = coordinates;
        this._dialogue = dialogue;
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

    //the dialog to be shown at the POI
    private Dialogue _dialogue;

    public Dialogue Dialogue
    {
        get => _dialogue;
        set => _dialogue = value;
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
