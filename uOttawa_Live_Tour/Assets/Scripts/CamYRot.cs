using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamYRot : MonoBehaviour
{
    //Used to track the camera/AR camera's position and only Y rotation
    //It is then used as a reference in front of which the 3d map is projected
    //This way it tracks position and stays in front of the player

    public GameObject camera;
    Vector3 rotation;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = camera.transform.position;

        rotation = transform.eulerAngles;
        rotation.y = camera.transform.eulerAngles.y;

        transform.eulerAngles = rotation; 
        
        
    }
}
