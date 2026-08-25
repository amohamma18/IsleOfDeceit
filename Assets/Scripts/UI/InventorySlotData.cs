public class InventorySlotData
{
    private ItemData itemData;
    private int amount; 

    public ItemData ItemData => itemData;

    public int Amount => amount;

    public InventorySlotData(ItemData itemData, int amount)
    {
        this.itemData = itemData;
        this.amount = amount;
    }
}
