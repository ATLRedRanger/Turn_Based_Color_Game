using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item_Consumable : Item
{
    public ConsumableType consumableType;
    public int amountToIncrease;

    public Item_Consumable(ConsumableType consumableType, int amountToIncrease)
    {
        this.consumableType = consumableType;
        this.amountToIncrease = amountToIncrease;
    }

    public override void Use(Unit_V2 unit)
    {
        switch(consumableType)
        {
            case ConsumableType.Health:
                unit.GainHealth(amountToIncrease);
                break;
            case ConsumableType.Stamina:
                unit.GainStamina(amountToIncrease);
                break;
        }
    }
}
