using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour {
    public GameObject menu, config, credits;

    public void IniciarJogo() {
        GameState.GerenteEstado(0);
        SceneManager.LoadScene(1);
    }

    public void Config() {
        config.SetActive(true);
        menu.SetActive(false);
    }

    public void Credits()
    {
        credits.SetActive(true);
        menu.SetActive(false);
    }

    public void Voltar()
    {
        menu.SetActive(true);
        config.SetActive(false);
        credits.SetActive(false);
    }

    public void Sair() {
        Debug.LogWarning("Fechando Jogo");
        Application.Quit();
    }
}
