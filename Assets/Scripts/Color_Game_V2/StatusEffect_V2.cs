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

    public StatusEffect_V2 DeepCopy()
    {
        StatusEffect_V2 status = new StatusEffect_V2();

        status.statusName = this.statusName;
        status.effectLength = this.effectLength;
        status.effectStack = this.effectStack;
        status.damageAmount = this.damageAmount;
        status.timeNeededInQue = this.timeNeededInQue;

        return status;
    }
}
