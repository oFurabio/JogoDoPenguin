using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrocaTutoras : MonoBehaviour {
    public List<Sprite> sprites; // Lista de sprites para serem trocados
    private int spriteIndex = 0; // �ndice do sprite atual
    public SpriteRenderer sr; // Refer�ncia ao componente SpriteRenderer
    public GameObject tutorial;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica se o objeto colidido possui a tag "Troca"
        {
            gameObject.SetActive(false); // Desativa o objeto com o componente SpriteRenderer
            spriteIndex++; // Incrementa o �ndice do sprite
            if (spriteIndex < sprites.Count) // Se ainda houver sprites na lista
            {
                sr.sprite = sprites[spriteIndex]; // Troca o sprite atual pelo pr�ximo da lista
            }
            else // Se a lista de sprites acabar
            {
                tutorial.SetActive(false);
            }
        }
    }

}
