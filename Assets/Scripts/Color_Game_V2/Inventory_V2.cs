using System.Collections.Generic;
using UnityEngine;

public class Inventory_V2 : MonoBehaviour
{

    private List<Item> inventory = new List<Item>();
    const int MAX_CAPACITY = 8;
    private int money = 0;

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

    public int GetMoney()
    {
        return money;
    }

    public void GainMoney(int amount)
    {
        money += amount;
        Debug.Log($"You have gained {amount} money. \n You now have {money} money.");
    }

    public void LoseMoney(int amount)
    {
        money -= amount;
        if(money < 0)
        {
            money = 0;
        }
        Debug.Log($"You have lost {amount} money. \n You now have {money} money.");
    }

}
