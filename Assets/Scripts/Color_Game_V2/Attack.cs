
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Should colored attacks cost less stamina than neutral ones?
//If so, how much? <insert formula here>
//How to incentivize the use of colored attacks?
public class Attack
{
    public string attackName;
    public int attackPower;
    public int attackAccuracy;
    public int colorCost;
    public int staminaCost;
    public int numOfHits;
    public int statusBuildUpAmount;
    public AttackType attackType;
    public Hue attackColor;
    public AttackBehavior attackBehavior;
    public Buffs attackBuff;
    public Debuffs attackDebuff;
    public bool isSingleTarget;
    public int attackAccuracyBonus = 0;
    public int critRoll = 0;
    public Attack(string attackName, int attackPower, int attackAccuracy, int attackAccuracyBonus, int critRoll, int colorCost, int numOfHits,
                    int statusBuildUpAmout, AttackType attackType, Hue attackColor, AttackBehavior attackBehavior, bool isSingleTarget)
    {
        this.attackName = attackName;
        this.attackPower = attackPower;
        this.attackAccuracy = attackAccuracy;
        this.attackAccuracyBonus = attackAccuracyBonus;
        this.critRoll = critRoll;
        this.colorCost = colorCost;
        this.numOfHits = numOfHits;
        this.statusBuildUpAmount = statusBuildUpAmout;
        this.attackType = attackType;
        this.attackColor = attackColor;
        this.attackBehavior = attackBehavior;
        this.isSingleTarget = isSingleTarget;
    }

    public bool DoesAttackHit(Unit_V2 attacker, Unit_V2 defender, bool greatestColor)
    {
        int roll = Random.Range(1, attackAccuracy);

        envBehavior(greatestColor);

        roll += attackAccuracyBonus;
        
        Debug.Log($"Does attack hit: Roll: {roll} + {attacker.unitName}'s  CombatBAB: {attacker.GetCombatBAB()} vs {defender.unitName}'s DC: ");//{defender.GetCombatDC()}");
        
        if (roll + attacker.GetCombatBAB() >= defender.GetCombatDC())
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

    public void envBehavior(bool greatestColor)
    {
        if (greatestColor)
        {
            attackAccuracyBonus = 2;
        }
        else
        {
            attackAccuracyBonus = 0;
        }
    }

}
