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

    public int moneyGiven;

    //Status Effects
    public bool isBurning;

    public int burnTimer = 3;

    public bool isExhausted;


    [SerializeField]
    private int currentLevel;

    private int currentExp;

    private int expToLevel;

    public int expGiven;

    //Inventory related variables
    public List<Item> itemList = new List<Item>(); 

    //Equipment related variables
    public Weapon equippedWeapon;

    public bool isWeaponEquipped = false;

    public int currentAxeExp = 0;

    public int currentStaffExp = 0;

    public int currentSwordExp = 0;

    public int currentHammerExp = 0;

    public int currentBowExp = 0;

    private int axeExpToLevel = 50;

    private int staffExpToLevel = 50;

    private int swordExpToLevel = 50;

    private int hammerExpToLevel = 50;

    private int bowExpToLevel = 50;

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
        enemyAttackDictionary["Violet Ball"] = attacksDatabase._violetBall;
    }

    private void LearnSpells()
    {
        switch (currentLevel)
        {
            case 1:
                break;
            case 2:
                break;
            default:
                unitSpellsDictionary["Fireball"] = attacksDatabase._fireBall;
                break;
        }
        
    } 

    public bool AmIDeadYet()
    {
        bool amIDead = false;

        if (currentHealth <= 0 && isPlayer == true)
        {
            currentHealth = 0;
            turnManagerScript.playersAlive --;
            Debug.Log(unitName + "is DEAD!");
            
            amIDead = true;
        }
        
        if(currentHealth < 1 && isPlayer != true)
        {

            turnManagerScript.enemiesAlive --;
            Destroy(this.gameObject);
            amIDead = true;
        }
        
        return amIDead;
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

    private void DidILevelUp()
    {

        if(currentExp >= expToLevel)
        {
            currentLevel++;
            LearnSpells();
            expToLevel += 100; //+/* some modifier or something
        }

    }

    private void DidWeaponLevelUp()
    {
        switch(equippedWeapon.weaponType)
        {
            case WeaponType.Axe:
                if(currentAxeExp >= axeExpToLevel) 
                {
                    axeMastery += 1;
                    axeExpToLevel += 100; //+/* some modifier or something
                    LearnAbilities();
                }
                break;
            case WeaponType.Staff:
                if (currentStaffExp >= staffExpToLevel)
                {
                    staffMastery += 1;
                    staffExpToLevel += 100; //+/* some modifier or something
                    LearnAbilities();
                }
                break;
            case WeaponType.Sword:
                if (currentSwordExp >= swordExpToLevel)
                {
                    swordMastery += 1;
                    swordExpToLevel += 100; //+/* some modifier or something
                    LearnAbilities();
                }
                break;
            case WeaponType.Hammer:
                if (currentHammerExp >= hammerExpToLevel)
                {
                    hammerMastery += 1;
                    hammerExpToLevel += 100; //+/* some modifier or something
                    LearnAbilities();
                }
                break;
            case WeaponType.Bow:
                if (currentBowExp >= bowExpToLevel)
                {
                    bowMastery += 1;
                    bowExpToLevel += 100; //+/* some modifier or something
                    LearnAbilities();
                }
                break;
            default: break;
        }
    }
}

//TODO: Make the enemy deal damage
//TODO: Make a stamina regen system