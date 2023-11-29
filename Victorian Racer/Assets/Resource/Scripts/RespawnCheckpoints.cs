using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnCheckpoints : MonoBehaviour
{
    [SerializeField] public AudioSource _bellSFX;
    [SerializeField] public AudioSource _graveyardBGMLeft;
    [SerializeField] public AudioSource _graveyardBGMRight;

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
            rb.velocity = new Vector3(rb.velocity.x * 0.1f, rb.velocity.y * 0.1f, rb.velocity.z * 0.1f);
            this.gameObject.transform.SetPositionAndRotation(_posCheckpoint, _rotCheckpoint);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            _posCheckpoint = new Vector3(other.transform.position.x, other.transform.position.y - 1.25f, other.transform.position.z);
            _rotCheckpoint = other.transform.rotation;
        }

        if (other.gameObject.CompareTag("ChurchBell"))
        {
            _bellSFX.time = 25.1f;
            _bellSFX.Play();
        }

        if (other.gameObject.CompareTag("GraveyardBGMLeft"))
        {
            _graveyardBGMLeft.Play();
        }

        if (other.gameObject.CompareTag("GraveyardBGMRight"))
        {
            _graveyardBGMRight.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("GraveyardBGMLeft"))
        {
            _graveyardBGMLeft.Stop();
        }

        if (other.gameObject.CompareTag("GraveyardBGMRight"))
        {
            _graveyardBGMRight.Stop();
        }
    }
}
