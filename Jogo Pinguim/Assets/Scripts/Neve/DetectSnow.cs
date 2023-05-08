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
            Debug.Log("dentro");
            snowInteractor.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Snow"))
        {
            Debug.Log("fora");
            snowInteractor.Pause();
        }
    }
}
