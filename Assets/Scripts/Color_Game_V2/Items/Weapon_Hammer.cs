using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Hammer : Weapon
{

    public float healthPercent = .6f;
    public float staminaPercent = .4f;

    public Weapon_Hammer(string itemName = "", string itemDescription = "", string itemID = "", int itemAmount = 0, WeaponType weaponType = WeaponType.Neutral, int weaponDamage = 0)
    {
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemID = itemID;
        this.weaponType = weaponType;
        this.weaponDamage = weaponDamage;
    }

    public override int GetWeaponDamage(Unit_V2 attacker, Unit_V2 defender)
    {
        if (defender.GetCurrentStamina() < attacker.GetCurrentStamina())
        {
            return weaponDamage * 2;
        }

        return weaponDamage;
    }
}
