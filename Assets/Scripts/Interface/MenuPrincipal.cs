using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuPrincipal : MonoBehaviour {
    [Header("Funcionamento")]
    public GameObject menu;
    public GameObject config, credits;

    [Header("Selecao")]
    public GameObject configPri;
    public GameObject creditPri, jogar;

    public SfxManager sfx;

    public void IniciarJogo() {

        sfx.Play("BotaoPapel");
        GameState.GerenteEstado(0);
        SceneManager.LoadScene(1);
    }

    public void Config() {
        AtivaPainel(config);

        TrocaPrioridade(configPri);
        sfx.Play("BotaoPapel");
    }

    public void Credits() {
        sfx.Play("BotaoPapel");
        AtivaPainel(credits);

        TrocaPrioridade(creditPri);
    }

    public void Voltar() {
        AtivaPainel(menu);
        sfx.Play("BotaoPapel");
        TrocaPrioridade(jogar);
    }

    private void AtivaPainel(GameObject go) {
        sfx.Play("BotaoPapel");
        menu.SetActive(false);
        config.SetActive(false);
        credits.SetActive(false);

        go.SetActive(true);
    }

    private void TrocaPrioridade(GameObject go) {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(go);
    }

    public void Sair() {
        sfx.Play("BotaoPapel");
        Debug.LogWarning("Fechando Jogo");
        Application.Quit();
    }
}
