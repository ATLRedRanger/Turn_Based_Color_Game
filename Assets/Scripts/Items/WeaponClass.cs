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

    public Weapon(string itemName, string itemDescription, int weaponLevelRequirement, int weaponDamage, float weaponHealthModifier, float weaponStaminaModifier, WeaponType weaponType)
    {
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.weaponLevelRequirement = weaponLevelRequirement;
        this.weaponDamage = weaponDamage;
        this.weaponHealthModifier = weaponHealthModifier;
        this.weaponStaminaModifier = weaponStaminaModifier;
        this.weaponType = weaponType;
    }
    

    public override void Use(Unit player)
    {
        Debug.Log("This is the weapon overriding");
    }

    public override void SpecialProperty()
    {
        base.SpecialProperty();
    }

}
