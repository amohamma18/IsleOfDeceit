using UnityEngine;

public class PlayerContext : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    public Inventory Inventory => Inventory;
}
