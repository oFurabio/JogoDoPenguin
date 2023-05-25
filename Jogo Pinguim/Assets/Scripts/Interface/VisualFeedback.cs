using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VisualFeedback : MonoBehaviour {
    public GameObject player;
    public TextMeshProUGUI velocidade, estado, frames, vida, angulo;

    private void Update() {
        float currentFrameRate = 1.0f / Time.deltaTime;
        frames.text = "FPS: " + currentFrameRate.ToString("F0");
        angulo.text = "Ângulo: " + player.GetComponent<PlayerMovement>().angle.ToString("F1");
    }

    private void FixedUpdate() {
        velocidade.text = "Velocidade: " + player.GetComponent<Rigidbody>().velocity.magnitude.ToString("F1");

        vida.text = "Vida: " + player.GetComponent<Health>().currentHealth.ToString();

        estado.text = player.GetComponent<PlayerMovement>().state.ToString();
    }
}
