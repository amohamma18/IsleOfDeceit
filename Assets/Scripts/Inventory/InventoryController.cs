using System;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private PlayerInputController inputController;
    [SerializeField] private CursorController cursorController;

    [SerializeField] private InventoryUI inventoryUI;

    private void Awake()
    {
        inputController.InventoryToggled += ToggleInventoryUI;
    }

    private void OnDestroy()
    {
        inputController.InventoryToggled -= ToggleInventoryUI;
    }

    private void ToggleInventoryUI()
    {
        if (!inventoryUI.IsInventoryOpen)
        {
            inventoryUI.OpenInventory();
            inputController.DisablePlayerInput();
            inputController.EnableUI();
            cursorController.UnlockCursor();
        }
        else
        {
            inventoryUI.CloseInventory();
            inputController.EnablePlayerInput();
            inputController.DisableUI();
            cursorController.LockCursor();
        }
    }


}
