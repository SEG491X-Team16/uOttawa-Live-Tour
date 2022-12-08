using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class TourStartSelector : MonoBehaviour
{
    public SceneSwitch sceneSwitch;
    private const string FrancaisLocale = "Français (fr)";
    private const string EnglishLocale = "English (en)";


    // Start is called before the first frame update
    void Start()
    {
        LocalizationSettings.InitializationOperation.WaitForCompletion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnglishTourSelected()
    {
        for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; i++)
        {
            if (LocalizationSettings.AvailableLocales.Locales[i].LocaleName == EnglishLocale)
            {
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[i];
            }
        }
        
        this.sceneSwitch.loadTour();
    }

    public void FrenchTourSelected()
    {
        for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; i++)
        {
            if (LocalizationSettings.AvailableLocales.Locales[i].LocaleName == FrancaisLocale)
            {
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[i];
            }
        }
        
        this.sceneSwitch.loadTour();
    }
}
