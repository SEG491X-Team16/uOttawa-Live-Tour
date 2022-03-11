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

    public PathSegment GetCurrentSegment() {
        return (this._segments.Length == 0) ? (null) : (this._segments[_currentSegmentIndex]);
    }

    public bool HasNextSegment() {
        return this._currentSegmentIndex < (this._segments.Length - 1);
    }

    public PathSegment GetNextSegment() {
        if (HasNextSegment()) {
            this._currentSegmentIndex++;
        }
        return GetCurrentSegment();
    }
}
