using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerBeam : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public GameObject marker;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, new Vector3(marker.transform.position.x, marker.transform.position.y, marker.transform.position.z));
        //lineRenderer.SetPosition(1, new Vector3(marker.transform.position.x, marker.transform.position.y - 1f, marker.transform.position.z));
        RaycastHit hit;
        if (Physics.Raycast(marker.transform.position, -transform.up, out hit)){
            if (hit.collider){
                lineRenderer.SetPosition(1, hit.point);
            } 
        } else {
                lineRenderer.SetPosition(1, new Vector3(marker.transform.position.x, marker.transform.position.y - 1f, marker.transform.position.z));
        }

    }
}