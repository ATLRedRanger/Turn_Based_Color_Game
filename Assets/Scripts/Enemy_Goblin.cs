
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy_Goblin : Unit
{

    private Color spriteColor;

    private int initialAttack;

    
    //public Dictionary<string, Attack> goblinDictionary = new Dictionary<string, Attack>();

    // Start is called before the first frame update
    public override void Start()
    {
        
        base.Start();
        spriteColor = spriteRenderer.color;
        initialAttack = physicalAttack;
        LootDropItems();

    }

    public override void EnemyAttacks()
    {
        base.EnemyAttacks();

    }

    public override void EnemyAi()
    {
       base.EnemyAi();
    }

    public override void  SpecialAbility()
    {
        if(unitAnimator != null)
        {
            if (envManaScript.currentGreen > envManaScript.currentBlue)
            {
                //spriteRenderer.color = Color.red;
                physicalAttack = (int)(physicalAttack * 1.5);
                unitAnimator.SetBool("isRaging", true);

            }
            else
            {
                //spriteRenderer.color = spriteColor;
                physicalAttack = initialAttack;
                unitAnimator.SetBool("isRaging", false);
            }
        }   
        
        //Just playing around with different color based abilities
        //Would need to figure out a system so the UI knows to put the healing
        //numbers on screen
        if(envManaScript.currentBlue > envManaScript.currentGreen)
        {
            GainHealth(regenHealthInt);
        }
        
    }

    public override void LootDropItems()
    {
        lootDrops.Add(itemDatabaseScript._healthPotion);
        lootDrops.Add(itemDatabaseScript._basicHammer);
    }

    public override int DropLoot()
    {
        int roll = base.DropLoot();
        
        return roll;

    }
}
