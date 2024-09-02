using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffs : StatusEffect_V2
{

    public int timeActive = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyBuff(Unit_V2 target)
    {
        target.GetListOfBuffs().Add(this);
        ActivateBuffEffect(target);
        timeActive++;
    }

    public void ActivateBuffEffect(Unit_V2 target)
    {
        switch (this.GetStatusName())
        {
            case "Buffington":
                target.SetSpeedTier(1);
                break;
        }
    }
}
