using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallAbility : MonoBehaviour
{
    [SerializeField] private GameObject _FireBallPrefab;
    [SerializeField] private Transform _SpawnpointFireBall;

    private CarControls _carControls;
    private float _AbilityCooldownTime = 5;
    private float _FireBallTimer = 5;

    private void Start()
    {
        _carControls = GetComponent<CarControls>();
    }

    void Update()
    {
        if (_carControls.ShootFireBall && _FireBallTimer <= 0)
        {
            Instantiate(_FireBallPrefab, _SpawnpointFireBall.position, _SpawnpointFireBall.rotation);
            _carControls.ShootFireBall  = false;
            _FireBallTimer = _AbilityCooldownTime;
        }
        else if (_carControls.ShootFireBall && _FireBallTimer >= 0)
        {
            _carControls.ShootFireBall = false;
        }
        else
        {
            _FireBallTimer -= Time.deltaTime;
            Debug.Log(_FireBallTimer);
        }
    }
}
