using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy_Goblin : Unit
{

    private Color spriteColor;
    //public Dictionary<string, Attack> goblinDictionary = new Dictionary<string, Attack>();

    // Start is called before the first frame update
    public override void Start()
    {
        
        base.Start();
        spriteColor = spriteRenderer.color;
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
        int baseAttack = 2;
        if (envManaScript.currentGreen > envManaScript.currentBlue)
        {
            spriteRenderer.color = Color.red;
            physicalAttack = 5;
            
        }
        else
        {
            spriteRenderer.color = spriteColor;
            physicalAttack = baseAttack;
        }
        
    }

}
