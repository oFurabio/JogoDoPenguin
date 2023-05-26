using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReativaInimigos : MonoBehaviour
{
    public GameObject[] inimigos;

    // Update is called once per frame
    void Update()
    {
        if (Health.dead)
        {
            for(int i = 0; i < inimigos.Length; i++) {
                inimigos[i].SetActive(true);
            }
        }
    }
}
