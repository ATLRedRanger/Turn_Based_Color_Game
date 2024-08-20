using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle_Eye : MonoBehaviour
{
    
    List<Unit_V2> listOfCombatants = new List<Unit_V2>();
    private CombatState theCombatState = CombatState.Active;
    private WhoseTurn whoseTurnIsIt = WhoseTurn.Nobody;
    private Unit_V2 player;
    private Unit_V2 enemyOne;
    private Unit_V2 enemyTwo;
    private int numOfEnemies;
    private int turnsInRound;
    private string currentLocation = "Cave";

    private Unit_Spawner unitSpawnerScript;
    private Environment envManaScript;
    private StatusEffectsDatabase_V2 statusEffectScript;

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(LoadScripts());

        
    }

    public void Test()
    {
        Combat();
    }

    IEnumerator LoadScripts()
    {
        yield return new WaitForSeconds(1);

        unitSpawnerScript = FindObjectOfType<Unit_Spawner>();
        envManaScript = FindObjectOfType<Environment>();
        statusEffectScript = FindObjectOfType<StatusEffectsDatabase_V2>();

        //yield return new WaitForSeconds(1);
        unitSpawnerScript.SpawnPlayer();
        player = unitSpawnerScript.player;
        Debug.Log("Finished Loading");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void SortCombatants(List<Unit_V2> listOfCombatants)
    {
        listOfCombatants.Sort((x, y) => x.GetCurrentSpeed().CompareTo(y.GetCurrentSpeed()));
    }

    private void GenerateEnemies()
    {
        player.gameObject.SetActive(true);
        int enemiesToGenerate = Random.Range(1, 3);
        Debug.Log(enemiesToGenerate);
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
        if (enemyOne != null)
        {
            enemyOne.TakeDamage(5);
            Debug.Log("Enemy One Health " + enemyOne.GetCurrentHp());
        }
        if (enemyTwo != null)
        {
            enemyTwo.TakeDamage(5);
            Debug.Log("Enemy Two Health " + enemyTwo.GetCurrentHp());
        }
    }

    private void EnemyTurn(Unit_V2 unit)
    {
        Debug.Log($"{unit.unitName}'s Turn");
        if (player != null)
        {
            player.TakeDamage(5);
            Debug.Log("Player Health " + player.GetCurrentHp());
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
}
