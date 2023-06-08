using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class InterfaceManager : MonoBehaviour
{
    [Header("Paineis")]
    public GameObject pauseMenu;
    public GameObject blur, configuracoesMenu, confirmacao, botoes, developer;

    [Header("Primeiro Botão")]
    public GameObject pausePri;
    public GameObject configPri, confirmaPri; 
    

    public SfxManager sfx;

   
    void Update()
    {
        if (Input.GetButtonDown("Debug"))
        {
            if (!developer.activeInHierarchy) {
                developer.SetActive(true);
            } else {
                developer.SetActive(false);
            }
        }
            

        if (Input.GetButtonDown("Cancel") && !GameState.fimDeJogo)
        {
            if (!GameState.jogoPausado)
            {
                Pause();
                
            }
                
            else
            {
                Resume();
            }
                
        }

        

    }

    private void Pause() {
        sfx.Play("Botao");
        blur.SetActive(true);
        pauseMenu.SetActive(true);
        GameState.GerenteEstado(1);
        TrocaSelecao(pausePri);
       
    }

    public void Resume() {
        sfx.Play("Botao");
        blur.SetActive(false);
        botoes.SetActive(true);
        pauseMenu.SetActive(false);
        configuracoesMenu.SetActive(false);
        confirmacao.SetActive(false);
        GameState.GerenteEstado(0);
    }

    public void OpenConfig()
    {
        if (!configuracoesMenu.activeInHierarchy)
        {
            sfx.Play("Botao");
            configuracoesMenu.SetActive(true);
            pauseMenu.SetActive(false);
            TrocaSelecao(configPri);
        }
        else
        {
            pauseMenu.SetActive(true);
            configuracoesMenu.SetActive(false);
            TrocaSelecao(pausePri);
        }
    }

    public void Reiniciar()
    {
        GameState.GerenteEstado(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MenuPrincipal()
    {
        sfx.Play("Botao");
        GameState.GerenteEstado(0);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(0);
        
    }

    public void Cancelar()
    {
        sfx.Play("Botao");
        botoes.SetActive(true);
        confirmacao.SetActive(false);
        TrocaSelecao(pausePri);
    }

    public void SairDoJogo()
    {
        if (!confirmacao.activeInHierarchy)
        {
            sfx.Play("Botao");
            confirmacao.SetActive(true);
            botoes.SetActive(false);
            TrocaSelecao(confirmaPri);
        }
        else
        {
            botoes.SetActive(true);
            confirmacao.SetActive(false);
            Debug.Log("Jogo fechado");
            Application.Quit();
        }

    }

    private void TrocaSelecao(GameObject opcao)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(opcao);
    }
}
