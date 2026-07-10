using UnityEngine;

public abstract class InteractablePickup : MonoBehaviour, IInteractable
{
    [SerializeField] protected ItemData itemData;

    public virtual string InteractionText => "Interact";

    public abstract void Interact(PlayerContext playerContext);

    protected void RemovePickup()
    {
        Destroy(gameObject);
    }
}
