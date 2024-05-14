using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Combat_Function_Fixed : MonoBehaviour
{

    public bool inCombat;

    public ENV_Mana envManaScript;

    public Unit_Spawner unitSpawnerScript;

    public Turn_Manager turnManagerScript;

    private StatusEffects statusEffectsScript;

    public UI uiScript;

    private int potentialAttackDamage;

    public Unit chosenEnemy;

    public int healthLost;

    public bool crit;

    public bool playAnimation = false;

    int roll;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(LoadingScripts());
    }

    IEnumerator LoadingScripts()
    {
        yield return new WaitForSeconds(.5f);

        unitSpawnerScript = FindObjectOfType<Unit_Spawner>();
        envManaScript = FindObjectOfType<ENV_Mana>();
        turnManagerScript = FindObjectOfType<Turn_Manager>();
        statusEffectsScript = FindObjectOfType<StatusEffects>();
        uiScript = FindObjectOfType<UI>();

    }

   
    
    
    
    
    //Combat In Steps:
    public IEnumerator CombatStepsTwo(Attack attack, Unit attacker, Unit defender)
    {
        inCombat = true;
        // 1) How many times does the chosen attack hit if it hits?
        for (int i = 0; i < attack.numOfAttacks; i++)
        {
            yield return new WaitForSeconds(.8f);
            // 2) Roll for accuracy on each attempt at a hit
            if (DidAttackHit(attack, attacker) == true)
            {
                uiScript.PlayAttackAnimation(attack, defender);
                // 3) If an attack is successful, how much damage is it potentially doing?
                //    -Check for crits in this stage
                //    -Check for weapon special abilities or modifiers
                int totalDamage = CalculateTotalDamage(attack, attacker, defender);
                // 4) How much damage is the attack doing after defenses and resistances?
                // 5) How much health damage does the defender take?
                // 6) How much stamina damage does the defender take?
                // 7) Are there any secondary effects of the attack?
                uiScript.UpdateUI();
            }
            if (defender.currentHealth < 1)
            {
                break;
            }
        }

        attacker.hadATurn = true;
        inCombat = false;
    }
    private StaminaLevels StaminaConversion(Unit unit)
    {
        //The higher the stamina, the better the accuracy the unit will have

        if (unit.currentStamina <= (unit.OgStamina * 1 / 4))
        {
            return StaminaLevels.OneQuarter;
        }
        if (unit.currentStamina > (unit.OgStamina * 1 / 4) && unit.currentStamina <= (unit.OgStamina * (1 / 2)))
        {
            return StaminaLevels.Half;
        }
        if ((unit.currentStamina > (1 / 2) && unit.currentStamina <= (unit.OgStamina * 3 / 4)))
        {
            return StaminaLevels.ThreeQuarters;
        }
        if (unit.currentStamina > (unit.OgStamina * 3 / 4))
        {
            return StaminaLevels.Full;
        }
        return StaminaLevels.Broken;
    }
    private int RollForAccuracy(Unit unit, float accuracyMultiple)
    {
        //Takes the unit's base accuracy and multiples it by the multiplier of the stanima value
        int dieRoll;
        int currentAccuracy = (int)(unit.baseAccuracy * accuracyMultiple);

        dieRoll = Random.Range(currentAccuracy, 101);

        return dieRoll;
    }
    public bool DidAttackHit(Attack attack, Unit unit)
    {
        //I want stamina to affect accuracy
        //I want to have 4 levels of affecting accuracy
        //The lower the stamina, the less accurate you are

        //Base Accurracy is the minimum random roll
        //Attack Accuracy is the roll + accuracy after stamina
        //Attack Accuracy is out of 100
        //Meet or beat Attack Accuracy to hit

        int finalAccuracy = 0;
        bool hit = false;

        StaminaLevels accuracyLevel = StaminaConversion(unit);

        switch (accuracyLevel)
        {
            case (StaminaLevels.Full):
                roll = RollForAccuracy(unit, 1f);
                finalAccuracy = roll + unit.baseAccuracy;
                break;
            case (StaminaLevels.ThreeQuarters):
                roll = RollForAccuracy(unit, .75f);
                finalAccuracy = roll + unit.baseAccuracy;
                break;
            case (StaminaLevels.Half):
                roll = RollForAccuracy(unit, .5f);
                finalAccuracy = roll + unit.baseAccuracy;
                break;
            case (StaminaLevels.OneQuarter):
                roll = RollForAccuracy(unit, .25f);
                finalAccuracy = roll + unit.baseAccuracy;
                break;
        }
        if (finalAccuracy >= attack.attackAccuracy)
        {

            hit = true;
            playAnimation = true;
        }
        Debug.Log("Final Accuracy is " + finalAccuracy + "Attack Accuracy is " + attack.attackAccuracy + "Roll " + roll);
        Debug.Log("Attack Hit " + hit);
        return hit;

    }
    private int CalcWeaponTotalDamage(Unit unit)
    {
        int weaponDamage = 0;
        int weaponBonusDamage = 0;
        if (unit.isWeaponEquipped)
        {
            switch (unit.equippedWeapon.weaponType)
            {
                case WeaponType.Axe:
                    weaponBonusDamage = (unit.axeMastery * weaponDamage) / 5;
                    break;
                case WeaponType.Bow:
                    weaponBonusDamage = (unit.bowMastery * weaponDamage) / 5;
                    break;
                case WeaponType.Hammer:
                    weaponBonusDamage = (unit.hammerMastery * weaponDamage) / 5;
                    break;
                case WeaponType.Spellbook:
                    break;
                case WeaponType.Staff:
                    break;
                case WeaponType.Sword:
                    weaponBonusDamage = Mathf.RoundToInt((unit.swordMastery * weaponDamage) / 5);
                    break;
            }
            Debug.Log($"WEAPON_DAMAGE: {weaponDamage} and WEAPON_BONUS_DAMAGE: {weaponBonusDamage}");
            weaponDamage += weaponBonusDamage;
            
        }
            return weaponDamage;
    }
    public bool CheckForCrit(Unit attacker)
    {
        //The thought process behind this is:
        //The higher your base accuracy, the more likely you are to crit
        //(The better you are at hitting your target, the more likely you are to hit critical points)
        //Having a high base accuracy should reward you with easier crits
        //Stamina management should also reward/punish your crits
        //If you're in the midst of battle and you're dying, you might get lucky, but not overly so. 
        //I want crits to feel rewarding, but shouldn't really decide the battle


        crit = false;

        int dieRoll = Random.Range(0, 101);
        int critChance = (int)(100 - (attacker.baseAccuracy * .10));
        int critCalc = (int)(dieRoll + (attacker.baseAccuracy * .10));

        StaminaLevels critThreshold = StaminaConversion(attacker);

        switch (critThreshold)
        {
            case StaminaLevels.Full:
                if ((critCalc * 1.25) > critChance)
                {
                    crit = true;
                }
                break;
            case StaminaLevels.ThreeQuarters:
                if ((critCalc * 1.15) > critChance)
                {
                    crit = true;
                }
                break;
            case StaminaLevels.Half:
                if ((critCalc * 1.05) > critChance)
                {
                    crit = true;
                }
                break;
            case StaminaLevels.OneQuarter:
                if ((critCalc * 1) > critChance)
                {
                    crit = true;
                }
                break;
        }
        
        return crit;
    }
    public bool CheckForWeakness(Attack attack, Unit defender)
    {
        bool weak = false;
        if(defender.weakness == attack.attackColor)
        {
            weak = true;
        }
        return weak;
    }
    private int CalculateTotalDamage(Attack attack, Unit attacker, Unit defender)
    {
        int totalDamage = 0;
        int unitBaseDamage = (attack.attackType == AttackType.Special) ? attacker.magicAttack : attacker.physicalAttack;
        int weaponTotalDamage = CalcWeaponTotalDamage(attacker);
        int totalAttackDamage = (CheckForCrit(attacker) == true) ? Mathf.RoundToInt((float)(attack.attackDamage * 1.5)) : attack.attackDamage;
        Debug.Log($"UNIT_BASE_DMG: {unitBaseDamage} + WEAPON_TOTAL_DMG: {weaponTotalDamage} + TOTAL_ATTACK_DMG: {totalAttackDamage}");
        totalDamage = unitBaseDamage + weaponTotalDamage + totalAttackDamage;
        if (CheckForWeakness(attack, defender))
        {
            totalDamage = Mathf.RoundToInt(totalDamage * 1.5f);
        }


        return totalDamage;
    }

    
}
