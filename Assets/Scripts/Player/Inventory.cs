using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
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

        Debug.Log($"Added {amount} of '{itemData.name}' to storage. Total: {items[itemData]}"); 
    }

    public int GetItemAmount(ItemData itemData)
    {
        if (items.ContainsKey(itemData))
        {
            return items[itemData];
        }
        return 0;
    }
}
