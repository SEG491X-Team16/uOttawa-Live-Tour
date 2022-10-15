using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using NUnit.Framework;

public class PathObjectTestSuite
{

    private const int NumberOfSegments = 4;

    private PathSegment[] segments = new PathSegment[NumberOfSegments];

    private PointOfInterest[] pois = new PointOfInterest[NumberOfSegments]; 

    //run before each test method
    [SetUp]
    public void Setup()
    {
        int ID = 0;
        for (int i = 0; i < NumberOfSegments; i++)
        {
            Waypoint[] waypoints = new Waypoint[2 + i];
            for(int j = 0; j < waypoints.Length; j++)
            {
                waypoints[j] = new Waypoint(new GPSCoords(i, -1 * i), ID);
                ++ID;
            }

            this.segments[i] = new PathSegment(waypoints);
            this.pois[i] = new PointOfInterest("anchorID", "buildingHighlight", new GPSCoords(0, 12), new Dialogue());
        }
    }

    // A Teardown method is run after every test method is run
    [TearDown]
    public void Teardown()
    {
        this.segments = new PathSegment[NumberOfSegments];
        this.pois = new PointOfInterest[NumberOfSegments];
    }

    [Test]
    public void SetEmptyPathSegmentAndPois()
    {
        Path path = new Path();

        Assert.DoesNotThrow(() => path.SetSegmentsAndPOIs(new PathSegment[0], new PointOfInterest[0]));
    }

    [Test]
    public void SetEmptyPathSegmentAndNonEmptyPois()
    {
        Path path = new Path();

        Assert.Throws<Path.InvalidPathException>(() => path.SetSegmentsAndPOIs(new PathSegment[0], this.pois));
    }

    [Test]
    public void SetNonEmptyPathSegmentAndEmptyPois()
    {
        Path path = new Path();

        Assert.Throws<Path.InvalidPathException>(() => path.SetSegmentsAndPOIs(this.segments, new PointOfInterest[0]));
    }

    [Test]
    public void SetPathSegmentAndPoisDifferentLengths()
    {
        PointOfInterest[] diffPois = new PointOfInterest[NumberOfSegments + 1];
        for (int i = 0; i < diffPois.Length; i++)
        {
            diffPois[i] = new PointOfInterest("anchorID", "buildingHighlight", new GPSCoords(0, 12), new Dialogue());
        }

        Path path = new Path();

        Assert.Throws<Path.InvalidPathException>(() => path.SetSegmentsAndPOIs(this.segments, diffPois));
    }

    [Test]
    public void SetPathSegmentAndPoisSameLength()
    {
        Path path = new Path();

        Assert.DoesNotThrow(() => path.SetSegmentsAndPOIs(this.segments, this.pois));
    }

    [Test]
    public void GetCurrentPoiEmpty()
    {
        Path path = new Path();

        Assert.AreEqual(null, path.GetCurrentPOI());
        Assert.AreEqual(false, path.HasNextSegment());
    }

    [Test]
    public void GetCurrentPoiNonEmpty()
    {
        Path path = new Path();
        path.SetSegmentsAndPOIs(this.segments, this.pois);

        Assert.AreEqual(this.pois[0], path.GetCurrentPOI());
        Assert.AreEqual(true, path.HasNextSegment());
    }

    [Test]
    public void GetCurrentPathSegmentEmpty()
    {
        Path path = new Path();

        Assert.AreEqual(null, path.GetCurrentSegment());
        Assert.AreEqual(false, path.HasNextSegment());  
    }

    [Test]
    public void GetCurrentPathSegmentNonEmpty()
    {
        Path path = new Path();
        path.SetSegmentsAndPOIs(this.segments, this.pois);

        Assert.AreEqual(this.segments[0], path.GetCurrentSegment());
        Assert.AreEqual(true, path.HasNextSegment());
    }

    [Test]
    public void InterateThroughPathSegments()
    {
        Path path = new Path();
        path.SetSegmentsAndPOIs(this.segments, this.pois);
        
        Assert.AreEqual(this.segments[0], path.GetCurrentSegment());
        Assert.AreEqual(this.pois[0], path.GetCurrentPOI());

        for(int i = 1; i < NumberOfSegments; i++)
        {
            Assert.AreEqual(true, path.HasNextSegment());
            Assert.AreEqual(this.segments[i], path.GetNextSegment());

            Assert.AreEqual(this.segments[i], path.GetCurrentSegment());
            Assert.AreEqual(this.pois[i], path.GetCurrentPOI());
        }

        //at end, cannot increment anymore
        Assert.AreEqual(false, path.HasNextSegment());
        Assert.AreEqual(this.segments[NumberOfSegments - 1], path.GetNextSegment());

        Assert.AreEqual(this.segments[NumberOfSegments - 1], path.GetCurrentSegment());
        Assert.AreEqual(this.pois[NumberOfSegments - 1], path.GetCurrentPOI());
    }

    [Test]
    public void IterateThroughEmptyPathSegments()
    {
        Path path = new Path();

        Assert.AreEqual(false, path.HasNextSegment());

        Assert.AreEqual(null, path.GetCurrentSegment());
        Assert.AreEqual(null, path.GetCurrentPOI());

        Assert.AreEqual(null, path.GetNextSegment());

        Assert.AreEqual(null, path.GetCurrentSegment());
        Assert.AreEqual(null, path.GetCurrentPOI());
    }

}
