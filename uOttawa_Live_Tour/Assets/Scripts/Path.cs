using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    private PathSegment[] _segments;
    public PathSegment[] Segments
    {
        get => _segments;
        set {
            _segments = value;
            _currentSegmentIndex = -1;
        }
    }

    private int _currentSegmentIndex = -1;
}
