﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Combat_Functions : MonoBehaviour
{
    //Scripts
    public AttacksDatabase attackDatabaseScript;

    public Unit_Spawner unitSpawnerScript;

    public CombatFunctions combatFunctionsScript;

    public Unit enemyOne;

    public Unit player;

    private Unit currentEnemy;

    public Attack chosenAttack;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadingScripts());
        
    }

    IEnumerator LoadingScripts()
    {
        yield return new WaitForSeconds(.5f);
        attackDatabaseScript = FindObjectOfType<AttacksDatabase>();

        unitSpawnerScript = FindObjectOfType<Unit_Spawner>();

        combatFunctionsScript = FindObjectOfType<CombatFunctions>();

        enemyOne = unitSpawnerScript.enemyOne;

        player = unitSpawnerScript.player;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyAttacking()
    {

        EnemyAttackChoice();

        if(combatFunctionsScript.HitorMiss(chosenAttack, enemyOne) == true )
            
        {
            combatFunctionsScript.DamageFromAttack(chosenAttack, enemyOne);
            combatFunctionsScript.ReduceStamina(chosenAttack, enemyOne);
            combatFunctionsScript.ReduceHealth(combatFunctionsScript.attackDamage, player, enemyOne);
            combatFunctionsScript.ReduceColorFromEnv(chosenAttack);
        }
        else
        {
            Debug.Log(enemyOne.unitName + " has missed!");
        }
        
    }
    
    public void EnemyAttackChoice()
    {
        //For each key-value pair in the units' dictionary of attacks
        foreach(var kvp in enemyOne.enemyAttackDictionary)
        {
            Debug.Log(kvp.Key);
            //if the enemy's stamina is greater than the stamina cost of the selected attack is greater than or equal to it
            if(enemyOne.currentStamina >= kvp.Value.staminaCost)
            {
                
                //The chosen attack is the value (the actual attack)
                chosenAttack = kvp.Value;
                //The key is the name of the attack
                Debug.Log(kvp.Key + " is the chosen ATTACK.");
            }
            else
            {
                enemyOne.isDefending = true;
            }
        }
    }

}

//TODO: Make the enemy defend when they don't have enough stamina to do a thing.
