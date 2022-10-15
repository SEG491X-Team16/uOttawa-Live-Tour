using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class respresents the entire Tour Path including all waypoints and POIs.
 */
public class Path
{
    //this error is throw when a bad POIs and Segments list are set
    [System.Serializable]
    public class InvalidPathException : System.Exception
    {
        public InvalidPathException() { }
        public InvalidPathException(string message) : base(message) { }
        public InvalidPathException(string message, System.Exception inner) : base(message, inner) { }
        protected InvalidPathException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    private PathSegment[] _segments = new PathSegment[0];
    public PathSegment[] Segments
    {
        get => _segments;
    }

    private PointOfInterest[] _pois = new PointOfInterest[0];

    public PointOfInterest[] POIs
    {
        get => _pois;
    }

    private int _currentSegmentIndex = 0;

    //POIs and Segments must be set together
    public void SetSegmentsAndPOIs(PathSegment[] segments, PointOfInterest[] pois) {
        if (segments.Length != pois.Length) {
            throw new InvalidPathException("POIs and Segments must be the same length");
        }

        this._segments = segments;
        this._pois = pois;
        this._currentSegmentIndex = 0;
    }

    public PointOfInterest GetCurrentPOI() {
        return (this._pois.Length == 0) ? (null) : (this._pois[_currentSegmentIndex]);
    }

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
        Debug.Log("get next seg");
        return GetCurrentSegment();
    }

    public void ClearVisiblePath() {
        foreach (PathSegment seg in this._segments) {
            seg.ClearVisibleWaypoints();
        }
    }
}
