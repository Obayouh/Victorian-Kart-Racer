using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PitchforkTrap : MonoBehaviour
{
    [SerializeField] private List<Pitchfork> ListPitchforks = new List<Pitchfork>();
    [SerializeField] private AudioSource _clangSFX;

    Coroutine pitchforkTriggerRoutine = null;
    bool pitchforkReloaded = true;

    private void Start()
    {
        Pitchfork[] pitchforkArray = this.gameObject.GetComponentsInChildren<Pitchfork>();
        foreach (Pitchfork pf in pitchforkArray)
        {
            ListPitchforks.Add(pf);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (pitchforkReloaded == true && pitchforkTriggerRoutine == null)
            {
                pitchforkTriggerRoutine = StartCoroutine(_TriggerPitchforks());
                _clangSFX.Play();
            }
        }
    }

    IEnumerator _TriggerPitchforks()
    {
        //Debug.Log("spikes triggered!");

        foreach (Pitchfork pf in ListPitchforks)
        {
            pf.Shoot();
            pitchforkReloaded = false;
        }

        yield return new WaitForSeconds(1f);

        foreach (Pitchfork pf in ListPitchforks)
        {
            pf.Retract();
        }

        yield return new WaitForSeconds(1f);

        pitchforkTriggerRoutine = null;
        pitchforkReloaded = true;
    }
}
