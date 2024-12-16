using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string itemName;
    public string itemDescription;
    public string itemID;
    public int itemAmount;
    

    public Item()
    {

    }

    public Item(string itemName = "", string itemDescription = "", string itemID = "", int itemAmount = 0)
    {
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemID = itemID;
        this.itemAmount = itemAmount;
    }

    public virtual void Use(Unit_V2 unit)
    {
        Debug.Log($"Using a {itemName}");
    }
}
