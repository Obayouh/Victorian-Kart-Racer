using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitchfork : MonoBehaviour
{
    [SerializeField, Range(0.25f, 4f)] private float waitDelay = 0.5f;

    public void Shoot()
    {
        if (this.transform.localPosition.y < 0f)
        {
            StartCoroutine(_Shoot());
        }
    }

    IEnumerator _Shoot()
    {
        yield return new WaitForSeconds(waitDelay);

        this.transform.localPosition += (Vector3.up * 1f);
    }

    public void Retract()
    {
        if (this.transform.localPosition.y > 0f)
        {
            StartCoroutine(_Retract());
        }
    }

    IEnumerator _Retract()
    {
        yield return new WaitForSeconds(waitDelay);

        this.transform.localPosition -= (Vector3.up * 1f);
    }
}
