using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrocadorRespawn : MonoBehaviour {
    public UnityEvent pickupCheckpoint;

    public Respawn resp;
    public SfxManager sfx;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            pickupCheckpoint.Invoke();

            sfx.Play("Point");
            resp.respawnPosition = gameObject.transform.position;
        }
    }
}
