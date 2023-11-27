using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CamRotation : MonoBehaviour
{
    private Cinemachine.CinemachineVirtualCamera vCam;

    [SerializeField] private Transform camLookAt;

    //private float duration;
    void Start()
    {
        vCam = GetComponent<Cinemachine.CinemachineVirtualCamera>();
        vCam.LookAt = camLookAt;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "CamTrigger")
        {
            vCam.LookAt = null;
        }
    }
}
