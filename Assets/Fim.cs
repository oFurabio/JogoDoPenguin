using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fim : MonoBehaviour {
    public UnityEvent quebrou;
    public GameObject cutscene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            quebrou.Invoke();
        }
    }

    public void FimDeJogo()
    {
        cutscene.SetActive(true);
    }
}
