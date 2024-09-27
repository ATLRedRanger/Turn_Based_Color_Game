using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuffs : StatusEffect_V2
{
    private int timeActive = 0;
    public Debuffs(string statusName = null, int effectLength = 0, int effectStack = 0, int damageAmount = 0, int timeNeededInQue = 0)
    {
        this.statusName = statusName;
        this.effectLength = effectLength;
        this.effectStack = effectStack;
        this.damageAmount = damageAmount;
        this.timeNeededInQue = timeNeededInQue;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetTimeActive()
    {
        return timeActive;
    }

    public void SetTimeActive(int value)
    {
        timeActive += value;
    }
 
    public void ApplyDebuff(Unit_V2 target)
    {
        Debug.Log("Applying Debuff");
        target.GetListOfDebuffs().Add(DebuffDeepCopy());
        
    }

    public void ActivateDebuffEffect(Unit_V2 target)
    {
        this.timeActive = 0;
        switch (statusName)
        {
            case "Debuffington":
                Debug.Log("Subtracting 1 from unit speedtier!");
                target.SetSpeedTier(-1);
                break;
        }
    }

    public void RevertDebuffEffect(Unit_V2 target)
    {
        switch (statusName)
        {
            case "Debuffington":
                Debug.Log("ADDing 1 to unit speedtier!");
                target.SetSpeedTier(1);
                break;
        }

        this.timeActive = 0;
    }

    public Debuffs DebuffDeepCopy()
    {
        Debuffs status = new Debuffs();

        status.statusName = this.statusName;
        status.effectLength = this.effectLength;
        status.effectStack = this.effectStack;
        status.damageAmount = this.damageAmount;
        status.timeNeededInQue = this.timeNeededInQue;

        return status;
    }
}
