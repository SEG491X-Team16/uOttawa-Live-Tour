using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * This class represents the Dialogues shown to the user at a POI.
 */
[System.Serializable]
public class Dialogue 
{
    public string buildingName;
    [TextArea(1,10)]
    public string[] informations;
    public AudioClip[] audioClips;
    //public Image[] images;
    public Image image;

}
