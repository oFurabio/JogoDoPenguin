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
    public float maxSlideTime;
    public float slideForce;
    private float slideTimer;

    public float slideYScale;
    private float startYScale;

    [Header("Input")]
    public KeyCode slideKey = KeyCode.LeftControl;
    private float hInput, vInput;

    void Start() {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();

        startYScale = playerObj.localScale.y;

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

        playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

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

        playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);
    }

}
