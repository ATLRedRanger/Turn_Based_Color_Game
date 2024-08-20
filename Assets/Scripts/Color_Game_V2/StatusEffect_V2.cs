using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_V2
{
    private string statusName;
    private int effectLength;
    private int effectStack = 0;
    private int damageAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public StatusEffect_V2(string statusName = null, int effectLength = 0, int effectStack = 0, int damageAmount = 0)
    {
        this.statusName = statusName;
        this.effectLength = effectLength;
        this.effectStack = effectStack;
        this.damageAmount = damageAmount;

    }

    public int GetStatusDamage()
    {

        return damageAmount;
    }
}
