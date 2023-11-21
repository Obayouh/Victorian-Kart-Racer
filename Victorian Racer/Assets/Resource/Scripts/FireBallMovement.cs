using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMovement : MonoBehaviour
{
    [SerializeField, Range(2f, 50f)] private float _moveSpeed = 2f;

    private float destroyTimer = 5f;

    void Update()
    {
        transform.Translate(_moveSpeed * Time.deltaTime * Vector3.forward);

        if (destroyTimer <= 0)
        {
            Invoke("DestroyFireball", destroyTimer);
        }
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
