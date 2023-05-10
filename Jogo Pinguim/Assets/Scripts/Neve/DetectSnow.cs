using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectSnow : MonoBehaviour
{
    public ParticleSystem snowInteractor;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Snow"))
        {
            snowInteractor.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Snow"))
        {
            snowInteractor.Pause();
        }
    }
}
