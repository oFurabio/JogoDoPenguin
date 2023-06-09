using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    [HideInInspector] public int currentHealth;
    public int maxHealth = 1;
    public static bool dead = false;

    private SfxManager sfx;

    private void Start() {
        dead = false;
        currentHealth = maxHealth;
        sfx = GetComponentInChildren<SfxManager>();
       
    }

    private void Update() {
        if (currentHealth <= 0 && !dead) {
            sfx.Play("Morte");
            dead = true;
        }

        if (dead) {
            GameState.fimDeJogo = true;
            GameState.GerenteEstado(2);
        }
    }
}
