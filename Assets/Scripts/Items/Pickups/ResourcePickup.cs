using UnityEngine;

public class ResourcePickup : InteractablePickup
{
    public override void Interact(PlayerContext playerContext)
    {
        playerContext.ResourceStorage.AddResource(itemData, 1);

        RemovePickup();
    }

    
}
