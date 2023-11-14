using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePos3D : MonoBehaviour
{
    [SerializeField] private Camera _currentCamera;
    public Vector3 MousePos;

    void Update()
    {
        Ray ray = _currentCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            transform.position = raycastHit.point;
            MousePos = transform.position;
        }
    }
}
