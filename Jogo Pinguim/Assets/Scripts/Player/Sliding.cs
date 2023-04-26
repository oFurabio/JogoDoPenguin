using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour {
    [Header("Referências")]
    public Transform orientation;
    public Transform playerObj;
    private Rigidbody rb;
    private PlayerMovement pm;

    [Header("Deslizando")]
    public float slideForce = 200f;
    private float jumpForce = 10f;
    private float forwardForce = 25f;
    private bool canDash = true;

    [Header("Teclas")]
    public KeyCode slideKey = KeyCode.LeftShift;
    float hInput, vInput;

    private void Start()    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
    }

    private void Update() {

        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(slideKey) && (hInput != 0 || vInput != 0)) {
            if (canDash) {
                canDash = false;
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                rb.AddForce(transform.up * jumpForce + orientation.forward * forwardForce, ForceMode.Impulse);
                StartSlide();
            } else {
                StartSlide();
            }
        }

        if (Input.GetKeyUp(slideKey) && pm.sliding) {
            StopSlide();
            if (pm.grounded)
                canDash = true;
        }
    }

    private void FixedUpdate() {
        if (pm.sliding)
            SlidingMovement();
    }

    private void StartSlide() {
        pm.sliding = true;
        
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

    private void StopSlide() {
        pm.sliding = false;
    }

}
