using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Header("Movimento")]
    private float moveSpeed;
    public float walkSpeed = 5f;
    public float slideSpeed = 10f;
    public float slideTopSpeed = 15f;

    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;

    public float speedIncreaseMultiplier = 1.5f;
    public float slopeIncreaseMultiplier = 2.5f;

    public float groundDrag = 4f;

    [Header("Salto")]
    public float jumpForce = 12f;
    public float secondJumpForce = 7f;
    public float airMultiplier = 0.4f;
    bool readyToJump = true;

    [Header("Teclas")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Checa o solo")]
    public float playerHeight = 1f;
    public LayerMask whatIsGround;
    [HideInInspector] public bool grounded = false;

    [Header("Inclinação")]
    public float maxSlopeAngle = 40f;
    private RaycastHit slopeHit;
    private bool exitingSlope = false;

    float hInput, vInput;

    [Header("Referências")]
    public Transform orientation;
    public Animator animator;

    Vector3 moveDirection;

    Rigidbody rb;

    [Header("Outros")]
    public MovementState state;
    public bool sliding;
    private bool dead = false;
    public enum MovementState {
        andando,
        deslizando,
        ar
    }

    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update() {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();
        Animacao();

        if (grounded) {
            rb.drag = groundDrag;
            exitingSlope = false;
            readyToJump = true;
        } else
            rb.drag = 0;
    }

    private void FixedUpdate() {
        MovePlayer();
    }

    private void MyInput() {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        //  Quando pular
        if (Input.GetKeyDown(jumpKey))
            Jump();

    }

    private void StateHandler() {
        //  Modo - Deslizando
        if (sliding) {
            state = MovementState.deslizando;

            if (OnSlope() && rb.velocity.y < 0.1f)
                desiredMoveSpeed = slideTopSpeed;
            else
                desiredMoveSpeed = slideSpeed;
        }

        //  Modo - Andando
        else if (grounded) {
            state = MovementState.andando;
            desiredMoveSpeed = walkSpeed;
        }

        //  Modo - Ar
        else {
            state = MovementState.ar;
        }

        if (Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 4f && moveSpeed != 0) {
            StopAllCoroutines();
            StartCoroutine(SmootlyLerpMoveSpeed());
        } else {
            moveSpeed = desiredMoveSpeed;
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;
    }

    private IEnumerator SmootlyLerpMoveSpeed() {
        float time = 0f;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        while (time < difference) {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);

            if (OnSlope()) {
                float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
                float slopeAngleIncrease = 1 + (slopeAngle / 90f);

                time += Time.deltaTime * speedIncreaseMultiplier * slopeIncreaseMultiplier * slopeAngleIncrease;
            } else
                time += Time.deltaTime * speedIncreaseMultiplier;

            yield return null;
        }

        moveSpeed = desiredMoveSpeed;
    }

    private void MovePlayer() {
        //  Calcula a direção do movimento
        moveDirection = orientation.forward * vInput + orientation.right * hInput;

        //  Na inclinação
        if (OnSlope() && !exitingSlope) {
            rb.AddForce(20f * moveSpeed * GetSlopeMoveDirection(moveDirection), ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        //  No chão
        else if(grounded)
            rb.AddForce(10 * moveSpeed * moveDirection, ForceMode.Force);

        //  No ar
        else if(!grounded)
            rb.AddForce(10 * airMultiplier * moveSpeed * moveDirection, ForceMode.Force);

        rb.useGravity = !OnSlope();
    }

    private void SpeedControl() {
        //  Limitando a velocidade na inclinação
        if (OnSlope() && !exitingSlope) {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        //  Limitando a velocidade no chão ou no ar
        else {
            Vector3 flatVel = new(rb.velocity.x, 0f, rb.velocity.z);

            //  Limitar a velocidade
            if (flatVel.magnitude > moveSpeed) {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
        
    }

    private void Jump() {
        if (grounded == true && readyToJump) {
            exitingSlope = true;
            readyToJump = false;

            //  Reset Y velocity
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        } else if (readyToJump && grounded == false) {
            exitingSlope = true;
            readyToJump = false;

            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            rb.AddForce(transform.up * secondJumpForce, ForceMode.Impulse);
        }
    }

    public bool OnSlope() {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f)) {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction) {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

    private void Animacao() {
        animator.SetBool("sliding", sliding);
        animator.SetBool("grounded", grounded);
        if (state == MovementState.andando && (vInput != 0 || hInput != 0 || vInput != 0 && hInput != 0)) {
            animator.SetBool("walk", true);
        } else {
            animator.SetBool("walk", false);
        }

        animator.SetFloat("verticalSpeed", rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.K)) {
            dead = !dead;

            if (sliding && dead)
                sliding = false;

            animator.SetBool("dead", dead);
        }
    }

}
