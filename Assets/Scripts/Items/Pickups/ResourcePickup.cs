using UnityEngine;

public class ResourcePickup : InteractablePickup
{
    public override void Interact(PlayerInteractor interactor)
    {
        interactor.ResourceStorage.AddResource(itemData, 1);

        RemovePickup();
    }

    
}
