using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StopBola : MonoBehaviour {
    public UnityEvent bola;
    public float limit;

    private Vector3 pose;
    private Rigidbody rb;
    private bool parou = false;

    private void Start() {
        pose = transform.position;
        rb = GetComponent<Rigidbody>();
        bola.Invoke();
    }

    private void Update()
    {
        if (transform.localPosition.z < limit && !parou)
        {
            parou = true;
            bola.Invoke();
        }

        if (Health.dead)
        {
            transform.position = pose;
            Debug.Log($"voltando ${bola} pro lugar");
        }
    }

    public void ParaBola() {
        rb.velocity = new Vector3(0f,0f,0f);
    }
}
