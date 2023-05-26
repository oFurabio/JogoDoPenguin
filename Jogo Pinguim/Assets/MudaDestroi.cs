using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudaDestroi : MonoBehaviour
{
    private int childCount;
    private int currentIndex;
    private GameObject[] childObjects;
    private float timer;
    private bool isActive;

    private void Start()
    {
        childCount = transform.childCount;
        childObjects = new GameObject[childCount];

        // Armazena todos os filhos do gameObject atual
        for (int i = 0; i < childCount; i++)
        {
            childObjects[i] = transform.GetChild(i).gameObject;
            childObjects[i].SetActive(false);
        }

        currentIndex = 0;
        isActive = true;
        ActivateChild();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // Aguarda 7 segundos antes de alternar para o próximo objeto
        if (timer >= 10f)
        {
            timer = 0f;
            currentIndex++;

            // Se todos os objetos foram ativados ao menos uma vez, destrói o gameObject atual
            if (currentIndex >= childCount)
            {
                Destroy(gameObject);
                return;
            }

            // Desativa o objeto anterior e ativa o próximo
            childObjects[currentIndex - 1].SetActive(false);
            ActivateChild();
        }
    }

    private void ActivateChild()
    {
        childObjects[currentIndex].SetActive(true);
    }

}
