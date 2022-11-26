using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * This class is added to one empty in a scene that needs to be able to switch to another scene.
 */
public class SceneSwitch : MonoBehaviour
{
    public void loadSettings()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("Scenes/Settings");
    }
    public void loadTour()
    {
        SceneManager.LoadScene("Scenes/TourStartDirections");
    }
    public void loadMenu()
    {
        SceneManager.LoadScene("Scenes/Menu");
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene("Scenes/MenuTest");
    }

    public void loadEnding()
    {
        SceneManager.LoadScene("Scenes/FinishTour");
    }

    public void loadTutorial()
    {
        SceneManager.LoadScene("Scenes/Tutorial");
    }

    public void loadTourPath()
    {
        SceneManager.LoadScene("Scenes/TourPath");
    }
}