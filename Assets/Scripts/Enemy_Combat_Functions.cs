using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Linq;


public class Enemy_Combat_Functions : MonoBehaviour
{
    //Scripts
    public AttacksDatabase attackDatabaseScript;

    public Unit_Spawner unitSpawnerScript;

    public CombatFunctions combatFunctionsScript;

    private Turn_Manager turnManagerScript;

    public ENV_Mana envManaScript;

    public UI uiScript;

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

        uiScript = FindObjectOfType<UI>();

        enemyOne = unitSpawnerScript.enemyOne;

        player = unitSpawnerScript.player;


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnemyAttacking()
    {
        //Debug.Log("ENEMY IS ATTACKING");
        EnemyAttackDecision();
        if (enemyOne.isDefending != true)
        {
            if (combatFunctionsScript.DidAttackHit(chosenAttack, enemyOne) == true)
            {
                uiScript.PlayAttackAnimation(chosenAttack, player);
                combatFunctionsScript.CheckForSpecialWeaponProperties(enemyOne);
                combatFunctionsScript.CheckForAttackAbilities(chosenAttack, player);
                combatFunctionsScript.PotentialDamage(chosenAttack, enemyOne);
                combatFunctionsScript.CheckForCrit(enemyOne);
                combatFunctionsScript.DamageAfterArmorandRes(chosenAttack, player);
                combatFunctionsScript.DamageAfterStatusCheck(player);
                combatFunctionsScript.ReduceHealthAndStaminaOfDefender(chosenAttack, enemyOne, player);
                uiScript.FloatingNumbersText(player, chosenAttack);
                combatFunctionsScript.ReduceStamina(chosenAttack, enemyOne);
                combatFunctionsScript.ReduceColorFromEnv(chosenAttack);
                combatFunctionsScript.ColorReturn(chosenAttack);
            }
        }
        
        
        enemyOne.hadATurn = true;

    }

    private void EnemyAttackDecision()
    {
        //Made an empty list of attacks
        //Loops through the enemy's attack dictionary and adds those attacks to the list
        //Shuffles the list
        //Loops through the shuffled list
        //If the attack is usable, break out of the loop to use the attack
        List<Attack> enemyAttackList = new List<Attack>();

        foreach (var kvp in enemyOne.enemyAttackDictionary)
        {
            enemyAttackList.Add(kvp.Value);
        }

        List<Attack> shuffledList = enemyAttackList.OrderBy(x => Random.value).ToList();

        foreach (var attack in shuffledList)
        {
            if (uiScript.IsAttackUsable(attack))
            {
                chosenAttack = attack;
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

    