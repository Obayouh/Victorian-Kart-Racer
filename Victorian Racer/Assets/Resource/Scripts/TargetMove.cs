using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour
{
    [SerializeField] private GameObject[] _WayPoints;
    private int _currentWaypointIndex = 0;
    [SerializeField] private float _Speed = 5f;


    void Update()
    {
        if (Vector3.Distance(_WayPoints[_currentWaypointIndex].transform.position, transform.position) < 0.1f)
        {
            _currentWaypointIndex++;
        }

        transform.position = Vector3.MoveTowards(transform.position, _WayPoints[_currentWaypointIndex].transform.position, Time.deltaTime * _Speed);

    }
}
