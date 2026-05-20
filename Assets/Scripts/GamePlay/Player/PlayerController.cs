using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public event Action OnPlayerJumped;
    public event Action<PlayerState> OnPlayerStateChanged;

    [Header("References")]
    [SerializeField] private Transform orientationT;

    [Header("Player Movement")]
    [SerializeField] private float moveSpeed;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private bool canJump;
    [SerializeField] private float airMultiplier;
    [SerializeField] private float airDrag;

    [Header("Sliding Settings")]
    [SerializeField] private float slideMuliplier;
    [SerializeField] private float slideDrag;

    [Header("Ground Check Settings")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundDrag;

    private StateController stateController;

    private Rigidbody rb;


    private float startingMoveSpeed;
    private float startingJumpForce;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 movementDirection;

    private bool isSliding;

    private Keyboard keyboard;

    private void Awake()
    {
        stateController = GetComponent<StateController>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        keyboard = Keyboard.current;

        startingMoveSpeed = moveSpeed;
        startingJumpForce = jumpForce;
    }

    private void Update()
    {
        if (GameManager.Instance.GetCurrentGameState() != GameState.Play
            && GameManager.Instance.GetCurrentGameState() != GameState.Resume)
        {
            return;
        }

        SetInputs();
        SetStates();
        SetPlayerDrag();
        LimitPlayerSpeed();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.GetCurrentGameState() != GameState.Play
            && GameManager.Instance.GetCurrentGameState() != GameState.Resume)
        {
            return;
        }

        SetPlayerMovement();
    }

    private void SetInputs()
    {
        horizontalInput = 0;
        verticalInput = 0;

        if (keyboard.aKey.isPressed)
            horizontalInput = -1;

        if (keyboard.dKey.isPressed)
            horizontalInput = 1;

        if (keyboard.wKey.isPressed)
            verticalInput = 1;

        if (keyboard.sKey.isPressed)
            verticalInput = -1;

        if (keyboard.qKey.wasPressedThisFrame)
        {
            isSliding = true;
        }
        else if (keyboard.eKey.wasPressedThisFrame)
        {
            isSliding = false;
        }
        else if (keyboard.spaceKey.wasPressedThisFrame && canJump && IsGrounded())
        {
            canJump = false;
            SetPlayerJump();

            Invoke(nameof(Resetjumping), jumpCooldown);
        }
    }

    private void SetStates()
    {
        var movementDirection = GetMovementDirection();
        var isGrounded = IsGrounded();
        var isSliding = IsSliding();
        var currentState = stateController.GetCurrentState();

        var newState = currentState switch
        {
            _ when movementDirection == Vector3.zero && isGrounded && !isSliding => PlayerState.Idle,
            _ when movementDirection != Vector3.zero && isGrounded && !isSliding => PlayerState.Move,
            _ when movementDirection != Vector3.zero && isGrounded && isSliding => PlayerState.Slide,
            _ when movementDirection == Vector3.zero && isGrounded && isSliding => PlayerState.SlideIdle,
            _ when !canJump && !isGrounded => PlayerState.Jump,
            _ => currentState
        };

        if (newState != currentState)
        {
            stateController.ChangeState(newState);
            OnPlayerStateChanged?.Invoke(newState);
        }
    }

    private void SetPlayerMovement()
    {
        movementDirection =
            orientationT.forward * verticalInput +
            orientationT.right * horizontalInput;

        float forceMultiplier = stateController.GetCurrentState() switch
        {
            PlayerState.Idle => 0f,
            PlayerState.Move => 1f,
            PlayerState.Slide => slideMuliplier,
            PlayerState.Jump => airMultiplier,
            _ => 1f
        };

        rb.AddForce(movementDirection.normalized * moveSpeed * forceMultiplier,
                ForceMode.Force);
    }

    private void SetPlayerDrag()
    {
        rb.linearDamping = stateController.GetCurrentState() switch
        {
            PlayerState.Move => groundDrag,
            PlayerState.Slide => slideDrag,
            PlayerState.Jump => airDrag,
            _ => rb.linearDamping
        };
    }

    private void LimitPlayerSpeed()
    {
        Vector3 flatVelocity =
            new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity =
                flatVelocity.normalized * moveSpeed;

            rb.linearVelocity =
                new Vector3(
                    limitedVelocity.x,
                    rb.linearVelocity.y,
                    limitedVelocity.z);
        }
    }

    private void SetPlayerJump()
    {
        OnPlayerJumped?.Invoke();
        rb.linearVelocity =
            new Vector3(
                rb.linearVelocity.x,
                0f,
                rb.linearVelocity.z);

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void Resetjumping()
    {
        canJump = true;
    }

    public void SetMovementSpeed(float speed, float duration)
    {
        moveSpeed += speed;
        Invoke(nameof(ResetMovementSpeed), duration);
    }

    private void ResetMovementSpeed()
    {
        moveSpeed = startingMoveSpeed;
    }

    public void SetJumpForce(float force, float duration)
    {
        jumpForce += force;
        Invoke(nameof(ResetJumpForce), duration);
    }

    private void ResetJumpForce()
    {
        jumpForce = startingJumpForce;
    }

    public Rigidbody GetPlayerRigidbody()
    {
        return rb;
    }

    #region Helper Functions
    private bool IsGrounded()
    {
        return Physics.Raycast(
            transform.position,
            Vector3.down,
            playerHeight * .5f + .2f,
            groundLayer);
    }

    private Vector3 GetMovementDirection()
    {
        return movementDirection.normalized;
    }

    private bool IsSliding()
    {
        return isSliding;
    }

    #endregion
}