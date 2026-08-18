using UnityEngine;

public class EquipmentSystem : MonoBehaviour
{
    [SerializeField] private Transform hand;

    private ItemData equippedTool;

    private GameObject currentToolInstance;

    public bool TryEquipTool(ItemData itemData)
    {
        if (hand == null) { 
            Debug.LogError("Hand transform is not assigned in the EquipmentSystem.");
            return false; }
        if (itemData == null) { 
            Debug.LogError("Item data is null in the EquipmentSystem.");
            return false; }
        if (itemData.ItemType != ItemType.Tool) { 
            Debug.LogError($"Item '{itemData.ItemName}' is not a tool in the EquipmentSystem.");
            return false; }
        if (itemData.ToolData == null) { 
            Debug.LogError($"Tool '{itemData.ItemName}' has no tool data assigned in the EquipmentSystem.");
            return false; }
        if (itemData.ToolData.ToolPrefab == null) { 
            Debug.LogError($"Tool '{itemData.ItemName}' has no tool prefab assigned in the EquipmentSystem.");
            return false; }

        if (equippedTool != null || currentToolInstance != null) { UnequipTool(); }

        equippedTool = itemData;

        currentToolInstance = Instantiate(itemData.ToolData.ToolPrefab, hand);
        Debug.Log($"Equipped tool: {itemData.ItemName}");

        return true;
    }

    private void UnequipTool()
    {
        Destroy(currentToolInstance);

        currentToolInstance = null;
        equippedTool = null;

        Debug.Log("Unequipped previous tool");
    }
}
