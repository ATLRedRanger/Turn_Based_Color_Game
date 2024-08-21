using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectsDatabase_V2 : MonoBehaviour
{
    public StatusEffect_V2 burn; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Awake()
    {
        burn = CreateStatus("Burn", 3, 0, 5);
    }

    public StatusEffect_V2 CreateStatus(string statusName = null, int effectLength = 0, int effectStack = 0, int damageAmount = 0)
    {
        var status = new StatusEffect_V2(statusName, effectLength, effectStack, damageAmount);

        return status;
    }

    
}
