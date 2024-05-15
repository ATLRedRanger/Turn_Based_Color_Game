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

    public Unit chosenEnemy;

    private int totalDamage;

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
                totalDamage = CalculateTotalDamage(attack, attacker, defender);
                // 4) How much damage is the attack doing after defenses and resistances/weaknesses?
                // 5) How much health damage does the defender take?
                if (defender.isDefending)
                {
                    totalDamage = totalDamage / 2;
                }
                defender.LoseHealth(totalDamage);
                // 6) How much stamina damage does the defender take?
                // 7) Are there any secondary effects of the attack?
                CheckForAttackAbilities(attack, attacker, defender);
                ReduceStamina(attack, attacker);
                ReduceColorFromEnv(attack);
                ColorReturn(attack);
                uiScript.FloatingNumbersText(defender, attack);
                yield return new WaitForSeconds(.7f);
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
    private bool DidAttackHit(Attack attack, Unit unit)
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
    private int CalcTotalWeaponDamage(Unit unit)
    {
       
        return unit.equippedWeapon.GetWeaponTotalDamage(unit); ;
    }
    private bool CheckForCrit(Unit attacker)
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
    private bool CheckForWeakness(Attack attack, Unit defender)
    {
        bool weak = false;
        if(defender.GetWeakness() == attack.attackColor)
        {
            weak = true;
        }
        return weak;
    }
    private bool CheckForResistance(Attack attack, Unit defender)
    {
        bool resist = false;
        if (defender.GetResistance() == attack.attackColor)
        {
            resist = true;
        }
        return resist;
    }
    private int CalculateTotalDamage(Attack attack, Unit attacker, Unit defender)
    {
        totalDamage = 0;
        int unitBaseDamage = (attack.attackType == AttackType.Special) ? attacker.magicAttack : attacker.physicalAttack;
        int weaponTotalDamage = CalcTotalWeaponDamage(attacker);
        int totalAttackDamage = (CheckForCrit(attacker) == true) ? Mathf.RoundToInt((float)(attack.attackDamage * 1.5)) : attack.attackDamage;
        int totalDefenses = (attack.attackType == AttackType.Special) ? defender.magicDefense : defender.physicalDefense;

        Debug.Log($"UNIT_BASE_DMG: {unitBaseDamage} + WEAPON_TOTAL_DMG: {weaponTotalDamage} + TOTAL_ATTACK_DMG: {totalAttackDamage}");
        totalDamage = unitBaseDamage + weaponTotalDamage + totalAttackDamage;

        if (CheckForWeakness(attack, defender))
        {
            totalDamage = Mathf.RoundToInt(totalDamage * 1.5f);
        }

        if (CheckForResistance(attack, defender))
        {
            totalDamage = Mathf.RoundToInt(totalDamage * .7f);
        }

        Debug.Log($"TOTAL_DAMAGE: {totalDamage} - TOTAL_DEFENSES: {totalDefenses}");
        totalDamage = totalDamage - totalDefenses;
        Debug.Log($"TOTAL_DAMAGE: {totalDamage}");
        return totalDamage;
    }
    public void CheckForAttackAbilities(Attack attack, Unit attacker, Unit defender)
    {
        //attack.AttackFunction(defender);
        attack.AttackStatusBehavior(attacker, defender);
    }
    public void ReduceStamina(Attack attack, Unit unit)
    {
        unit.LoseStamina(attack.staminaCost);

    }
    public void ReduceColorFromEnv(Attack attack)
    {

        switch (attack.attackColor)
        {
            case Hue.Red:

                envManaScript.currentRed -= attack.colorCost;

                break;
            case Hue.Orange:
                envManaScript.currentOrange -= attack.colorCost;
                break;
            case Hue.Yellow:
                envManaScript.currentYellow -= attack.colorCost;
                break;
            case Hue.Green:
                envManaScript.currentGreen -= attack.colorCost;
                break;
            case Hue.Blue:
                envManaScript.currentBlue -= attack.colorCost;
                break;
            case Hue.Violet:
                envManaScript.currentViolet -= attack.colorCost;
                break;
            default:
                break;
        }
    }
    public void ColorReturn(Attack attack)
    {
        //int roll = Random.Range(0, 5);

        switch (attack.attackColor)
        {
            case Hue.Red:
                envManaScript.currentOrange += attack.colorCost;
                if (envManaScript.currentOrange > envManaScript.maxOrange)
                {
                    envManaScript.maxOrange = envManaScript.currentOrange;
                }
                break;
            case Hue.Orange:
                envManaScript.currentYellow += attack.colorCost;
                if (envManaScript.currentYellow > envManaScript.maxYellow)
                {
                    envManaScript.maxYellow = envManaScript.currentYellow;
                }
                break;
            case Hue.Yellow:
                envManaScript.currentGreen += attack.colorCost;
                if (envManaScript.currentGreen > envManaScript.maxGreen)
                {
                    envManaScript.maxGreen = envManaScript.currentGreen;
                }
                break;
            case Hue.Green:
                envManaScript.currentBlue += attack.colorCost;
                if (envManaScript.currentBlue > envManaScript.maxBlue)
                {
                    envManaScript.maxBlue = envManaScript.currentBlue;
                }
                break;
            case Hue.Blue:
                envManaScript.currentViolet += attack.colorCost;
                if (envManaScript.currentViolet > envManaScript.maxViolet)
                {
                    envManaScript.maxViolet = envManaScript.currentViolet;
                }
                break;
            case Hue.Violet:
                envManaScript.currentRed += attack.colorCost;
                if (envManaScript.currentRed > envManaScript.maxRed)
                {
                    envManaScript.maxRed = envManaScript.currentRed;
                }
                break;
            default:
                break;
        }
    }
}
