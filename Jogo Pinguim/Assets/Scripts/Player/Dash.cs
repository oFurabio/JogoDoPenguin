using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Referências")]
    public Transform orientation;
    private Rigidbody rb;
    private PlayerMovement pm;

    [Header("Dash")]
    public float dashForce = 40f;
    public float dashUpwardForce = 6f;
    public float dashDuration = 0.25f;

    [Header("Config")]
    public bool disableGravity;
    public bool resetVel;

    [Header("Cooldown")]
    public float dashCd = 1.5f;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
    }

    public void Dashar() {
        pm.dashing = true;

        Vector3 forceToApply = orientation.forward * dashForce + transform.up * dashUpwardForce;

        if (disableGravity)
            rb.useGravity = false;

        delayedForceToApply = forceToApply;
        Invoke(nameof(DelayedDashForce), 0.025f);

        Invoke(nameof(ResetDash), dashDuration);
    }

    private Vector3 delayedForceToApply;

    private void DelayedDashForce()
    {
        if (resetVel)
            rb.velocity = Vector3.zero;

        rb.AddForce(delayedForceToApply, ForceMode.Impulse);
    }

    private void ResetDash()
    {
        pm.dashing = false;

        if (disableGravity)
            rb.useGravity = true;
    }

}
