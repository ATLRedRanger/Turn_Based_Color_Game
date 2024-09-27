using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectsDatabase_V2 : MonoBehaviour
{
    public StatusEffect_V2 burn;
    public StatusEffect_V2 futureSight;
    public Buffs buffington;
    public Debuffs deBuffington;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Awake()
    {
        burn = CreateStatus("Burn", 3, 0, 5, 0);
        futureSight = CreateStatus("Future Sight", 0, 0, 15, 3);
        buffington = CreateBuff("Buffington", 5);
        deBuffington = CreateDebuff("Debuffington", 3);
    }

    public StatusEffect_V2 CreateStatus(string statusName = null, int effectLength = 0, int effectStack = 0, int damageAmount = 0, int timeNeededInQue = 0)
    {
        var status = new StatusEffect_V2(statusName, effectLength, effectStack, damageAmount, timeNeededInQue);

        return status;
    }

    public Buffs CreateBuff(string statusName = null, int effectLength = 0, int effectStack = 0, int damageAmount = 0, int timeNeededInQue = 0)
    {
        var buff = new Buffs(statusName, effectLength, effectStack, damageAmount, timeNeededInQue);

        return buff;
    }

    public Debuffs CreateDebuff(string statusName = null, int effectLength = 0, int effectStack = 0, int damageAmount = 0, int timeNeededInQue = 0)
    {
        var buff = new Debuffs(statusName, effectLength, effectStack, damageAmount, timeNeededInQue);

        return buff;
    }
}
