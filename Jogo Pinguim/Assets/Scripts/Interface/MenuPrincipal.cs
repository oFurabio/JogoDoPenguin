using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour {
    public GameObject menu, config;

    #region botoes
    public void IniciarJogo() {
        SceneManager.LoadScene("Jogo");
    }

    public void Configurar() {
        if (!config.activeInHierarchy) {
            menu.SetActive(false);
            config.SetActive(true);
        } else {
            config.SetActive(false);
            menu.SetActive(true);
        }
    }
    
    public void Sair() {
        Application.Quit();
    }
    #endregion

    #region configuracoes
    public void TelaCheia() {
        Debug.Log("Tela Cheia");
    }
    #endregion
}
