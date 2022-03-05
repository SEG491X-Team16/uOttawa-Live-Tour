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
    

}
