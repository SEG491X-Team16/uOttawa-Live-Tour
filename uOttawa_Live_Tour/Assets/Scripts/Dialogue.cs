using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue 
{
    public string buildingName;
    [TextArea(1,10)]
    public string[] informations;
    //public Image[] images;
    public Image image;

}
