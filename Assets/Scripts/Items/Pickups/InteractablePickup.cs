using UnityEngine;

public abstract class InteractablePickup : MonoBehaviour, IInteractable
{
    [SerializeField] protected ItemData itemData;

    public abstract void Interact(PlayerInteractor interactor);

    protected void RemovePickup()
    {
        Destroy(gameObject);
    }
}
