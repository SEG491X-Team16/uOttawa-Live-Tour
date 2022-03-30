using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSegment 
{
    private Waypoint[] _waypoints;
    public Waypoint[] Waypoints
    {
        get => _waypoints;
        set => _waypoints = value;
    }

    private int _visibleStart = -1;
    private int _visibleEnd = -1;
    
    public Waypoint GetLastWaypoint() {
        return (this._waypoints.Length == 0) ? (null) : (this._waypoints[this._waypoints.Length - 1]);
    }

    public Waypoint GetVisibleStart() {
        return (this._visibleStart == -1) ? (null) : (this._waypoints[this._visibleStart]);
    }

    public Waypoint GetNextVisibleStart() {
        return (this._visibleStart == this._waypoints.Length - 1) ? (null) : (this._waypoints[this._visibleStart + 1]);
    }

    public Waypoint GetVisibleEnd() {
        return (this._visibleEnd == -1) ? (null) : (this._waypoints[this._visibleEnd]);
    }

    public bool IncrementVisibleStart() {
        if (this._visibleStart < this._waypoints.Length - 1) {
            this._visibleStart++;

            if (this._visibleEnd == -1) {
                this._visibleEnd = 0;
            }

            return true;
        }
        return false;
    }

    public bool IncrementVisibleEnd() {
        if (this._visibleEnd == this._visibleStart) {
            return false;
        }

        this._visibleEnd++;
        return true;
    }

    public void ClearVisibleWaypoints() {
        foreach (Waypoint way in this._waypoints) {
            way.ClearInGameInstance();
        }
    }
}
