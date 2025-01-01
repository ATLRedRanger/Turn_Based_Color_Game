using System.Collections.Generic;
using UnityEngine;

public class Weapon_Spellbook : Weapon
{
    public int spellbookTier;
    private int spellbookExp = 0;
    private int spellbookLevel = 1;
    [SerializeField]
    private int expNeededToLevel = 100;
    //private int MAXSPELLCOUNT = 4;
    public List<Attack> spellbookAttacks = new List<Attack>();
    //public int numOfAttacks = 0;

    private Attack firstSpellbookAttack = null;
    private Attack secondSpellbookAttack = null;
    private Attack thirdSpellbookAttack = null;
    private Attack fourthSpellbookAttack = null;

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
        Debug.Log($"This spellbook has gained {exp} experience!");
        spellbookExp += exp;
        if (spellbookExp > expNeededToLevel)
        {
            GainLevel();
        }
    }

    public void GainLevel()
    {
        spellbookLevel += 1;
        switch (spellbookLevel)
        {
            case 2:
                if(secondSpellbookAttack != null)
                {
                    spellbookAttacks.Add(secondSpellbookAttack);
                }
                break;
            case 5:
                if (thirdSpellbookAttack != null)
                {
                    spellbookAttacks.Add(thirdSpellbookAttack);
                }
                break;
            case 10:
                if (fourthSpellbookAttack != null)
                {
                    spellbookAttacks.Add(secondSpellbookAttack);
                }
                break;
        }
        expNeededToLevel = 100 * spellbookLevel * (1 - (spellbookLevel / 10));
    }

    public void AddAttackToSpellbook(Attack attack)
    {
        /*if(spellbookAttacks.Count < MAXSPELLCOUNT)
        {
            spellbookAttacks.Add(attack);
            numOfAttacks++;
        }*/
        if(firstSpellbookAttack == null)
        {
            firstSpellbookAttack = attack;
            spellbookAttacks.Add(firstSpellbookAttack);
        }
        else if(secondSpellbookAttack == null)
        {
            secondSpellbookAttack = attack;
        }
        else if(thirdSpellbookAttack == null)
        {
            thirdSpellbookAttack = attack;
        }
        else if(fourthSpellbookAttack == null)
        {
            fourthSpellbookAttack = attack;
        }
        else
        {
            Debug.Log("Spellbook is full!");
        }
    }

    public Attack GetFirstSpellbookSpell()
    {
        return firstSpellbookAttack;
    }

    public Attack GetSecondSpellbookSpell()
    {
        return secondSpellbookAttack;
    }

    public Attack GetThirdSpellbookSpell()
    {
        return thirdSpellbookAttack;
    }

    public Attack GetFourthSpellbookSpell()
    {
        return fourthSpellbookAttack;
    }
}
