using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivaATroca : MonoBehaviour {
    public TrocarTutorial tt;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tt.Trocas(int.Parse(gameObject.name));
        }
    }
}
