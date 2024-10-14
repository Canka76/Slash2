using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewCharacterMovement : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField] private float moveSpeed = 5f;

    public PlayerInputActions playerControls;
    private InputAction move;
    private InputAction jump;
    private Vector2 moveDirection = Vector2.zero;
    
    private bool isGround = true;

    [SerializeField] private float jumpStrength = 5f;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        jump = playerControls.Player.Jump;
        move = playerControls.Player.Move;
        move.Enable();
        jump.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the movement input as a Vector2
        moveDirection = move.ReadValue<Vector2>();
        
    }

    private void FixedUpdate()
    {
        // Apply horizontal movement (X and Z axes)
        Vector3 movement = new Vector3(moveDirection.x, 0f, moveDirection.y) * moveSpeed;

        // Maintain vertical velocity (for gravity or jumping)
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        // Handle jumping
        JumpChecker();
    }

    void JumpChecker()
    {
        if (isGround && Input.GetKey(KeyCode.Space))
        {
            // Apply jump force when grounded and jump is pressed
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
            isGround = false;  // Prevent double jumping
            Debug.Log("jump");
        }
    }

    // Detect when the player is grounded
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            Debug.Log("Ground");
        }
    }

    // Detect when the player leaves the ground
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
}