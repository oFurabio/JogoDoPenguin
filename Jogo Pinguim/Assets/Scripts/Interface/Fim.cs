using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fim : MonoBehaviour {
    public GameObject fimDeJogo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            Time.timeScale = 0f;
            fimDeJogo.SetActive(true);
            InterfaceManager.fimDeJogo = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
