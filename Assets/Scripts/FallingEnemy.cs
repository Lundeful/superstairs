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
        // Vertical movement
        float verticalSpeed = controller.velocity.y;
        if (controller.isGrounded)
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
    }

    private void jump()
    {
        verticalSpeed += jumpForce;
    }
}
