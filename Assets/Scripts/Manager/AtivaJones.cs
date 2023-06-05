using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivaJones : MonoBehaviour
{
    public bool resetPosition;
    public Vector3 posiIni;
    public GameObject jones;

    private void Start() {
        posiIni = jones.transform.position;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            jones.SetActive(true);
        }
    }

    private void Update()
    {
        if (Health.dead)
        {
            jones.SetActive(false);
            jones.transform.position = posiIni;
        }
    }
}
