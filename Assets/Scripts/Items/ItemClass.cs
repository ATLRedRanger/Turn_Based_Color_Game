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

    public Item(string itemName, ItemType itemType, string itemDescription, int itemAmount)
    {
        this.itemName = itemName;
        this.itemType = itemType;
        this.itemDescription = itemDescription;
        this.itemAmount = itemAmount;

        
    }
    
    public virtual void Use(Unit player)
    {
        //Debug.Log("Overide This");
    }
    
    public virtual void SpecialProperty() 
    {
        Debug.Log("ITEM'S SPECIAL PROPERTY!");
    }
  
}
