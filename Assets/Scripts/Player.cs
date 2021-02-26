using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private readonly float gravity = -9.81f;

    public CharacterController controller;

    [Range(1, 20)]
    public float MovementSpeed = 5f;

    [Header("Jumping")]
    [Range(0, 200)]
    public float jumpForce = 30f;
    [Range(0, 20)]
    public float jumpResetTimer = 5f;
    [Range(1, 5f)]
    public float fallMultiplier = 2.5f;

    [Header("Physics")]
    [Range(0.1f, 2)]
    public float gravityMultiplier = 1f;

    float inputX;
    bool jumpPressed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CaptureUserInput();
    }

    private void CaptureUserInput()
    {
        inputX = Input.GetAxis("Horizontal");
        jumpPressed = Input.GetKey(KeyCode.Space);
    }

    private void FixedUpdate()
    {
        // Vertical movement
        float verticalSpeed = controller.velocity.y;
        if (controller.isGrounded)
        {
            // Reset speed on landing
            verticalSpeed = -0.1f;
        }
        else
        {
            if (verticalSpeed < 0 || !jumpPressed) verticalSpeed += gravity * gravityMultiplier * fallMultiplier;
            else verticalSpeed += gravity * gravityMultiplier;
        }

        // Add jumpforce
        if (controller.isGrounded && jumpPressed)
        {
            verticalSpeed += jumpForce;
        }


        // Horizontal movement
        controller.Move(new Vector2(inputX * MovementSpeed, verticalSpeed) * Time.fixedDeltaTime);
    }
}
