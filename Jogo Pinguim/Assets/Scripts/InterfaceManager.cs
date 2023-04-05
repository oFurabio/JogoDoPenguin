using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfaceManager : MonoBehaviour {
    public static bool jogoPausado;

    public GameObject pauseMenu, configuracoes;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        jogoPausado = false;
    }

    private void Update() {
        if (Input.GetButtonDown("Cancel")) {
            if (jogoPausado)
                Continuar();
            else
                Pause();
        }

    }

    public void ReiniciarCena() {
        Continuar();
        ThirdPersonCam.CursorHandler();
        SceneManager.LoadScene(1);
    }

    public void Pause() {
        jogoPausado = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        ThirdPersonCam.CursorHandler();
    }

    public void Continuar() {
        jogoPausado = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        configuracoes.SetActive(false);
        ThirdPersonCam.CursorHandler();
    }

    public void Configuracoes() {
        if (!configuracoes.activeInHierarchy) {
            pauseMenu.SetActive(false);
            configuracoes.SetActive(true);
        } else {
            configuracoes.SetActive(false);
            pauseMenu.SetActive(true);
        }
    }

    public void VoltarMenu() {
        Continuar();
        ThirdPersonCam.CursorHandler();
        SceneManager.LoadScene(0);
    }

    public void FecharJogo() {
        Application.Quit();
    }
}
