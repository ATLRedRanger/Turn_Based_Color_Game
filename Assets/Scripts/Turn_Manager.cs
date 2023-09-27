using JetBrains.Annotations;
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

    public Unit enemyOne;

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
        
        enemyOne = unitSpawnerScript.enemyOne;
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



        for (int i = 0; i < unitSpawnerScript.listOfCombatants.Count; i++)
        {

            if (unitSpawnerScript.listOfCombatants[i].isPlayer == true)
            {
                i++;
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
        if (player.hadATurn != true)
        {
            PlayerTurn();

        }
        else
        {
            EnemyTurn();
        }
    }
    public void BeginTurn()
    {


        if (state == BattleState.PLAYERTURN)
        {
            StartCoroutine(WaitForTime(1.5f));
            combatFunctionsScript.RegenStamina(player);
            player.isDefending = false;
            StartCoroutine(WaitForTime(1.5f));
        }
        if (state == BattleState.ENEMYTURN)
        {
            StartCoroutine(WaitForTime(1.5f));
            combatFunctionsScript.RegenStamina(enemyOne);
            enemyOne.isDefending = false;
            StartCoroutine(WaitForTime(1.5f));
        }

        ui_Script.MenuVisibile();
        ui_Script.UpdateUI();
    }
    public void EndTurn()
    {

        CleanUpStep();

        if (player.myTurn == true)
        {

            EnemyTurn();
        }
        else
        {
            PlayerTurn();
        }

        CleanUpStep();
    }
    public void CleanUpStep()
    {
        StatusCheck();
        DeathCheck();
        CombatantsCheck();
        ui_Script.UpdateUI();
    }
    private void StatusCheck()
    {
        //Function for statuses to be applied

        //statusEffectsScript.BurningCondition();
        Burning();
    }
    private void DeathCheck()
    {
        

        if (enemyOne.AmIDeadYet())
        {
            turnOrder.Clear();
            //unitSpawnerScript.listOfCombatants.Remove(enemyOne);
            
        }

        player.AmIDeadYet();
    }
    public void PlayerTurn()
    {
        if (player.currentHealth > 0)
        {
            state = BattleState.PLAYERTURN;

            enemyOne.myTurn = false;

            player.myTurn = true;
        }

        if (player.currentHealth < 0)
        {
            Debug.Log("PLAYER DEAD");
            state = BattleState.LOST;
            playersAlive--;
            ui_Script._fightButton.SetActive(false);
        }
        

        turnCount += 1;
        BeginTurn();
    }
    public void EnemyTurn()
    {
        if(enemyOne != null)
        {
            player.myTurn = false;
            enemyOne.myTurn = true;


            if (enemyOne.currentHealth > 0)
            {
                state = BattleState.ENEMYTURN;

                enemyOne.EnemyAi();

            }

            turnCount += 1;
            BeginTurn();
        }
        
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


        StartCoroutine(TimeForBattle());

        ui_Script.CloseEndBattle();

        turnCount = 0;
    }
    
    



    public void Burning()
    {

        int playerBurnDamage = (int)(Mathf.Round(player.maxHealth / 5));
        int enemyOneBurnDamage = (int)(Mathf.Round(enemyOne.maxHealth / 5));
        //Happens when the event for isBurned is triggered
        if (player.isBurning)
        {
            //Debug.Log("PLAYER IS BURNING!");
            player.currentHealth -= playerBurnDamage;

            player.burnTimer -= 1;
        }
        if (player.burnTimer < 1)
        {

            player.isBurning = false;
        }
        if (enemyOne.isBurning)
        {
            Debug.Log("ENEMY IS BURNING!");
            enemyOne.currentHealth -= enemyOneBurnDamage;

            enemyOne.burnTimer -= 1;
        }
        if (enemyOne.burnTimer < 1)
        {
            Debug.Log("ENEMY IS NOT BURNING!");
            enemyOne.isBurning = false;
        }

    }

    IEnumerator WaitForTime(float time)
    {
        
        yield return new WaitForSeconds(time);
        
    }
}
