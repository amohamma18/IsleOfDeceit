using System;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private PlayerInputController playerInput;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private float sensitivity = 100f;

    private float pitch;

    private void Update()
    {
        Look();
    }

    private void Look()
    {
        Vector2 input = playerInput.LookInput;

        float mouseX = input.x * sensitivity * Time.deltaTime;
        float mouseY = input.y * sensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        pitch -= mouseY;

        pitch = Mathf.Clamp(pitch, -90f, 90f);

        cameraPivot.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
}
