using UnityEngine;

public class Player : MonoBehaviour
{
    private readonly float gravity = -9.81f;

    [Header("Components")]
    public CharacterController controller;

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

    // Player state
    private float playerInput = 0f;
    private bool jumpPressed = false;
    private float framesOnGround = 0f;
    private float velocityRef; // Reference variable used for SmoothDamp

    void Update() => CaptureUserInput();

    private void CaptureUserInput()
    {
        playerInput = Input.GetAxis("Horizontal");
        jumpPressed = Input.GetKey(KeyCode.Space);
    }

    private void FixedUpdate()
    {
        HandleJumpTimer();
        MovePlayer();
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
}
