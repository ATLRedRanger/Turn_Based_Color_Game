using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_V2
{
    public string statusName;
    public int effectLength = 0;
    public int effectStack = 0;
    public int damageAmount = 0;
    public int timeNeededInQue;

    public StatusEffect_V2(string statusName = null, int effectLength = 0, int effectStack = 0, int damageAmount = 0, int timeNeededInQue = 0)
    {
        this.statusName = statusName;
        this.effectLength = effectLength;
        this.effectStack = effectStack;
        this.damageAmount = damageAmount;
        this.timeNeededInQue = timeNeededInQue;
    }

    public int GetStatusDamage()
    {

        return damageAmount + effectStack;
    }

    public string GetStatusName()
    {
        return statusName;
    }

    public int GetEffectLength()
    {
        return effectLength;
    }
}
