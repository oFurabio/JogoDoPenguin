using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    private Rigidbody rb;
    [HideInInspector] public int currentHealth;
    public int maxHealth = 1;
    public static bool dead = false;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        dead = false;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0)
            dead = true;
        if(dead)
        {
            AudioManager.instance.PlaySFX("MortePlayer");
        }
        if (dead) {
            InterfaceManager.fimDeJogo = true;
            GameState.GerenteEstado(2);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) {
            currentHealth--;
        }

    }
}
