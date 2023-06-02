using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Referências")]
    public Transform orientation;
    public Transform playerObj;
    private Rigidbody rb;
    private PlayerMovement pm;

    [Header("Dash")]
    public float dashForce = 40f;
    public float dashUpwardForce = 0f;
    public float dashDuration = 0.25f;

    [Header("Config")]
    public bool useCameraForward = true;
    public bool allowAllDirections = true;
    public bool disableGravity = true;
    public bool resetVel = true;

    [Header("Cooldown")]
    public float dashCd = 0f;
    private float dashCdTimer;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (dashCdTimer > 0)
            dashCdTimer -= Time.deltaTime;
    }

    public void Dashar() {
        if (dashCdTimer > 0) return;
        else dashCdTimer = dashCd;

        pm.dashing = true;

        Transform forwardT;

        if (useCameraForward)
            forwardT = orientation; /// Onde você olha
        else
            forwardT = playerObj; /// Onde o boneco aponta

        Vector3 direction = GetDirection(forwardT);

        Vector3 forceToApply = direction * dashForce + orientation.up * dashUpwardForce;

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
        if (Input.GetButton("Slide"))
        {
            AudioManager.instance.PlaySFX("Dash");
            pm.sliding = true;
        }
        
        pm.dashing = false;

        if (disableGravity)
            rb.useGravity = true;
    }

    private Vector3 GetDirection(Transform forwardT)
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3();

        if (allowAllDirections)
            direction = forwardT.forward * vInput + forwardT.right * hInput;
        else
            direction = forwardT.forward;

        if (vInput == 0 && hInput == 0)
            direction = forwardT.forward;

        return direction.normalized;
    }

}
