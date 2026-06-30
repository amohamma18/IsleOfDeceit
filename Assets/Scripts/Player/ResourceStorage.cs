using System.Collections.Generic;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    private Dictionary<ItemData, int> resources = new Dictionary<ItemData, int>();

    public void AddResource(ItemData item, int amount)
    {
        if (item.itemType != ItemType.Resource)
        {
            Debug.LogWarning($"Item '{item.name}' is not a resource.");
            return;
        }
        if (resources.ContainsKey(item))
        {
            resources[item] += amount;
        }
        else
        {
            resources[item] = amount;
        }

        Debug.Log($"Added {amount} of '{item.name}' to storage. Total: {resources[item]}"); 
    }

    public int GetResourceAmount(ItemData item)
    {
        if (resources.ContainsKey(item))
        {
            return resources[item];
        }
        return 0;
    }
}
