using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEditor;

public class WaypointObjectTestSuite
{
    [Test]
    public void TestWaypointCreation()
    {
        Waypoint waypoint = new Waypoint(new GPSCoords(), 0);

        Assert.AreEqual(waypoint.Coordinates.Latitude, 0);
        Assert.AreEqual(waypoint.Coordinates.Longitude, 0);
        Assert.AreEqual(waypoint.ID, 0);
        Assert.That(!waypoint.IsVisible());
    }

    [Test]
    public void TestWaypointUpdate()
    {
        Waypoint waypoint = new Waypoint(new GPSCoords(), 0);

        Assert.AreEqual(waypoint.Coordinates.Latitude, 0);
        Assert.AreEqual(waypoint.Coordinates.Longitude, 0);
        Assert.AreEqual(waypoint.ID, 0);
        Assert.That(!waypoint.IsVisible());

        waypoint.Coordinates = new GPSCoords(2.3f, -1.0f);
        waypoint.ID = 2;
    
        Assert.AreEqual(waypoint.Coordinates.Latitude, 2.3f, 0.001);
        Assert.AreEqual(waypoint.Coordinates.Longitude, -1.0f, 0.001);
        Assert.AreEqual(waypoint.ID, 2);
    }

    [UnityTest]
    public IEnumerator SetWaypointGameObject()
    {
        Waypoint waypoint = new Waypoint(new GPSCoords(), 0);

        Assert.That(!waypoint.IsVisible());

        GameObject waypointArrow =  MonoBehaviour.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Models/arrow.fbx"));
        waypoint.SetInGameInstance(waypointArrow);
        yield return new WaitForSeconds(0.1f);

        Assert.That(waypoint.IsVisible());

        Object.Destroy(waypointArrow);
    }

    [UnityTest]
    public IEnumerator ClearWaypointGameObject()
    {
        Waypoint waypoint = new Waypoint(new GPSCoords(), 0);

        Assert.That(!waypoint.IsVisible());

        GameObject waypointArrow =  MonoBehaviour.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Models/arrow.fbx"));
        waypoint.SetInGameInstance(waypointArrow);
        yield return new WaitForSeconds(0.1f);

        Assert.That(waypoint.IsVisible());

        waypoint.ClearInGameInstance();
        yield return new WaitForSeconds(0.1f);

        Assert.That(!waypoint.IsVisible());

        Object.Destroy(waypointArrow);
    }
}
