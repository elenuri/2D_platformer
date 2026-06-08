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

    [Header("Run VFX")]
    public Transform runParticlePoint;
    public ParticleSystem runParticles;

    [Header("Dash Ghost")]
    public GameObject dashGhostPrefab;
    public float ghostSpawnInterval = 0.05f;

    [Header("Jump / Land VFX")]
    public GameObject jumpVFXPrefab;
    public GameObject landVFXPrefab;

    [Header("Respawn")]
    public Transform spawnPoint;
    public float deathY = -15f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    private bool isGrounded;
    private bool wasGrounded;

    private float moveInput;
    private bool isSprinting;

    private float ghostTimer;

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

        CheckFallDeath();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            SpawnJumpVFX();

            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                0f
            );

            rb.AddForce(
                Vector2.up * jumpForce,
                ForceMode2D.Impulse
            );
        }

        HandleFlip();
        HandleDashGhosts();
    }

    void FixedUpdate()
    {
        wasGrounded = isGrounded;

        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        if (!wasGrounded && isGrounded)
        {
            SpawnLandVFX();
        }

        HandleMovement();
    }

    void LateUpdate()
    {
        UpdateAnimation();
        HandleRunParticles();
    }

    void HandleMovement()
    {
        float targetSpeed =
            isSprinting ? sprintSpeed : moveSpeed;

        float targetVelocityX =
            moveInput * targetSpeed;

        float accelRate =
            (Mathf.Abs(targetVelocityX) > 0.01f)
            ? accelerationRate
            : decelerationRate;

        if (!isGrounded)
        {
            accelRate *= airControlMultiplier;
        }

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

        if (moveInput != 0 &&
            Mathf.Abs(newVelocityX) < 0.1f)
        {
            newVelocityX = moveInput * 1.5f;
        }

        if (moveInput == 0 &&
            Mathf.Abs(newVelocityX) < 0.05f)
        {
            newVelocityX = 0f;
        }

        rb.linearVelocity = new Vector2(
            newVelocityX,
            rb.linearVelocity.y
        );
    }

    void UpdateAnimation()
    {
        animator.SetFloat(
            "Speed",
            Mathf.Abs(rb.linearVelocity.x)
        );

        animator.SetBool(
            "IsGrounded",
            isGrounded
        );

        animator.SetFloat(
            "VerticalVelocity",
            rb.linearVelocity.y
        );
    }

    void HandleFlip()
    {
        if (moveInput > 0)
        {
            sr.flipX = false;

            if (runParticlePoint != null)
            {
                runParticlePoint.localPosition =
                    new Vector3(
                        -0.3f,
                        -0.5f,
                        0f
                    );
            }
        }
        else if (moveInput < 0)
        {
            sr.flipX = true;

            if (runParticlePoint != null)
            {
                runParticlePoint.localPosition =
                    new Vector3(
                        0.3f,
                        -0.5f,
                        0f
                    );
            }
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
                runParticles.Play();
            }
        }
        else
        {
            if (runParticles.isPlaying)
            {
                runParticles.Stop();
            }
        }
    }

    void HandleDashGhosts()
    {
        if (dashGhostPrefab == null)
            return;

        bool shouldSpawnGhosts =
            isGrounded &&
            isSprinting &&
            Mathf.Abs(moveInput) > 0.1f;

        if (!shouldSpawnGhosts)
            return;

        ghostTimer -= Time.deltaTime;

        if (ghostTimer <= 0f)
        {
            SpawnGhost();
            ghostTimer = ghostSpawnInterval;
        }
    }

    void SpawnGhost()
    {
        GameObject ghost = Instantiate(
            dashGhostPrefab,
            transform.position,
            Quaternion.identity
        );

        SpriteRenderer ghostSR =
            ghost.GetComponent<SpriteRenderer>();

        if (ghostSR == null)
        {
            Debug.LogError(
                "DashGhost prefab has no SpriteRenderer!"
            );
            return;
        }

        ghostSR.sprite = sr.sprite;
        ghostSR.flipX = sr.flipX;

        ghostSR.sortingLayerID =
            sr.sortingLayerID;

        ghostSR.sortingOrder =
            sr.sortingOrder - 1;
    }

    void SpawnJumpVFX()
    {
        if (jumpVFXPrefab == null) return;

        GameObject vfx = Instantiate(
            jumpVFXPrefab,
            groundCheck.position,
            Quaternion.identity
        );

        Vector3 scale = vfx.transform.localScale;

        scale.x = sr.flipX
            ? -Mathf.Abs(scale.x)
            : Mathf.Abs(scale.x);

        vfx.transform.localScale = scale;
    }

    void SpawnLandVFX()
    {
        if (landVFXPrefab == null) return;

        Instantiate(
            landVFXPrefab,
            groundCheck.position + Vector3.down * 0.2f,
            Quaternion.identity
        );
    }

    void CheckFallDeath()
    {
        if (transform.position.y < deathY)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        if (spawnPoint == null)
        {
            Debug.LogWarning("No Spawn Point assigned!");
            return;
        }

        transform.position = spawnPoint.position;
        rb.linearVelocity = Vector2.zero;
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