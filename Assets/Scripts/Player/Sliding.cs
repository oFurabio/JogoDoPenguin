using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour {
    [Header("Referências")]
    public Transform orientation;
    public Transform playerObj;
    public CapsuleCollider cc;
    private Rigidbody rb;
    private PlayerMovement pm;
    private Dash dash;

    [Header("Deslizando")]
    [Range(50f, 200f)]
    public float slideForce = 200f;
    public bool canDash = true;

    [Header("Rotação")]
    public Vector3 inicial = new(0f, 1.075f, 0.05f);
    public Vector3 deslizando = new(0f, 0.5f, 0.5f);

    float hInput, vInput;

    private void Start()    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
        dash = GetComponent<Dash>();

    }

    private void Update() {
        if (Health.dead)
        {
            vInput = 0f;
            StopSlide();
        }

        if (pm.PodeMover())
        {
            vInput = Input.GetAxis("Vertical");
            hInput = Input.GetAxis("Horizontal");

            if (Input.GetButtonDown("Slide") && (vInput != 0f || hInput != 0f))
                StartSlide();

            if (Input.GetButtonUp("Slide") && pm.sliding)
                StopSlide();

            if (vInput == 0f && hInput == 0)
                StopSlide();

            if (pm.Grounded())
                canDash = true;

            if (pm.sliding)
                Rotacionar();
        }
    }

    private void FixedUpdate() {
        if (pm.sliding)
            SlidingMovement();
    }

    private void StartSlide() {
        cc.center = deslizando;
        cc.direction = 2;

        if (canDash) {
            canDash = false;
            dash.Dashar();
        } else
        {
            pm.sliding = true;
        }
    }

    private void SlidingMovement() {
        Vector3 inputDirection = orientation.forward * vInput + orientation.right * hInput;

        //  Deslizando normalmente
        if (!pm.OnSlope() || rb.velocity.y > -0.1f)
            rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);

        //  Deslizando numa descida
        else
            rb.AddForce(pm.GetSlopeMoveDirection(inputDirection) * slideForce, ForceMode.Force);
    }

    public void StopSlide() {
        pm.sliding = false;

        cc.center = inicial;
        cc.direction = 1;
    }
    
    private void Rotacionar() {
        if (!pm.Grounded()) {
            playerObj.rotation = Quaternion.Euler(0f, playerObj.eulerAngles.y, playerObj.eulerAngles.z);
        } else {
        if(rb.velocity.y > 0f)
            playerObj.rotation = Quaternion.Euler(-pm.angle, playerObj.eulerAngles.y, playerObj.eulerAngles.z);
        else
            playerObj.rotation = Quaternion.Euler(pm.angle, playerObj.eulerAngles.y, playerObj.eulerAngles.z);
        }
    }
}
