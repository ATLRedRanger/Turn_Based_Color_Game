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


    // Start is called before the first frame update
    void Start()
    {
        unitSpawnerScript = GetComponent<Unit_Spawner>();
        combatFunctionsScript = FindObjectOfType<CombatFunctions>();
        statusEffectsScript = FindObjectOfType<StatusEffects>();
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
        
        
        statusEffectsScript.BurningCondition();

        if (unitSpawnerScript.player.myTurn == true)
        {
            
            EnemyTurn();
        }
        else 
        {
            PlayerTurn();
        }

        CombatantsCheck();
        ui_Script.UpdateUI();
        StartCoroutine(WaitForTime());
        

    }

    public void BeginTurn()
    {
        
        
        if(state == BattleState.PLAYERTURN)
        {
            StartCoroutine(WaitForTime());
            combatFunctionsScript.RegenStamina(unitSpawnerScript.player);
            unitSpawnerScript.player.isDefending = false;
            StartCoroutine(WaitForTime());
        }
        if(state == BattleState.ENEMYTURN)
        {
            StartCoroutine(WaitForTime());
            combatFunctionsScript.RegenStamina(unitSpawnerScript.enemyOne);
            unitSpawnerScript.enemyOne.isDefending = false;
            StartCoroutine(WaitForTime());
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
            EndBattle();
        }
        
        if(playersAlive == 0)
        {
            PlayerLost();
        }
    }

    public void EndBattle()
    {
        state = BattleState.WON;
        ui_Script._fightButton.SetActive(false);
        Debug.Log("Player gains exp, loot and other end battle things happen.");
    }

    public void PlayerLost()
    {
        state = BattleState.LOST;
        ui_Script._fightButton.SetActive(false);
        Debug.Log("Game Over");
    }

    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(1.5f);
        
    }
}
