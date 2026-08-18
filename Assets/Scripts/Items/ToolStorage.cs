using System.Collections.Generic;
using UnityEngine; // Remember to remove this once the debug logs are no longer needed

public class ToolStorage
{
    private List<ItemData> items = new List<ItemData>();

    public IReadOnlyList<ItemData> Items => items;

    public void AddItem(ItemData itemData, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            items.Add(itemData);
        }

        Debug.Log($"Added {amount} of '{itemData.ItemName}' to inventory");
    }

    public int GetItemAmount(ItemData itemData)
    {
        int amount = 0;

        foreach (ItemData item in items) {  
            if (item == itemData) { amount++; }
        }

        return amount;
    }

    public bool TryRemoveItem(ItemData itemData, int amount)
    {
        if (GetItemAmount(itemData) < amount) { return false; }

        for (int i = 0; i < amount; i++)
        {
            items.Remove(itemData);
        }

        Debug.Log($"Removed {amount} of '{itemData.ItemName}' from inventory. Remaining: {GetItemAmount(itemData)}");
        return true;
    }
}
