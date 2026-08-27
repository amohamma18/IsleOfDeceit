using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private PlayerInputActions inputActions;

    public Vector2 MoveInput { get; private set; }

    public Vector2 LookInput { get; private set; }

    public event Action InteractPressed;

    public event Action JumpPressed;

    public event Action InventoryToggled;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();

        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;

        inputActions.Player.Look.performed += OnLook;
        inputActions.Player.Look.canceled += OnLook;

        inputActions.Player.Interact.performed += OnInteract;

        inputActions.Player.Jump.performed += OnJump;

        inputActions.Global.Inventory.performed += OnInventoryToggle;
    }



    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;

        inputActions.Player.Look.performed -= OnLook;
        inputActions.Player.Look.canceled -= OnLook;

        inputActions.Player.Interact.performed -= OnInteract;

        inputActions.Player.Jump.performed -= OnJump;

        inputActions.Global.Inventory.performed -= OnInventoryToggle;

        inputActions.Disable();
    }

    public void EnablePlayerInput()
    {
        inputActions.Player.Enable();
    }

    public void DisablePlayerInput()
    {
        inputActions.Player.Disable();
    }

    public void EnableUI()
    {
        inputActions.UI.Enable();
    }

    public void DisableUI()
    {
        inputActions.UI.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }
    
    private void OnInteract(InputAction.CallbackContext context)
    {
        InteractPressed?.Invoke();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        JumpPressed?.Invoke();
    }

    private void OnInventoryToggle(InputAction.CallbackContext context)
    {
        InventoryToggled?.Invoke();
    }

}
