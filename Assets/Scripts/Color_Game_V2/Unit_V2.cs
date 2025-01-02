using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_V2 : MonoBehaviour
{
    public Attack_Database attackDatabaseScript;
    public Item_Database itemDatabaseScript;

    public string unitName;

    //Base Stats
    [SerializeField]
    private int currentHp;
    [SerializeField]
    private int maxHp = 10;
    [SerializeField]
    private int currentStamina;
    [SerializeField]
    private int maxStamina = 10;
    [SerializeField]
    private int baseDamage = 0;
    [SerializeField]
    private int baseAttackBonus = 0;
    [SerializeField]
    private int armorClass = 0;
    [SerializeField]
    private int difficultyClass;

    public bool isDefending = false;
    public bool usedItem = false;

    [SerializeField]
    private int baseSpeed;

    //private int attackTier = 0;

    //private int defenseTier = 0;
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

    public SpriteRenderer unitSpriteRenderer;
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
        unitSpriteRenderer = GetComponent<SpriteRenderer>();
        currentHp = maxHp;
        currentStamina = maxStamina;
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
    public int GetCurrentStamina()
    {
        return currentStamina;
    }
    public int GetMaxStamina()
    {
        return maxStamina;
    }

    public int GetUnitDamage()
    {
        if(equippedWeapon != null)
        {
            return baseDamage + equippedWeapon.baseDamage;
        }

        return baseDamage;
    }

    public int GetBAB()
    {
        return baseAttackBonus;
    }

    public virtual int GetCombatBAB()
    {
        if(equippedWeapon != null)
        {
            return baseAttackBonus + equippedWeapon.bonusModifier;
        }
        return baseAttackBonus;
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

        return armorClass;
    }

    public int GetDC()
    {
        return difficultyClass;
    }

    public virtual int GetCombatDC()
    {
        return difficultyClass;
    }

    /*
    public int GetCurrentAttack()
    {
        if(equippedWeapon != null)
        {
            return Mathf.RoundToInt((baseAttack + equippedWeapon.GetWeaponBaseDamage()) * TierBonus(attackTier));
        }

        return Mathf.RoundToInt((float)(baseAttack * (TierBonus(attackTier))));
    }
    public int GetCurrentDefense()
    {
        int defense = Mathf.RoundToInt((float)(baseDefense * (TierBonus(defenseTier))));
        return defense;
    }
    */

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
        Debug.Log($"{unitName}'s SpeedTier: {speedTier}");
        speedTier += value;
        Debug.Log($"{unitName}'s SpeedTier: {speedTier}");
    }
    
    public int GetSpeedTier()
    {
        Debug.Log($"{unitName}'s SpeedTier: {speedTier}");
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
    /*
    public void GainStamina(int amount)
    {
        currentHp += amount;
    }

    public void ReduceStamina(int staminaCost)
    {
        currentStamina -= staminaCost;
        if(currentStamina < 0)
        {
            currentStamina = 0;
        }
    }

    public StaminaLevels StaminaLevelConversion()
    {
        int staminaPercent = Mathf.RoundToInt((currentStamina / maxStamina) * 100);

        switch (staminaPercent)
        {
            case 100:
                return StaminaLevels.Full;
            case var expression when (staminaPercent > 50 && staminaPercent < 100):
                return StaminaLevels.ThreeQuarters;
            case var expression when (staminaPercent > 25 && staminaPercent <= 50):
                return StaminaLevels.Half;
            case var expression when (staminaPercent > 0 && staminaPercent <= 25):
                return StaminaLevels.ThreeQuarters;
            default:
                return StaminaLevels.Empty;
        }
    }*/

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
        if (!unitStatusEffects.Contains(statusEffect)){
            unitStatusEffects.Add(statusEffect);
        }
        
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
