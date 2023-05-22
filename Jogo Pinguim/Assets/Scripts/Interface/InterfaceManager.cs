using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class InterfaceManager : MonoBehaviour
{
    public static bool jogoPausado = false;
    public static bool cursorVisivel = false;
    public static bool fimDeJogo = false;
    private bool respawnando = false;
    private Health health;
    public Transform player;
    public GameObject pauseMenu, configuracoesMenu, confirmacao, confirmacaoMenu, botoes, developer;
    public GameObject opPrimeiro;

    private void Start()
    {
        GameObject player = GameObject.Find("Player"); // Assuming the Player GameObject has the "Player" tag assigned
        health = player.GetComponent<Health>();
        Cursor.lockState = CursorLockMode.Locked;
        respawnando = false;
        Cursor.visible = false;
        cursorVisivel = false;
        fimDeJogo = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Debug"))
        {
            if (developer.activeInHierarchy)
            {
                developer.SetActive(false);
            } else {
                developer.SetActive(true);
            }
        }

        if (fimDeJogo && !respawnando)
        {
            respawnando = true;
            FimDeJogo();
        }
            

        if (Input.GetButtonDown("Cancel") && !fimDeJogo)
        {
            if (!jogoPausado)
                Pause();
            else
                Resume();
        }
    }

    private void Pause()
    {
        jogoPausado = true;
        CursorHandler();
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(opPrimeiro);
    }

    public void Resume()
    {
        jogoPausado = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        configuracoesMenu.SetActive(false);
        confirmacao.SetActive(false);
        botoes.SetActive(true);
    }

    public void CursorHandler()
    {
        if (cursorVisivel)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void OpenConfig()
    {
        if (!configuracoesMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(false);
            configuracoesMenu.SetActive(true);
        }
        else
        {
            configuracoesMenu.SetActive(false);
            pauseMenu.SetActive(true);
        }
    }

    public void Reiniciar()
    {
        jogoPausado = false;
        fimDeJogo = false;
        CursorHandler();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MenuPrincipal()
    {
        if (confirmacaoMenu.activeInHierarchy)
        {
            confirmacaoMenu.SetActive(false);
            botoes.SetActive(true);
            Resume();
            CursorHandler();
            SceneManager.LoadScene("Menu");
        } else {
            botoes.SetActive(false);
            confirmacaoMenu.SetActive(true);
        }
    }

    public void SairDoJogo()
    {
        if (confirmacao.activeInHierarchy)
        {
            confirmacao.SetActive(false);
            botoes.SetActive(true);
            Debug.Log("Jogo fechado");
            Application.Quit();
        }
        else
        {
            botoes.SetActive(false);
            confirmacao.SetActive(true);
        }

    }

    public void Cancelar()
    {
        confirmacao.SetActive(false);
        confirmacaoMenu.SetActive(false);
        botoes.SetActive(true);
    }

    public bool PodeMover()
    {
        if (jogoPausado || fimDeJogo)
            return false;

        return true;
    }

    public void Respawn()
    {
        Debug.Log("Respawning...");
        player.transform.position = new(0f, 1f, -15f);
        fimDeJogo = false;
        Health.dead = false;
        health.currentHealth = 1;
        respawnando = false;

    }

    private void FimDeJogo()
    {
        Invoke(nameof(Respawn), 3f);
    }
}
