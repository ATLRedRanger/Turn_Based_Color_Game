using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public WeaponType weaponType;
    public int baseDamage;
    public int bonusModifier;
    //private int critChance;
    //private int critPercent;
    //private int weaponDamage;
    public Attack weaponAttack = null;

    public Weapon()
    {

    }
    /*public Weapon(string itemName = "", string itemDescription = "", string itemID = "", int itemAmount = 0, WeaponType weaponType = WeaponType.Neutral, int baseDamage = 0)
    {
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemID = itemID;
        this.itemAmount = itemAmount;
        this.weaponType = weaponType;
        this.baseDamage = baseDamage;
    }*/
    public Weapon(string itemName = "", string itemDescription = "", string itemID = "", int itemAmount = 0, WeaponType weaponType = WeaponType.Neutral, int baseDamage = 0, int bonusModifier = 0, Attack weaponAttack = null)
    {
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemID = itemID;
        this.itemAmount = itemAmount;
        this.weaponType = weaponType;
        this.baseDamage = baseDamage;
        this.bonusModifier = bonusModifier;
        this.weaponAttack = weaponAttack;
    }
    public override void Use(Unit_V2 unit)
    {
        base.Use(unit);
    }

    /*
    public virtual int GetWeaponDamage(Unit_V2 attacker, Unit_V2 defender)
    {
        weaponDamage = baseDamage;
        return weaponDamage;
    }*/

    public virtual int GetWeaponBaseDamage()
    {
        return baseDamage;
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