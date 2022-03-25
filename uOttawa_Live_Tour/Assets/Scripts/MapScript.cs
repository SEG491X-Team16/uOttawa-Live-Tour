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

    public Material material;

    private string[] buildings = {"15/17", "34/36", "38", "40", "90U", "109", "110", "143", "227", "538/540", "542", "555", "559", "600", "603", "631", "ARC", "ATK", "AWHC", "BRS", "BSC", "CBY", "CRG", "CRX", "CTE", "DMS", "DRO", "Empty", "FSS", "FTX", "GNN", "GSD", "HGN", "HNN", "HSY", "KED", "LABO", "LBC", "LMX", "LPR", "LR3", "LRR", "MCE", "MHN", "MNN", "MNO", "MNT", "MRD", "MRN", "MRT", "ODL", "Plane", "PRZ", "SCR", "SMD", "SMN", "STE", "STM", "STN", "STT", "SWT", "TBT", "THN", "UCU", "VNR", "WCA", "WLD"};

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
        //move map to position infront of cameraYRot so it is always a fixed position in front of the camera
        transform.position = cameraY.transform.position + cameraY.transform.forward * forwardOffset + cameraY.transform.up * upwardOffset;

        //update rotation to mimic camera movement change
        if(lastRotation != cameraY.transform.eulerAngles.y){
            float deltaYRotation = cameraY.transform.eulerAngles.y - lastRotation;

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + deltaYRotation,transform.eulerAngles.z);

            lastRotation = cameraY.transform.eulerAngles.y;
        }


        //restrict scaling
        if (transform.localScale.x > maxScale && transform.localScale.y > maxScale && transform.localScale.z >= maxScale){
            transform.localScale = new Vector3 (maxScale, maxScale, maxScale);
        }
        if (transform.localScale.x < minScale && transform.localScale.y < minScale && transform.localScale.z < minScale){
            transform.localScale = new Vector3 (minScale, minScale, minScale);
        }
         

    }

    public void printChildren() {
        string names = "";
        for (int i = 0; i < transform.childCount; i ++){
            names += ("\"" + transform.GetChild(i).name +"\", ");   
        }
        Debug.Log(names);
    }

    public void highlightBuilding(string name) {
        for (int i = 0; i < transform.childCount; i ++){
            if (transform.GetChild(i).name == name){
                transform.GetChild(i).GetComponent<Renderer>().material = material;
            }
        }
     }
}
