using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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

    public Animator unitAnimator;

    //Status Effects
    public List<Statuses> statusEffects = new List<Statuses>();

    //Burning
    public bool isBurning;
    public int burnTimer = 3;
    public bool isExhausted;
    //Stunned
    public bool isStunned;
    public int stunnedTimer = 3;
    public int stunnedMaxStamina;
    public int OgStamina;
    //Vampped: Had to make this a status effect because of the way the attack class is written
    public bool isVampped;
    

    [SerializeField] private int currentLevel;

    public int currentExp;

    [SerializeField] private int expToLevel;

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

    public Unit_Spawner unit_SpawnerScript;

    //Dictionaries
    public Dictionary<string, Attack> unitAttackDictionary = new Dictionary<string, Attack>();

    public Dictionary<string, Attack> unitSpellsDictionary = new Dictionary<string, Attack>();

    public Dictionary<string, Attack> enemyAttackDictionary = new Dictionary<string, Attack>();

    public SpriteRenderer spriteRenderer;

    private IEnumerator coroutine;
    public virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        attacksDatabase = FindObjectOfType<AttacksDatabase>();
        enemyCombatScript = FindObjectOfType<Enemy_Combat_Functions>();
        envManaScript = FindObjectOfType<ENV_Mana>();
        turnManagerScript = FindObjectOfType<Turn_Manager>();
        unit_SpawnerScript = FindObjectOfType<Unit_Spawner>();
        LearnAbilities();
        EnemyAttacks();
        //LearnSpells();
        OgStamina = maxStamina;
        stunnedMaxStamina = maxStamina / 2;
        
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
        switch (axeMastery)
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
        
        enemyAttackDictionary["FireBall"] = attacksDatabase._fireBall;
        enemyAttackDictionary["Chop"] = attacksDatabase._chop;
        enemyAttackDictionary["Violet Ball"] = attacksDatabase._violetBall;
        enemyAttackDictionary["Orange Spike"] = attacksDatabase._orangeSpike;
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
                unitAttackDictionary["Fireball"] = attacksDatabase._fireBall;
                break;
        }

    }

    public bool AmIDeadYet()
    {
        bool amIDead = false;

        if (currentHealth <= 0 && isPlayer == true)
        {

            turnManagerScript.playersAlive--;
            Debug.Log(unitName + "is DEAD!");

            amIDead = true;
        }

        if (currentHealth < 1 && isPlayer != true && turnManagerScript.enemiesAlive >= 1)
        {
            Debug.Log("ENEMY HAS DIED");
            turnManagerScript.enemiesAlive--;
            unit_SpawnerScript.listOfCombatants.Remove(this);

            Destroy(this.gameObject);
            amIDead = true;
        }

        return amIDead;
    }

    public virtual void EnemyAi()
    {



        enemyCombatScript.EnemyAttacking();




    }

    /*IEnumerator Waiting(float waitTime)
    {
        
        
        yield return new WaitForSeconds(waitTime);
        
        turnManagerScript.PlayerTurn();
    }*/



    /*private void OnEnable()
    {
        //Subscribes to the isBurned event
        StatusEffects.isBurned += Burning;
    }

    private void OnDisable()
    {
        //Unsubscribes from the isBurned event
        StatusEffects.isBurned -= Burning;
    }*/

    //Status Effects


    public void DidILevelUp()
    {

        if (currentExp >= expToLevel)
        {
            Debug.Log("You've gained a new level!");
            currentLevel++;
            LearnSpells();
            expToLevel += 100; //+/* some modifier or something
            currentExp = 0;
        }

    }

    public void DidWeaponLevelUp()
    {
        switch (equippedWeapon.weaponType)
        {
            case WeaponType.Axe:
                if (currentAxeExp >= axeExpToLevel)
                {
                    Debug.Log("You've gained a new Axe level!");
                    axeMastery += 1;
                    axeExpToLevel += 100; //+/* some modifier or something
                    LearnAbilities();
                    currentAxeExp = 0;
                }
                break;
            case WeaponType.Staff:
                if (currentStaffExp >= staffExpToLevel)
                {
                    Debug.Log("You've gained a new Staff level!");
                    staffMastery += 1;
                    staffExpToLevel += 100; //+/* some modifier or something
                    LearnAbilities();
                    currentStaffExp = 0;
                }
                break;
            case WeaponType.Sword:
                if (currentSwordExp >= swordExpToLevel)
                {
                    Debug.Log("You've gained a new Sword level!");
                    swordMastery += 1;
                    swordExpToLevel += 100; //+/* some modifier or something
                    LearnAbilities();
                    currentSwordExp = 0;
                }
                break;
            case WeaponType.Hammer:
                if (currentHammerExp >= hammerExpToLevel)
                {
                    Debug.Log("You've gained a new Hammer level!");
                    hammerMastery += 1;
                    hammerExpToLevel += 100; //+/* some modifier or something
                    LearnAbilities();
                    currentHammerExp = 0;
                }
                break;
            case WeaponType.Bow:
                if (currentBowExp >= bowExpToLevel)
                {
                    Debug.Log("You've gained a new Bow level!");
                    bowMastery += 1;
                    bowExpToLevel += 100; //+/* some modifier or something
                    LearnAbilities();
                    currentBowExp = 0;
                }
                break;
            default: break;
        }
    }

    public void GainExperience(int enemyExp, int weaponExp)
    {
        currentExp += enemyExp;
        switch (equippedWeapon.weaponType)
        {
            case WeaponType.Axe:
                currentAxeExp += weaponExp;
                break;
            case WeaponType.Bow:
                currentBowExp += weaponExp;
                break;
            case WeaponType.Staff:
                currentStaffExp += weaponExp;
                break;
            case WeaponType.Sword:
                currentSwordExp += weaponExp;
                break;
            case WeaponType.Hammer:
                currentHammerExp += weaponExp;
                break;
        }
        DidILevelUp();
        DidWeaponLevelUp();
    }

    public virtual void SpecialAbility()
    {
        Debug.Log("Special Ability");
        
    }
}

//TODO: Make a base Unit with things every unit should have
//TODO: Then make a player unit with stuff only the player should have
//TODO: To help make things more legible