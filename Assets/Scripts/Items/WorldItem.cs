using UnityEngine;

public class WorldItem : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData itemData;

    public void Interact(PlayerInteractor interactor)
    {
        interactor.ResourceStorage.AddResource(itemData, 1);

        Destroy(gameObject);
    }
}
