using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfaceManager : MonoBehaviour {
    public static bool jogoPausado = false;
    public static bool cursorVisivel = false;
    public static bool fimDeJogo = false;
    public GameObject pauseMenu, configuracoesMenu, confirmacao, confirmacaoMenu, botoes;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cursorVisivel = false;
        fimDeJogo = false;
    }

    void Update() {
        if (Input.GetButtonDown("Cancel") && !fimDeJogo) {
            if (!jogoPausado)
                Pause();
            else
                Resume();
        }
    }

    private void Pause() {
        jogoPausado = true;
        CursorHandler();
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void Resume() {
        jogoPausado = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        configuracoesMenu.SetActive(false);
        confirmacao.SetActive(false);
        botoes.SetActive(true);
    }

    public void CursorHandler() {
        if (cursorVisivel) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void OpenConfig() {
        if (!configuracoesMenu.activeInHierarchy) {
            pauseMenu.SetActive(false);
            configuracoesMenu.SetActive(true);
        } else {
            configuracoesMenu.SetActive(false);
            pauseMenu.SetActive(true);
        }
    }

    public void Reiniciar() {
        jogoPausado = false;
        fimDeJogo = false;
        CursorHandler();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MenuPrincipal() {
        if (confirmacaoMenu.activeInHierarchy) {
            confirmacaoMenu.SetActive(false);
            botoes.SetActive(true);
            Resume();
            CursorHandler();
            SceneManager.LoadScene("Menu");
        } else if (fimDeJogo) {
            fimDeJogo = false;
            Resume();
            CursorHandler();
            SceneManager.LoadScene("Menu");
        } else {
            botoes.SetActive(false);
            confirmacaoMenu.SetActive(true);
        }
    }

    public void SairDoJogo() {
        if (confirmacao.activeInHierarchy) {
            confirmacao.SetActive(false);
            botoes.SetActive(true);
            Debug.Log("Jogo fechado");
            Application.Quit();
        } else {
            botoes.SetActive(false);
            confirmacao.SetActive(true);
        }

    }

    public void Cancelar() {
        confirmacao.SetActive(false);
        confirmacaoMenu.SetActive(false);
        botoes.SetActive(true);
    }
}
