using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectSnow : MonoBehaviour {
    private PlayerMovement pm;
    public ParticleSystem snowInteractor;
    public GameObject aus;

    private void Awake()
    {
        pm = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Snow")) {
            snowInteractor.Play();
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Snow")) {
            if (GameState.estado == GameState.EstadoJogo.Gameplay)
            {
                if (pm.state == PlayerMovement.MovementState.andando && (pm.hInput != 0 || pm.vInput != 0))
                    aus.SetActive(true);
                else
                    aus.SetActive(false);
            }
            else
                aus.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Snow"))
        {
            snowInteractor.Pause();
            aus.SetActive(false);
        }
    }
}
