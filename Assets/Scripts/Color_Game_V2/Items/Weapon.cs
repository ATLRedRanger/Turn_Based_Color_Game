using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public WeaponType weaponType;
    public int weaponDamage;
    public float critChance;

    public Weapon()
    {

    }
    public Weapon(string itemName = "", string itemDescription = "", string itemID = "", int itemAmount = 0, WeaponType weaponType = WeaponType.Neutral, int weaponDamage = 0)
    {
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemID = itemID;
        this.itemAmount = itemAmount;
        this.weaponType = weaponType;
        this.weaponDamage = weaponDamage;
    }

    public override void Use(Unit_V2 unit)
    {
        base.Use(unit);
    }

    public virtual int GetWeaponDamage(Unit_V2 attacker, Unit_V2 defender)
    {
        return weaponDamage;
    }
}

/* What kind of weapons do I want?
 * Axe - Deals more health damage
 * Bow - Multi-hit
 * Hammer - Deals more stamina damage
 * Sword - Utilizes stances
 * Staff - Boosts the damage of spells with the same color type
 * Spellbook - Has a certain number of spells. Ccontinued use levels up the spellbook which increases the potency of the spells.
 */