using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private PlayerInputController playerInput;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.8f;
    private float verticalVelocity;

    private void Update()
    {
        Move();
        ApplyGravity();
    }

    private void OnEnable()
    {
        playerInput.JumpPressed += Jump;
    }

    private void OnDisable()
    {
        playerInput.JumpPressed -= Jump;
    }

    private void ApplyGravity()
    {
        if (characterController.isGrounded && verticalVelocity < 0)
        {
            // The vertical velocity is set to a small negative value to ensure the player stays grounded and doesn't float above the ground for a brief moment.
            verticalVelocity = -2f;
        }

        // Every update this applies gravity to the player's vertical velocity so their velocity upwards is reduced over time and they fall back down to the ground.
        verticalVelocity += gravity * Time.deltaTime;

        Vector3 gravityMovement = new Vector3(0, verticalVelocity, 0);
        characterController.Move(gravityMovement * Time.deltaTime);
    }

    private void Move()
    {
        Vector2 input = playerInput.MoveInput;

        Vector3 movement = transform.right * input.x + transform.forward * input.y;

        characterController.Move(movement * moveSpeed * Time.deltaTime);
    }


    private void Jump()
    {
        if (characterController.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
