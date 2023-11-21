using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    [SerializeField] private Transform _player;

    [HideInInspector] public float _CurrentDistance;
    [SerializeField, Range(1f, 20f)] private float _NormalDistance = 5f; //Distance between _Player and camera
    [SerializeField, Min(0f)] private float _FocusRadius = 1f; //When the cam and _Player are over this amount the camera adjusts
    [SerializeField, Range(0f, 1f)] private float _FocusCentering = 0.5f; //Camera moves back to perfect center with some smoothing
    [SerializeField, Range(1f, 360f)] private float _RotationSpeed = 90f; //How fast the cam turns to catch up to the _Player's pos
    [SerializeField, Range(-89f, 89f)] private float _MinVerticalAngle = -30f, _MaxVerticalAngle = 60f; //Clamp for vert movement when adjusting cam angle
    [SerializeField, Min(0f)] private float _AlignDelay = 5f; //How long it takes before the cam adjusts itself when left alone
    [SerializeField, Range(0f, 90f)] private float _AlignSmoothRange = 45f; //Smoothing between angle positions

    private float _lastManualRotationTime;

    private Vector3 _focusPoint, _previousFocusPoint;
    private Vector3 _prevMousePos;
    private Vector2 _orbitAngles = new(30f, 0f);

    private void Awake()
    {
        _focusPoint = _player.position;
        transform.localRotation = Quaternion.Euler(_orbitAngles);
    }
    private void Start()
    {
        _CurrentDistance = _NormalDistance;
    }
    private void Update()
    {
        Vector3 pos = _player.transform.position; //Fills the pos Vector with the _Player's position at all times
        transform.position = pos;
    }

    private void LateUpdate()
    {
        UpdateFocusPoint();
        Quaternion lookRotation;
        if (ManualRotation() || AutomaticRotation())
        {
            ConstrainAngles();
            lookRotation = Quaternion.Euler(_orbitAngles);
        }
        else
        {
            lookRotation = transform.localRotation;
        }
        Vector3 lookDirection = lookRotation * Vector3.forward;
        Vector3 lookPosition = _focusPoint - lookDirection * _CurrentDistance;
        transform.SetPositionAndRotation(lookPosition, lookRotation);
    }

    //Prevents the maxmimum vertical angle of the camera to be below the minimum vertical angle, otherwise you break the camera
    private void OnValidate()
    {
        if (_MaxVerticalAngle < _MinVerticalAngle)
        {
            _MaxVerticalAngle = _MinVerticalAngle;
        }
    }

    /// <summary>
    /// Limits the angles used for looking up, down, and around the _Player to prevent weird camera angles
    /// </summary>
    private void ConstrainAngles()
    {
        _orbitAngles.x = Mathf.Clamp(_orbitAngles.x, _MinVerticalAngle, _MaxVerticalAngle);

        if (_orbitAngles.y < 0f)
        {
            _orbitAngles.y += 360f;
        }
        else if (_orbitAngles.y >= 360f)
        {
            _orbitAngles.y -= 360f;
        }
    }

    /// <summary>
    /// The camera constantly focuses on the Player, and this function keeps track of the Player, maintains a certain _distance from it,
    /// and allows the _ball a little leeway in moving before the camera moves with the Player
    /// </summary>
    private void UpdateFocusPoint()
    {
        _previousFocusPoint = _focusPoint;
        Vector3 targetPoint = _player.position;

        if (_FocusRadius > 0f)
        {
            float distance = Vector3.Distance(targetPoint, _focusPoint);
            float t = 1f;
            if (distance > 0.01f && _FocusCentering > 0f)
            {
                t = Mathf.Pow(1f - _FocusCentering, Time.unscaledDeltaTime);
            }
            if (distance > _FocusRadius)
            {
                t = Mathf.Min(t, _FocusRadius / distance);
            }
            _focusPoint = Vector3.Lerp(targetPoint, _focusPoint, t);
        }
        else
        {
            _focusPoint = targetPoint;
        }
    }
    /// <summary>
    /// Allows You to freely move the camera around the _Player while holding the right mouse button
    /// </summary>
    /// <returns></returns>
    private bool ManualRotation()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 mouseDelta = Input.mousePosition - _prevMousePos;

            Vector3 moveDelta = mouseDelta * (360f / Screen.height);

            _orbitAngles += new Vector2(moveDelta.y, moveDelta.x);

            _prevMousePos = Input.mousePosition;
            if (mouseDelta != new Vector3(0, 0, 0))
            {
                _lastManualRotationTime = Time.unscaledDeltaTime;
                return true;
            }
        }
        _prevMousePos = Input.mousePosition;
        return false;
    }

    /// <summary>
    /// The camera automatically tracks the Player when turning, and this also allows the camera to keep up with the _ball itself
    /// </summary>
    /// <returns></returns>
    private bool AutomaticRotation()
    {
        if (Time.unscaledTime - _lastManualRotationTime < _AlignDelay)
        {
            return false;
        }

        Vector2 movement = new Vector2(_focusPoint.x - _previousFocusPoint.x,
            _focusPoint.z - _previousFocusPoint.z);

        float movementDeltaSqr = movement.sqrMagnitude;
        if (movementDeltaSqr < 0.0001f)
        {
            return false;
        }

        float headingAngle = GetAngle(movement / Mathf.Sqrt(movementDeltaSqr));
        float deltaAbs = Mathf.Abs(Mathf.DeltaAngle(_orbitAngles.y, headingAngle));
        float rotationChange = _RotationSpeed * Mathf.Min(Time.unscaledDeltaTime, movementDeltaSqr);

        if (deltaAbs < _AlignSmoothRange)
        {
            rotationChange *= deltaAbs / _AlignSmoothRange;
        }
        else if (180f - deltaAbs < _AlignSmoothRange)
        {
            rotationChange *= (180f - deltaAbs) / _AlignSmoothRange;
        }
        _orbitAngles.y = Mathf.MoveTowardsAngle(_orbitAngles.y, headingAngle, rotationChange);
        return true;
    }

    static float GetAngle(Vector2 pDirection)
    {
        float angle = Mathf.Acos(pDirection.y) * Mathf.Rad2Deg;
        return pDirection.x < 0f ? 360f - angle : angle;
    }
}
