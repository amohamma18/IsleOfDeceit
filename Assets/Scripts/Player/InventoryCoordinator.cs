using UnityEngine;

public class InventoryCoordinator : MonoBehaviour
{
    private ResourceStorage resourceStorage;
    private ToolStorage toolStorage;
    // [SerializeField] private StoryItemStorage storyItemStorage; once we have storyItemStorage

    private void Awake()
    {
        resourceStorage = new ResourceStorage();
        toolStorage = new ToolStorage();
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
                //storyItemStorage.AddItem(itemData, amount);
                break;
            default:
                Debug.LogWarning($"Unhandled item type: {itemData.ItemType}");
                break;
        }
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
                //return storyItemStorage.GetItemAmount(itemData);
            default:
                Debug.LogWarning($"Unhandled item type: {itemData.ItemType}");
                return 0; // Default return if item type is unhandled
        }
    }

    public bool TryRemoveItem(ItemData itemData, int amount)
    {
        if (!IsItemDataValid(itemData) || !IsAmountValid(amount)) { return false; }

        switch (itemData.ItemType)
        {
            case ItemType.Resource:
                return resourceStorage.TryRemoveItem(itemData, amount);
            case ItemType.Tool:
                return toolStorage.TryRemoveItem(itemData, amount);
            case ItemType.Story:
                //return storyItemStorage.TryRemoveItem(itemData, amount);
            default:
                Debug.LogWarning($"Unhandled item type: {itemData.ItemType}");
                return false; // Default return if item type is unhandled
        }
    }
}
