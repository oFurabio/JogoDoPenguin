using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FecharTutorial : MonoBehaviour {
    public GameObject blur;

    public void Fecha() {
        blur.SetActive(false);
        gameObject.SetActive(false);
        GameState.GerenteEstado(0);
    }
}
