using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using NUnit.Framework;

public class GPSCoordsObjectTestSuite
{
    [Test]
    public void BothPointsZero()
    {
        GPSCoords p1 = new GPSCoords(0, 0);
        GPSCoords p2 = new GPSCoords(0, 0);

        Assert.AreEqual(0, p1.GetDistance(p2), 0.1);
    }

    [Test]
    public void OtherPointZero()
    {
        GPSCoords p1 = new GPSCoords(3f, -4f);
        GPSCoords p2 = new GPSCoords(0, 0);

        Assert.AreEqual(949668.645, p1.GetDistance(p2), 0.001);
    }

    [Test]
    public void FirstPointZero()
    {
        GPSCoords p1 = new GPSCoords(0, 0);
        GPSCoords p2 = new GPSCoords(-3f, -4f);

        Assert.AreEqual(949668.645, p1.GetDistance(p2), 0.001);
    }

    [Test]
    public void BothPointsSame()
    {
        GPSCoords p1 = new GPSCoords(20.3f, -45.675f);
        GPSCoords p2 = new GPSCoords(20.3f, -45.675f);

        Assert.AreEqual(0, p1.GetDistance(p2), 0.001);
    }

    [Test]
    public void PointsAreDifferent()
    {
        GPSCoords p1 = new GPSCoords(20.3f, -45.675f);
        GPSCoords p2 = new GPSCoords(-49.2321f, 23.483f);

        Assert.AreEqual(15129843.514, p1.GetDistance(p2), 0.001);
    }

    [Test]
    public void PointsVeryVeryClose()
    {
        GPSCoords p1 = new GPSCoords(-49.3492f, 45.67457f);
        GPSCoords p2 = new GPSCoords(-49.3493f, 45.67457f);

        Assert.AreEqual(11.028, p1.GetDistance(p2), 0.001);
    }

    [Test]
    public void PointsInDifferentHemispheres()
    {
        GPSCoords p1 = new GPSCoords(20.2423f, 34.493f);
        GPSCoords p2 = new GPSCoords(-23.231f, 34.342f);

        Assert.AreEqual(4834121.410, p1.GetDistance(p2), 0.001);
    }

    [Test]
    public void PointsInSameHemispheres()
    {
        GPSCoords p1 = new GPSCoords(20.2423f, 34.493f);
        GPSCoords p2 = new GPSCoords(23.231f, 34.342f);

        Assert.AreEqual(333788.624, p1.GetDistance(p2), 0.001);
    }

    [Test]
    public void PointsOnDifferentSideOfMeridian()
    {
        GPSCoords p1 = new GPSCoords(20.2423f, 34.493f);
        GPSCoords p2 = new GPSCoords(23.231f, -34.342f);

        Assert.AreEqual(13350084.336, p1.GetDistance(p2), 0.001);
    }

    [Test]
    public void PointsOnSameSideOfMeridian()
    {
        GPSCoords p1 = new GPSCoords(20.2423f, 34.493f);
        GPSCoords p2 = new GPSCoords(23.231f, 35.453f);

        Assert.AreEqual(386984.254, p1.GetDistance(p2), 0.001);
    }

}
