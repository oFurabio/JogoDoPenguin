using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class som : MonoBehaviour
{
    public AudioClip soundEffect;

    private AudioSource audioSource;

    private void Start()
    {
        // Obtenha a refer�ncia do componente AudioSource
        audioSource = GetComponent<AudioSource>();
        // Atribua o efeito sonoro ao componente AudioSource
        audioSource.clip = soundEffect;
    }

    private void Update()
    {
        // Verifica se o bot�o esquerdo do mouse foi pressionado
        if (Input.GetMouseButtonDown(0))
        {
            // Verifique se o cursor do mouse est� sobre o bot�o
            if (IsMouseOverButton())
            {
                // Reproduz o efeito sonoro
                audioSource.Play();
            }
        }
    }

    private bool IsMouseOverButton()
    {
        // Obtenha a posi��o do cursor do mouse em rela��o � janela do jogo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Obtenha o Collider2D do bot�o
        Collider2D buttonCollider = GetComponent<Collider2D>();

        // Verifique se a posi��o do cursor est� sobre o bot�o
        return buttonCollider.bounds.Contains(mousePosition);
    }
}
