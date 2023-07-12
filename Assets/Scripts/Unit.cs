using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;

    public int maxHealth;

    public int currentHealth;

    public int baseAttack;

    public int baseDefense;

    public int currentSpeed; 
    
    public int maxStamina;
    
    public int currentStamina;
    [SerializeField]
    private int currentLevel;
    
    public bool isPlayer;

    public bool hadATurn;

    public bool myTurn;

    public GameObject gameOrganizer;

    public AttacksDatabase attacksDatabase;

    public Turn_Manager turnManagerScript;

    public Dictionary<string, Attack> playerAttackDictionary = new Dictionary<string, Attack>();

    private IEnumerator coroutine;
    void Start()
    {

        
        attacksDatabase = FindObjectOfType<AttacksDatabase>();
        LearnAttacks();
        
    }

    public void SetTurnManager(Turn_Manager t)
    {
       turnManagerScript = t;
    }
    
    //Adding attacks to an attack dictionary
    private void LearnAttacks()
    {
        switch (currentLevel)
        {
            
            case 1:
                playerAttackDictionary["Punch"] = attacksDatabase._punch;
                playerAttackDictionary["Kick"] = attacksDatabase._kick;
                break;
            case 3:
                playerAttackDictionary.Add("Chop", attacksDatabase._chop);

                break;
        }

    }

    public void AmIDeadYet()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log(unitName + "is DEAD!");
        }
        
             
    }

    public void EnemyAi()
    {
        Debug.Log("ENEMY DID A THING");

        coroutine = Waiting(2.0f);
        StartCoroutine(coroutine);
        
        
    }
   
    IEnumerator Waiting(float waitTime)
    {
        
        
        yield return new WaitForSeconds(waitTime);
        
        turnManagerScript.PlayerTurn();
    }
}

//TODO: MAKE THE ENEMY TAKE DAMAGE