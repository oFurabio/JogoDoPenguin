using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrocaControles : MonoBehaviour {
    public GameObject teclado, joystick;
    [Header("")]
    public GameObject botaoTeclado, botaoGamepad;

    public void AbreJoystick() {
        joystick.SetActive(true);
        teclado.SetActive(false);

        TrocaSelecao(botaoTeclado);
    }

    public void AbreTeclado()
    {
        teclado.SetActive(true);
        joystick.SetActive(false);

        TrocaSelecao(botaoGamepad);
    }

    private void TrocaSelecao(GameObject opcao) {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(opcao);
    }
}
