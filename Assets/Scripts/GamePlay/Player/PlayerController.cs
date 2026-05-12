using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform orientationT;

    [Header("Player Movement")]
    [SerializeField] private KeyCode movementKey;
    [SerializeField] private float moveSpeed;

    [Header("Jump Settings")]
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private bool canJump;

    [Header("Sliding Settings")]
    [SerializeField] private KeyCode slideKey;
    [SerializeField] private float slideMuliplier;
    [SerializeField] private float slideDrag;

    [Header("Ground Check Settings")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundDrag;


    private Rigidbody rb;
    private float horizontalInput, verticalInput;
    private bool isSliding;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        SetInputs();
        SetPlayerDrag();
        LimitPlayerSpeed();
    }

    private void FixedUpdate()
    {
        SetPlayerMovement();
    }

    private void SetInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(slideKey))
        {
            isSliding = true;
            Debug.Log("Sliding");
        }
        else if (Input.GetKey(movementKey)) 
        {
            isSliding= false;
            Debug.Log("Not Sliding");
        }
        else if (Input.GetKey(jumpKey) && canJump && IsGrounded())
        {
            canJump = false;
            SetPlayerJump();
            Invoke(nameof(Resetjumping), jumpCooldown);
        }
    }

    private void SetPlayerMovement()
    {
        Vector3 moveDirection = orientationT.forward * verticalInput + orientationT.right * horizontalInput;
        
        if(isSliding)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * slideMuliplier, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
        }

    }

    private void SetPlayerDrag()
    {
        rb.linearDamping = isSliding ? slideDrag : groundDrag; 
    }

    private void LimitPlayerSpeed()
    {
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVelocity.x, rb.linearVelocity.y, limitedVelocity.z);
        }
    }

    private void SetPlayerJump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void Resetjumping()
    {
        canJump = true;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f + .2f , groundLayer);
    }
}
