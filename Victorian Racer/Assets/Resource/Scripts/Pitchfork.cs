using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitchfork : MonoBehaviour
{
    [SerializeField] private float travelSpeed;

    private void Start()
    {
        Invoke("DestroyBullet", 4f);
    }

    void Update()
    {
        transform.Translate(0, travelSpeed * Time.deltaTime, 0);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
