using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrocadorRespawn : MonoBehaviour {
    public Respawn resp;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            resp.respawnPosition = gameObject.transform.position;
            gameObject.SetActive(false);
        }
    }
}
