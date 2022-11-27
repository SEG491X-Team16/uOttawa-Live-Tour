using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject[] textScreens = new GameObject[5];
    public Text nextButtonText;
    private int screenIndex = 0;    
    public GameObject camera;
    public Image[] dots;

    // Start is called before the first frame update
    void Start()
    {
        textScreens[screenIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void next(){
        if (screenIndex == textScreens.Length -1){
            camera.GetComponent<SceneSwitch>().loadTourPath();
        }
        if (screenIndex < textScreens.Length -1){
            textScreens[screenIndex].SetActive(false);
             dots[screenIndex].color = new Color(dots[screenIndex].color.r, dots[screenIndex].color.g, dots[screenIndex].color.b, 0.588f);
            screenIndex++;
            textScreens[screenIndex].SetActive(true);
            dots[screenIndex].color = new Color(dots[screenIndex].color.r, dots[screenIndex].color.g, dots[screenIndex].color.b, 1f);
        }
        if (screenIndex == textScreens.Length -1){
            nextButtonText.text = "Finish";
        }
    }

    public void previous(){
        if (screenIndex == textScreens.Length -1){
            nextButtonText.text = "Next";
        }
        if (screenIndex > 0){
            textScreens[screenIndex].SetActive(false);
            dots[screenIndex].color = new Color(dots[screenIndex].color.r, dots[screenIndex].color.g, dots[screenIndex].color.b, 0.588f);
            screenIndex--;
            textScreens[screenIndex].SetActive(true);
            dots[screenIndex].color = new Color(dots[screenIndex].color.r, dots[screenIndex].color.g, dots[screenIndex].color.b, 1f);
        }
        
    }
}
