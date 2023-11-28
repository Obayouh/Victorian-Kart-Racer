using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _leftFence;
    [SerializeField] private GameObject _rightFence;
    private bool _leftPathTrigger = false;
    private bool _rightPathTrigger = false;

    private void Update()
    {
        if (_leftPathTrigger == true)
        {
            _leftFence.SetActive(true);
            _rightFence.SetActive(false);
        }

        if (_rightPathTrigger == true)
        {
            _leftFence.SetActive(false);
            _rightFence.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PathTriggerLeft"))
        {
            _leftPathTrigger = true;
            _rightPathTrigger = false;
        }

        if (other.gameObject.CompareTag("PathTriggerRight"))
        {
            _leftPathTrigger = false;
            _rightPathTrigger = true;
        }
    }
}
