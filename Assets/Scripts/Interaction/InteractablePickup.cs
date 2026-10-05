using UnityEngine;

public class InteractablePickup : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData itemData;

    [SerializeField] private int amount = 1;

    public string InteractionText => "Interact";

    public void Interact(PlayerContext playerContext)
    {
        playerContext.InventoryCoordinator.AddItem(itemData, amount);

        RemovePickup();
    }

    private void RemovePickup()
    {
        Destroy(gameObject);
    }
}
