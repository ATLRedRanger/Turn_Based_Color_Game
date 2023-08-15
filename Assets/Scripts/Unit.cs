using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    //Unit Attributes
    public string unitName;

    public int maxHealth;

    public int currentHealth;

    public int physicalAttack;

    public int physicalDefense;

    public int magicAttack;

    public int magicDefense;

    public int currentSpeed; 
    
    public int maxStamina;
    
    public int currentStamina;

    public int staminaRegenModifier;

    public int baseAccuracy;

    public bool isDefending;

    //Status Effects
    public bool isBurning;
    public int burnTimer = 3;

    public bool isExhausted;


    [SerializeField]
    private int currentLevel;

    //Inventory related variables
    public List<Item> itemList = new List<Item>(); 

    //Equipment related variables
    public Weapon equippedWeapon;
    public bool isWeaponEquipped = false;
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

    public Dictionary<string, Attack> unitSpellsDictionary = new Dictionary<string, Attack>();

    public Dictionary<string, Attack> enemyAttackDictionary = new Dictionary<string, Attack>();

    private IEnumerator coroutine;
    public virtual void Start()
    {

        
        attacksDatabase = FindObjectOfType<AttacksDatabase>();
        enemyCombatScript = FindObjectOfType<Enemy_Combat_Functions>();
        envManaScript = FindObjectOfType<ENV_Mana>();
        turnManagerScript = FindObjectOfType<Turn_Manager>();
        LearnAbilities();
        EnemyAttacks();
        //LearnSpells();
        
    }
    
    //Adding attacks to an attack dictionary
    private void LearnAbilities()
    {
        switch (currentLevel)
        {
            
            case 1:
                unitAttackDictionary["Punch"] = attacksDatabase._punch;
                unitAttackDictionary["Kick"] = attacksDatabase._kick;
                unitAttackDictionary["Fireball"] = attacksDatabase._fireBall;
                unitAttackDictionary["Yellow Splash"] = attacksDatabase._yellowSplash;
                unitAttackDictionary["Orange Spike"] = attacksDatabase._orangeSpike;
                unitAttackDictionary["Blue Crush"] = attacksDatabase._blueCrush;
                unitAttackDictionary["Green Punch"] = attacksDatabase._greenPunch;

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

    public virtual void EnemyAttacks()
    {
        enemyAttackDictionary["Slash"] = attacksDatabase._slash;
    }

    private void LearnSpells()
    {
        unitSpellsDictionary["Fireball"] = attacksDatabase._fireBall;
    } 

    public void CanUseAttack()
    {
        foreach(var kvp in unitAttackDictionary)
        {
            if(kvp.Value.weaponType == equippedWeapon.weaponType && currentStamina >= kvp.Value.staminaCost)
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

    public virtual void EnemyAi()
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

    internal void SetTurnManager(Turn_Manager turnManagerScript)
    {
        throw new NotImplementedException();
    }

    private void OnEnable()
    {
        //Subscribes to the isBurned event
        StatusEffects.isBurned += Burning;
    }

    private void OnDisable()
    {
        //Unsubscribes from the isBurned event
        StatusEffects.isBurned -= Burning;
    }

    //Status Effects
    void Burning()
    {
        //Happens when the event for isBurned is triggered
        if (isBurning)
        {
            currentHealth -= (maxHealth * 1/5);
            burnTimer -= 1;
        }
        if(burnTimer < 1)
        {
            isBurning = false;
        }

    }
}

//TODO: Make the enemy deal damage
//TODO: Make a stamina regen system