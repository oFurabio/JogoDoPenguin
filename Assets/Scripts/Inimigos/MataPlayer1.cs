using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MataPlayer1 : MonoBehaviour {
    private PlayerMovement pm;
    private Health ph;

    private void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        ph = FindObjectOfType<Health>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            ph.currentHealth--;
        }
    }

}
