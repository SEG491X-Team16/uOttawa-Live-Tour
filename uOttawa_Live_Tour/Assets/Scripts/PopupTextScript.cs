using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTextScript : MonoBehaviour
{
    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(camera.transform);
        transform.rotation = Quaternion.LookRotation(camera.transform.forward);
    }
}
