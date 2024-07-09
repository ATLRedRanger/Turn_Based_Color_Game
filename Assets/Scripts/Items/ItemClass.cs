using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item 
{

    public string itemName;

    public ItemType itemType;

    public string itemDescription;

    public int itemAmount;

    public string itemID;

    public Sprite itemSprite;
    
    public Unit_Spawner unitSpawnerScript;

    public Unit player;

    void Start()
    {
        
    }

    /*IEnumerator LoadingScripts()
    {
        //yield return new WaitForSeconds(.5f);
        //unitSpawnerScript = FindObjectOfType<Unit_Spawner>();
        //player = unitSpawnerScript.player;
    }*/

    public Item()
    {
        this.itemAmount = 1;
    }

    public Item(string itemName, ItemType itemType, Sprite itemSprite, string itemDescription, int itemAmount, string itemID)
    {
        this.itemName = itemName;
        this.itemType = itemType;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        this.itemAmount = itemAmount;
        this.itemID = itemID;

        
    }
    
    public virtual void Use(Unit player)
    {
        //Debug.Log("Overide This");
        Debug.Log($"ITEM ID: {itemID}");
    }
    
    public virtual void SpecialProperty(Unit unit) 
    {
        Debug.Log("ITEM'S SPECIAL PROPERTY!");
    }
  
}
