using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {
    public static EstadoJogo estado;

    public enum EstadoJogo {
        Gameplay,
        Pausado,
        Fim
    }

    private void Awake() {
        estado = EstadoJogo.Gameplay;
    }

    public static void GerenteEstado(int num) {
        //  Jogo pausado
        if (num == 1) {
            estado = EstadoJogo.Pausado;
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        else if (num == 2) {
            estado = EstadoJogo.Fim;
            AudioManager.instance.musicSource.Stop();           
          //AudioManager.instance.sfxSource.Stop();
       
            Time.timeScale = 0.5f;
        }

        //  Jogo rodando
        else {
            estado = EstadoJogo.Gameplay;
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
