using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{

    public int weaponLevelRequirement;

    public int weaponDamage;

    public float weaponHealthModifier;

    public float weaponStaminaModifier;

    public WeaponType weaponType;

    public Weapon()
    {
    }

    public Weapon(string itemName, ItemType itemType, string itemDescription, int weaponLevelRequirement, int weaponDamage, float weaponHealthModifier, float weaponStaminaModifier, WeaponType weaponType)
    {
        this.itemName = itemName;
        this.itemType = itemType;
        this.itemDescription = itemDescription;
        this.weaponLevelRequirement = weaponLevelRequirement;
        this.weaponDamage = weaponDamage;
        this.weaponHealthModifier = weaponHealthModifier;
        this.weaponStaminaModifier = weaponStaminaModifier;
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

}
