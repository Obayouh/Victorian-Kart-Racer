using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private float _firetimer = 5;

    void Update()
    {
        transform.Translate(_moveSpeed * Time.deltaTime * Vector3.forward);

        _firetimer -= Time.deltaTime;
        if (_firetimer <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {        
            Destroy(this.gameObject);
        }
    }
}
