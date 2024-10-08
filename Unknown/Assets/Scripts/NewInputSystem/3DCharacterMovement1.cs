using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;

public class NewCharacterMovement : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField] private float moveSpeed = 5f;
    
    public PlayerInputActions playerControls;
    private InputAction move;
    private Vector2 moveDirection = Vector2.zero;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        // Read the movement input as a Vector2 from the player controls (e.g., WASD or joystick).
        moveDirection = move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        // Use the X and Z axes for horizontal movement (keep Y axis velocity unchanged for gravity)
        Vector3 movement = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.y) * moveSpeed;
        rb.velocity = movement;
    }
}