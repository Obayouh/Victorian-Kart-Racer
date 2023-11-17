using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarControls : MonoBehaviour
{
    public Wheel[] wheels;


    [SerializeField] private float Power;
    [SerializeField] private float MaxAngle;
    [SerializeField] private float Turnspeed;

    private float m_Forward;
    private float m_Angle;
    private float m_Brake;

    private Rigidbody carRB;

    //Max overall speed to avoid becoming too fast for the track
    [SerializeField] private float _NormalMaxMagnitude;
    [SerializeField] private float _CurrentMaxMagnitude;

    //Enables Tire Marks to appear under the tires and what the minimum overall speed needs to be before the marks are able to appear
    //[SerializeField] private TrailRenderer[] trails;
    //[SerializeField] private float minSpeedForMarks = 10f;

    [SerializeField] private float downForce = 1f;

    void Start()
    {
        carRB = GetComponent<Rigidbody>();

        carRB.centerOfMass -= new Vector3(0f, 0.7f, 0f);

        carRB.AddForce(-transform.up * downForce);

        _CurrentMaxMagnitude = _NormalMaxMagnitude;
    }

    void Update()
    {
        m_Forward = Input.GetAxis("Vertical");
        if (m_Forward == 0)
        {
            m_Brake = Input.GetAxis("Vertical");
        }
        m_Angle = Input.GetAxis("Horizontal");

        //if (m_Angle != 0 && carRB.velocity.magnitude > minSpeedForMarks)
        //{
        //    foreach (var trail in trails)
        //    {
        //        trail.emitting = true;
        //    }
        //}
        //else
        //{
        //    foreach (var trail in trails)
        //    {
        //        trail.emitting = false;
        //    }
        //}

        if (Input.GetKey(KeyCode.LeftShift))
        {
            TurnCar();
        }
    }

    private void FixedUpdate()
    {
        foreach (Wheel _wheel in wheels)
        {
            _wheel.Accelerate(m_Forward * Power);
            _wheel.Brake(m_Brake * Power);
            _wheel.Turn(m_Angle * MaxAngle);
        }

        //Caps your max speed so you don't gain speed infinitely
        if (carRB.velocity.magnitude >= _CurrentMaxMagnitude)
        {
            carRB.velocity = Vector3.ClampMagnitude(carRB.velocity, _CurrentMaxMagnitude);
        }
    }

    public void TurnCar()
    {
        float turnDriftVelocity = Vector3.Dot(transform.forward, (carRB.position + carRB.velocity - carRB.position).normalized);
        float turnDrift = m_Angle * Turnspeed * Time.deltaTime * turnDriftVelocity;
        transform.Rotate(0, turnDrift, 0, Space.World);
    }
}
