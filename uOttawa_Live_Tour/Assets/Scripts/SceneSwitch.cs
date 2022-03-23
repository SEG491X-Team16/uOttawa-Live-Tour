using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
}