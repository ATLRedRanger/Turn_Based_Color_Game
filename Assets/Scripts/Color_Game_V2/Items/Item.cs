using UnityEngine;

public class Item
{
    public string itemName;
    public string itemDescription;
    public string itemID;
    public int itemAmount;
    public ItemTier itemTier;
    

    public Item()
    {

    }

    public Item(string itemName = "", string itemDescription = "", string itemID = "", int itemAmount = 0, ItemTier itemTier = ItemTier.Common)
    {
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemID = itemID;
        this.itemAmount = itemAmount;
        this.itemTier = itemTier;
    }

    public virtual void Use(Unit_V2 unit)
    {
        Debug.Log($"Using a {itemName}");
    }
}
