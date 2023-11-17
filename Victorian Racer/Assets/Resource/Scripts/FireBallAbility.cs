using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallAbility : MonoBehaviour
{
    [SerializeField] private GameObject _FireBallPrefab;
    [SerializeField] private Transform _SpawnpointFireBall;

    private bool ShootFireBall = false;

    private CarControls _carControls;
    private float _AbilityCooldownTime = 5;
    private float _FireBallTimer = 5;

    private void Start()
    {
        _carControls = GetComponent<CarControls>();
    }

    void Update()
    {
        if (_FireBallTimer <= 0)
        {
            if (ShootFireBall == true)
            {
                Instantiate(_FireBallPrefab, _SpawnpointFireBall.position, _SpawnpointFireBall.rotation);
                ShootFireBall = false;
                _FireBallTimer = _AbilityCooldownTime;
            }
        }
        else if (ShootFireBall == true && _FireBallTimer >= 0)
        {
            ShootFireBall = false;
        }
        else
        {
            _FireBallTimer -= Time.deltaTime;
            //Debug.Log(_FireBallTimer);
        }
    }
}
