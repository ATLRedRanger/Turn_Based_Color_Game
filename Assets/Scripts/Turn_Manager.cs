﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST}
public class Turn_Manager : MonoBehaviour
{
    public BattleState state;

    public int turnCount;

    public Unit_Spawner unitSpawnerScript;

    public UI ui_Script;

    public List<Unit> turnOrder;

    private CombatFunctions combatFunctionsScript;


    // Start is called before the first frame update
    void Start()
    {
        unitSpawnerScript = GetComponent<Unit_Spawner>();
        combatFunctionsScript = FindObjectOfType<CombatFunctions>();

        state = BattleState.START;
        Debug.Log("BattleState is " + state);

        StartCoroutine(TimeForBattle());
    }

    IEnumerator TimeForBattle()
    {

        yield return new WaitForSeconds(2);

        WhoseTurnIsIt();
        
    }

    public void SortCombatants()
    {
       unitSpawnerScript.listOfCombatants.Sort((y, x) => x.currentSpeed.CompareTo(y.currentSpeed));

       for(int i = 0; i < unitSpawnerScript.listOfCombatants.Count; i++)
        {
            turnOrder.Add(unitSpawnerScript.listOfCombatants[i]);
            
        }
        
       
    }

    void WhoseTurnIsIt()
    {
        
        //Sorting the list of enemies by their speed stat
        SortCombatants();
        
        
        if (unitSpawnerScript.listOfCombatants[0].isPlayer == false)
        {
            
            EnemyTurn();
            
        }
        else
        {
            
            PlayerTurn();
            
        }
    }
    
    public void TurnOrderCheck()
    {
        if (unitSpawnerScript.player.hadATurn != true)
        {
            PlayerTurn();

        }
        else
        {
            EnemyTurn();
        }
    }


    public void EndTurn()
    {
        

        if (unitSpawnerScript.player.myTurn == true)
        {
            EnemyTurn();
        }
        else 
        {
            PlayerTurn();
        }

        ui_Script.UpdateUI();

    }

    public void BeginTurn()
    {
        
        Debug.Log("Beginning The Turn");
        if(state == BattleState.PLAYERTURN)
        {
            combatFunctionsScript.RegenStamina(unitSpawnerScript.player);
            Debug.Log(unitSpawnerScript.player.currentStamina + " APPLE");
            
        }
        if(state == BattleState.ENEMYTURN)
        {
            combatFunctionsScript.RegenStamina(unitSpawnerScript.enemyOne);
            
        }
        ui_Script.UpdateUI();
    }

    public void PlayerTurn()
    {
        state = BattleState.PLAYERTURN;
        BeginTurn();

        Debug.Log("PLAYER TURN");

        unitSpawnerScript.enemyOne.myTurn = false;
        unitSpawnerScript.player.myTurn = true;
        ui_Script.MenuVisibile();
        
        turnCount += 1;
    }

    public void EnemyTurn()
    {
        state = BattleState.ENEMYTURN;
        BeginTurn();

        Debug.Log("ENEMY TURN");
        
        unitSpawnerScript.player.myTurn = false;
        unitSpawnerScript.enemyOne.myTurn = true;
        unitSpawnerScript.enemyOne.EnemyAi();
        ui_Script.MenuVisibile();
        turnCount += 1;
    }


}
