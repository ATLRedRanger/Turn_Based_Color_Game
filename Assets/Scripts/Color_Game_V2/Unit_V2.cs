using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_V2 : MonoBehaviour
{
    public Attack_Database attackDatabaseScript;
    public Item_Database itemDatabaseScript;
    public StatusEffectsDatabase_V2 statusEffectsScript;

    public string unitName;

    //Base Stats
    [SerializeField]
    private int currentHp;
    [SerializeField]
    private int maxHp = 10;

    [SerializeField]
    private int baseDamage = 0;
    [SerializeField]
    private int baseAttackBonus = 0;
    [SerializeField]
    private int armorClass = 0;
    [SerializeField]
    private int difficultyClass;
    private int baseAttackBonusModifier = 0;
    private int armorClassBonusModifier = 0;
    private int dcBonousModifier = 0;
    [SerializeField]
    private int postiveBonus = 0;
    [SerializeField]
    private int negativeBonus = 0;

    public bool isDefending = false;
    public bool usedItem = false;

    [SerializeField]
    private int baseSpeed;

    [SerializeField]
    private int speedTier = 0;

    public Weapon equippedWeapon = null;

    //Status Effect Variables
    [SerializeField]
    private int burnAmount = 0;
    [SerializeField]
    private int burnThreshhold = 5;
    [SerializeField]
    private int burnTimer = 0;

    //Reistances And Weaknesses
    private Dictionary<Hue, float> unitColorResistances = new Dictionary<Hue, float>();
    //private Dictionary<WeaponType, float> unitWeaponResistances = new Dictionary<WeaponType, float>();

    [SerializeField]
    private float redResistance;
    [SerializeField]
    private float orangeResistance;
    [SerializeField]
    private float yellowResistance;
    [SerializeField]
    private float greenResistance;
    [SerializeField]
    private float blueResistance;
    [SerializeField]
    private float violetResistance;
    [SerializeField]

    /*
    private float axeResistance;
    [SerializeField]
    private float bowResistance;
    [SerializeField]
    private float hammerResistance;
    [SerializeField]
    private float spellbookResistance;
    [SerializeField]
    private float swordResistance;
    */

    public List<StatusEffect_V2> unitStatusEffects = new List<StatusEffect_V2>();
    private List<Buffs> unitBuffsList = new List<Buffs>();
    private List<Debuffs> unitDebuffsList = new List<Debuffs>();
    public Dictionary<string, Attack> unitAttackDictionary = new Dictionary<string, Attack>();

    // Start is called before the first frame update
    public virtual void Start()
    {
        attackDatabaseScript = FindObjectOfType<Attack_Database>();
        itemDatabaseScript = FindObjectOfType<Item_Database>();
        statusEffectsScript = FindObjectOfType<StatusEffectsDatabase_V2>();
        currentHp = maxHp;
        
        SetColorResistances();
        //SetWeaponResistances();

    }

    public virtual void SetColorResistances()
    {
        unitColorResistances[Hue.Red] = redResistance;
        unitColorResistances[Hue.Orange] = orangeResistance;
        unitColorResistances[Hue.Yellow] = yellowResistance;
        unitColorResistances[Hue.Green] = greenResistance;
        unitColorResistances[Hue.Blue] = blueResistance;
        unitColorResistances[Hue.Violet] = violetResistance;
        unitColorResistances[Hue.Neutral] = 0;
    }

    /*
    public virtual void SetWeaponResistances()
    {
        unitWeaponResistances[WeaponType.Axe] = axeResistance;
        unitWeaponResistances[WeaponType.Bow] = bowResistance;
        unitWeaponResistances[WeaponType.Hammer] = hammerResistance;
        unitWeaponResistances[WeaponType.Spellbook] = spellbookResistance;
        unitWeaponResistances[WeaponType.Sword] = swordResistance;
        unitWeaponResistances[WeaponType.Neutral] = 0;
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
    public int GetCurrentHp()
    {
        return currentHp;
    }
    public int GetMaxHp()
    {
        return maxHp;
    }
    
    public int GetUnitDamage()
    {
        if(equippedWeapon != null)
        {
            //Debug.Log($"UNIT BASE DAMAGE: {baseDamage} + Weapon Base Damage: {equippedWeapon.baseDamage}");
            return baseDamage + equippedWeapon.baseDamage;
        }

        //Debug.Log($"UNIT BASE DAMAGE: {baseDamage}");
        return baseDamage;
    }
    public int GetACBonusModifier()
    {
        int bonus = armorClassBonusModifier;

        if (DoesStatusExist(statusEffectsScript.burn))
        {
            bonus += postiveBonus;
        } 
        

        return bonus;
    }

    public int GetBABBonusModifier()
    {
        return baseAttackBonusModifier;
    }

    public int GetDCBonusModifier()
    {
        return dcBonousModifier;
    }

    public int GetBAB()
    {
        return baseAttackBonus;
    }

    public virtual int GetCombatBAB()
    {
        if(equippedWeapon != null)
        {
            return GetBAB() + GetBABBonusModifier() + equippedWeapon.bonusModifier;
        }
        Debug.Log($"GetBAB(): {GetBAB()} + GetBABBonusModifier(): {GetBABBonusModifier()}");
        return GetBAB() + GetBABBonusModifier();
    }

    public int GetAC()
    {
        return armorClass;
    }

    public virtual int GetCombatAC()
    {
        //When armor is introduced, this is where I'll put the armor modifiers
        /*
         if(equippedArmor != null)
        {
            return armorClass + equippedArmor.bonusModifier;
        }*/
        Debug.Log($"GetAC(): {GetAC()} + GetBABBonusModifier(): {GetACBonusModifier()}");
        return GetAC() + GetACBonusModifier();
    }

    public int GetDC()
    {
        return difficultyClass;
    }

    public virtual int GetCombatDC()
    {
        return GetDC() + GetDCBonusModifier();
    }

    public int GetCurrentSpeed()
    {

        return Mathf.RoundToInt((float)(baseSpeed * (TierBonus(speedTier))));
         
    }

    /*
    public void SetDefenseTier(int value)
    {
        defenseTier += value;
    }
    public void SetAttackTier(int value)
    {
        
        attackTier += value;
        
    }*/

    public void SetSpeedTier(int value)
    {
        //Debug.Log($"{unitName}'s SpeedTier: {speedTier}");
        speedTier += value;
        //Debug.Log($"{unitName}'s SpeedTier: {speedTier}");
    }
    
    public int GetSpeedTier()
    {
        //Debug.Log($"{unitName}'s SpeedTier: {speedTier}");
        return speedTier;
    }


    public List<Buffs> GetListOfBuffs()
    {
        return unitBuffsList;
    }

    public List<Debuffs> GetListOfDebuffs()
    {
        return unitDebuffsList;
    }

    public Dictionary<Hue, float> GetColorResistances()
    {
        return unitColorResistances;
    }

    /*
    public Dictionary<WeaponType, float> GetWeaponResistances()
    {
        return unitWeaponResistances;
    }*/

    public Dictionary<string, Attack> GetAttackDictionary()
    {
        return unitAttackDictionary;
    }
    public virtual void TakeDamage(int damage)
    {
        
        currentHp -= damage;
        if(currentHp < 0)
        {
            currentHp = 0;
        }
        
        Debug.Log($"{unitName} has taken {damage} damage and their currentHP is: {currentHp}");
    }

    public virtual void GainHealth(int amount)
    {
        
        currentHp += amount;
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
            Debug.Log($"{unitName} has gained {amount} health and their currentHP is: {currentHp}");
    }
    
    public float TierBonus(int tier)
    {
        switch (tier)
        {
            case -5:
                return 0;
            case -4:
                return .2f;
            case -3:
                return .4f;
            case -2:
                return .6f;
            case -1:
                return .8f;
            case 1:
                return 1.2f;
            case 2:
                return 1.4f;
            case 3:
                return 1.6f;
            case 4:
                return 1.8f;
            case 5:
                return 2;
            default:
                return 1;
        }
    }

    public void AddStatus(StatusEffect_V2 statusEffect)
    {
        if (unitStatusEffects.Contains(statusEffect) == false)
        {
            Debug.Log($"Status Effect {statusEffect.GetStatusName()} is being added!");
            unitStatusEffects.Add(statusEffect);
        }

        Debug.Log($"Status Effects Count: {unitStatusEffects.Count}");
    }

    public bool DoesStatusExist(StatusEffect_V2 statusEffect)
    {
        if(statusEffect is Buffs)
        {
            Debug.Log("Checking if Buff");
            foreach (StatusEffect_V2 status in unitBuffsList)
            {
                if (status.GetStatusName() == statusEffect.GetStatusName())
                {
                    return true;
                }
            }
        }
        else if(statusEffect is Debuffs)
        {
            Debug.Log("Checking if Debuff");
            foreach (StatusEffect_V2 status in unitDebuffsList)
            {
                if (status.GetStatusName() == statusEffect.GetStatusName())
                {
                    return true;
                }
            }
        }
        else
        {
            Debug.Log("Checking if Status");
            foreach (StatusEffect_V2 status in unitStatusEffects)
            {
                if (status.GetStatusName() == statusEffect.GetStatusName())
                {
                    return true;
                }
            }
            
        }
        return false;

    }

    public int GetBurnAmount()
    {
        return burnAmount;
    }

    public int GetBurnThreshhold()
    {
        return burnThreshhold;
    }

    public int GetBurnTimer()
    {
        return burnTimer;
    }
    public void AddToBurnAmount(int amount)
    {
        
        burnAmount += amount;
    }
    public void AddToBurnTimer(int amount)
    {
        burnTimer += amount;
    }

    public void SetBurnTimerToZero()
    {
        burnTimer = 0;
        Debug.Log($"Burn Timer being set to 0: {burnTimer}");
    }

    public void SetBurnAmountToZero()
    {
        burnAmount = 0;
        Debug.Log($"Burn Amount being set to 0: {burnAmount}");
    }

    public void AddAttackToDictionary(Attack attack)
    {
        //Debug.Log("Adding Attack");
        unitAttackDictionary[attack.attackName] = attack;
    }
}
