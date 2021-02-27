using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private readonly float gravity = -9.81f;

    [Header("Components")]
    public CharacterController controller;
    public Transform gameProgresser;
    public SpriteRenderer spriteRenderer;
    public GameObject superSprite;
    public GameObject scoreObject;

    [Header("PlayerMovement")]
    [Range(1, 60)]
    public float MovementSpeed = 5f;

    [Tooltip("Player acceleration smoothing")]
    [Range(0, 2)]
    public float smoothing = .2f;

    [Range(0, 200)]
    public float jumpForce = 30f;

    [Tooltip("Amount of frames player needs to be grounded to be able to jump again")]
    [Range(0, 20)]
    public float jumpResetTimer = 5f;

    [Tooltip("Multiply gravity when velocity is pointing downwards")]
    [Range(1, 5f)]
    public float fallMultiplier = 2.5f;

    [Tooltip("Base gravity multiplier")]
    [Range(0.1f, 2)]
    public float gravityMultiplier = 1f;

    [Range(20, 300)]
    public float playerResetDistance = 100f;

    [Range(0.1f, 10)]
    public float groundCheckDistance = 5f;

    // Player state
    private Transform groundCheck;
    private float playerInput = 0f;
    private bool jumpPressed = false;
    private float framesOnGround = 0f;
    private float velocityRef; // Reference variable used for SmoothDamp
    private Animator animator;
    private bool facingRight = true;
    private Vector3 startPosition;
    private float scoreFloat;
    private bool isAlive = true;

    // Superpower
    private bool isSuper = false;
    private float powerDuration = 3f;
    private float powerTimer;
    private System.Random random;
    private bool powerAboveGround = false;
    private int powerRotationSpeed = 15;
    private Vector3 initialPosition;

    private void Start()
    {
        if (controller == null || gameProgresser == null)
        {
            throw new MissingComponentException("Missing vital components in player script!");
        }

        random = new System.Random();
        groundCheck = GameObject.Find("GroundCheck").transform;
        animator = GetComponent<Animator>();
        startPosition = gameProgresser.position;
        MovePlayerToGameProgresser();
    }

    private void MovePlayerToGameProgresser()
    {
        controller.transform.position = gameProgresser.position + Vector3.up * 2;
    }

    void Update() => CaptureUserInput();

    private void CaptureUserInput()
    {
        playerInput = Input.GetAxis("Horizontal");
        jumpPressed = Input.GetKey(KeyCode.Space);
    }

    private void FixedUpdate()
    {
        if (!isAlive) return;

        HandleJumpTimer();
        RotatePlayer();
        MovePlayer();
        PreventPlayerOutOfBounds();
        UpdateAnimatorValues();
        HandleSuperpower();
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreFloat = (gameProgresser.position - startPosition).magnitude;
        var score = scoreObject.GetComponent<Text>();
        score.text = $"Score: {(int)scoreFloat}";
    }

    private void HandleJumpTimer()
    {
        if (controller.isGrounded && framesOnGround < jumpResetTimer) framesOnGround++;
        else if (!controller.isGrounded) framesOnGround = 0f;
    }

    private float GetHorizontalMovement()
    {
        var desiredMovement = playerInput * MovementSpeed;
        var horizontalSpeed = Mathf.SmoothDamp(controller.velocity.x, desiredMovement, ref velocityRef, smoothing);
        return horizontalSpeed;
    }
    private float GetVerticalMovement()
    {
        // Handle gravity
        float verticalSpeed = controller.velocity.y;
        if (controller.isGrounded)
        {
            verticalSpeed = -0.1f;
        }
        else
        {
            if (verticalSpeed < 0 || !jumpPressed) verticalSpeed += gravity * gravityMultiplier * fallMultiplier;
            else verticalSpeed += gravity * gravityMultiplier;
        }

        // Handle jumping
        var canJump = controller.isGrounded && framesOnGround >= jumpResetTimer;
        if (jumpPressed && canJump)
        {
            verticalSpeed += jumpForce;
        }

        return verticalSpeed;
    }

    private void MovePlayer()
    {
        controller.Move(new Vector2(GetHorizontalMovement(), GetVerticalMovement()) * Time.fixedDeltaTime);
    }

    private void RotatePlayer()
    {
        if (!isSuper)
        {
            if (controller.velocity.x > 0 && !facingRight) Flip();
            else if (controller.velocity.x < 0 && facingRight) Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }

    private void PreventPlayerOutOfBounds()
    {
        var distanceFromCenter = (gameProgresser.position - controller.transform.position).magnitude;
        if (distanceFromCenter > playerResetDistance) MovePlayerToGameProgresser();
    }
    private void UpdateAnimatorValues()
    {
        animator.SetFloat("velX", controller.velocity.x);
        animator.SetBool("inAir", !controller.isGrounded);
    }

    private void EnableSuperpower()
    {
        isSuper = true;
        powerTimer = powerDuration;
        powerRotationSpeed = random.Next(5, 20);
        powerAboveGround = powerRotationSpeed >= 10;
        spriteRenderer.enabled = false;
        superSprite.SetActive(true);
    }

    private void DisableSuperpower()
    {
        isSuper = false;
        spriteRenderer.enabled = true;
        superSprite.SetActive(false);
    }

    private void HandleSuperpower()
    {
        powerTimer -= 1 * Time.fixedDeltaTime;
        if (isSuper && powerTimer > 0f)
        {
            superSprite.transform.Rotate(0, 0, powerRotationSpeed);
            if (powerAboveGround) superSprite.transform.position = this.transform.position + Vector3.up * 7 + new Vector3(0f, Mathf.Sin(Time.time * 2), 1);
            else superSprite.transform.position = this.transform.position + Vector3.down * 5;
        }
        else if (isSuper && powerTimer <=  0f)
        {
            DisableSuperpower();
        }
    }

    private void Die()
    {
        isAlive = false;
        Debug.Log("You died");
        SceneManager.LoadScene("End_Game");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Drops")
        {
            EnableSuperpower();
            Destroy(other.gameObject);
        }

        if (other.tag == "Enemy")
        {
            if (!isSuper) Die();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 11) Debug.Log("Enemy");
    }
}
