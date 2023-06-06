using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class som : MonoBehaviour
{
    public AudioClip soundEffect;

    private AudioSource audioSource;

    private void Start()
    {
        // Obtenha a referência do componente AudioSource
        audioSource = GetComponent<AudioSource>();
        // Atribua o efeito sonoro ao componente AudioSource
        audioSource.clip = soundEffect;
    }

    private void Update()
    {
        // Verifica se o botão esquerdo do mouse foi pressionado
        if (Input.GetMouseButtonDown(0))
        {
            // Verifique se o cursor do mouse está sobre o botão
            if (IsMouseOverButton())
            {
                // Reproduz o efeito sonoro
                audioSource.Play();
            }
        }
    }

    private bool IsMouseOverButton()
    {
        // Obtenha a posição do cursor do mouse em relação à janela do jogo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Obtenha o Collider2D do botão
        Collider2D buttonCollider = GetComponent<Collider2D>();

        // Verifique se a posição do cursor está sobre o botão
        return buttonCollider.bounds.Contains(mousePosition);
    }
}
