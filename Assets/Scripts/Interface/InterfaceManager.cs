using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class InterfaceManager : MonoBehaviour {
    public TrocarTutorial tt;
    
    [Header("Paineis")]
    public GameObject pauseMenu;
    public GameObject blur, configuracoesMenu, confirmacao, botoes, developer, controles;

    [Header("Primeiro Botão")]
    public GameObject pausePri;
    public GameObject configPri, confirmaPri, controlesPri;
    

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
            

        if (Input.GetButtonDown("Cancel") && !GameState.fimDeJogo) {
            if (!GameState.jogoPausado)
                Pause();
            else
                Resume();
        }

        

    }

    private void Pause() {
        blur.SetActive(true);
        pauseMenu.SetActive(true);
        TrocaSelecao(pausePri);

        GameState.GerenteEstado(1);
    }

    public void Resume() {
        sfx.Play("Botao");
        tt.DesativaTodos();
        AtivaPainel(botoes);

        blur.SetActive(false);

        GameState.GerenteEstado(0);
    }

    public void OpenConfig()
    {
        if (!configuracoesMenu.activeInHierarchy)
        {
            sfx.Play("Botao");
            AtivaPainel(configuracoesMenu);
            TrocaSelecao(configPri);
        }
        else
        {
            pauseMenu.SetActive(true);
            botoes.SetActive(true);
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

    public void Controles() {
        if (!controles.activeInHierarchy) {
            AtivaPainel(controles);
            TrocaSelecao(controlesPri);
        } else {
            AtivaPainel(configuracoesMenu);
            TrocaSelecao(configPri);
        }
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

    private void AtivaPainel(GameObject go) {
        pauseMenu.SetActive(false);
        configuracoesMenu.SetActive(false);
        confirmacao.SetActive(false);
        botoes.SetActive(false);
        developer.SetActive(false);
        controles.SetActive(false);

        go.SetActive(true);
    }

    private void TrocaSelecao(GameObject opcao)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(opcao);
    }
}
