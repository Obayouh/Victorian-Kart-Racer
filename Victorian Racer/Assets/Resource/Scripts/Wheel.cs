using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public bool IsPowered;
    public bool CanTurn;

    [SerializeField] private bool flipWheel;

    public GameObject Visual;

    private WheelCollider m_WheelCollider;

    private void Start()
    {
        m_WheelCollider = GetComponent<WheelCollider>();
    }

    private void Update()
    {
        Vector3 _position;
        Quaternion _rotation;

        m_WheelCollider.GetWorldPose(out _position, out _rotation);
        Visual.transform.position = _position;
        Visual.transform.rotation = _rotation * Quaternion.Euler(0, -180, 0);

        if (flipWheel == true)
        {
            Visual.transform.rotation = _rotation * Quaternion.Euler(0, -180, 0);
        }
        else
        {
            Visual.transform.rotation = _rotation;
        }
    }

    public void Accelerate(float _power)
    {
        if (IsPowered)
            m_WheelCollider.motorTorque = _power;
    }

    public void Turn(float _angle)
    {
        if (CanTurn)
            m_WheelCollider.steerAngle = _angle;
    }

    public void Brake(float _power)
    {
        m_WheelCollider.brakeTorque = _power;
    }
}
