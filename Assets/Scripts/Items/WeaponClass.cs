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

    private int weaponMasteryBonusDamage = 0;

    private int weaponSpecialBonusDamage = 0;

    public int totalWeaponDamage;

    public WeaponType weaponType;

    public Attack weaponAttack;


    public Weapon()
    {
    }

    public Weapon(string itemName, ItemType itemType, Sprite itemSprite, string itemDescription, int weaponLevelRequirement, int weaponBaseDamage, int weaponMasteryBonusDamage,
        int weaponSpecialBonusDamage, int totalWeaponDamage, string itemID, WeaponType weaponType, Attack weaponAttack)
    {
        this.itemName = itemName;
        this.itemType = itemType;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        this.weaponLevelRequirement = weaponLevelRequirement;
        this.weaponBaseDamage = weaponBaseDamage;
        this.weaponMasteryBonusDamage = weaponMasteryBonusDamage;
        this.weaponSpecialBonusDamage = weaponSpecialBonusDamage;
        this.totalWeaponDamage = totalWeaponDamage;
        this.itemID = itemID;
        this.weaponType = weaponType;
        this.weaponAttack = weaponAttack;
        
    }

    public int GetTotalWeaponDamage(Unit unit)
    {

        SetWeaponMasteryBonusDamage(unit);
        SpecialProperty(unit);
        Debug.Log($"TOTAL_WEAPON_DMG = {totalWeaponDamage} + WPN_BSE: {weaponBaseDamage} + WPN_BNS: {weaponMasteryBonusDamage} + WPN_SPCL: {weaponSpecialBonusDamage}");
        totalWeaponDamage = weaponBaseDamage + weaponMasteryBonusDamage + weaponSpecialBonusDamage;

        return totalWeaponDamage;
    }

    
    public int GetWeaponBonusDamage()
    {
        return weaponMasteryBonusDamage;
    }
    public void SetWeaponMasteryBonusDamage(Unit user)
    {
        switch (weaponType)
        {
            case WeaponType.Axe:
                weaponMasteryBonusDamage = (user.axeMastery * weaponBaseDamage) / 5;
                break;
            case WeaponType.Bow:
                weaponMasteryBonusDamage = (user.bowMastery * weaponBaseDamage) / 5;
                break;
            case WeaponType.Hammer:
                weaponMasteryBonusDamage = (user.hammerMastery * weaponBaseDamage) / 5;
                break;
            case WeaponType.Spellbook:
                weaponMasteryBonusDamage = 0;
                break;
            case WeaponType.Staff:
                weaponMasteryBonusDamage = 0;
                break;
            case WeaponType.Sword:
                weaponMasteryBonusDamage = (user.swordMastery * weaponBaseDamage) / 5;
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
                ApplyRedsDarkGreatsword(unit);
                break;
        }
    }
    private void ApplyRedsDarkGreatsword(Unit equipped)
    {
        if (equipped.magicAttack > equipped.physicalAttack)
        {
            //Debug.Log($"REDS_Greatsword: {weaponSpecialBonusDamage}");
            weaponSpecialBonusDamage = Mathf.RoundToInt(equipped.magicAttack * .1f);
            
        }
    }
    public int Hammer(Unit attacker, Unit defender)
    {
        int modifiedDamage = 0;

        if (defender.currentStamina < attacker.currentStamina)
        {
            modifiedDamage = (int)(weaponBaseDamage * 3);
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
        
    }
}
//Bows have higher crit modifiers than other weapons, but their attacks have lower damage and stamina reqs. 