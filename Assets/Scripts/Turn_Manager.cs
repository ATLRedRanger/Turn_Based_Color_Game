using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST}
public class Turn_Manager : MonoBehaviour
{
    public BattleState state;

    public int turnCount;

    public int enemiesAlive;

    public int playersAlive;

    public Unit player;

    public Unit_Spawner unitSpawnerScript;

    public UI ui_Script;

    public List<Unit> turnOrder;

    private CombatFunctions combatFunctionsScript;

    private StatusEffects statusEffectsScript;

    private ENV_Mana envManaScript;

    private Enemy_Combat_Functions enemyFunctionsScript;


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
        Debug.Log("BattleState is " + state);

        StartCoroutine(TimeForBattle());
    }

    IEnumerator TimeForBattle()
    {

        yield return new WaitForSeconds(.05f);

        WhoseTurnIsIt();
        
    }

    public void SortCombatants()
    {
        unitSpawnerScript.listOfCombatants.Sort((y, x) => x.currentSpeed.CompareTo(y.currentSpeed));

       

       for(int i = 0; i < unitSpawnerScript.listOfCombatants.Count; i++)
        {
            if (unitSpawnerScript.listOfCombatants[i].isPlayer == true)
            {
                i ++;
            }
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
        

        

        //Checks if a unit has a status and then applies the appropriate effects
        StatusCheck();
        
        

        //Checks if the combatants are alive
        DeathCheck();
        

        if (unitSpawnerScript.player.myTurn == true)
        {
            
            EnemyTurn();
        }
        else 
        {
            PlayerTurn();
        }

        CombatantsCheck();




        
        //StartCoroutine(WaitForTime());
        ui_Script.UpdateUI();
    }

    public void BeginTurn()
    {
        
        
        if(state == BattleState.PLAYERTURN)
        {
            StartCoroutine(WaitForTime(1.5f));
            combatFunctionsScript.RegenStamina(unitSpawnerScript.player);
            unitSpawnerScript.player.isDefending = false;
            StartCoroutine(WaitForTime(1.5f));
        }
        if(state == BattleState.ENEMYTURN)
        {
            StartCoroutine(WaitForTime(1.5f));
            combatFunctionsScript.RegenStamina(unitSpawnerScript.enemyOne);
            unitSpawnerScript.enemyOne.isDefending = false;
            StartCoroutine(WaitForTime(1.5f));
        }
        
        ui_Script.MenuVisibile();
        ui_Script.UpdateUI();
    }

    public void PlayerTurn()
    {
        if(player.currentHealth > 0)
        {
            state = BattleState.PLAYERTURN;

            unitSpawnerScript.enemyOne.myTurn = false;

            unitSpawnerScript.player.myTurn = true;
        }
        
        if(player.currentHealth < 0)
        {
            state = BattleState.LOST;
            ui_Script._fightButton.SetActive(false);
        }


        turnCount += 1;
        BeginTurn();
    }

    public void EnemyTurn()
    {
        unitSpawnerScript.player.myTurn = false;
        unitSpawnerScript.enemyOne.myTurn = true;
        

        if (unitSpawnerScript.enemyOne.currentHealth > 0)
        {
            state = BattleState.ENEMYTURN;

            unitSpawnerScript.enemyOne.EnemyAi();     
            
        }

        turnCount += 1;
        BeginTurn();
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

    private void StatusCheck()
    {
        //Function for statuses to be applied

        statusEffectsScript.BurningCondition();
    }

    public void PlayerWon()
    {
        state = BattleState.WON;
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

    private void DeathCheck()
    {
        if (unitSpawnerScript.enemyOne.AmIDeadYet())
        {
            turnOrder.Remove(unitSpawnerScript.enemyOne);
            unitSpawnerScript.listOfCombatants.Remove(unitSpawnerScript.enemyOne);
        }
        player.AmIDeadYet();
    }

    public void NewBattle()
    {
        unitSpawnerScript.SelectEnemy();
        envManaScript.StartingLocation();
        enemiesAlive++;

        state = BattleState.START;
        ui_Script.NewBattleStuff();
        enemyFunctionsScript.NewBattleStuff();
        
       
        StartCoroutine(TimeForBattle());
        ui_Script.CloseEndBattle();
        
        
    }


    IEnumerator WaitForTime(float time)
    {
        
        yield return new WaitForSeconds(time);
        
    }
}
