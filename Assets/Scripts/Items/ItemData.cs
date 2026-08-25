using UnityEngine;

public enum ItemType
{
    Resource,
    Tool,
    Story
}

public enum ItemAction
{
    Use,
    Equip,
    Examine
}

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item Data")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string itemName;

    [SerializeField] private ItemType itemType;

    [SerializeField] private ItemAction action;

    [SerializeField] private ToolData toolData;

    public string ItemName => itemName;
    public ItemType ItemType => itemType;

    public ItemAction Action => action;

    public ToolData ToolData => toolData;
}
