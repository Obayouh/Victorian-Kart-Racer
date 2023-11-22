using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMovement : MonoBehaviour
{
    [SerializeField, Range(2f, 50f)] private float _moveSpeed = 2f;

    private float destroyTimer = 5f;

    Rigidbody rb;

    private void Start()
    {
        Invoke("DestroyFireball", destroyTimer);

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.AddForce(0, 0, _moveSpeed, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {        
            DestroyFireball();
        }
    }

    private void DestroyFireball()
    {
        Destroy(this.gameObject);
    }
}
