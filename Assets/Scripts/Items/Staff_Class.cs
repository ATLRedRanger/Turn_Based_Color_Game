using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : Weapon
{

    public int statusEffectChance;

    public Hue affinity;

    public Staff(string itemName, ItemType itemType, string itemDescription, int weaponLevelRequirement, int weaponBaseDamage, float weaponHealthModifier, float weaponStaminaModifier, float weaponCritModifier, string itemID, WeaponType weaponType, int statusEffectChance, Hue affinity)
    {
        this.itemName = itemName;
        this.itemType = itemType;  
        this.itemDescription = itemDescription;
        this.weaponLevelRequirement = weaponLevelRequirement;
        this.weaponBaseDamage = weaponBaseDamage;
        this.weaponHealthModifier = weaponHealthModifier;
        this.weaponStaminaModifier = weaponStaminaModifier;
        this.weaponCritModifier = weaponCritModifier;
        this.itemID = itemID;
        this.weaponType = weaponType;
        this.statusEffectChance = statusEffectChance;
        this.affinity = affinity;
        //this.itemAmount = 1;
    }

    public override void Use(Unit player)
    {
        Debug.Log("This is the weapon overriding");
    }




}
