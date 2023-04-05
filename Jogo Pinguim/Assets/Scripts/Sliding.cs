using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour {
    [Header("Referencias")]
    public Transform orientation;
    public Transform playerObj;
    private Rigidbody rb;
    private PlayerMovement pm;

    [Header("Sliding")]
    private float maxSlideTime;
    public float slideForce;
    private float slideTimer;

    [Header("Input")]
    public KeyCode slideKey = KeyCode.LeftControl;
    private float hInput, vInput;

    void Start() {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
    }

    private void Update() {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(slideKey) && (hInput != 0 || vInput != 0)) {
            StartSlide();
        }

        if (Input.GetKeyUp(slideKey) && pm.sliding) {
            StopSlide();
        }

    }

    private void FixedUpdate() {
        if (pm.sliding) {
            SlidingMovement();
        }

    }

    private void StartSlide() {
        pm.sliding = true;

        slideTimer = maxSlideTime;
    }

    private void SlidingMovement() {
        Vector3 inputDirection = orientation.forward * vInput + orientation.right * hInput;

        //  Sliding normal
        if (!pm.OnSlope() || rb.velocity.y > -0.1f) {
            rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);

            slideTimer -= Time.deltaTime;
        }

        //  Sliding down a slope
        else {
            rb.AddForce(pm.GetSlopeMoveDirection(inputDirection) * slideForce, ForceMode.Force);
        }

        if (slideTimer <= 0) {
            StopSlide();
        }

    }

    private void StopSlide() {
        pm.sliding = false;
    }

}
