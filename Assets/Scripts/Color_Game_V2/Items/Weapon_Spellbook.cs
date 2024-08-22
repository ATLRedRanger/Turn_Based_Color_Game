using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Spellbook : Weapon
{
    public int spellbookTier;
    private int spellbookExp = 0;
    private int spellbookLevel = 1;
    private int expNeededToLevel;
    public Weapon_Spellbook(string itemName, string itemDescription, string itemID, int itemAmount, WeaponType weaponType, int weaponDamage, int spellbookTier)
    {
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemID = itemID;
        this.weaponType = weaponType;
        this.weaponDamage = weaponDamage;
        this.spellbookTier = spellbookTier;
        
    }

    public override void Use(Unit_V2 unit)
    {
        base.Use(unit);
    }

    public void GainExp(int exp)
    {
        spellbookExp += exp;
        if (spellbookExp > expNeededToLevel)
        {
            GainLevel();
        }
    }

    public void GainLevel()
    {
        spellbookLevel += 1;
    }
}
