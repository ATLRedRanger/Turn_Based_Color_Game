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
        target.GetListOfDebuffs().Add(this);
        
    }

    public void ActivateDebuffEffect(Unit_V2 target)
    {
        switch (statusName)
        {
            case "Debuffington":
                target.SetSpeedTier(-1);
                break;
        }
    }
}