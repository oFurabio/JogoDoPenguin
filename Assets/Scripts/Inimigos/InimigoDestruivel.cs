using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoDestruivel : MonoBehaviour {
    private Vector3 posicao;
    private PlayerMovement pm;
    private Health ph;
    public SfxManager sfx;
    public float delay = 4f;

    private void Start() {
        posicao = transform.position;
        pm = FindObjectOfType<PlayerMovement>();
        ph = FindObjectOfType<Health>();
       
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {

            if (pm.sliding || pm.dashing)
            {
                pm.Ataque();
                Invoke(nameof(Respawn), delay);              
                gameObject.SetActive(false);
                sfx.Play("MorteInimigo");

            } else {
                ph.currentHealth--;
            }
        }
    }

    public void Respawn() {
        transform.position = posicao;
        gameObject.SetActive(true);
    }
}
