using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverrideCounter : MonoBehaviour
{
    private int maxTimeout = 1000;
    public int currentTime = 0;

    public GameObject overrideButton;


    private bool active = true;
    public bool Active{
        get => active;
        set => active = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(active && currentTime < maxTimeout){
            currentTime++;
        } else if (currentTime >= maxTimeout){
            overrideButton.SetActive(true);
        } 
        if (!active){
            currentTime = 0;
        }
        
    }
}


