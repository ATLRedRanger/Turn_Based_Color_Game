﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    //Unit Attributes
    public string unitName;

    public int maxHealth;

    public int currentHealth;

    public int baseAttack;

    public int baseDefense;

    public int currentSpeed; 
    
    public int maxStamina;
    
    public int currentStamina;

    public int staminaRegenModifier;

    public int baseAccuracy;

    [SerializeField]
    private int currentLevel;

    //Inventory related variables
    public List<Item> itemList = new List<Item>(); 

    //Equipment related variables
    public Weapon equippedWeapon;
    public int axeMastery;
    public int staffMastery;
    public int swordMastery;
    public int hammerMastery;
    public int bowMastery;

    //Turn related variables
    public bool isPlayer;

    public bool hadATurn;

    public bool myTurn;

    public GameObject gameOrganizer;

    //Scripts
    public AttacksDatabase attacksDatabase;

    public Turn_Manager turnManagerScript;

    public Enemy_Combat_Functions enemyCombatScript;

    public ENV_Mana envManaScript;

    //Dictionaries
    public Dictionary<string, Attack> unitAttackDictionary = new Dictionary<string, Attack>();


    private IEnumerator coroutine;
    void Start()
    {

        
        attacksDatabase = FindObjectOfType<AttacksDatabase>();
        enemyCombatScript = FindObjectOfType<Enemy_Combat_Functions>();
        envManaScript = FindObjectOfType<ENV_Mana>();
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
                unitAttackDictionary["Punch"] = attacksDatabase._punch;
                unitAttackDictionary["Kick"] = attacksDatabase._kick;
                unitAttackDictionary["Fireball"] = attacksDatabase._fireBall;
                break;
        }
        switch(axeMastery)
        {
            case 1:
                unitAttackDictionary["Chop"] = attacksDatabase._chop;
                break;
        }
        switch (staffMastery)
        {
            case 1:
                unitAttackDictionary["Violet Ball"] = attacksDatabase._violetBall;
                break;
        }
        switch (swordMastery)
        {
            case 1:
                unitAttackDictionary["Slash"] = attacksDatabase._slash;
                break;
        }
        switch (hammerMastery)
        {
            case 1:
                unitAttackDictionary["Slam"] = attacksDatabase._slam;
                break;
        }
        switch (bowMastery)
        {
            case 1:
                unitAttackDictionary["Quick Shot"] = attacksDatabase._quickShot;
                break;
        }
    }

    public void CanUseAttack()
    {
        foreach(var kvp in unitAttackDictionary)
        {
            if(kvp.Value.attackType == equippedWeapon.weaponType && currentStamina >= kvp.Value.staminaCost)
            {

            }
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
        
        

        enemyCombatScript.EnemyAttacking();

        coroutine = Waiting(2.0f);
        StartCoroutine(coroutine);
        
        
    }
   
    IEnumerator Waiting(float waitTime)
    {
        
        
        yield return new WaitForSeconds(waitTime);
        
        turnManagerScript.PlayerTurn();
    }
}

//TODO: Make the enemy deal damage
//TODO: Make a stamina regen system