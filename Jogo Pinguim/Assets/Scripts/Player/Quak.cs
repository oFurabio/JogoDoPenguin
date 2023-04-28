using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quak : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.X) && !isPlaying)
        {
            // Tocar o som e definir isPlaying como true
            audioSource.Play();
            isPlaying = true;

            // Desativar o som após 3 segundos
            Invoke("DisableSound", 3f);
        }
    }

    void DisableSound()
    {
        // Parar o som e definir isPlaying como false
        audioSource.Stop();
        isPlaying = false;
    }
}
