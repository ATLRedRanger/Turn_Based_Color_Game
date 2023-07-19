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
        Debug.Log("Enemy dealing " + combatFunctionsScript.attackDamage);

        combatFunctionsScript.ReduceStamina(chosenAttack, enemyOne);
        Debug.Log(enemyOne.unitName + " has " + enemyOne.currentStamina + " stamina reminaing.");

        combatFunctionsScript.ReduceHealth(combatFunctionsScript.attackDamage, player);
        Debug.Log(player.unitName + " has " + player.currentHealth + " health reminaing.");
    }
    
    public void EnemyAttackChoice()
    {
        foreach(var kvp in enemyOne.unitAttackDictionary)
        {
            if(enemyOne.currentStamina >= kvp.Value.staminaCost)
            {
                chosenAttack = kvp.Value;
                Debug.Log(kvp.Key + " is the chosen ATTACK.");
            }
        }
    }

}
