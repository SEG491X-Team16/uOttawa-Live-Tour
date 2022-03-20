using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;


public class StartDirectionsTestSuite

{

    private GameObject scene;

    private GameObject startScenePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/StartDirectionsPrefab.prefab");

    [SetUp]
    public void Setup()
    {
        scene = Object.Instantiate(startScenePrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(scene.gameObject);

    }

    [UnityTest]
    public IEnumerator StartDirectionsTestSuiteCameraMovesToMidpoint()
    {
        Button button2 = getChildGameObjectByName(getChildGameObjectByName( getChildGameObjectByName(scene, "Canvas"), "Instructions"), "ContinueButton").GetComponent<Button>();
        button2.onClick.Invoke(); 

        GameObject map = getChildGameObjectByName(scene, "uOttawaMap");
        GameObject start = getChildGameObjectByName(map, "pinMarker");
        GameObject player = getChildGameObjectByName(map, "personMarker");

        GameObject camera = getChildGameObjectByName(scene, "Main Camera");


        yield return new WaitForSecondsRealtime(2);

        Assert.True(Mathf.Round(camera.transform.position.x) == Mathf.Round((player.transform.position.x + start.transform.position.x)/2));
        Assert.True(Mathf.Round(camera.transform.position.z) == Mathf.Round((player.transform.position.z + start.transform.position.z)/2)); 
     }

    [UnityTest]
    public IEnumerator StartDirectionsTestSuitePlayerOutOfBounds()
    {
        StartDirections startScript = getChildGameObjectByName(scene, "GPS").GetComponent<StartDirections>();
    
        double offset = 0.000001d;
        double latMax = startScript.latMax;
        double lonMax = startScript.lonMax;
        double latMin = startScript.latMin;
        double lonMin = startScript.lonMin;

        yield return null;
        
        Assert.False(startScript.IsOnCampus(latMax + offset, lonMax));
        Assert.False(startScript.IsOnCampus(latMax, lonMax + offset));
        Assert.False(startScript.IsOnCampus(latMin - offset, lonMin));
        Assert.False(startScript.IsOnCampus(latMin, lonMin - offset));
    }

    [UnityTest]
    public IEnumerator StartDirectionsTestSuitePlayerWithinBounds()
    {
        StartDirections startScript = getChildGameObjectByName(scene, "GPS").GetComponent<StartDirections>();
    
        double latMax = startScript.latMax;
        double lonMax = startScript.lonMax;
        double latMin = startScript.latMin;
        double lonMin = startScript.lonMin;

        yield return null;
        
        Assert.True(startScript.IsOnCampus(latMax , lonMax));
        Assert.True(startScript.IsOnCampus(latMin , lonMin));
    }

    [UnityTest]
    public IEnumerator StartDirectionsTestSuitePlayerWithinStartingRange()
    {
        GameObject map = getChildGameObjectByName(scene, "uOttawaMap");
        GameObject start = getChildGameObjectByName(map, "pinMarker");
        GameObject player = getChildGameObjectByName(map, "personMarker");
        StartDirections startScript = getChildGameObjectByName(scene, "GPS").GetComponent<StartDirections>();
      
        double startLat = startScript.startLat;
        double startLon = startScript.startLon;

        double offsetLat = (double)Random.Range(-0.00005f, 0.00005f);
        double offsetLon = (double)Random.Range(-0.00005f, 0.00005f);
    
        float distance = startScript.CalculateDistance(startLat + offsetLat, startLon + offsetLon, startLat, startLon);

        yield return null;

        Assert.True( Mathf.Round(distance) < 10);
        
    }

    [UnityTest]
    public IEnumerator StartDirectionsTestSuiteStartingPointSet()
    {
        StartDirections startScript = getChildGameObjectByName(scene, "GPS").GetComponent<StartDirections>();
        GameObject map = getChildGameObjectByName(scene, "uOttawaMap");
        GameObject start = getChildGameObjectByName(map, "pinMarker");
        MoveMarker moveMarkerStart = start.GetComponent<MoveMarker>();

        yield return null;
        Assert.IsTrue(startScript.startLon == moveMarkerStart.lon);
        Assert.IsTrue(startScript.startLat == moveMarkerStart.lat);

        

    }


    public GameObject getChildGameObjectByName(GameObject parent, string name) {
        for (int i = 0; i < parent.transform.childCount; i ++){
            if (parent.transform.GetChild(i).name == name){
                return parent.transform.GetChild(i).gameObject;
            }
        }
        Debug.Log("child not found");
        return null;
     }

}
