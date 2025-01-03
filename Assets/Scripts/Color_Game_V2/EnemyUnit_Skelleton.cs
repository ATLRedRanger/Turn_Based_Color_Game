using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit_Skelleton : EnemyUnit_V2
{
    private Item commonDrop = null;
    private Item uncommonDrop = null;
    private Item rareDrop = null;
    private Item superRareDrop = null;
    public SpriteRenderer sprite;

    
    public override void Start()
    {
        base.Start();
        //foreach(var kvp in unitAttackDictionary)
        //{
        //    Debug.Log(kvp.Key);
        //}
        //AddAttackToDictionary(attackDatabaseScript._basicSlimeAttack);
        commonDrop = itemDatabaseScript.healthPotion;
        uncommonDrop = itemDatabaseScript.basicBow;
        rareDrop = itemDatabaseScript.basicAxe;
        superRareDrop = itemDatabaseScript.redSpellbook;
        sprite = GetComponent<SpriteRenderer>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);


    }

    public override void UnitColorBehavior(Dictionary<Hue, int> envColors)
    {
        if (GetSensitiveColor() != Hue.Neutral && GetTolerantColor() != Hue.Neutral)
        {
            if (envColors[GetTolerantColor()] > envColors[GetSensitiveColor()])
            {
                sprite.color = Color.red;
            }
            else
            {
                sprite.color = Color.white;
            }
        }

    }

    public override Attack EnemyAttackDecision(Environment env)
    {
        return base.EnemyAttackDecision(env);
    }
}