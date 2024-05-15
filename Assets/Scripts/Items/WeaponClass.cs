using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using static UnityEngine.UI.CanvasScaler;

//What attributes do weapons in SoulsLike games have? 
//Name, Weight, Attack Power (both w/ and w/o the scaling), Critical Damage, Attribute Scaling, Passive Effects, Attribute Requirements, Special Attacks (if applicable) 
//Name:
//Description:
//Weight:
//Attack Power:
//Critical Damage:
//Attribute Scaling:
//Passive Effects:
//Attribute Requirements:
//Special(Weapon) Attacks:
public class Weapon : Item
{

    public int weaponLevelRequirement;

    public int weaponBaseDamage;

    private int weaponBonusDamage = 0;

    private int weaponSpecialBonusDamage = 0;

    public int weaponTotalDamage;

    public WeaponType weaponType;

    public Attack weaponAttack;

    public Weapon()
    {
    }

    public Weapon(string itemName, ItemType itemType, string itemDescription, int weaponLevelRequirement, int weaponBaseDamage, int weaponBonusDamage, int weaponSpecialBonusDamage, int weaponTotalDamage, string itemID, WeaponType weaponType, Attack weaponAttack)
    {
        this.itemName = itemName;
        this.itemType = itemType;
        this.itemDescription = itemDescription;
        this.weaponLevelRequirement = weaponLevelRequirement;
        this.weaponBaseDamage = weaponBaseDamage;
        this.weaponBonusDamage = weaponBonusDamage;
        this.weaponSpecialBonusDamage = weaponSpecialBonusDamage;
        this.weaponTotalDamage = weaponTotalDamage;
        this.itemID = itemID;
        this.weaponType = weaponType;
        this.weaponAttack = weaponAttack;
        
    }

    public int GetWeaponTotalDamage(Unit unit)
    {

        SetWeaponBonusDamage(unit);
        SpecialProperty(unit);
        weaponTotalDamage = weaponTotalDamage + weaponBaseDamage + weaponBonusDamage + weaponSpecialBonusDamage;

        return weaponTotalDamage;
    }
    public int GetWeaponBonusDamage()
    {
        return weaponBonusDamage;
    }
    public void SetWeaponBonusDamage(Unit user)
    {
        switch (weaponType)
        {
            case WeaponType.Axe:
                weaponBonusDamage = (user.axeMastery * weaponBaseDamage) / 5;
                break;
            case WeaponType.Bow:
                weaponBonusDamage = (user.bowMastery * weaponBaseDamage) / 5;
                break;
            case WeaponType.Hammer:
                weaponBonusDamage = (user.hammerMastery * weaponBaseDamage) / 5;
                break;
            case WeaponType.Spellbook:
                weaponBonusDamage = 0;
                break;
            case WeaponType.Staff:
                weaponBonusDamage = 0;
                break;
            case WeaponType.Sword:
                weaponBonusDamage = (user.swordMastery * weaponBaseDamage) / 5;
                break;
        }
    }

    public override void Use(Unit user)
    {
        Debug.Log("This is the weapon overriding");
    }

    public override void SpecialProperty(Unit unit)
    {
        base.SpecialProperty(unit);
        switch (itemName)
        {
            case "Red's Dark Greatsword":
                if(unit.magicAttack > unit.physicalAttack)
                {
                    weaponSpecialBonusDamage += Mathf.RoundToInt(unit.magicAttack * .5f);
                }
                break;
        }
    }

    public int Hammer(Unit attacker)
    {
        int modifiedDamage = 0;
        //Hammer gains bonus damage based on attacker stamina levels
        
        if (attacker.currentStamina <= (attacker.OgStamina * 1 / 4))
        {
            modifiedDamage = weaponBaseDamage;
        }
        if (attacker.currentStamina > (attacker.OgStamina * 1 / 4) && attacker.currentStamina <= (attacker.OgStamina * (1 / 2)))
        {
            modifiedDamage = (int)(weaponBaseDamage * 1.2);
        }
        if ((attacker.currentStamina > (1 / 2) && attacker.currentStamina <= (attacker.OgStamina * 3 / 4)))
        {
            modifiedDamage = (int)(weaponBaseDamage * 1.5);
        }
        if (attacker.currentStamina > (attacker.OgStamina * 3 / 4))
        {
            modifiedDamage = (int)(weaponBaseDamage * 2);
        }
        
        return modifiedDamage;
    }

    public int Axe(Unit attacker, Unit defender)
    {
        int modifiedDamage = 0;

        if(defender.physicalDefense > attacker.physicalDefense)
        {
            modifiedDamage = (int)(weaponBaseDamage * 3);
        }

        return modifiedDamage;
    }

    public void Sword(Unit attacker)
    {
        //Really don't know what I want to do for swords to be different
        //Played around with a switch stance type mechanic, but I don't think I like it
        //This current mechanic is vanilla and just says if the attacker's stamina is below half
        //Use their special ability
        //I haven't given anybody specials except the slime so this is basically a placeholder ability
        if(attacker.currentStamina < (int)(attacker.maxStamina * 1 / 2))
        {
            attacker.SpecialAbility();
        }
    }
}
//Bows have higher crit modifiers than other weapons, but their attacks have lower damage and stamina reqs. 