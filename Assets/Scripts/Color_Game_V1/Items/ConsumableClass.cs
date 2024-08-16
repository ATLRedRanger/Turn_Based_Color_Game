using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item
{
    

    public ConsumableType consumableType;

    public int refillAmount;

    public Hue tintColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Consumable(string itemName, ItemType itemType, Sprite itemSprite, string itemDescription, int itemAmount, ConsumableType consumableType, int refillAmount, Hue tintColor)
    {
        
        this.itemName = itemName;
        this.itemType = itemType;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        this.itemAmount = itemAmount;
        this.consumableType = consumableType;
        this.refillAmount = refillAmount;
        this.tintColor = tintColor;
    }

    public override void Use(Unit player)
    {
        switch (consumableType)
        {
            case ConsumableType.Health:
                player.currentHealth += refillAmount;
                if (player.currentHealth > player.maxHealth)
                {
                    player.currentHealth = player.maxHealth;
                }
                break;
            case ConsumableType.Stamina:
                player.currentStamina += refillAmount;
                if (player.currentStamina > player.maxStamina)
                {
                    player.currentStamina = player.maxStamina;
                }
                break;
            case ConsumableType.Tint:
                player.IsTinted(tintColor);
                Debug.Log($"PLAYER tintCOLOR: {player.tintColor}");
                break;
        }
       
        itemAmount -= 1;
        
        player.hadATurn = true;

       
    }

}
