using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_V2 : MonoBehaviour
{

    private List<Item> inventory = new List<Item>();
    const int MAX_CAPACITY = 8;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddToInventory(Item item)
    {
       
        if (inventory.Contains(item))
        {
            
            int index = inventory.IndexOf(item);
            inventory[index].itemAmount += 1;

            Debug.Log($"You have {item.itemAmount} of {item.itemName}.");
        }
        else if(inventory.Count < MAX_CAPACITY)
        {
            inventory.Add(item);
            Debug.Log($"{item.itemName} has been added to your inventory. Inventory Count: {inventory.Count}");
        }
        else
        {
            Debug.Log("Inventory is full.");
        }
 
    }

    public void RemoveFromInventory(Item item)
    {
        
        if (inventory.Contains(item))
        {
            int index = inventory.IndexOf(item);
            inventory[index].itemAmount -= 1;
            Debug.Log($"You have {item.itemAmount} of {item.itemName}.");
            if (inventory[index].itemAmount < 1)
            {
                inventory.Remove(item);
                Debug.Log($"{item.itemName} has been removed from your inventory. Inventory Count: {inventory.Count}");
            }
        }

    }

    public List<Item> GetInventory()
    {
        return inventory;
    }
}
