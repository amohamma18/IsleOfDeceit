using UnityEngine;

public class ResourcePickup : InteractablePickup
{
    public override void Interact(PlayerContext playerContext)
    {
        playerContext.Inventory.AddItem(itemData, 1);

        RemovePickup();
    }

    
}
