using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

public class Tutorial : MonoBehaviour
{
    public GameObject[] textScreens = new GameObject[5];
    public Text nextButtonText;
    private int screenIndex = 0;    
    public GameObject camera;
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
            screenIndex++;
            textScreens[screenIndex].SetActive(true);
        }
        if (screenIndex == textScreens.Length -1){
            if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.GetLocale("fr")){
                nextButtonText.text = "Finir";
            } else{
                nextButtonText.text = "Finish";
            }
        }
    }

    public void previous(){
        if (screenIndex == textScreens.Length -1){
            if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.GetLocale("fr")){
                nextButtonText.text = "Suivant";
            } else{
                nextButtonText.text = "Next";
            }
        }
        if (screenIndex > 0){
            textScreens[screenIndex].SetActive(false);
            screenIndex--;
            textScreens[screenIndex].SetActive(true);
        }
        
    }
}
