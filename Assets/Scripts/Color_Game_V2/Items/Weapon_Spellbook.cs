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
    private int MAXSPELLCOUNT = 4;
    public List<Attack> spellbookAttacks = new List<Attack>();
    public int numOfAttacks = 0;


    public Weapon_Spellbook(string itemName = "", string itemDescription = "", string itemID = "", int itemAmount = 0,
        WeaponType weaponType = WeaponType.Spellbook, int baseDamage = 0, int spellbookTier = 1, ItemTier itemTier = ItemTier.Common)
    {
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemID = itemID;
        this.weaponType = weaponType;
        this.baseDamage = baseDamage;
        this.spellbookTier = spellbookTier;
        this.itemTier = itemTier;
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
        if(spellbookAttacks.Count < MAXSPELLCOUNT)
        {
            spellbookAttacks.Add(attack);
            numOfAttacks++;
        }

    }
}
