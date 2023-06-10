using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivaJones : MonoBehaviour {
    public GameObject jones;

    private void Update() {
        if (Health.dead)
            jones.SetActive(true);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            jones.SetActive(false);
        }
    }
}
