using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack 
{
    public string attackName;
    public int attackPower;
    public int attackAccuracy;
    public int colorCost;
    public int staminaCost;
    public int numOfHits;
    public AttackType attackType;
    public Hue attackColor;
    public AttackBehavior attackBehavior;

    
    public Attack(string attackName, int attackPower, int attackAccuracy, int colorCost, int staminaCost, int numOfHits,
                    AttackType attackType, Hue attackColor, AttackBehavior attackBehavior)
    {
        this.attackName = attackName;
        this.attackPower = attackPower;
        this.attackAccuracy = attackAccuracy;
        this.colorCost = colorCost;
        this.staminaCost = staminaCost;
        this.numOfHits = numOfHits;
        this.attackType = attackType;
        this.attackColor = attackColor;
        this.attackBehavior = attackBehavior;
    }

    public bool DoesAttackHit(Unit_V2 attacker)
    {
        int roll = Random.Range(0, 101);
        StaminaLevels staminaLevels = attacker.StaminaLevelConversion();

        if (staminaLevels == StaminaLevels.Full)
        {
            roll -= 20;
        }
        else if (staminaLevels == StaminaLevels.ThreeQuarters)
        {
            roll -= 15;
        }else if (staminaLevels == StaminaLevels.Half)
        {
            roll -= 10;
        }else if (staminaLevels == StaminaLevels.OneQuarter)
        {
            roll -= 5;
        }
        else
        {
            roll += 0;
        }
        
        if (roll < attackAccuracy)
        {
            return true;
        }
        return false;
    }
}
