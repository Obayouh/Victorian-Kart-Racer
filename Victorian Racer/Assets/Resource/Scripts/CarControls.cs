using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarControls : MonoBehaviour
{
    public Wheel[] wheels;
    public WheelCollider[] wheelColliders;

    private CamControl camControl;

    private WheelFrictionCurve _normalForwardFriction;
    private WheelFrictionCurve _normalSidewaysFriction;
    private WheelFrictionCurve _driftForwardFriction;
    private WheelFrictionCurve _driftSidewaysFriction;

    [SerializeField] private float Power;
    [SerializeField] private float MaxAngle;
    [SerializeField] private float Turnspeed;

    //Max overall speed to avoid becoming too fast for the track
    [SerializeField] private float _NormalMaxMagnitude;
    [SerializeField] private float _CurrentMaxMagnitude;

    [SerializeField, Range(3f, 15f)] private float speedboostDuration = 5f;
    [SerializeField, Range(3f, 15f)] private float speedboostAmount = 5f;
    [SerializeField] private float speedboostCooldown = 10f;

    [SerializeField] public AudioSource _carDriveSFX;
    [SerializeField] public AudioSource _driftSFX;
    [SerializeField] public AudioSource _BGM;

    [SerializeField] private GameObject _smokeScreen;

    //Enables Tire Marks to appear under the tires and what the minimum overall speed needs to be before the marks are able to appear
    //[SerializeField] private TrailRenderer[] trails;
    //[SerializeField] private float minSpeedForMarks = 10f;

    private float m_Forward;
    private float m_Angle;
    private float m_Brake;

    private float downForce = 1f;

    private Rigidbody carRB;
    private RaceCountdown _raceCountdown;
    private Laptime _laptime;

    Coroutine currentCoroutine = null;

    void Start()
    {
        _carDriveSFX.Play();
        _BGM.Play();

        carRB = GetComponent<Rigidbody>();
        _raceCountdown = FindObjectOfType<RaceCountdown>();
        _laptime = FindObjectOfType<Laptime>();

        carRB.centerOfMass -= new Vector3(0f, 0.7f, 0f);

        camControl = FindObjectOfType<CamControl>();

        carRB.AddForce(-transform.up * downForce);

        _CurrentMaxMagnitude = _NormalMaxMagnitude;
    }

    void Update()
    {
        m_Forward = Input.GetAxis("Vertical");
        //if (m_Forward == 0)
        //{
        //    m_Brake = Input.GetAxis("Vertical");
        //}
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
            _driftSFX.Play();
            ChangeToDriftWheels();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            BackToNormalWheels();
        }

        if (currentCoroutine == null)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                currentCoroutine = StartCoroutine(TempSpeedboost());
            }
        }

        if (_raceCountdown.speedboostImage.fillAmount != 1)
        {
            _raceCountdown.speedboostImage.fillAmount += 2 / speedboostCooldown * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        foreach (Wheel _wheel in wheels)
        {
            _wheel.Accelerate(m_Forward * Power);
            //_wheel.Brake(m_Brake * Power);
            _wheel.Turn(m_Angle * MaxAngle);
        }

        //Caps your max speed so you don't gain speed infinitely
        if (carRB.velocity.magnitude >= _CurrentMaxMagnitude)
        {
            carRB.velocity = Vector3.ClampMagnitude(carRB.velocity, _CurrentMaxMagnitude);
        }
    }

    private void ChangeToDriftWheels()
    {
        _driftForwardFriction.extremumSlip = 0.3f;
        _driftForwardFriction.extremumValue = 1f;
        _driftForwardFriction.asymptoteSlip = 0.25f;
        _driftForwardFriction.asymptoteValue = 0.05f;
        _driftForwardFriction.stiffness = 1f;

        _driftSidewaysFriction.extremumSlip = 0.7f;
        _driftSidewaysFriction.extremumValue = 1f;
        _driftSidewaysFriction.asymptoteSlip = 0.9f;
        _driftSidewaysFriction.asymptoteValue = 0.7f;
        _driftSidewaysFriction.stiffness = 0.95f;

        wheelColliders[0].forwardFriction = _driftForwardFriction;
        wheelColliders[1].forwardFriction = _driftForwardFriction;
        wheelColliders[0].sidewaysFriction = _driftSidewaysFriction;
        wheelColliders[1].sidewaysFriction = _driftSidewaysFriction;
    }

    private void BackToNormalWheels()
    {
        _normalForwardFriction.extremumSlip = 0.4f;
        _normalForwardFriction.extremumValue = 1f;
        _normalForwardFriction.asymptoteSlip = 0.8f;
        _normalForwardFriction.asymptoteValue = 0.5f;
        _normalForwardFriction.stiffness = 1f;

        _normalSidewaysFriction.extremumSlip = 0.2f;
        _normalSidewaysFriction.extremumValue = 1f;
        _normalSidewaysFriction.asymptoteSlip = 0.05f;
        _normalSidewaysFriction.asymptoteValue = 0.75f;
        _normalSidewaysFriction.stiffness = 1f;

        wheelColliders[0].forwardFriction = _normalForwardFriction;
        wheelColliders[1].forwardFriction = _normalForwardFriction;
        wheelColliders[0].sidewaysFriction = _normalSidewaysFriction;
        wheelColliders[1].sidewaysFriction = _normalSidewaysFriction;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fireball"))
        {
            StartCoroutine(TemporarySlowdownFireball(Random.Range(1.5f,3f), 10f));
        }

        if (other.gameObject.CompareTag("Pitchfork"))
        {
            StartCoroutine(PitchforkTempSlowdown(Random.Range(1.5f, 3f), 10f));
        }
    }

    IEnumerator TemporarySlowdownFireball(float waitTime, float speedDecrease)
    {
        _smokeScreen.SetActive(true);
        _CurrentMaxMagnitude = _NormalMaxMagnitude - speedDecrease;

        yield return new WaitForSeconds(waitTime);

        _CurrentMaxMagnitude = _NormalMaxMagnitude;
        _smokeScreen.SetActive(false);
    }

    IEnumerator PitchforkTempSlowdown(float waitTime, float speedDecrease)
    {
        _laptime.elapsedTime += 2;
        _CurrentMaxMagnitude = _NormalMaxMagnitude - speedDecrease;

        yield return new WaitForSeconds(waitTime);

        _CurrentMaxMagnitude = _NormalMaxMagnitude;
    }

    IEnumerator TempSpeedboost()
    {
        _CurrentMaxMagnitude += speedboostAmount;

        camControl._CurrentDistance += 3f;

        _raceCountdown.speedboostImage.fillAmount = 0;

        yield return new WaitForSeconds(speedboostDuration);

        _CurrentMaxMagnitude = _NormalMaxMagnitude;

        camControl._CurrentDistance -= 3f;

        currentCoroutine = null;

    }
}
