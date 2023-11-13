using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item
{
    

    public ConsumableType consumableType;

    public int refillAmount;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Consumable(string itemName, string itemDescription, int itemAmount, ConsumableType consumableType, int refillAmount)
    {
        
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemAmount = itemAmount;
        this.consumableType = consumableType;
        this.refillAmount = refillAmount;
    }

    public override void Use(Unit player)
    {
        
        if(consumableType == ConsumableType.Health)
        {
            player.currentHealth += refillAmount;  
            if(player.currentHealth > player.maxHealth)
            {
                player.currentHealth = player.maxHealth;
            }
        }
        if(consumableType != ConsumableType.Stamina)
        {
            player.currentStamina += refillAmount;
            if (player.currentStamina > player.maxStamina)
            {
                player.currentStamina = player.maxStamina;
            }
        }

        itemAmount -= 1;
        
        player.hadATurn = true;

       
    }

}
