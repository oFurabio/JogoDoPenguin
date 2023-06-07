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

    public void IniciarJogo() {
        GameState.GerenteEstado(0);
        SceneManager.LoadScene(1);
    }

    public void Config() {
        AtivaPainel(config);

        TrocaPrioridade(configPri);
    }

    public void Credits() {
        AtivaPainel(credits);

        TrocaPrioridade(creditPri);
    }

    public void Voltar() {
        AtivaPainel(menu);

        TrocaPrioridade(jogar);
    }

    private void AtivaPainel(GameObject go) {
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
        Debug.LogWarning("Fechando Jogo");
        Application.Quit();
    }
}
