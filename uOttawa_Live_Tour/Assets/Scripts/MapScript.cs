using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    //empty object with same position and y rotation as camera
    public GameObject cameraY;
    Vector3 rotation;

    public float yOffset;
    public float forwardOffset;
    public float upwardOffset;
    
    void Update()
    {
        //move map to position infront of cameraYRot so it is always a fixed position in front of the camera
        transform.position = cameraY.transform.position + cameraY.transform.forward * forwardOffset + cameraY.transform.up * upwardOffset;

        //rotate map about y axis with same camera rotation
        rotation = transform.eulerAngles;
        rotation.y = cameraY.transform.eulerAngles.y + yOffset;
        transform.eulerAngles = rotation; 

    }


}
