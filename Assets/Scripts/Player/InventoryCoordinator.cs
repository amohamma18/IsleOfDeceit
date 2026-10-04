using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCoordinator : MonoBehaviour
{
    private ResourceStorage resourceStorage;
    private ToolStorage toolStorage;
    private StoryItemStorage storyItemStorage;

    public event Action OnInventoryChanged;

    [SerializeField] private EquipmentSystem equipmentSystem;

    private void Awake()
    {
        resourceStorage = new ResourceStorage();
        toolStorage = new ToolStorage();
        storyItemStorage = new StoryItemStorage();
    }
    private bool IsItemDataValid(ItemData itemData)
    {
        if (itemData == null)
        {
            Debug.LogError("ItemData is null");
            return false;
        }
        return true;
    }
    private bool IsAmountValid(int amount)
    {
        if (amount <= 0)
        {
            Debug.LogError("Amount must be greater than 0");
            return false;
        }
        return true;
    }

    public void AddItem(ItemData itemData, int amount)
    {
        if (!IsItemDataValid(itemData) || !IsAmountValid(amount)) { return; }

        switch (itemData.ItemType)
        {
            case ItemType.Resource:
                resourceStorage.AddItem(itemData, amount); 
                break;
            case ItemType.Tool:
                toolStorage.AddItem(itemData, amount);
                break;
            case ItemType.Story:
                storyItemStorage.AddItem(itemData, amount);
                break;
            default:
                Debug.LogWarning($"Unhandled item type: {itemData.ItemType}");
                break;
        }
        OnInventoryChanged?.Invoke();
    }

    public bool IsItemEquipped(ItemData itemData)
    {
        if (!IsItemDataValid(itemData)) { return false; }
        return equipmentSystem.IsEquipped(itemData);
    }

    public int GetItemAmount(ItemData itemData)
    {
        if (!IsItemDataValid(itemData)) { return 0; }

        switch (itemData.ItemType)
        {
            case ItemType.Resource:
                return resourceStorage.GetItemAmount(itemData);
            case ItemType.Tool:
                return toolStorage.GetItemAmount(itemData);
            case ItemType.Story:
                return storyItemStorage.GetItemAmount(itemData);
            default:
                Debug.LogWarning($"Unhandled item type: {itemData.ItemType}");
                return 0; // Default return if item type is unhandled
        }
    }

    public bool TryRemoveItem(ItemData itemData, int amount)
    {
        if (!IsItemDataValid(itemData) || !IsAmountValid(amount)) { return false; }
        bool removed = false;
        switch (itemData.ItemType)
        {
            case ItemType.Resource:
                removed = resourceStorage.TryRemoveItem(itemData, amount);
                break;
            case ItemType.Tool:
                removed = toolStorage.TryRemoveItem(itemData, amount);
                break;
            case ItemType.Story:
                removed = storyItemStorage.TryRemoveItem(itemData, amount);
                break;
            default:
                Debug.LogWarning($"Unhandled item type: {itemData.ItemType}");
                return false; // Default return if item type is unhandled
        }
        if (removed) { OnInventoryChanged?.Invoke(); } 
        return removed;
    }

    public bool PerformItemAction(ItemData itemData)
    {
        if (!IsItemDataValid(itemData)) { return false; }
        switch (itemData.Action)
        {
            case ItemAction.Equip:
                return TryToggleEquipItem(itemData);

            case ItemAction.Use:
                // TODO: Implement use logic here
                return false;
            case ItemAction.Examine:
                // TODO: Implement examine logic here
                return false;
            default:
                Debug.LogWarning($"Unhandled item action: {itemData.Action}");
                return false;
        }
    }

    public bool TryToggleEquipItem(ItemData itemData)
    {
        if (!IsItemDataValid(itemData)) { return false; }

        if (GetItemAmount(itemData) <= 0)
        {
            Debug.LogError($"Item '{itemData.ItemName}' is not available in the inventory to equip.");
            return false;
        }

        return equipmentSystem.TryToggleTool(itemData);
    }

    public IReadOnlyDictionary<ItemData, int> GetResourceItems() => resourceStorage.Items;
    public IReadOnlyList<ItemData> GetToolItems() => toolStorage.Items;
    public IReadOnlyDictionary<ItemData, int> GetStoryItems() => storyItemStorage.Items;
}
