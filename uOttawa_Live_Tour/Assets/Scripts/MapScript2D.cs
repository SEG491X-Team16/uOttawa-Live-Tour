using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapScript2D : MonoBehaviour
{
    public GameObject camera;

    public float yOffset;
    public float forwardOffset;

    void Start (){
        //move map to position infront of cameraYRot so it is always a fixed position in front of the camera
        transform.position = camera.transform.position + camera.transform.forward * forwardOffset + camera.transform.up;
        Vector3 rotation = transform.eulerAngles;
        rotation.y += yOffset;
        transform.eulerAngles = rotation; 

    }
    
    void Update()
    {
        
    }
}
