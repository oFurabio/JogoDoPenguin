using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class InterfaceManager : MonoBehaviour
{
    public static bool jogoPausado = false;
    public static bool fimDeJogo = false;

    public GameObject pauseMenu, configuracoesMenu, confirmacao, confirmacaoMenu, botoes, developer;
    public GameObject continuar, voltar, cancela1, cancela2;

    private void Start() {
        jogoPausado = false;
        fimDeJogo = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Debug"))
        {
            if (developer.activeInHierarchy) {
                developer.SetActive(false);
            } else {
                developer.SetActive(true);
            }
        }
            

        if (Input.GetButtonDown("Cancel") && !fimDeJogo)
        {
            if (!jogoPausado)
                Pause();
            else
                Resume();
        }
    }

    private void Pause() {
        jogoPausado = true;
        pauseMenu.SetActive(true);
        GameState.GerenteEstado(1);
        AbriuMenu(continuar);
    }

    public void Resume() {
        jogoPausado = false;
        pauseMenu.SetActive(false);
        configuracoesMenu.SetActive(false);
        confirmacao.SetActive(false);
        botoes.SetActive(true);
        AbriuMenu(continuar);
        GameState.GerenteEstado(0);
    }

    public void OpenConfig()
    {
        if (!configuracoesMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(false);
            configuracoesMenu.SetActive(true);
            AbriuMenu(voltar);
        }
        else
        {
            configuracoesMenu.SetActive(false);
            pauseMenu.SetActive(true);
            AbriuMenu(continuar);
        }
    }

    public void Reiniciar()
    {
        jogoPausado = false;
        GameState.GerenteEstado(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MenuPrincipal()
    {
        if (confirmacaoMenu.activeInHierarchy)
        {
            confirmacaoMenu.SetActive(false);
            botoes.SetActive(true);
            GameState.GerenteEstado(0);
            SceneManager.LoadScene("Menu");
        } else {
            botoes.SetActive(false);
            confirmacaoMenu.SetActive(true);
            AbriuMenu(cancela1);
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
            AbriuMenu(cancela2);
        }

    }

    public void Cancelar()
    {
        confirmacao.SetActive(false);
        confirmacaoMenu.SetActive(false);
        botoes.SetActive(true);
        AbriuMenu(continuar);
    }

    private void AbriuMenu(GameObject opcao)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(opcao);
    }
}
