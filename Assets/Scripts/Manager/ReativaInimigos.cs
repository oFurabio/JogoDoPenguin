using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReativaInimigos : MonoBehaviour {
    public GameObject[] inimigos;
    public float respawnDelay = 5f;
    private int i;

    private void Update() {
        if (Health.dead) {
            for(i = 0; i < inimigos.Length; i++) {
                inimigos[i].SetActive(true);
            }
        }
    }
}
