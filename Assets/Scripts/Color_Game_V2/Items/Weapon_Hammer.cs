using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Hammer : Weapon
{
    /*
    public float healthPercent = .6f;
    public float staminaPercent = .4f;
    private int weaponDamage = 0;
    */
    public Weapon_Hammer(string itemName = "", string itemDescription = "", string itemID = "", int itemAmount = 0, WeaponType weaponType = WeaponType.Hammer, int baseDamage = 0)
    {
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemID = itemID;
        this.weaponType = weaponType;
        this.baseDamage = baseDamage;
    }

}
