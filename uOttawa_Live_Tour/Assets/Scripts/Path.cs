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
            _currentSegmentIndex = 0;
        }
    }

    private int _currentSegmentIndex = 0;

    public PathSegment getCurrentSegment() {
        return (this._segments.Length == 0) ? (null) : (this._segments[_currentSegmentIndex]);
    }

    public bool hasNextSegment() {
        return this._currentSegmentIndex < (this._segments.Length - 1);
    }

    public PathSegment getNextSegment() {
        if (hasNextSegment()) {
            this._currentSegmentIndex++;
        }
        return getCurrentSegment();
    }
}
