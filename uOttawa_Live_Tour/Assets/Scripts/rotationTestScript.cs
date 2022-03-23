using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationTestScript : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 relativePos = transform.position - target.position;

        transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up)* Quaternion.AngleAxis(90, Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
