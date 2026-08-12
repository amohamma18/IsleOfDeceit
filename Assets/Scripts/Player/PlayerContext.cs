using UnityEngine;

public class PlayerContext : MonoBehaviour
{
    [SerializeField] private InventoryCoordinator inventoryCoordinator;

    public InventoryCoordinator InventoryCoordinator => inventoryCoordinator;
}
