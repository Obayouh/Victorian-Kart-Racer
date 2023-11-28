using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineSwap : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera1; //Intro camera
    [SerializeField] private CinemachineVirtualCamera _virtualCamera2; //Gameplay camera

    private bool introCam = true;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.I) && introCam == true)
        {
            StartCoroutine(PreventSpam());
        }
    }
    IEnumerator PreventSpam()
    {

        if (introCam)
        {
            _virtualCamera1.Priority = 0;
            _virtualCamera2.Priority = 1;
        }
        else
        {
            _virtualCamera1.Priority = 1;
            _virtualCamera2.Priority = 0;
        }
        introCam = !introCam;

        yield return new WaitForSeconds(3f);
    }
}
