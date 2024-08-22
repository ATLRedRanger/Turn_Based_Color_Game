using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Hammer : Weapon
{
    public Weapon_Hammer(string itemName, string itemDescription, string itemID, int itemAmount, WeaponType weaponType, int weaponDamage)
    {
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemID = itemID;
        this.weaponType = weaponType;
        this.weaponDamage = weaponDamage;
    }
}
