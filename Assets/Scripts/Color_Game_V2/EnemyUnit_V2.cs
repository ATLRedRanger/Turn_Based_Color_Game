using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyUnit_V2 : Unit_V2
{

    private Hue sensitiveColor = Hue.Green;
    private Hue tolerantColor = Hue.Blue;
    private List<Attack> attackList = new List<Attack>();
    private Item commonDrop = null;
    private Item uncommonDrop = null;
    private Item rareDrop = null;
    private Item superRareDrop = null;
    

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        //AddAttackToDictionary(attackDatabaseScript._fireball);
        AddAttackToDictionary(attackDatabaseScript._basicSlimeAttack);
        AddAttackToDictionary(attackDatabaseScript._basicAttack);
        commonDrop = itemDatabaseScript.basicAxe;
        uncommonDrop = itemDatabaseScript.basicSpellbook;
        rareDrop = itemDatabaseScript.burnHeal;
        superRareDrop = itemDatabaseScript.redSpellbook;
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

    public Item DroppedItem()
    {
        int roll = Random.Range(1, 101);
        Debug.Log($"DroppedItem() Roll: {roll}");
        if(roll == 1)
        {
            return superRareDrop;
        }
        else if(roll <= 11)
        {
            return rareDrop;
        }
        else if(roll <= 30)
        {
            return uncommonDrop;
        }
        else
        {
            return commonDrop;
        }

        
    }
}
