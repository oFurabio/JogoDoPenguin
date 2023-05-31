using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MataPlayer : MonoBehaviour {
    private PlayerMovement pm;
    private Health ph;

    private void Start()
    {
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
        gameObject.SetActive(true);
    }
}
