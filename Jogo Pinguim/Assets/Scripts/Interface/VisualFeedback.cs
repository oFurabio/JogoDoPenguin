using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VisualFeedback : MonoBehaviour {
    public GameObject player;
    public TextMeshProUGUI velocidade, estado;

    private void FixedUpdate() {
        velocidade.text = "Speed: " + player.GetComponent<Rigidbody>().velocity.magnitude.ToString("F1");

        estado.text = player.GetComponent<PlayerMovement>().state.ToString();
    }
}
