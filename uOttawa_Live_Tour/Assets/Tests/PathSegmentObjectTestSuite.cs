using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using NUnit.Framework;

public class PathSegmentObjectTestSuite
{
    private const int WaypointsLength = 10;

    private Waypoint[] waypoints = new Waypoint[WaypointsLength];

    //run before each test method
    [SetUp]
    public void Setup()
    {
        for (int i = 0; i < WaypointsLength; i++)
        {
            this.waypoints[i] = new Waypoint(new GPSCoords((float)i, -1f * (float)i), i);
        }
    }

    // A Teardown method is run after every test method is run
    [TearDown]
    public void Teardown()
    {
        this.waypoints = new Waypoint[10];
    }

    [Test]
    public void CreatePathSegment()
    {
        PathSegment seg = new PathSegment(this.waypoints);

        for (int i = 0; i < WaypointsLength; i++)
        {
            Assert.AreEqual(seg.Waypoints[i].Coordinates, new GPSCoords((float)i, -1f * (float)i));
            Assert.AreEqual(seg.Waypoints[i].ID, i);
            Assert.That(!seg.Waypoints[i].IsVisible());
        }
    }

    [Test]
    public void GetLastWaypoint()
    {
        PathSegment seg = new PathSegment(this.waypoints);

        Assert.AreEqual(seg.GetLastWaypoint(), seg.Waypoints[WaypointsLength - 1]);
    }

    [Test]
    public void GetLastWaypointEmptyWaypoints()
    {
        Waypoint[] waypoints = { };
        PathSegment seg = new PathSegment(waypoints);

        Assert.AreEqual(seg.GetLastWaypoint(), null);
    }

    [Test]
    public void InitialVisibleStartandEnd()
    {
        PathSegment seg = new PathSegment(this.waypoints);

        Assert.AreEqual(seg.GetVisibleStart(), null);
        Assert.AreEqual(seg.GetVisibleEnd(), null);
        Assert.AreEqual(seg.GetNextVisibleStart(), seg.Waypoints[0]);
    }

    [Test]
    public void IncrementVisibleStart()
    {
        PathSegment seg = new PathSegment(this.waypoints);

        Assert.AreEqual(seg.GetVisibleStart(), null);
        Assert.AreEqual(seg.GetVisibleEnd(), null);
        Assert.AreEqual(seg.GetNextVisibleStart(), seg.Waypoints[0]);

        Assert.That(seg.IncrementVisibleStart());

        Assert.AreEqual(seg.GetVisibleStart(), seg.Waypoints[0]);
        Assert.AreEqual(seg.GetVisibleEnd(), seg.Waypoints[0]);
        Assert.AreEqual(seg.GetNextVisibleStart(), seg.Waypoints[1]);
    }

    [Test]
    public void IncrementVisibleEnd()
    {
        PathSegment seg = new PathSegment(this.waypoints);

        Assert.AreEqual(seg.GetVisibleStart(), null);
        Assert.AreEqual(seg.GetVisibleEnd(), null);
        Assert.AreEqual(seg.GetNextVisibleStart(), seg.Waypoints[0]);

        Assert.That(seg.IncrementVisibleStart());
        Assert.That(seg.IncrementVisibleStart());
        Assert.That(seg.IncrementVisibleStart());
        Assert.That(seg.IncrementVisibleEnd());

        Assert.AreEqual(seg.GetVisibleStart(), seg.Waypoints[2]);
        Assert.AreEqual(seg.GetVisibleEnd(), seg.Waypoints[1]);
        Assert.AreEqual(seg.GetNextVisibleStart(), seg.Waypoints[3]);
    }

    [Test]
    public void CannotIncrementVisibleEndAheadOfStart()
    {
        PathSegment seg = new PathSegment(this.waypoints);

        Assert.AreEqual(seg.GetVisibleStart(), null);
        Assert.AreEqual(seg.GetVisibleEnd(), null);
        Assert.AreEqual(seg.GetNextVisibleStart(), seg.Waypoints[0]);

        Assert.That(!seg.IncrementVisibleEnd());

        Assert.AreEqual(seg.GetVisibleStart(), null);
        Assert.AreEqual(seg.GetVisibleEnd(), null);
        Assert.AreEqual(seg.GetNextVisibleStart(), seg.Waypoints[0]);
    }

    [Test]
    public void CannotIncrementVisibleStartPastEndOfWaypoints()
    {
        PathSegment seg = new PathSegment(this.waypoints);

        Assert.AreEqual(seg.GetVisibleStart(), null);
        Assert.AreEqual(seg.GetVisibleEnd(), null);
        Assert.AreEqual(seg.GetNextVisibleStart(), seg.Waypoints[0]);

        for(int i = 0; i < WaypointsLength; i++)
        {
            Assert.That(seg.IncrementVisibleStart());
            Assert.AreEqual(seg.GetVisibleStart(), seg.Waypoints[i]);
        }

        Assert.AreEqual(seg.GetVisibleStart(), seg.Waypoints[WaypointsLength - 1]);
        Assert.AreEqual(seg.GetVisibleEnd(), seg.Waypoints[0]);
        Assert.AreEqual(seg.GetNextVisibleStart(), null);

        Assert.That(!seg.IncrementVisibleStart());
    }

    [Test]
    public void CannotIncrementVisibleEndPastEndOfWaypoints()
    {
        PathSegment seg = new PathSegment(this.waypoints);

        Assert.AreEqual(seg.GetVisibleStart(), null);
        Assert.AreEqual(seg.GetVisibleEnd(), null);
        Assert.AreEqual(seg.GetNextVisibleStart(), seg.Waypoints[0]);

        Assert.That(seg.IncrementVisibleStart());
        for(int i = 0; i < WaypointsLength - 1; i++)
        {
            Assert.That(seg.IncrementVisibleStart());
            Assert.That(seg.IncrementVisibleEnd());
        }

        Assert.AreEqual(seg.GetVisibleStart(), seg.Waypoints[WaypointsLength - 1]);
        Assert.AreEqual(seg.GetVisibleEnd(), seg.Waypoints[WaypointsLength - 1]);
        Assert.AreEqual(seg.GetNextVisibleStart(), null);

        Assert.That(!seg.IncrementVisibleEnd());
    }

    [UnityTest]
    public IEnumerator ClearAllWaypoints()
    {
        PathSegment seg = new PathSegment(this.waypoints);

        for (int i = 0; i < (4 % WaypointsLength); i++)
        {
            GameObject waypointArrow =  MonoBehaviour.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Models/arrow.fbx"));
            seg.Waypoints[i].SetInGameInstance(waypointArrow);
            Assert.That(seg.IncrementVisibleStart());
            Assert.That(seg.Waypoints[i].IsVisible());
        }
        yield return new WaitForSeconds(0.1f);

        seg.ClearVisibleWaypoints();

        for (int i = 0; i < WaypointsLength; i++)
        {
            Assert.That(!seg.Waypoints[i].IsVisible());
        }
    }

    [UnityTest]
    public IEnumerator ClearAllWaypointsEmptyWaypoints()
    {
        Waypoint[] waypoints = { };
        PathSegment seg = new PathSegment(waypoints);

        Assert.DoesNotThrow(() => seg.ClearVisibleWaypoints());

        yield return null;
    }
}
