using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Axe : Weapon
{

    public float healthPercent = .8f;
    public float staminaPercent = .2f;
    private int weaponDamage;
    public Weapon_Axe(string itemName = "", string itemDescription = "", string itemID = "", int itemAmount = 0, WeaponType weaponType = WeaponType.Neutral, int baseDamage = 0)
    {
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemID = itemID;
        this.weaponType = weaponType;
        this.baseDamage = baseDamage;
        
    }

    public override void Use(Unit_V2 unit)
    {
        base.Use(unit);
    }

    /*
    public override int GetWeaponDamage(Unit_V2 attacker, Unit_V2 defender)
    {
        return base.GetWeaponDamage(attacker, defender);
    }*/

}

