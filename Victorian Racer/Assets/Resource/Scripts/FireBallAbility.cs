using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallAbility : MonoBehaviour
{
    [SerializeField] private GameObject _FireBallPrefab;
    [SerializeField] private Transform _SpawnpointFireBall;

    private bool canShoot = false;

    [SerializeField, Range(1f, 10f)] private float _ShootCooldownTime = 5f;
    private float _FireBallTimer;



    void Update()
    {
        _FireBallTimer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ShootFireball();
        }
    }

    private void ShootFireball()
    {
        if (_FireBallTimer >= _ShootCooldownTime)
        {
            canShoot = true;
            if (_FireBallTimer >= _ShootCooldownTime && canShoot == true)
            {
                Instantiate(_FireBallPrefab, _SpawnpointFireBall.position, _SpawnpointFireBall.rotation);
                Rigidbody rb = _FireBallPrefab.GetComponent<Rigidbody>();
                rb.AddForce(_SpawnpointFireBall.forward * 5f, ForceMode.Impulse);
            }
        }
        _FireBallTimer = 0f;
        canShoot = false;
    }
}
