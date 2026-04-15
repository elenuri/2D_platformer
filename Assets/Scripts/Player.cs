using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f;
    public float sprintSpeed = 12f;
    public float acceleration = 10f;

    [Header("Jump")]
    public float jumpForce = 6f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;
    private bool isSprinting;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Input
        moveInput = Input.GetAxis("Horizontal");
        isSprinting = Input.GetKey(KeyCode.LeftShift);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // Keep horizontal momentum, only affect Y
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        // Choose speed
        float targetSpeed = isSprinting ? sprintSpeed : moveSpeed;

        // Smooth movement (prevents weird stuck feeling)
        float targetVelocityX = moveInput * targetSpeed;

        float velocityX = Mathf.Lerp(
            rb.linearVelocity.x,
            targetVelocityX,
            acceleration * Time.fixedDeltaTime
        );

        rb.linearVelocity = new Vector2(velocityX, rb.linearVelocity.y);
    }
}