using System.Collections.Generic;
using UnityEngine;

public class StoryItemStorage
{
    private Dictionary<ItemData, int> items = new Dictionary<ItemData, int>();

    public void AddItem(ItemData itemData, int amount)
    {
        if (items.ContainsKey(itemData))
        {
            items[itemData] += amount;
        }
        else
        {
            items[itemData] = amount;
        }

        Debug.Log($"Added {amount} of '{itemData.ItemName}' to inventory. Total: {items[itemData]}");
    }

    public int GetItemAmount(ItemData itemData)
    {
        if (items.ContainsKey(itemData))
        {
            return items[itemData];
        }
        return 0;
    }

    public bool TryRemoveItem(ItemData itemData, int amount)
    {
        if (GetItemAmount(itemData) < amount) { return false; }

        items[itemData] -= amount;

        if (items[itemData] == 0)
        {
            items.Remove(itemData);
        }

        Debug.Log($"Removed {amount} of '{itemData.ItemName}' from inventory. Remaining: {GetItemAmount(itemData)}");
        return true;
    }
}
