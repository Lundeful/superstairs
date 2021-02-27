using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingEnemy: MonoBehaviour
{

    private readonly float gravity = -9.81f;

    public CharacterController controller;

    [Range(-20, 20)]
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

    public bool shouldJump;
    private float verticalSpeed;
    private float framesOnGround = 0f;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (controller.isGrounded && framesOnGround < jumpResetTimer) framesOnGround++;
        else if (!controller.isGrounded) framesOnGround = 0f;

        var isGrounded = Physics.Raycast(transform.position, Vector3.down, .2f);
        // Vertical movement
        verticalSpeed = controller.velocity.y;
        if (controller.isGrounded && framesOnGround >= jumpResetTimer)
        {
            // Reset speed on landing
            verticalSpeed = -0.1f;
            jump();
        }
        else
        {
            //Falls to ground
            if (verticalSpeed < 0) verticalSpeed += gravity * gravityMultiplier * fallMultiplier;
            else verticalSpeed += gravity * gravityMultiplier;
        }

        // Horizontal movement
        controller.Move(new Vector2(MovementSpeed, verticalSpeed) * Time.fixedDeltaTime);

        // Rotation

        var rotSpeed = controller.isGrounded ? 25 : 10;
        transform.Rotate(0, 0, rotSpeed);
    }

    private void jump()
    {
        verticalSpeed += jumpForce;
    }
}
