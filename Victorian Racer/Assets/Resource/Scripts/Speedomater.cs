using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Speedomater : MonoBehaviour
{
    public Rigidbody target;
    public float maxSpeed = 0.0f; 
    [Header("UI")]
    public Text speedLabel; 

    private float speed = 0.0f;
    private void Update()
    {
        speed = target.velocity.magnitude * 3.6f;

        if (speedLabel != null)
            speedLabel.text = ((int)speed) + " km/h";
    }
}

