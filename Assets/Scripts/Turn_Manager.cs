﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST}
public class Turn_Manager : MonoBehaviour
{
    public BattleState state;

    public int turnIndex;

    public int enemiesAlive;

    public int playersAlive;

    public Unit player;

    public Unit enemyOne;

    public Unit_Spawner unitSpawnerScript;

    public UI ui_Script;

    public List<Unit> turnOrder;

    private CombatFunctions combatFunctionsScript;

    private StatusEffects statusEffectsScript;

    private ENV_Mana envManaScript;

    private Enemy_Combat_Functions enemyFunctionsScript;
    
    public Dictionary<int, Unit> unitReferences = new Dictionary<int, Unit>();


    // Start is called before the first frame update
    void Start()
    {
        unitSpawnerScript = GetComponent<Unit_Spawner>();
        combatFunctionsScript = FindObjectOfType<CombatFunctions>();
        statusEffectsScript = FindObjectOfType<StatusEffects>();
        enemyFunctionsScript = FindObjectOfType<Enemy_Combat_Functions>();
        envManaScript = FindObjectOfType<ENV_Mana>();
        state = BattleState.START;
        player = unitSpawnerScript.player;
        
        enemyOne = unitSpawnerScript.enemyOne;
        Debug.Log("BattleState is " + state);

        StartCoroutine(ControlCenter());
    }

    IEnumerator TimeForBattle()
    {

        yield return new WaitForSeconds(.05f);

        ControlCenter();
        
    }
    
    IEnumerator ControlCenter()
    {
        //TurnIndex is the location in the turnOrder list of the current round.
        //TurnOrder is the sorted list of the combatants by speed.
        //UnitReferences is the dictionary with the key that corresponds to the real objects.
        //TurnOrder is the local copy and UnitReferences is the real objects. 

        //TODO: Add time between events. Add a pause after animations, turns, events, etc.
        while(IsBattleWonOrLost() == false)
        {
            BeginRound();
            turnIndex = 0;
            
            while(turnIndex != -1 && IsBattleWonOrLost() == false)
            {
                
                CheckPlayerTurnIsTrue();
                
                UntapPhase(turnOrder[turnIndex]);
                
                if (state == BattleState.PLAYERTURN)
                {
                    ui_Script.MenuVisibile();
                    ui_Script.UpdateUI();
                    
                    yield return new WaitUntil(() => player.hadATurn == true);
                }
                else
                {
                    
                    ui_Script.MenuVisibile();
                    ui_Script.UpdateUI();
                    unitReferences[turnIndex].enemyCombatScript.EnemyAttacking();

                }
                
                AfterCombatPhase();
                EndTurn();
                turnIndex++;
                if(turnIndex >= turnOrder.Count)
                {
                    turnIndex = 0;
                }
            }
        }

    }

    private bool IsBattleWonOrLost()
    {
        if(state == BattleState.WON || state == BattleState.LOST)
        {
            return true;
        }

        return false;
    }

    private void CheckPlayerTurnIsTrue()
    {
        if (unitReferences[turnIndex].isPlayer == true)
        {
            state = BattleState.PLAYERTURN;
        }
        else
        {
            state = BattleState.ENEMYTURN;
        }
    }
    public void BeginRound() //Uses new turn scheme
    {
        player.hadATurn = false;
        Debug.Log("Begin Turn");
        ui_Script.MenuVisibile();
        ui_Script.UpdateUI();

        WhoseTurnIsIt();

        //UntapPhase();

        Debug.Log("Begin Turn");
    }
    private void UntapPhase(Unit unit)
    {
        Debug.Log("Untap Phase");
        ui_Script.MenuVisibile();
        ui_Script.UpdateUI();

        unitReferences[turnIndex].isDefending = false;

        UnitStaminaRegen();

        StatusEffectsCheck();

        DeathCheck();

        CombatantsCheck();
        
        ui_Script.UpdateUI();
        
        //TODO: Make the turn end when someone dies during this phase. 
        
    }
    public void AfterCombatPhase()
    {
        DeathCheck();

        CombatantsCheck();

        ReduceBuffsAndDebuffs();

        ui_Script.UpdateUI();


    }
    public void EndTurn()
    {
        Debug.Log("End Turn");
        

        if(state == BattleState.PLAYERTURN)
        {
            player.hadATurn = false;
        }
        
        ui_Script.MenuVisibile();
        CleanUpStep();
        
        
    }

