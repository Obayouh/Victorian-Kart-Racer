using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private float _firetimer = 5;

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + _moveSpeed);

        _firetimer -= Time.deltaTime;
        if (_firetimer <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //spin out enemy
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Pitchfork") || collision.gameObject.CompareTag("Tomato"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }

        if (!collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
