using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Spawner : MonoBehaviour
{
    public List<Unit> listOfEnemies;
    [SerializeField]
    private Unit playerUnit;

    public List<GameObject> enemyPositions;

    public GameObject playerPosition;

    public List<Unit> listOfCombatants;

    public Unit player;

    public Unit enemyOne;

    public Unit chosenEnemy;

    public Turn_Manager turnManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        turnManagerScript = GetComponent<Turn_Manager>();
        SpawnPlayer();
        SelectEnemy();
        

    }






    public void SpawnPlayer()
    {
        player = Instantiate(playerUnit, playerPosition.transform).GetComponent<Unit>();
        
        listOfCombatants.Add(player);
        
    }
    void SelectEnemy()
    {


        int numberofEnemiesToSpawn = 1;//Random.Range(1, enemyPositions.Count);



        for (int i = 0; i < numberofEnemiesToSpawn; i++)
        {
            listOfCombatants.Add(GenerateEnemy(i));
            
        }



        /*for (int i =0; i < listOfAliveEnemies.Count; i++)
        {
            
            Debug.Log(listOfAliveEnemies[i].unitName);
        }*/

    }


    //Input - Position to spawn something in. 
    //Output - We want a unit. 
    //Logic - whatEnemy is what enemy we're trying to spawn in a list of possible enemies. 
    //We're instantiating a blank enemy from the list of enemies [specific enemy], at this position that is being
    //given the input from the for loop and getting the unit data at the end. 
    Unit GenerateEnemy(int enemyPosition)
    {

        int whatEnemy = Random.Range(0, listOfEnemies.Count);

        enemyOne = (Instantiate(listOfEnemies[whatEnemy], enemyPositions[enemyPosition].transform).GetComponent<Unit>());
        enemyOne.SetTurnManager(turnManagerScript);
        

        return enemyOne;
    }

}