    //Untap: State switches to whomever turn it is.




    public void SortCombatants()
    {

        unitSpawnerScript.listOfCombatants.Sort((y, x) => x.currentSpeed.CompareTo(y.currentSpeed));



        for (int i = 0; i < unitSpawnerScript.listOfCombatants.Count; i++)
        {

            turnOrder.Add(unitSpawnerScript.listOfCombatants[i]);
            Debug.Log("TURN ORDER COUNT" + turnOrder.Count);
            unitReferences[i] = unitSpawnerScript.listOfCombatants[i];
            //TODO: Verify this is the right unit
        }

        //BeginTurn();
    }
    void WhoseTurnIsIt()
    {
        turnOrder.Clear();
        SortCombatants();
        
        if (turnOrder[0].isPlayer == true)
        {
            state = BattleState.PLAYERTURN;
        }
        else
        {
            state = BattleState.ENEMYTURN;
        }
        /*if (unitSpawnerScript.listOfCombatants[0].isPlayer == false && enemyOne.hadATurn == false)
        {
            state = BattleState.ENEMYTURN;
            //EnemyTurn();

        }
        else //(unitSpawnerScript.listOfCombatants[0].isPlayer == true && player.hadATurn == false)
        {
            state = BattleState.PLAYERTURN;
            //PlayerTurn();

        }*/
        ui_Script.MenuVisibile();
    }
    private void ReduceBuffsAndDebuffs()
    {
        Debug.Log("Reduce buffs and debuffs");
    }
    public void CombatantsCheck()
    {
        if(enemiesAlive == 0)
        {
            PlayerWon();
            
        }
        
        if(playersAlive == 0)
        {
            PlayerLost();
            
        }
        
    }
    private void DeathCheck()
    {
        //This checks everyone to see if they're dead.
        for(int i = 0; i < turnOrder.Count; i++)
        {
            unitReferences[i].AmIDeadYet();
        }
        
    }

    private void UnitStaminaRegen()
    {
        
        combatFunctionsScript.RegenStamina(unitReferences[turnIndex]);
        
    }
    private void StatusEffectsCheck()
    {
        //Function for statuses to be applied

        //statusEffectsScript.BurningCondition();
        Burning();
    }
    public void CleanUpStep()
    {
        
        DeathCheck();
        CombatantsCheck();
        ui_Script.UpdateUI();
    }
    public void PlayerWon()
    {
        state = BattleState.WON;
        player.GainExperience(enemyOne.expGiven, 10);
        ui_Script.EndBattleUI();
        ui_Script._fightButton.SetActive(false);
        player.DidILevelUp();
        player.DidWeaponLevelUp();
    }
    public void PlayerLost()
    {
        state = BattleState.LOST;
        ui_Script.EndBattleUI();
        ui_Script._fightButton.SetActive(false);

    }
    public void NewBattle()
    {
        unitSpawnerScript.SelectEnemy();
        envManaScript.StartingLocation();
        enemyOne = unitSpawnerScript.enemyOne;


        state = BattleState.START;

        ui_Script.NewBattleStuff();
        enemyFunctionsScript.NewBattleStuff();

        ui_Script.enemyOne = enemyOne;
        StartCoroutine(ControlCenter());

        ui_Script.CloseEndBattle();


        
    }

    //Statuses
    public void Burning()
    {

        int burnDamage = (int)(Mathf.Round(unitReferences[turnIndex].maxHealth / 5));
        
        
        

        //Happens when the event for isBurned is triggered
        if (unitReferences[turnIndex].isBurning)
        {
            //Debug.Log("PLAYER IS BURNING!");
            unitReferences[turnIndex].currentHealth -= burnDamage;

            unitReferences[turnIndex].burnTimer -= 1;
        }
        if (unitReferences[turnIndex].burnTimer < 1)
        {

            unitReferences[turnIndex].isBurning = false;
        }

    }

    IEnumerator WaitForTime(float time)
    {
        
        yield return new WaitForSeconds(time);
        
    }
}
