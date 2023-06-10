using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StopBola : MonoBehaviour {
    public UnityEvent bola;
    public float limit;
    
    private Rigidbody rb;
    private bool parou = false;

    private void Start() {
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
    }

    public void ParaBola() {
        rb.velocity = new Vector3(0f,0f,0f);
    }
}
