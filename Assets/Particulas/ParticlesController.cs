using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : MonoBehaviour {
    public ParticleSystem walkSmoke, burst, hitFeedback;
    private PlayerMovement pm;
    private Rigidbody rb;

    bool walkSmokeToggle;

    private void Start()
    {
        pm = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        if (pm.Grounded() && pm.sliding && rb.velocity.magnitude > 0.1f && !walkSmokeToggle) {
            walkSmoke.Play();
            walkSmokeToggle = true;
        } else if (rb.velocity.magnitude < 7f && walkSmokeToggle) {
            walkSmoke.Stop();
            walkSmokeToggle = false;
        } else if (!pm.Grounded() && walkSmokeToggle) {
            walkSmoke.Stop();
            walkSmokeToggle = false;
        }
    }
}
