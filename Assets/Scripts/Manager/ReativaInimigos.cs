using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReativaInimigos : MonoBehaviour {
    public GameObject[] inimigos;
    public float respawnDelay;
    private int i;
    //bool putiz = false;

    void Update() {
        if (Health.dead) {
            for(i = 0; i < inimigos.Length; i++) {
                inimigos[i].SetActive(true);
            }
        }
    }
}
