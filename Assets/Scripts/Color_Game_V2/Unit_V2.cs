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

    private int currentStamina;

    private int maxStamina;

    private int baseAttack;

    private int baseDefense;

    [SerializeField]
    private int baseSpeed;

    private int attackTier = 0;

    private int defenseTier = 0;

    private int speedTier = 0;


    //Status Effect Variables
    [SerializeField]
    private int burnAmount = 0;
    [SerializeField]
    private int burnThreshhold;
    [SerializeField]
    private int burnTimer = 0;

    [SerializeField]
    private Hue unitWeakness;

    [SerializeField]
    private Hue unitResistance;

    public List<StatusEffect_V2> unitStatusEffects = new List<StatusEffect_V2>();

    public Dictionary<string, Attack> unitAttackDictionary = new Dictionary<string, Attack>();

    // Start is called before the first frame update
    public virtual void Start()
    {
        attackDatabaseScript = FindObjectOfType<Attack_Database>();

        currentHp = maxHp;
        currentStamina = maxStamina;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int GetCurrentHp()
    {
        return currentHp;
    }

    public int GetCurrentStamina()
    {
        return currentStamina;
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

    public Dictionary<string, Attack> GetAttackDictionary()
    {
        Debug.Log(unitAttackDictionary.Count);
        return unitAttackDictionary;
    }
    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        Debug.Log($"{unitName} has taken {damage} damage and their currentHP is: {currentHp}");
    }

    public void GainHealth(int health)
    {
        currentHp += health;
        Debug.Log($"{unitName} has gained {health} health and their currentHP is: {currentHp}");
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

    public void AddAttackToDictionary(Attack attack)
    {
        Debug.Log("Adding Attack");
        unitAttackDictionary[attack.attackName] = attack;
    }
}
