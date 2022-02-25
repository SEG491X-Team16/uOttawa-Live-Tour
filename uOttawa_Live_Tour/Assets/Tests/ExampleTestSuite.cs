using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ExampleTestSuite
{
    //pass the app from the setup to the tests
    // private Game game;

    // A setup method is run before every test function is run
    [SetUp]
    public void Setup()
    {
        //do whatever setup we need here
        int i = 0;

        //normally this will be creating a game object for the app
        // GameObject gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
        // game = gameGameObject.GetComponent<Game>();
    }

    // A Teardown method is run after every test method is run
    [TearDown]
    public void Teardown()
    {
        //do whatever cleanuo we need here
        float a = 0.0f;

        //normally this is cleaning up the game object
        // Object.Destroy(game.gameObject);
    }

    // A Test behaves as an ordinary method
    [Test]
    public void ExampleTestSuiteSimplePasses()
    {
        // Use the Assert class to test conditions
        Assert.That((true || false), "simple test");
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ExampleTestSuiteWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        //test our code using asserts
        Assert.IsTrue(true);
    }
}
