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

    [Header("VFX")]
    public Transform runParticlePoint;
    public ParticleSystem runParticles;

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

        if (runParticlePoint == null)
            Debug.LogWarning("Run Particle Point is not assigned!");

        if (runParticles == null)
            Debug.LogWarning("Run Particles is not assigned!");
    }

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        isSprinting = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        HandleFlip();
    }

    void FixedUpdate()
    {
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
        HandleRunParticles();
    }

    void HandleMovement()
    {
        float targetSpeed = isSprinting ? sprintSpeed : moveSpeed;
        float targetVelocityX = moveInput * targetSpeed;

        float accelRate = (Mathf.Abs(targetVelocityX) > 0.01f)
            ? accelerationRate
            : decelerationRate;

        if (!isGrounded)
            accelRate *= airControlMultiplier;

        if (moveInput != 0 &&
            Mathf.Sign(moveInput) != Mathf.Sign(rb.linearVelocity.x))
        {
            accelRate *= directionChangeBoost;
        }

        float newVelocityX = Mathf.MoveTowards(
            rb.linearVelocity.x,
            targetVelocityX,
            accelRate * Time.fixedDeltaTime
        );

        if (moveInput != 0 && Mathf.Abs(newVelocityX) < 0.1f)
            newVelocityX = moveInput * 1.5f;

        if (moveInput == 0 && Mathf.Abs(newVelocityX) < 0.05f)
            newVelocityX = 0f;

        rb.linearVelocity = new Vector2(
            newVelocityX,
            rb.linearVelocity.y
        );
    }

    void UpdateAnimation()
    {
        if (animator == null) return;

        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("VerticalVelocity", rb.linearVelocity.y);
    }

    void HandleFlip()
    {
        if (runParticlePoint == null) return;

        if (moveInput > 0)
        {
            sr.flipX = false;

            runParticlePoint.localPosition =
                new Vector3(-0.3f, -0.5f, 0f);
        }
        else if (moveInput < 0)
        {
            sr.flipX = true;

            runParticlePoint.localPosition =
                new Vector3(0.3f, -0.5f, 0f);
        }
    }

    void HandleRunParticles()
    {
        if (runParticles == null) return;

        bool isRunning =
            isGrounded &&
            Mathf.Abs(moveInput) > 0.1f &&
            Mathf.Abs(rb.linearVelocity.x) > 1f;

        if (isRunning)
        {
            if (!runParticles.isPlaying)
            {
                Debug.Log("PLAY PARTICLES");
                runParticles.Play();
            }
        }
        else
        {
            if (runParticles.isPlaying)
            {
                Debug.Log("STOP PARTICLES");
                runParticles.Stop();
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(
            groundCheck.position,
            groundCheckRadius
        );
    }
}