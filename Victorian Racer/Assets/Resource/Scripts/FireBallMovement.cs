using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMovement : MonoBehaviour
{
    [SerializeField, Range(2f, 50f)] private float _moveSpeed = 2f;

    private float destroyTimer = 5f;

    private void Start()
    {
        Invoke("DestroyFireball", destroyTimer);
    }

    void Update()
    {

        transform.Translate(_moveSpeed * Time.deltaTime * Vector3.forward);

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
