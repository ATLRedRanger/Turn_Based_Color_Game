using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{

    public int weaponLevelRequirement;

    public int weaponDamage;

    public WeaponType weaponType;

    public Weapon(string itemName, string itemDescription, int weaponLevelRequirement, int weaponDamage, WeaponType weaponType)
    {
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.weaponLevelRequirement = weaponLevelRequirement;
        this.weaponDamage = weaponDamage;
        this.weaponType = weaponType;
    }
    

    public override void Use()
    {
        Debug.Log("This is the weapon overriding");
    }

    
}
