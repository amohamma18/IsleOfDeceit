using UnityEngine;

public enum ItemType
{
    Resource,
    Tool,
    Story
}

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item Data")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string itemName;

    [SerializeField] private ItemType itemType;

    public string ItemName => itemName;
    public ItemType ItemType => itemType;
}
