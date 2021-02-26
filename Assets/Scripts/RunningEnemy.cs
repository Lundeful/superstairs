using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningEnemy : MonoBehaviour
{
    public CharacterController controller;

    private readonly float gravity = -9.81f;
    private float verticalSpeed;
    public bool shouldJump;

    [Range(-20, 20)]
    public float MovementSpeed = 5f;

    [Range(0, 20)]
    public float jumpResetTimer = 5f;
    [Range(1, 5f)]
    public float fallMultiplier = 2.5f;

    [Header("Physics")]
    [Range(0.1f, 2)]
    public float gravityMultiplier = 1f;


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
            //Falls to ground
            if (verticalSpeed < 0) verticalSpeed += gravity * gravityMultiplier * fallMultiplier;
            else verticalSpeed += gravity * gravityMultiplier;
        }

        // Horizontal movement
        controller.Move(new Vector2(MovementSpeed, verticalSpeed) * Time.fixedDeltaTime);
    }
}
