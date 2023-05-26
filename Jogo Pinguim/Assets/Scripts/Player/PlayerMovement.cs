using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimento")]
    [Range(1f, 20f)]
    public float walkSpeed = 5f;
    [Range(1f, 20f)]
    public float slideSpeed = 10f;
    [Range(1f, 40f)]
    public float slideTopSpeed = 15f;
    [Range(1f, 40f)]
    public float dashSpeed = 20f;
    private float moveSpeed;

    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;

    public float speedIncreaseMultiplier = 1.25f;
    public float slopeIncreaseMultiplier = 1.75f;

    public float groundDrag = 4f;

    [Header("Salto")]
    [Range(1f, 20f)]
    public float jumpForce = 12f;
    [Range(1f, 15f)]
    public float secondJumpForce = 7f;
    public float jumpCooldown = 0.25f;
    public float airMultiplier = 0.4f;

    private bool canDouble = true;
    private bool readyToJump = true;

    [Header("Checa o solo")]
    public LayerMask whatIsGround;
    public float playerHeight = 0f;
    public float aBit = 0f;
    public float radius = 0.4f;

    [Header("Inclinação")]
    [Range(0f, 75f)]
    public float maxSlopeAngle = 45f;
    public float angle;
    private RaycastHit slopeHit;
    private bool exitingSlope = false;


    [Header("Referências")]
    public Transform orientation;
    public Animator animator;
    public Transform playerObj;
    private ParticlesController ps;
    private Rigidbody rb;


    [Header("Outros")]
    public MovementState state;
    public bool sliding;
    public bool dashing;
    private float hInput, vInput;
    Vector3 moveDirection;

    public enum MovementState
    {
        andando,
        deslizando,
        dashando,
        ar
    }

    private void Start()
    {
        ps = GetComponent<ParticlesController>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void OnDrawGizmosSelected()
    {
        // Desenha uma esfera com o mesmo raio e posição do CheckSphere
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerObj.transform.position, radius);
    }

    private void Update()
    {
        Debug.DrawRay(playerObj.transform.position, Vector3.down, Color.red);

        if (Health.dead) {
            vInput = 0f;
            hInput = 0f;
        }

        if (PodeMover())
            MyInput();

        SpeedControl();
        StateHandler();
        Animacao();

        if (state == MovementState.andando || state == MovementState.deslizando)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        if (Grounded()) {
            canDouble = true;
        }
    }

    private void FixedUpdate() {
        MovePlayer();
    }

    public bool Grounded() {
        return Physics.CheckSphere(playerObj.transform.position, radius, whatIsGround);
    }

    public bool PodeMover()
    {
        if (InterfaceManager.jogoPausado || InterfaceManager.fimDeJogo)
            return false;

        return true;
    }

    private void MyInput()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        if (Input.GetButton("Jump") && readyToJump && Grounded())
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if(Input.GetButtonDown("Jump") &&!Grounded() && canDouble) {
            SecondaryJump();
        }
    }

    private void StateHandler()
    {
        if (dashing)
        {
            state = MovementState.dashando;
            desiredMoveSpeed = dashSpeed;
        }
        //  Modo - Deslizando
        else if (sliding && Grounded())
        {
            state = MovementState.deslizando;

            if (OnSlope() && rb.velocity.y < 0.1f)
                desiredMoveSpeed = slideTopSpeed;
            else
                desiredMoveSpeed = slideSpeed;
        }

        //  Modo - Andando
        else if (Grounded())
        {
            state = MovementState.andando;
            desiredMoveSpeed = walkSpeed;
        }

        //  Modo - Ar
        else
        {
            state = MovementState.ar;
        }

        if (Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 4f && moveSpeed != 0)
        {
            StopAllCoroutines();
            StartCoroutine(SmootlyLerpMoveSpeed());
        }
        else
        {
            moveSpeed = desiredMoveSpeed;
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;
    }

    private IEnumerator SmootlyLerpMoveSpeed()
    {
        float time = 0f;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);

            if (OnSlope())
            {
                float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
                float slopeAngleIncrease = 1 + (slopeAngle / 90f);

                time += Time.deltaTime * speedIncreaseMultiplier * slopeIncreaseMultiplier * slopeAngleIncrease;
            }
            else
                time += Time.deltaTime * speedIncreaseMultiplier;

            yield return null;
        }

        moveSpeed = desiredMoveSpeed;
    }

    private void MovePlayer()
    {
        if (state == MovementState.dashando) return;

        //  Calcula a direção do movimento
        moveDirection = orientation.forward * vInput + orientation.right * hInput;

        //  Na inclinação
        if (OnSlope() && !exitingSlope)
        {
            //Debug.Log("Na inclinação " + angle);

            rb.AddForce(20f * moveSpeed * GetSlopeMoveDirection(moveDirection), ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        //  No chão
        else if (Grounded())
            rb.AddForce(10 * moveSpeed * moveDirection, ForceMode.Force);

        //  No ar
        else if (!Grounded())
            rb.AddForce(10 * airMultiplier * moveSpeed * moveDirection, ForceMode.Force);

        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        //  Limitando a velocidade na inclinação
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        //  Limitando a velocidade no chão ou no ar
        else
        {
            Vector3 flatVel = new(rb.velocity.x, 0f, rb.velocity.z);

            //  Limitar a velocidade
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }

    }

    private void Jump()
    {
        exitingSlope = true;
        ps.burst.Play();

        rb.velocity = new(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void SecondaryJump()
    {
        canDouble = false;
        ps.burst.Play();

        rb.velocity = new(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * secondJumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }

    public bool OnSlope() {
        if (Physics.Raycast(playerObj.transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + aBit, whatIsGround)) {
            angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction) {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

    public void Ataque() {
        rb.velocity = new(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(1.5f * secondJumpForce * transform.up, ForceMode.Impulse);
        readyToJump = true;
    }

    private void Animacao()
    {
        animator.SetBool("sliding", sliding);
        animator.SetBool("grounded", Grounded());

        if (state == MovementState.andando && (vInput != 0 || hInput != 0))
            animator.SetBool("walk", true);
        else
            animator.SetBool("walk", false);

        animator.SetFloat("verticalSpeed", rb.velocity.y);
        animator.SetBool("dead", Health.dead);
    }
}