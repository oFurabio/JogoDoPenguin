using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FecharTutorial : MonoBehaviour {
    public void Fecha()
    {
        gameObject.SetActive(false);
        GameState.GerenteEstado(0);
    }
}
