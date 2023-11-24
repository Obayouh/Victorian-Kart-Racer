using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 _posCheckpoint;
    private Quaternion _rotCheckpoint;
    private float _deathZone = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (gameObject.transform.position.y <= _deathZone || Input.GetKeyDown(KeyCode.R))
        {
            rb.velocity = new Vector3(rb.velocity.x * 0, rb.velocity.y * 0, rb.velocity.z * 0);
            this.gameObject.transform.SetPositionAndRotation(_posCheckpoint, _rotCheckpoint);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            _posCheckpoint = new Vector3(other.transform.position.x, other.transform.position.y - 2, other.transform.position.z);
            _rotCheckpoint = new Quaternion(other.transform.rotation.x, other.transform.rotation.y + 270, other.transform.rotation.z - 90, other.transform.rotation.w);
        }
    }
}
