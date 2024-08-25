using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_V2
{
    private string statusName;
    private int effectLength = 0;
    public int effectStack = 0;
    private int damageAmount = 0;
    public int timeNeededInQue = 3;
    public StatusEffect_V2(string statusName = null, int effectLength = 0, int effectStack = 0, int damageAmount = 0)
    {
        this.statusName = statusName;
        this.effectLength = effectLength;
        this.effectStack = effectStack;
        this.damageAmount = damageAmount;

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
