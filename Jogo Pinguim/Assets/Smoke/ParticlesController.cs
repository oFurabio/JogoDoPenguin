using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : MonoBehaviour {
    public ParticleSystem walkSmoke, burst;
    public PlayerMovement pm;
    public Rigidbody rb;

    bool walkSmokeToggle;

    void Update()
    {
        if (pm.Grounded() && pm.sliding && rb.velocity.magnitude > 0.1f && !walkSmokeToggle)
        {
            walkSmoke.Play();
            walkSmokeToggle = true;
        }
        else if (rb.velocity.magnitude < 0.1f && walkSmokeToggle)
        {
            walkSmoke.Stop();
            walkSmokeToggle = false;
        }
        else if (!pm.Grounded() && walkSmokeToggle)
        {
            walkSmoke.Stop();
            walkSmokeToggle = false;
        }

    }
}
