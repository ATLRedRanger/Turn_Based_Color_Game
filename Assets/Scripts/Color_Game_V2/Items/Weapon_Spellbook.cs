using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Spellbook : Weapon
{
    public int spellbookTier;
    private int spellbookExp = 0;
    private int spellbookLevel = 1;
    [SerializeField]
    private int expNeededToLevel = 100;
    public Dictionary<string, Attack> spellbookAttackDictionary = new Dictionary<string, Attack>();

    public Weapon_Spellbook(string itemName = "", string itemDescription = "", string itemID = "", int itemAmount = 0, WeaponType weaponType = WeaponType.Neutral, int weaponDamage = 0, int spellbookTier = 1)
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
        expNeededToLevel = 100 * spellbookLevel * (1 - (spellbookLevel / 10));
    }

    public void AddAttackToSpellbook(Attack attack)
    {
        spellbookAttackDictionary[attack.attackName] = attack;
    }
}
