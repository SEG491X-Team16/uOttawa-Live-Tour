using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTour : MonoBehaviour
{
    public void exitTheTour()
    {
        SceneManager.LoadScene("Scenes/Menu");
    }
}
