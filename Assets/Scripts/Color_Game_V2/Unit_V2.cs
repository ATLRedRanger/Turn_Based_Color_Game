using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_V2 : MonoBehaviour
{
    public Attack_Database attackDatabaseScript;

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
    private int baseAttack;
    [SerializeField]
    private int baseDefense;

    public bool isDefending = false;

    [SerializeField]
    private int baseSpeed;

    private int attackTier = 0;

    private int defenseTier = 0;

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
    private Dictionary<WeaponType, float> unitWeaponResistances = new Dictionary<WeaponType, float>();
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
    private float axeResistance;
    [SerializeField]
    private float bowResistance;
    [SerializeField]
    private float hammerResistance;
    [SerializeField]
    private float swordResistance;

    public List<StatusEffect_V2> unitStatusEffects = new List<StatusEffect_V2>();
    private List<Buffs> unitBuffsList = new List<Buffs>();

    public Dictionary<string, Attack> unitAttackDictionary = new Dictionary<string, Attack>();

    // Start is called before the first frame update
    public virtual void Start()
    {
        attackDatabaseScript = FindObjectOfType<Attack_Database>();

        currentHp = maxHp;
        currentStamina = maxStamina;
        SetColorResistances();
        
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

    public virtual void SetWeaponResistances()
    {
        unitWeaponResistances[WeaponType.Axe] = axeResistance;
        unitWeaponResistances[WeaponType.Bow] = bowResistance;
        unitWeaponResistances[WeaponType.Hammer] = hammerResistance;
        unitWeaponResistances[WeaponType.Sword] = swordResistance;
        unitWeaponResistances[WeaponType.Neutral] = 0;
    }
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
    public int GetCurrentAttack()
    {
        int attack = Mathf.RoundToInt((float)(baseSpeed * (TierBonus(attackTier))));
        return attack;
    }
    public int GetCurrentDefense()
    {
        int defense = Mathf.RoundToInt((float)(baseSpeed * (TierBonus(defenseTier))));
        return defense;
    }
    public int GetCurrentSpeed()
    {
        int speed = Mathf.RoundToInt((float)(baseSpeed * (TierBonus(speedTier))));
        return speed;
    }
    
    public void SetSpeedTier(int value)
    {
        speedTier += value;
    }
    
    public List<Buffs> GetListOfBuffs()
    {
        return unitBuffsList;
    }

    public Dictionary<Hue, float> GetColorResistances()
    {
        return unitColorResistances;
    }

    public Dictionary<WeaponType, float> GetWeaponResistances()
    {
        return unitWeaponResistances;
    }

    public Dictionary<string, Attack> GetAttackDictionary()
    {
        return unitAttackDictionary;
    }
    public void TakeDamage(int damage)
    {
        if(isDefending)
        {
            currentHp -= damage / 2;
        }
        else
        {
            currentHp -= damage;
        }
        Debug.Log($"{unitName} has taken {damage} damage and their currentHP is: {currentHp}");
    }

    public void GainHealth(int amount)
    {
        currentHp += amount;
        Debug.Log($"{unitName} has gained {amount} health and their currentHP is: {currentHp}");
    }

    public void GainStamina(int amount)
    {
        currentHp += amount;
    }

    public void ReduceStamina(int staminaCost)
    {
        currentStamina -= staminaCost;
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
    }

    public float TierBonus(int tier)
    {
        switch (tier)
        {
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
        foreach (StatusEffect_V2 status in unitStatusEffects)
        {
            if (status.GetStatusName() == statusEffect.GetStatusName())
            {
                return true;
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
        Debug.Log("Adding Attack");
        unitAttackDictionary[attack.attackName] = attack;
    }
}
