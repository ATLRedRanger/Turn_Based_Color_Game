using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    [SerializeField] private int currentLevel;

    public int currentExp;

    [SerializeField] private int expToLevel;

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

    //Dictionaries
    public Dictionary<string, Attack> unitAttackDictionary = new Dictionary<string, Attack>();

    public Dictionary<string, Attack> unitSpellsDictionary = new Dictionary<string, Attack>();

    // Start is called before the first frame update
    public override void Start()
    {

        base.Start();
        LearnAbilities();
        LearnSpells();

    }
    //Adding attacks to an attack dictionary
    private void LearnAbilities()
    {
        switch (currentLevel)
        {

            case 1:
                unitAttackDictionary["Punch"] = attacksDatabase._punch;
                unitAttackDictionary["Kick"] = attacksDatabase._kick;
                //unitAttackDictionary["Fireball"] = attacksDatabase._fireBall;





                break;
            case 2:
                unitAttackDictionary["Green Punch"] = attacksDatabase._greenPunch;
                break;
            case 3:
                unitAttackDictionary["Blue Crush"] = attacksDatabase._blueCrush;
                unitAttackDictionary["Orange Spike"] = attacksDatabase._orangeSpike;
                break;
        }
        switch (axeMastery)
        {
            case 1:
                //unitAttackDictionary["Chop"] = attacksDatabase._chop;
                unitAttackDictionary.Add("Chop", attacksDatabase._chop);
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
    private void LearnSpells()
    {
        
        switch (currentLevel)
        {
            case 1:
                unitAttackDictionary["Fireball"] = attacksDatabase._fireBall;
                break;
            case 2:
                unitAttackDictionary["Yellow Splash"] = attacksDatabase._yellowSplash;
                break;
            default:


                //unitAttackDictionary.Add("Fireball", attacksDatabase._fireBall);

                break;
        }

    }
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
}
