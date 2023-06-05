using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {
    [HideInInspector] public Vector3 respawnPosition;
    private bool respawnando;
    private Health vida;

    private void Start() {
        vida = GetComponent<Health>();
        respawnPosition = gameObject.transform.position;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F9))
            Respawnar();

        if (GameState.fimDeJogo && !respawnando) {
            respawnando = true;
            FimDeJogo();
        }
    }

    private void Respawnar() {
        Debug.Log("Respawning...");
        GameState.fimDeJogo = false;
        Health.dead = false;
        vida.currentHealth = 1;
        transform.position = respawnPosition;
        respawnando = false;
        GameState.GerenteEstado(0);
    }

    private void FimDeJogo() {
        Invoke(nameof(Respawnar), 2f);
    }
}
