using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
    
    public ParticleSystem walkSmoke;
    bool walkSmokeToggle;

    public ParticleSystem burst;

    public PlayerMovement pm;
    public Rigidbody rb;

    void Update()
    {
        if (pm.grounded && pm.sliding && rb.velocity.magnitude > 0.1f && !walkSmokeToggle)
        {
            walkSmoke.Play();
            walkSmokeToggle = true;
        }
        else if (rb.velocity.magnitude < 0.1f && walkSmokeToggle)
        {
            walkSmoke.Stop();
            walkSmokeToggle = false;
        }
        else if (!pm.grounded && walkSmokeToggle)
        {
            walkSmoke.Stop();
            walkSmokeToggle = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            burst.Play();
        }
    }
}
