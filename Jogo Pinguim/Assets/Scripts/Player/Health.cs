using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    public int currentHealth;
    public int maxHealth = 3;
    public static bool dead = false;

    private void Start() {
        dead = false;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0)
            dead = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) {
            currentHealth--;
        }

    }
}
