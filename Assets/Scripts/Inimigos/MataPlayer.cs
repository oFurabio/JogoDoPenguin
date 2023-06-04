using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MataPlayer : MonoBehaviour {
    private Vector3 posicao;
    private PlayerMovement pm;
    private Health ph;

    private void Start()
    {
        posicao = transform.position;
        pm = FindObjectOfType<PlayerMovement>();
        ph = FindObjectOfType<Health>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (pm.sliding || pm.dashing) {
                pm.Ataque();
                Invoke(nameof(Respawn), 4);
                gameObject.SetActive(false);
            } else {
                ph.currentHealth--;
            }
        }
    }


    public void Respawn() {
        Debug.Log("Inimigando");
        transform.position = posicao;
        gameObject.SetActive(true);
    }
}
