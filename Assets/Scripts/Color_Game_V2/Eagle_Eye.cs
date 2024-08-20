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
        GenerateEnemies();
    }

    IEnumerator LoadScripts()
    {
        yield return new WaitForSeconds(1);

        unitSpawnerScript = FindObjectOfType<Unit_Spawner>();
        envManaScript = FindObjectOfType<Environment>();
        statusEffectScript = FindObjectOfType<StatusEffectsDatabase_V2>();

        yield return new WaitForSeconds(1);
        unitSpawnerScript.SpawnPlayer();
        player = unitSpawnerScript.player;

        
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
        
        int enemiesToGenerate = Random.Range(1, 2);
        Debug.Log(enemiesToGenerate);
        switch(enemiesToGenerate)
        {
            case 1:
                enemyOne = unitSpawnerScript.GenerateEnemy(0);
                Debug.Log($"EnemyOne is {enemyOne.unitName}");
                listOfCombatants.Add(enemyOne);
                
                break;
            case 2:
                enemyTwo = unitSpawnerScript.GenerateEnemy(1);
                Debug.Log($"EnemyOne is {enemyOne.unitName}");
                listOfCombatants.Add(enemyTwo);
                
                break;
            default:
                break;
        }
    }

    public void Combat()
    {
        GenerateEnemies();
        Debug.Log($"List Of Combatants: {listOfCombatants.Count}");
        theCombatState = CombatState.Active;
        listOfCombatants.Add(player);

        int currentRound = 0;

        while (theCombatState == CombatState.Active)
        {
            currentRound++;

            SortCombatants(listOfCombatants);

            foreach (Unit_V2 unit in listOfCombatants)
            {
                if (unit is Player_V2)
                {
                    //PlayerTurn(unit)
                    if (IsPlayerAlive(player))
                    {
                        continue;
                    }
                    else
                    {
                        theCombatState = CombatState.Lost;
                        break;
                    }
                }
                else if (unit is EnemyUnit_V2)
                {
                     //EnemyTurn(unit)
                     if (IsPlayerAlive(player))
                     {
                         continue;
                     }
                     else
                     {
                         theCombatState = CombatState.Lost;
                         break;
                     }
                }
                   
            }

            if (listOfCombatants.Count == 1)
            {
                theCombatState = CombatState.Won;
                break;
            }
            //End of turn stuff
            if (IsPlayerAlive(player))
            {
                continue;
            }
            else
            {
                theCombatState = CombatState.Lost;
                break;
            }
            
           
        }

        if (theCombatState == CombatState.Won)
        {
            //PlayerWon()
        }
        else
        {
            //PlayerLost()
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

}
