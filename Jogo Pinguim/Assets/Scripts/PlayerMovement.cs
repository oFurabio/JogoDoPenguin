using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
    [Header("Movimento")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float slideSpeed;

    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;

    public float speedIncreaseMultiplier;
    public float slopeIncreaseMultiplier;

    public float groundDrag;
    public Text texto_velocidade;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Checagem de chão")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Aclives e declives")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    public Transform orientation;

    float hInput, vInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;
    public enum MovementState {
        walking,
        sprinting,
        sliding,
        air
    }

    public bool sliding;

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;

    }

    private void Update() {
        // Checa o que é chão
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();

        //  Lida com a resistência
        if (grounded) {
            rb.drag = groundDrag;
        } else {
            rb.drag = 0;
        }

    }

    private void FixedUpdate() {
        MovePlayer();

    }

    private void MyInput() {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        //  When to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded) {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        //  Start sprint
        if (Input.GetKeyDown(sprintKey)) {
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

    }

    private void StateHandler() {
        //  Mode - Sliding
        if (sliding) {
            state = MovementState.sliding;

            if (OnSlope() && rb.velocity.y < 0.1f) {
                desiredMoveSpeed = slideSpeed;
            } else {
                desiredMoveSpeed = sprintSpeed;
            }
        }

        //  Mode - Sprinting
        else if (grounded && Input.GetKey(sprintKey)) {
            state = MovementState.sprinting;
            desiredMoveSpeed = sprintSpeed;
        }

        //  Mode - Walking
        else if (grounded) {
            state = MovementState.walking;
            desiredMoveSpeed = walkSpeed;
        }

        //  Mode - Air
        else {
            state = MovementState.air;
        }

        if (Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 4f && moveSpeed != 0) {
            StopAllCoroutines();
            StartCoroutine(SmoothlyLerpMoveSpeed());
        } else {
            moveSpeed = desiredMoveSpeed;
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;

    }

    private IEnumerator SmoothlyLerpMoveSpeed() {
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        while (time < difference) {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);

            if (OnSlope()) {
                float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
                float slopeAngleIncriase = 1 + (slopeAngle / 90f);

                time += Time.deltaTime * speedIncreaseMultiplier * slopeIncreaseMultiplier * slopeAngleIncriase;
            } else {
                time += Time.deltaTime * speedIncreaseMultiplier;
            }

            yield return null;
        }

        moveSpeed = desiredMoveSpeed;

    }

    private void MovePlayer() {
        //  Calcula a direção do movimento
        moveDirection = orientation.forward * vInput + orientation.right * hInput;

        // No aclive
        if (OnSlope() && !exitingSlope) {
            rb.AddForce(20f * moveSpeed * GetSlopeMoveDirection(moveDirection), ForceMode.Force);

            if (rb.velocity.y > 0) {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }

        //  No chão
        if (grounded)
            rb.AddForce(10f * moveSpeed * moveDirection.normalized, ForceMode.Force);

        //  No ar
        else if (!grounded)
            rb.AddForce(10f * airMultiplier * moveSpeed * moveDirection.normalized, ForceMode.Force);

        //  Desligar a gravidade
        rb.useGravity = !OnSlope();

    }

    private void SpeedControl() {
        //  Limitar a velocidade no aclive
        Vector3 flatVel = new(rb.velocity.x, 0f, rb.velocity.z);

        if (OnSlope() && !exitingSlope) {
            if (rb.velocity.magnitude > moveSpeed) {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }

        } else {

            if (flatVel.magnitude > moveSpeed) {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }

        SetText("Vloecidad: " + Mathf.Round(flatVel.magnitude));

    }

    private void Jump() {
        exitingSlope = true;

        //reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

    }

    private void ResetJump() {
        readyToJump = true;

        exitingSlope = false;

    }

    public bool OnSlope() {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f)) {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;

    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction) {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;

    }

    private void SetText(string text) {
        texto_velocidade.text = text;
    }

}
