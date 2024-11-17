using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyUnit_V2 : Unit_V2
{

    private Hue sensitiveColor = Hue.Green;
    private Hue tolerantColor = Hue.Blue;
    private List<Attack> attackList = new List<Attack>();
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        //AddAttackToDictionary(attackDatabaseScript._fireball);
        //AddAttackToDictionary(attackDatabaseScript._basicSlimeAttack);
        AddAttackToDictionary(attackDatabaseScript._basicAttack);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnitColorBehavior(Dictionary<Hue, int> envColors)
    {
        if (envColors[tolerantColor] > envColors[sensitiveColor])
        {
            //SetSpeedTier(1);
        }
    }

    private bool IsAttackUseable(Attack attack, Environment env)
    {
        if (attack.attackColor == Hue.Neutral && this.GetCurrentStamina() >= attack.staminaCost) 
        {
            return true;
        }
        else if (this.GetCurrentStamina() >= attack.staminaCost && env.GetCurrentColorDictionary()[attack.attackColor] >= attack.colorCost)
        {
            return true;
        }

        return false;
    }

    public Attack EnemyAttackDecision(Environment env)
    {
       
        foreach(var kvp in unitAttackDictionary)
        {
            attackList.Add(kvp.Value);
        }

        foreach (Attack attack in attackList)
        {
            if (IsAttackUseable(attack, env))
            {
                return attack;
            }
        }

        return null;
    }
}
