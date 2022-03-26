using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayCast : MonoBehaviour
{
    [SerializeField] Transform Cube;
    // Start is called before the first frame update
    public Vector3 commandpoint;

    
    void Start()
    {
       
    }

    private void Update()
    {
        int layerMask = 1 << 8;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity,layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Cube.position = hit.point;
            commandpoint = hit.point;
        }
    }
    
}
