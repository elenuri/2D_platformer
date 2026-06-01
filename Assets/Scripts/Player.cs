using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f;
    public float sprintSpeed = 12f;

    [Header("Movement Feel")]
    public float accelerationRate = 25f;
    public float decelerationRate = 40f;
    public float airControlMultiplier = 0.6f;
    public float directionChangeBoost = 2f;

    [Header("Jump")]
    public float jumpForce = 6f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    private bool isGrounded;
    private float moveInput;
    private bool isSprinting;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        isSprinting = Input.GetKey(KeyCode.LeftShift);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        HandleFlip();
    }

    void FixedUpdate()
    {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        HandleMovement();
    }

    void LateUpdate()
    {
        UpdateAnimation();
    }

    void HandleMovement()
    {
        float targetSpeed = isSprinting ? sprintSpeed : moveSpeed;
        float targetVelocityX = moveInput * targetSpeed;

        // Decide accel vs decel
        float accelRate = (Mathf.Abs(targetVelocityX) > 0.01f)
            ? accelerationRate
            : decelerationRate;

        // Air control
        if (!isGrounded)
        {
            accelRate *= airControlMultiplier;
        }

        // Stronger direction change
        if (moveInput != 0 && Mathf.Sign(moveInput) != Mathf.Sign(rb.linearVelocity.x))
        {
            accelRate *= directionChangeBoost;
        }

        // Move towards target velocity
        float newVelocityX = Mathf.MoveTowards(
            rb.linearVelocity.x,
            targetVelocityX,
            accelRate * Time.fixedDeltaTime
        );

        // Anti-stuck safeguard
        if (moveInput != 0 && Mathf.Abs(newVelocityX) < 0.1f)
        {
            newVelocityX = moveInput * 1.5f;
        }

        // Prevent micro drifting
        if (moveInput == 0 && Mathf.Abs(newVelocityX) < 0.05f)
        {
            newVelocityX = 0f;
        }

        rb.linearVelocity = new Vector2(newVelocityX, rb.linearVelocity.y);
    }

    // Animation
    void UpdateAnimation()
    {
        if (animator == null) return;

        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("VerticalVelocity", rb.linearVelocity.y);
    }

    // Flip sprite without changing scale
    void HandleFlip()
    {
        if (moveInput > 0)
        {
            sr.flipX = false;
        }
        else if (moveInput < 0)
        {
            sr.flipX = true;
        }
    }

    // Draw ground check in Scene view
    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}