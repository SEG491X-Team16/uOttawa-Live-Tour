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
    
    public Waypoint getLastWaypoint() {
        return (this._waypoints.Length == 0) ? (null) : (this._waypoints[this._waypoints.Length - 1]);
    }

    public Waypoint getVisibleStart() {
        return (this._visibleStart == -1) ? (null) : (this._waypoints[this._visibleStart]);
    }

    public Waypoint getVisibleEnd() {
        return (this._visibleEnd == -1) ? (null) : (this._waypoints[this._visibleEnd]);
    }
}
