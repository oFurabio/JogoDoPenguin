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
        config.SetActive(true);
        menu.SetActive(false);

        TrocaPrioridade(configPri);
    }

    public void Credits()
    {
        credits.SetActive(true);
        menu.SetActive(false);

        TrocaPrioridade(creditPri);
    }

    public void Voltar()
    {
        menu.SetActive(true);
        config.SetActive(false);
        credits.SetActive(false);

        TrocaPrioridade(jogar);
    }

    private void TrocaPrioridade(GameObject go)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(go);
    }

    public void Sair() {
        Debug.LogWarning("Fechando Jogo");
        Application.Quit();
    }
}
