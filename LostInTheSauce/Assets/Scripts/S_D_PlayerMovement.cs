using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Player's movement speed
    private Rigidbody2D rb;  // Rigidbody for physics-based movement
    private Vector2 moveInput;  // Store input (WASD or Arrow keys)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Get Rigidbody2D component on start
    }

    void Update()
    {
        // Get input for movement (WASD or Arrow keys)
        moveInput.x = Input.GetAxisRaw("Horizontal");  // Left/Right (A/D or Arrow keys)
        moveInput.y = Input.GetAxisRaw("Vertical");    // Up/Down (W/S or Arrow keys)
        moveInput.Normalize();  // Normalize to prevent faster diagonal movement
    }

    void FixedUpdate()
    {
        // Apply movement to the Rigidbody2D (for physics)
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}
