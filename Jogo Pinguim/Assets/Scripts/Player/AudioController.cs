using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;
    private bool isPlaying = false;

    void Start()
    {
        // Obtenha o componente AudioSource do objeto
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Verifique se a tecla "X" foi pressionada
        if (Input.GetKeyDown(KeyCode.J) && !isPlaying)
        {
            // Tocar o som e definir isPlaying como true
            audioSource.Play();
            isPlaying = true;

            // Desativar o som após 1 segundo
            Invoke(nameof(DisableSound), 1f);
        }
    }

    void DisableSound() {
        // Parar o som e definir isPlaying como false
        audioSource.Stop();
        isPlaying = false;
    }
}
