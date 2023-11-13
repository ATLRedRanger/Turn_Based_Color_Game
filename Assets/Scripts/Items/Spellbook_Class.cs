using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellbook : Weapon
{

    public int spellBookTier;

    public int spellBookLevel;

    public Spellbook(Weapon weapon, int spellBookTier, int spellBookLevel, string itemName, WeaponType weaponType)
    {
        this.itemName = itemName;
        itemDescription = weapon.itemDescription;
        weaponLevelRequirement = weapon.weaponLevelRequirement;
        weaponDamage = weapon.weaponDamage;
        weaponHealthModifier = weapon.weaponHealthModifier;
        weaponStaminaModifier = weapon.weaponStaminaModifier;
        this.weaponType = weaponType;
        this.spellBookTier = spellBookTier;
        this.spellBookLevel = spellBookLevel;
    }
}
