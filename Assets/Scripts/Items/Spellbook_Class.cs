using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellbook : Weapon
{

    public int spellBookTier;

    public int spellBookLevel;

    public int spellBookExperience;

    public Hue affinity;

    public Dictionary<string, Attack> spellBookSpells = new Dictionary<string, Attack>();

    public Spellbook(Weapon weapon, int spellBookTier, int spellBookLevel, string itemName, WeaponType weaponType, Hue affinity)
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
        this.affinity = affinity;
    }

    public void AddSpellToSpellbook(Attack attack)
    {
        //Adds a spell to the spellbook
        //If there are less spells in the book than the tier of the spellbook
        if (spellBookSpells.Count < spellBookTier)
        {
            spellBookSpells.Add(attack.attackName, attack);
        }
        else
        {
            Debug.Log("This spellbook can only hold " + spellBookTier + " spells.");
        }
    }

    public void GainExperience(int experience)
    {
        spellBookExperience += experience;
    }
}
