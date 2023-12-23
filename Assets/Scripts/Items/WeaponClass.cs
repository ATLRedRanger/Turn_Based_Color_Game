using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class Weapon : Item
{

    public int weaponLevelRequirement;

    public int weaponDamage;

    public float weaponHealthModifier;

    public float weaponStaminaModifier;

    public float weaponCritModifier;

    public WeaponType weaponType;

    public Weapon()
    {
    }

    public Weapon(string itemName, ItemType itemType, string itemDescription, int weaponLevelRequirement, int weaponDamage, float weaponHealthModifier, float weaponStaminaModifier, float weaponCritModifier, WeaponType weaponType)
    {
        this.itemName = itemName;
        this.itemType = itemType;
        this.itemDescription = itemDescription;
        this.weaponLevelRequirement = weaponLevelRequirement;
        this.weaponDamage = weaponDamage;
        this.weaponHealthModifier = weaponHealthModifier;
        this.weaponStaminaModifier = weaponStaminaModifier;
        this.weaponCritModifier = weaponCritModifier;
        this.weaponType = weaponType;
        //this.itemAmount = 1;
    }
    

    public override void Use(Unit user)
    {
        Debug.Log("This is the weapon overriding");
    }

    public override void SpecialProperty()
    {
        base.SpecialProperty();
    }

    public int Hammer(Unit attacker)
    {
        int modifiedDamage = 0;
        //Hammer gains bonus damage based on attacker stamina levels
        
        if (attacker.currentStamina <= (attacker.OgStamina * 1 / 4))
        {
            modifiedDamage = weaponDamage;
        }
        if (attacker.currentStamina > (attacker.OgStamina * 1 / 4) && attacker.currentStamina <= (attacker.OgStamina * (1 / 2)))
        {
            modifiedDamage = (int)(weaponDamage * 1.2);
        }
        if ((attacker.currentStamina > (1 / 2) && attacker.currentStamina <= (attacker.OgStamina * 3 / 4)))
        {
            modifiedDamage = (int)(weaponDamage * 1.5);
        }
        if (attacker.currentStamina > (attacker.OgStamina * 3 / 4))
        {
            modifiedDamage = (int)(weaponDamage * 2);
        }
        
        return modifiedDamage;
    }
}
//Bows have higher crit modifiers than other weapons, but their attacks have lower damage and stamina reqs. 