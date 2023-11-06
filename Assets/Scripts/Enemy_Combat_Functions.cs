using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Combat_Functions : MonoBehaviour
{
    //Scripts
    public AttacksDatabase attackDatabaseScript;

    public Unit_Spawner unitSpawnerScript;

    public CombatFunctions combatFunctionsScript;

    private Turn_Manager turnManagerScript;

    public ENV_Mana envManaScript;

    public Unit enemyOne;

    public Unit player;

    private Unit currentEnemy;

    public Attack chosenAttack;

    public bool canAttack;
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

        turnManagerScript = FindObjectOfType<Turn_Manager>();

        envManaScript = FindObjectOfType<ENV_Mana>();

        enemyOne = unitSpawnerScript.enemyOne;

        player = unitSpawnerScript.player;


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnemyAttacking()
    {
        Debug.Log("ENEMY IS ATTACKING");
        EnemyAttackChoice();

        
        if (combatFunctionsScript.HitorMiss(chosenAttack, enemyOne) == true)

        {
            
            combatFunctionsScript.DamageFromAttack(chosenAttack, enemyOne);
            combatFunctionsScript.ReduceStamina(chosenAttack, enemyOne);
            combatFunctionsScript.ReduceHealth(combatFunctionsScript.attackDamage, player, enemyOne);
            combatFunctionsScript.ReduceColorFromEnv(chosenAttack);
            combatFunctionsScript.ColorReturn(chosenAttack);
            
        }
        else
        {
            enemyOne.isDefending = true;
        }


        enemyOne.hadATurn = true;

    }

    public void EnemyAttackChoice()
    {

        

        //For each key-value pair in the units' dictionary of attacks
        foreach (var kvp in enemyOne.enemyAttackDictionary)
        {
            switch (kvp.Value.attackType)
            {
                case AttackType.Special:
                    switch (kvp.Value.attackColor)
                    {
                        case Hue.Red:
                            if (kvp.Value.colorCost <= envManaScript.currentRed && enemyOne.currentStamina >= kvp.Value.staminaCost)
                            {
                                chosenAttack = kvp.Value;
                                canAttack = true;
                            }
                            break;
                        case Hue.Orange:
                            if (kvp.Value.colorCost <= envManaScript.currentOrange && enemyOne.currentStamina >= kvp.Value.staminaCost)
                            {
                                chosenAttack = kvp.Value;
                                canAttack = true;
                            }
                            break;
                        case Hue.Yellow:
                            if (kvp.Value.colorCost <= envManaScript.currentYellow && enemyOne.currentStamina >= kvp.Value.staminaCost)
                            {
                                chosenAttack = kvp.Value;
                                canAttack = true;
                            }
                            break;
                        case Hue.Green:
                            if (kvp.Value.colorCost <= envManaScript.currentGreen && enemyOne.currentStamina >= kvp.Value.staminaCost)
                            {

                                chosenAttack = kvp.Value;
                                canAttack = true;
                            }
                            break;
                        case Hue.Blue:
                            if (kvp.Value.colorCost <= envManaScript.currentBlue && enemyOne.currentStamina >= kvp.Value.staminaCost)
                            {
                                chosenAttack = kvp.Value;
                                canAttack = true;
                            }
                            break;
                        case Hue.Violet:
                            if (kvp.Value.colorCost <= envManaScript.currentViolet && enemyOne.currentStamina >= kvp.Value.staminaCost)
                            {
                                chosenAttack = kvp.Value;
                                canAttack = true;
                            }
                            break;
                        default: 
                            break;
                    }
                    break;
                default:
                    if (enemyOne.currentStamina >= kvp.Value.staminaCost)
                    {
                        chosenAttack = kvp.Value;
                        canAttack = true;
                    }
                    break;
            }

        }
    }

    public void NewBattleStuff()
    {
        
        attackDatabaseScript = FindObjectOfType<AttacksDatabase>();

        unitSpawnerScript = FindObjectOfType<Unit_Spawner>();

        combatFunctionsScript = FindObjectOfType<CombatFunctions>();

        envManaScript = FindObjectOfType<ENV_Mana>();

        enemyOne = unitSpawnerScript.enemyOne;

        player = unitSpawnerScript.player;
    }

}

//TODO: Make the enemy defend when they don't have enough stamina to do a thing.

    