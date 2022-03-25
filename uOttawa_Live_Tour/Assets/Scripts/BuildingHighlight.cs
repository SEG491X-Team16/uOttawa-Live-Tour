using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class BuildingHighlight : MonoBehaviour
{

    public Material materialSelect;
    public Material material;

    public GameObject textPrefab;
    private Camera camera;
    private GameObject text;
    private bool selected;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }
    

    // Update is called once per frame
    void Update()
    {
        //check for input
        if (Input.GetMouseButtonDown(0)){

            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            //cast ray from camera to input location and check if gameObject was selected
            if (Physics.Raycast(ray, out hit, 1500)){

                if(hit.collider.gameObject.transform.parent.name == name && !selected){
                    GetComponent<Renderer>().material = materialSelect;

                    Vector3 renderPosition = new Vector3 (hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y +.1f, hit.collider.gameObject.transform.position.z);
                    text = Object.Instantiate(textPrefab, renderPosition, Quaternion.identity, transform.parent);

                    text.GetComponent<TextMeshPro>().text = name;
                    
                    selected = true;
                
                } else if(selected){
                    GetComponent<Renderer>().material = material;
                    Destroy(text);
                    selected = false;
                }
            }
        }
        
    }
}
