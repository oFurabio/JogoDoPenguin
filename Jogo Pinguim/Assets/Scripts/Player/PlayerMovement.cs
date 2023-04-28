using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimento")]
    private float moveSpeed;
    public float walkSpeed = 5f;
    public float slideSpeed = 10f;
    public float slideTopSpeed = 15f;

    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;

    public float speedIncreaseMultiplier = 0.75f;
    public float slopeIncreaseMultiplier = 1.25f;

    public float groundDrag = 4f;

    [Header("Salto")]
    public float jumpForce = 12f;
    public float secondJumpForce = 7f;
    public float airMultiplier = 0.4f;
    bool readyToJump = true;

    [Header("Deslizando")]
    public float slideForce = 50f;
    public float dashjumpForce = 10f;
    public float forwardForce = 25f;
    private bool canDash = true;
    private Quaternion initialRotation; // Rotação inicial do jogador

    [Header("Teclas")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode slideKey = KeyCode.LeftShift;

    [Header("Checa o solo")]
    public float playerHeight = 0.0625f;
    public LayerMask whatIsGround;
    public float yOffset = 0.1f;
    public float zOffset = 0.1f;
    public float radius = 0.1f;
    [HideInInspector] public bool grounded = false;

    [Header("Inclinação")]
    private float rotationAngle;
    public float maxSlopeAngle = 40f;
    [SerializeField] private float angle = 0;
    private RaycastHit slopeHit;
    private bool exitingSlope = false;

    float hInput, vInput;

    [Header("Referências")]
    public Transform orientation;
    public Animator animator;
    public Transform playerObj;

    Vector3 moveDirection;

    Rigidbody rb;

    [Header("Outros")]
    public MovementState state;
    private Quaternion slidingRotation;
    public bool sliding;

    public enum MovementState
    {
        andando,
        deslizando,
        ar
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        //rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        if (!sliding)
            grounded = Physics.CheckSphere(transform.position - new Vector3(0, playerHeight * 0.5f + yOffset, zOffset), radius, whatIsGround);
        else
            grounded = Physics.CheckSphere(transform.position - new Vector3(0, playerHeight * 0.5f + yOffset, zOffset), radius, whatIsGround);

        if (!InterfaceManager.jogoPausado)
            MyInput();

        SpeedControl();
        StateHandler();
        Animacao();

        if (grounded)
        {
            rb.drag = groundDrag;
            canDash = true;
            readyToJump = true;
        }
        else
            rb.drag = 0;

        if (sliding)
        {
            //playerObj.transform.rotation = Quaternion.Euler(rotationAngle, playerObj.rotation.y, playerObj.rotation.z);
            slidingRotation = Quaternion.Euler(rotationAngle, playerObj.eulerAngles.y, playerObj.eulerAngles.z);
            playerObj.transform.rotation = Quaternion.Lerp(playerObj.transform.rotation, slidingRotation, slideSpeed * Time.deltaTime);

        }
    }

    private void FixedUpdate()
    {
        MovePlayer();

        if (sliding)
            SlidingMovement();
    }

    private void MyInput()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        //  Quando pular
        if (Input.GetKeyDown(jumpKey))
            Jump();

        if (Input.GetKeyDown(slideKey) && (hInput != 0 || vInput != 0))
            StartSlide();

        if (Input.GetKeyUp(slideKey) && sliding)
            StopSlide();

        /*if (hInput == 0 && vInput == 0)
            StopSlide();*/

    }

    private void StateHandler()
    {
        //  Modo - Deslizando
        if (sliding)
        {
            state = MovementState.deslizando;

            if (OnSlope() && rb.velocity.y < 0.1f)
                desiredMoveSpeed = slideTopSpeed;
            else
                desiredMoveSpeed = slideSpeed;
        }

        //  Modo - Andando
        else if (grounded)
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
        //  Calcula a direção do movimento
        moveDirection = orientation.forward * vInput + orientation.right * hInput;

        //  Na inclinação
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(20f * moveSpeed * GetSlopeMoveDirection(moveDirection), ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        //  No chão
        else if (grounded)
            rb.AddForce(10 * moveSpeed * moveDirection, ForceMode.Force);

        //  No ar
        else if (!grounded)
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
        StopSlide();
        if (grounded == true && readyToJump)
        {
            exitingSlope = true;
            readyToJump = false;

            //  Reset Y velocity
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        else if (readyToJump && grounded == false)
        {
            exitingSlope = true;
            readyToJump = false;

            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            rb.AddForce(transform.up * secondJumpForce, ForceMode.Impulse);
        }
    }

    public bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        rotationAngle = 90 - (angle - GetSlopeMoveDirection(moveDirection).y);

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

    private void OffsetHandler()
    {
        if (sliding)
        {
            yOffset = 0.05f;
            zOffset = 0f;
        }
        else
        {
            yOffset = -0.425f;
            zOffset = -0.075f;
        }

    }

    private void StartSlide()
    {

        if (canDash)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            rb.AddForce(transform.up * jumpForce + orientation.forward * forwardForce, ForceMode.Impulse);
        }

        sliding = true;
        OffsetHandler();
    }

    private void SlidingMovement()
    {
        canDash = false;

        //  Deslizando normalmente
        if (!OnSlope() || rb.velocity.y > -0.1f)
            rb.AddForce(moveDirection.normalized * slideForce, ForceMode.Force);

        //  Deslizando numa descida
        else
            rb.AddForce(GetSlopeMoveDirection(moveDirection) * slideForce, ForceMode.Force);
    }

    private void StopSlide()
    {
        sliding = false;
        OffsetHandler();

        if (grounded)
            moveSpeed = walkSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        // Desenha uma esfera com o mesmo raio e posição do CheckSphere
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position - new Vector3(0, playerHeight * 0.5f + yOffset, zOffset), radius);
    }

    private void Animacao()
    {
        animator.SetBool("sliding", sliding);
        animator.SetBool("grounded", grounded);

        if (state == MovementState.andando && (vInput != 0 || hInput != 0 || vInput != 0 && hInput != 0))
        {
            animator.SetBool("walk", true);
        }
        else
        {
            animator.SetBool("walk", false);
        }

        animator.SetFloat("verticalSpeed", rb.velocity.y);
        animator.SetBool("dead", Health.dead);
    }

}