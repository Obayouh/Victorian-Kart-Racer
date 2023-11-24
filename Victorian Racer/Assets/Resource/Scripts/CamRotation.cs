using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotation : MonoBehaviour
{

    private Transform camTransform;

    private Vector3 startCamRotation;
    private Vector3 endCamRotation;
    void Start()
    {
        camTransform = GetComponent<Transform>();

        //transform.position = startCamRotation;
    }

    void Update()
    {
        
    }

    IEnumerator CamPanDown()
    {
        endCamRotation = new Vector3(90f,startCamRotation.y,startCamRotation.z);
        Vector3.Slerp(startCamRotation, endCamRotation, 4);
        yield return null;
    }

    IEnumerator CamPanUp() 
    {
        yield return new WaitForSeconds(1f);

    }
}
