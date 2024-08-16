using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class Spellbook : Weapon
{

    public int spellBookTier;

    public int spellBookLevel;

    public int spellBookExperience;

    public Hue affinity;

    public Dictionary<string, Attack> spellBookSpells = new Dictionary<string, Attack>();

    public List<Attack> spellBookAttackList = new List<Attack>();
    

    public Spellbook(Weapon weapon, ItemType itemType, int spellBookTier, int spellBookLevel, string itemName, string itemDesctiption, string itemID, WeaponType weaponType, Hue affinity)
    {
        this.itemName = itemName;
        this.itemType = itemType;
        this.itemDescription = itemDesctiption;
        weaponLevelRequirement = weapon.weaponLevelRequirement;
        weaponBaseDamage = weapon.weaponBaseDamage;
        this.itemID = itemID;
        this.weaponType = weaponType;
        this.spellBookTier = spellBookTier;
        this.spellBookLevel = spellBookLevel;
        this.affinity = affinity;
        
    }

    public void AddSpellToSpellbook(Attack attack)
    {
        spellBookAttackList.Add(attack);
        
       
    }

    public void GainExperience(int experience)
    {
        spellBookExperience += experience;
        SpellBookLevelUp();
    }

    private void SpellBookLevelUp()
    {
        if(spellBookExperience >= 125 * spellBookLevel)
        {
            spellBookLevel += 1;
            StrengthenSpells();
        }
    }

    private void StrengthenSpells()
    {
        for(int i = 0; i < spellBookAttackList.Count; i++)
        {
            spellBookAttackList[i].attackDamage += 5;
        }
    }
}
