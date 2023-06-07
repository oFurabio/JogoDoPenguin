using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrocadorRespawn : MonoBehaviour {
    public Respawn resp;
    public SfxManager sfx;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            sfx.Play("Point");
            resp.respawnPosition = gameObject.transform.position;
            gameObject.SetActive(false);
        }
    }
}
