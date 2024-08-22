using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Axe : Weapon
{


    public Weapon_Axe(string itemName, string itemDescription, string itemID, int itemAmount, WeaponType weaponType, int weaponDamage)
    {
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemID = itemID;
        this.weaponType = weaponType;
        this.weaponDamage = weaponDamage;
        
    }

    public override void Use(Unit_V2 unit)
    {
        base.Use(unit);
    }
}
