using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoDestruivel : MonoBehaviour {
    private Vector3 posicao;
    private PlayerMovement pm;
    private Health ph;
    public SfxManager sfx;
    public int som;
    public float delay = 4f;

    private void Start() {
        posicao = transform.position;
        pm = FindObjectOfType<PlayerMovement>();
        ph = FindObjectOfType<Health>();
       
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {

            if (pm.sliding || pm.dashing)
            {
                pm.Ataque();
                Invoke(nameof(Respawn), delay);              
                gameObject.SetActive(false);
                if (som == 1)
                    sfx.Play("MorteInimigo 1");
                else
                    sfx.Play("MorteInimigo 2");

            } else {
                ph.currentHealth--;
            }
        }
    }

    public void Respawn() {
        transform.position = posicao;
        gameObject.SetActive(true);
    }
}
