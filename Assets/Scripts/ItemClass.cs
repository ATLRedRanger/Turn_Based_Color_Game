using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item 
{

    public string itemName;

    public string itemDescription;

    public int itemAmount;

    public Unit_Spawner unitSpawnerScript;

    public Unit player;
    void Start()
    {
        //StartCoroutine(LoadingScripts());
    }

    /*IEnumerator LoadingScripts()
    {
        //yield return new WaitForSeconds(.5f);
        //unitSpawnerScript = FindObjectOfType<Unit_Spawner>();
        //player = unitSpawnerScript.player;
    }*/

    public Item()
    {
        //This constructor is here for children classes.
    }

    public Item(string itemName, string itemDescription, int itemAmount)
    {
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemAmount = itemAmount;

        
    }
    
    public virtual void Use()
    {
        //Debug.Log("Overide This");
    }
    

}
