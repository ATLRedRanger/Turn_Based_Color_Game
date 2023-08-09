using System.Collections;
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

        combatFunctionsScript.DamageFromAttack(chosenAttack, enemyOne);
        Debug.Log(chosenAttack.attackDamage + " + " + enemyOne.baseAttack);

        combatFunctionsScript.ReduceStamina(chosenAttack, enemyOne);
        Debug.Log(enemyOne.unitName + " has " + enemyOne.currentStamina + " stamina reminaing.");

        combatFunctionsScript.ReduceHealth(combatFunctionsScript.attackDamage, player);
        Debug.Log(player.unitName + " has " + player.currentHealth + " health reminaing.");

        combatFunctionsScript.ReduceColorFromEnv(chosenAttack);
    }
    
    public void EnemyAttackChoice()
    {
        //For each key-value pair in the units' dictionary of attacks
        foreach(var kvp in enemyOne.enemyAttackDictionary)
        {
            Debug.Log(kvp.Key);
            //if the enemy's stamina is greater than the stamina cost of the selected attack is rgeater than or equal to it
            if(enemyOne.currentStamina >= kvp.Value.staminaCost)
            {
                
                //The chosen attack is the value (the actual attack)
                chosenAttack = kvp.Value;
                //The key is the name of the attack
                Debug.Log(kvp.Key + " is the chosen ATTACK.");
            }
        }
    }

}

//TODO: Make the enemy defend when they don't have enough stamina to do a thing.
