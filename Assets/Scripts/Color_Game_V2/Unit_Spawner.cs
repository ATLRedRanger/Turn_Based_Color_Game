using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Unit_Spawner : MonoBehaviour
{
    public List<Unit_V2> listOfEnemies;

    public List<GameObject> enemyPositions;

    public GameObject playerPosition;

    public Unit_V2 player;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    public Unit_V2 SpawnPlayer()
    {
        var playerObj = Instantiate(player, playerPosition.transform).GetComponent<Unit_V2>();
        return playerObj;
    }


    //Input - Position to spawn something in. 
    //Output - We want a unit. 
    //Logic - whatEnemy is what enemy we're trying to spawn in a list of possible enemies. 
    //We're instantiating a blank enemy from the list of enemies [specific enemy], at this position that is being
    //given the input from the for loop and getting the unit data at the end. 
    public Unit_V2 GenerateEnemy(int enemyPosition)
    {

        int whatEnemy = Random.Range(0, listOfEnemies.Count);

        Unit_V2 enemy = (Instantiate(listOfEnemies[whatEnemy], enemyPositions[enemyPosition].transform).GetComponent<Unit_V2>());
        Debug.Log(enemy.unitName);

        return enemy;
    }




}
