using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public WeaponType weaponType;
    public int baseDamage;
    private int critChance;
    private int critPercent;
    private int weaponDamage;

    public Weapon()
    {

    }
    public Weapon(string itemName = "", string itemDescription = "", string itemID = "", int itemAmount = 0, WeaponType weaponType = WeaponType.Neutral, int baseDamage = 0)
    {
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemID = itemID;
        this.itemAmount = itemAmount;
        this.weaponType = weaponType;
        this.baseDamage = baseDamage;
    }

    public override void Use(Unit_V2 unit)
    {
        base.Use(unit);
    }

    public virtual int GetWeaponDamage(Unit_V2 attacker, Unit_V2 defender)
    {
        weaponDamage = baseDamage;
        return weaponDamage;
    }

    public virtual int GetWeaponBaseDamage()
    {
        return baseDamage;
    }

    public virtual float GetWeaponCritChance()
    {
        switch (weaponType)
        {
            case WeaponType.Axe:
                critChance = 10;
                break;
            case WeaponType.Bow:
                critChance = 25;
                break;
            case WeaponType.Hammer:
                critChance = 10;
                break;
            case WeaponType.Spellbook:
                critChance = 10;
                break;
            case WeaponType.Staff:
                critChance = 10;
                break;
            case WeaponType.Sword:
                critChance = 10;
                break;
            default:
                critChance = 0;
                break;
        }
        return critChance;
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