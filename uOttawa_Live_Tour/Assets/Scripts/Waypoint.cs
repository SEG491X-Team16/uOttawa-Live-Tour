using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class represents one of the arrows along the tour path that
 * a trail for the user to follow along the tour.
 */
public class Waypoint
{

    public Waypoint(GPSCoords coordinates, int id) {
        this._coordinates = coordinates;
        this._id = id;
    }

    private GPSCoords _coordinates = new GPSCoords(0, 0);

    public GPSCoords Coordinates
    {
        get => _coordinates;
        set => _coordinates = value;
    }

    private int _id = 0;

    public int ID
    {
        get => _id;
        set => _id = value;
    }

    private GameObject _inGameInstance;

    public void SetInGameInstance(GameObject instance) {
        this._inGameInstance = instance;
    }

    public void ClearInGameInstance() {
        if (this._inGameInstance != null) {
            GameObject.Destroy(_inGameInstance);
            this._inGameInstance = null;
        }
    }

    public bool IsVisible() {
        return this._inGameInstance != null;
    }
    
}
