using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_V2 : MonoBehaviour
{
    private string unitName;

    //Base Stats
    [SerializeField]
    private int currentHp = 10;
    [SerializeField]
    private int maxHp;

    private int currentStamina;

    private int maxStamina;

    private int baseAttack;

    private int baseDefense;

    private int baseSpeed;

    private int attackTier = 0;

    private int defenseTier = 0;

    private int speedTier = 0;

    //Status Effect Variables
    private int burnAmount = 0;
    private int burnThreshhold;
    private int burnTimer = 0;

    [SerializeField]
    private Hue unitWeakness;

    [SerializeField]
    private Hue unitResistance;

    private List<StatusEffect_V2> unitStatusEffects = new List<StatusEffect_V2>();

    private Dictionary<string, Attack> unitAttackDictionary = new Dictionary<string, Attack>();

    // Start is called before the first frame update
    void Start()
    {
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
        return baseAttack * (attackTier + 1);
    }
    public int GetCurrentDefense()
    {
        return baseDefense * (defenseTier + 1); ;
    }
    public int GetCurrentSpeed()
    {
        return baseSpeed * (speedTier + 1);
    }
    public void TakeDamage(int damage)
    {
        currentHp -= damage;
    }

    public void GainHealth(int health)
    {
        currentHp += health;
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

    public void AddStatus(StatusEffect_V2 statusEffect)
    {
        unitStatusEffects.Add(statusEffect);
    }
}
