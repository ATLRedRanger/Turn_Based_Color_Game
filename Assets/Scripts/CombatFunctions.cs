using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatFunctions : MonoBehaviour
{

    public AttacksDatabase attackDatabase;

    //public Unit unit;

    public ENV_Mana envManaScript;

    public int attackDamage;

    public Unit chosenEnemy;

    int roll;
    
    // Start is called before the first frame update
    void Start()
    {
        
        envManaScript = FindObjectOfType<ENV_Mana>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public bool HitorMiss(Attack attack, Unit unit)
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
        if(finalAccuracy >= attack.attackAccuracy)
        {
            
            hit = true;
        }
        Debug.Log("Final Accuracy is " + finalAccuracy + "Attack Accuracy is " + attack.attackAccuracy);
        Debug.Log("This roll is " + roll);
        return hit;

    }

    private int RollForAccuracy(Unit unit, float accuracyMultiple)
    {
        int dieRoll;
        int currentAccuracy = (int)(unit.currentStamina * accuracyMultiple);

        dieRoll = Random.Range(currentAccuracy, 100);
        return dieRoll;
    }

    public int ReduceStamina(Attack attack, Unit unit)
    {
        unit.currentStamina -= attack.staminaCost;

        
        return unit.currentStamina;
    }

    public void RegenStamina(Unit unit)
    {
        //Regenerate stamina at a rate of 1/5th of their maximum stanima

        int staminaRegend = ((unit.maxStamina * 1 / 5)*unit.staminaRegenModifier);

        unit.currentStamina += staminaRegend;

        Debug.Log(unit.currentStamina + " is the current stamina of " + unit.unitName + " and the max stamina is " + unit.maxStamina);

        if(unit.currentStamina >= unit.maxStamina)
        {
            unit.currentStamina = unit.maxStamina;
        }

        Debug.Log(unit.unitName + " has gained " + staminaRegend + " stamina.");
    }

    public int DamageFromAttack(Attack attack, Unit unit)
    {
        attackDamage = unit.baseAttack + attack.attackDamage;

        Debug.Log(attackDamage + "is the amount of damage dealt by"+unit.unitName);
        return attackDamage;

    }

    public void ReduceHealth(int damage, Unit unit)
    {
        if(unit.currentStamina <= 0)
        {
            unit.currentHealth -= damage * 2;
            unit.AmIDeadYet();
        }
        else
        {
            unit.currentHealth -= damage;
            unit.AmIDeadYet();
        }
        
        
    }

    public void ReduceColorFromEnv(Attack attack)
    {
        switch (attack.attackColor)
        {
            case Color.Red:
                envManaScript.currentRed -= attack.colorCost;
                break;
            case Color.Orange:
                envManaScript.currentOrange -= attack.colorCost;
                break;
            case Color.Yellow:
                envManaScript.currentYellow -= attack.colorCost;
                break;
            case Color.Green:
                envManaScript.currentGreen -= attack.colorCost;
                break;
            case Color.Blue:
                envManaScript.currentBlue -= attack.colorCost;
                break;
            case Color.Violet:
                envManaScript.currentViolet -= attack.colorCost;
                break;
        }
    }
    
    private StaminaLevels StaminaConversion(Unit unit)
    {
        if (unit.currentStamina <= (unit.maxStamina*1/4))
        {
            return StaminaLevels.OneQuarter;
        }
        if(unit.currentStamina > (unit.maxStamina*1/4) && unit.currentStamina <= (unit.maxStamina*(1 / 2)))
        {
            return StaminaLevels.Half;
        }
        if((unit.currentStamina > (1/2) && unit.currentStamina <= (unit.maxStamina*3/4)))
        {
            return StaminaLevels.ThreeQuarters;
        }
        if(unit.currentStamina > (unit.maxStamina * 3 / 4))
        {
            return StaminaLevels.Full;
        }
        return StaminaLevels.Broken;
    }
    
}
//TODO: Accuracy? I would like to tie in stamina, health or both into accuracy. Concerned that it could snowball.
//Ie: You are dealing damage to OP, meaning they're accuracy drops and therefor open the door for you to deal more damage.
//Maybe just stamina then. 

//Refactor the UI so that the UI functions call the Combat functions of the same name
//IE UI_Fireball() needs to call CombatFucntions_Fireball()
