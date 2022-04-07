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

    private float lastRotation;
    private float maxScale = 0.3f;
    private float minScale = 0.07f;

    void Start()
    {
        //move map to position infront of cameraYRot so it is always a fixed position in front of the camera
        transform.position = cameraY.transform.position + cameraY.transform.forward * forwardOffset + cameraY.transform.up * upwardOffset;

        //rotate map about y axis with same camera rotation
        rotation = transform.eulerAngles;
        rotation.y = cameraY.transform.eulerAngles.y + yOffset;
        transform.eulerAngles = rotation; 

        lastRotation = cameraY.transform.eulerAngles.y;

    }
    
    void Update()
    {
        UpdatePosition();
        UpdateRotation();
        RestrictScaling();
    }

    void UpdatePosition(){
        //move map to position infront of cameraYRot so it is always a fixed position in front of the camera
        transform.position = cameraY.transform.position + cameraY.transform.forward * forwardOffset + cameraY.transform.up * upwardOffset;
    }

    void UpdateRotation(){
        //update rotation to mimic camera movement change
        if(lastRotation != cameraY.transform.eulerAngles.y){
            float deltaYRotation = cameraY.transform.eulerAngles.y - lastRotation;

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + deltaYRotation,transform.eulerAngles.z);

            lastRotation = cameraY.transform.eulerAngles.y;
        }
    }

    void RestrictScaling(){
        //restrict scaling
        if (transform.localScale.x > maxScale && transform.localScale.y > maxScale && transform.localScale.z >= maxScale){
            transform.localScale = new Vector3 (maxScale, maxScale, maxScale);
        }
        if (transform.localScale.x < minScale && transform.localScale.y < minScale && transform.localScale.z < minScale){
            transform.localScale = new Vector3 (minScale, minScale, minScale);
        }
    }

    
}
