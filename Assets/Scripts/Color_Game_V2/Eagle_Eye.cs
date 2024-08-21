using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle_Eye : MonoBehaviour
{
    private List<object> statusDamageQue = new List<object>();
    private List<Unit_V2> listOfCombatants = new List<Unit_V2>();
    private CombatState theCombatState = CombatState.Active;
    private WhoseTurn whoseTurnIsIt = WhoseTurn.Nobody;
    [SerializeField]
    private Unit_V2 player;
    private Unit_V2 enemyOne;
    private Unit_V2 enemyTwo;
    private int numOfEnemies;
    private int turnsInRound;
    private string currentLocation = "Cave";

    private Attack chosenAttack = null;

    //Scripts
    private Unit_Spawner unitSpawnerScript;
    private Environment envManaScript;
    private StatusEffectsDatabase_V2 statusEffectScript;
    private Attack_Database attackDatabaseScript;
    private ButtonsAndPanels buttonsAndPanelsScript;
    private UI_V2 uiScript;

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(LoadScripts());

        
    }

    public void Test()
    {
        //player.AddAttackToDictionary(attackDatabaseScript._fireball);
        //player.GetAttackDictionary();
        //player.TakeDamage(7);
        
        CheckAttack_StatusBuildupRelationship(player.GetAttackDictionary()["Fireball"], enemyOne);
        CheckStatuses(listOfCombatants);
        EndOfRoundStatusDamage();
    }

    IEnumerator LoadScripts()
    {
        yield return new WaitForSeconds(1);

        unitSpawnerScript = FindObjectOfType<Unit_Spawner>();
        envManaScript = FindObjectOfType<Environment>();
        statusEffectScript = FindObjectOfType<StatusEffectsDatabase_V2>();
        attackDatabaseScript = FindObjectOfType<Attack_Database>();
        buttonsAndPanelsScript = FindObjectOfType<ButtonsAndPanels>();
        uiScript = FindObjectOfType<UI_V2>();

        //yield return new WaitForSeconds(1);
        //unitSpawnerScript.SpawnPlayer();
        player = unitSpawnerScript.SpawnPlayer();
        enemyOne = unitSpawnerScript.GenerateEnemy(0);
        listOfCombatants.Add(enemyOne);
        Debug.Log("Finished Loading");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void SortCombatants(List<Unit_V2> listOfCombatants)
    {
        listOfCombatants.Sort((y, x) => x.GetCurrentSpeed().CompareTo(y.GetCurrentSpeed()));
    }

    private void GenerateEnemies()
    {
        player.gameObject.SetActive(true);
        int enemiesToGenerate = Random.Range(1, 3);
        Debug.Log($"Generated Enemies: {enemiesToGenerate}");
        for(int i = 0; i < enemiesToGenerate; i++) 
        {
            switch (i)
            {
                case 0:
                    enemyOne = unitSpawnerScript.GenerateEnemy(0);
                    Debug.Log($"EnemyOne is {enemyOne.unitName}");
                    listOfCombatants.Add(enemyOne);

                    break;
                case 1:
                    enemyTwo = unitSpawnerScript.GenerateEnemy(1);
                    Debug.Log($"EnemyTwo is {enemyTwo.unitName}");
                    listOfCombatants.Add(enemyTwo);

                    break;
                default:
                    break;
            }
        }
        
    }

    public void Combat()
    {
        GenerateEnemies();
        List<Unit_V2> deadUnits = new List<Unit_V2>();
        theCombatState = CombatState.Active;
        listOfCombatants.Add(player);
        Debug.Log($"List Of Combatants: {listOfCombatants.Count}");
        int currentRound = 0;

        while (theCombatState == CombatState.Active)
        {
            currentRound++;
            Debug.Log($"Current Round: {currentRound}");

            SortCombatants(listOfCombatants);

            foreach (Unit_V2 unit in listOfCombatants)
            {
                Debug.Log($"Current Unit: {unit.unitName}");
                if (unit is Player_V2)
                {
                    whoseTurnIsIt = WhoseTurn.Player;
                    buttonsAndPanelsScript.ToggleFightPanel();
                    PlayerTurn();
                    if (IsPlayerAlive(player))
                    {
                        Debug.Log("Player is alive");
                    }
                    else
                    {
                        theCombatState = CombatState.Lost;
                        break;
                    }
                }
                else if (unit is EnemyUnit_V2)
                {
                    if (unit.GetCurrentHp() < 1)
                    {
                        Debug.Log($"Adding {unit} to DeadUnitList");
                        deadUnits.Add(unit);
                        
                    }
                    else
                    {
                        EnemyTurn(unit);
                        if (IsPlayerAlive(player))
                        {
                            Debug.Log("Player is alive");
                        }
                        else
                        {
                            theCombatState = CombatState.Lost;
                            break;
                        }
                    }
                    
                }
            }
            foreach (Unit_V2 unit in listOfCombatants)
            {
                if ( unit is EnemyUnit_V2 && !deadUnits.Contains(unit) && unit.GetCurrentHp() < 1)
                {
                   deadUnits.Add(unit);
                }
            }
            if (listOfCombatants.Count - deadUnits.Count == 1)
            {
                theCombatState = CombatState.Won;
                break;
            }
            //End of turn stuff
            if (IsPlayerAlive(player))
            {
                Debug.Log("Player is alive");
            }
            else
            {
                theCombatState = CombatState.Lost;
                break;
            }
        }
        if (theCombatState == CombatState.Won)
        {
            PlayerWon();
        }
        else
        {
            PlayerLost();
        }
    }
    
    private bool IsPlayerAlive(Unit_V2 player)
    {
        if (player.GetCurrentHp() > 1)
        {
            return true;
        }

        return false;
    }

    private void PlayerTurn()
    {
        Debug.Log("Player Turn");
        /*
        if (enemyOne != null)
        {
            enemyOne.TakeDamage(5);
            
        }
        if (enemyTwo != null)
        {
            enemyTwo.TakeDamage(5);
            
        }*/
        //StartCoroutine(WaitForAttackChoice());
    }

    private void CheckAttack_StatusBuildupRelationship(Attack attack, Unit_V2 defender)
    {
        switch(attack.attackBehavior)
        {
            case AttackBehavior.Burn:
                defender.AddToBurnAmount(1);
                if (defender.DoesStatusExist(statusEffectScript.burn))
                {
                    foreach (StatusEffect_V2 status in defender.unitStatusEffects)
                    {
                        if (status.GetStatusName() == "Burn")
                        {
                            status.effectStack += 1;
                            Debug.Log($"{defender.unitName}'s burn stack: {status.effectStack}.");
                        }
                    }
                }
                else
                {
                    if (defender.GetBurnAmount() >= defender.GetBurnThreshhold())
                    {
                        Debug.Log($"{defender.unitName} is now burning!");
                        defender.AddStatus(statusEffectScript.burn);
                        defender.SetBurnAmountToZero();
                    }
                }
                
                break;
        }
    }

    private void CheckStatuses(List<Unit_V2> listOfCombatants)
    {
        int i = 0;
        List<StatusEffect_V2> removeStatus = new List<StatusEffect_V2>();

        foreach (Unit_V2 unit in listOfCombatants)
        {
            foreach (StatusEffect_V2 status in unit.unitStatusEffects)
            {
                Debug.Log("First Foreach");
                switch (status.GetStatusName())
                {
                    case "Burn":
                        Debug.Log($"{unit.unitName}'s burnTimer: {unit.GetBurnTimer()}");
                        if (unit.GetBurnTimer() >= status.GetEffectLength())
                        {
                            Debug.Log("Testing");
                            unit.SetBurnTimerToZero();
                            removeStatus.Add(status);
                        }
                        else
                        {
                            statusDamageQue.Add(unit);
                            statusDamageQue.Add(status.GetStatusDamage());
                            unit.AddToBurnTimer(1);
                            i++;
                        }
                        break;
                }

            }
            foreach (StatusEffect_V2 status in removeStatus)
            {
                Debug.Log("Second Foreach");
                Debug.Log($"Trying to remove {status.GetStatusName()}");
                if (unit.DoesStatusExist(status))
                {
                    Debug.Log($"{status.GetStatusName()} has been removed.");
                    unit.unitStatusEffects.Remove(status);
                }
            }
            Debug.Log(unit.unitStatusEffects.Count);
            removeStatus.Clear();
        }
    }

    public void EndOfRoundStatusDamage()
    {
        Unit_V2 unit = null;
        int damage = 0;
        //StatusEffect_V2 status = null;
        if (statusDamageQue.Count != 0)
        {
            foreach(object obj in statusDamageQue)
            {
                if (obj is Unit_V2)
                {
                    unit = (Unit_V2)obj;
                }

                if (obj is int)
                {
                    damage = (int)obj;
                }

                unit.TakeDamage(damage);
                unit = null;
                damage = 0;
            }
        }
    }

    IEnumerator WaitForAttackChoice()
    {
        Debug.Log("Start");

        yield return new WaitUntil(AttackIsChosen);

        Debug.Log($"Chosen Attack: {chosenAttack.attackName}");
    }
    private void EnemyTurn(Unit_V2 unit)
    {
        Debug.Log($"{unit.unitName}'s Turn");
        if (player != null)
        {
            player.TakeDamage(5);
            
        }
    }

    private void PlayerWon()
    {
        Debug.Log("Player Won");
    }
    private void PlayerLost()
    {
        Debug.Log("Player Lost");
    }

    private void GenerateEnvironment()
    {
        currentLocation = "Forest";
        envManaScript.GenerateEnvironment(currentLocation);
    }

    private void SetMaxColorAmounts()
    {
        uiScript.SetMaxColorAmounts(envManaScript.currentRed, envManaScript.currentOrange, envManaScript.currentYellow,
                                        envManaScript.currentGreen, envManaScript.currentBlue, envManaScript.currentViolet);
    }

    private void UpdateEnvironmentColors()
    {
        uiScript.UpdateEnvironmentColors(envManaScript.currentRed, envManaScript.currentOrange, envManaScript.currentYellow,
                                        envManaScript.currentGreen, envManaScript.currentBlue, envManaScript.currentViolet);
    }

    public void AttackChangeNotification(string attack)
    {
        switch(attack)
        {
            case "Fireball":
                chosenAttack = attackDatabaseScript._fireball;
                break;
        }
    }

    private bool AttackIsChosen()
    {
        if(chosenAttack != null)
        {
            return true;
        }
        return false;
    }
}
