
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
    private int statusBuildUpAmount;
    public AttackType attackType;
    public Hue attackColor;
    public AttackBehavior attackBehavior;
    public Buffs attackBuff;
    public Debuffs attackDebuff;
    
    public Attack(string attackName, int attackPower, int attackAccuracy, int colorCost, int staminaCost, int numOfHits,
                    int statusBuildUpAmout, AttackType attackType, Hue attackColor, AttackBehavior attackBehavior)
    {
        this.attackName = attackName;
        this.attackPower = attackPower;
        this.attackAccuracy = attackAccuracy;
        this.colorCost = colorCost;
        this.staminaCost = staminaCost;
        this.numOfHits = numOfHits;
        this.statusBuildUpAmount = statusBuildUpAmout;
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

    public void SetAttackBuff(Buffs buff)
    {
        attackBuff = buff;
    }

    public void SetAttackDebuff(Debuffs debuff)
    {
        attackDebuff = debuff;
    }

    public int GetStatusBuildUpAmount()
    {
        return statusBuildUpAmount;
    }
}
