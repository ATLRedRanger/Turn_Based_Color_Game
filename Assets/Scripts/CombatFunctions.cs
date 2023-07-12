using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatFunctions : MonoBehaviour
{

    public AttacksDatabase attackDatabase;

    public Unit unit;

    public ENV_Mana envManaScript;

    public int attackDamage;

    
    // Start is called before the first frame update
    void Start()
    {
        
        envManaScript = FindObjectOfType<ENV_Mana>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int ReduceStamina(Attack attack, Unit unit)
    {
        unit.currentStamina -= attack.staminaCost;

        Debug.Log(unit.currentStamina + "is the current stamina of" + unit.unitName);
        return unit.currentStamina;
    }

    public int DamageFromAttack(Attack attack, Unit unit)
    {
        attackDamage = unit.baseAttack + attack.attackDamage;

        Debug.Log(attackDamage + "is the amount of damage dealt by"+unit.unitName);
        return attackDamage;

    }

    public void ReduceHealth(int damage, Unit unit)
    {
        unit.currentHealth -= damage;
        
    }

    public void ReduceColorFromEnv(Attack attack)
    {
        switch (attack.attackColor)
        {
            case Color.Red:
                envManaScript.currentRed -= attack.colorCost;
                break;
            case Color.Orange:
                envManaScript.currentOrange -= attack.colorCost;
                break;
            case Color.Yellow:
                envManaScript.currentYellow -= attack.colorCost;
                break;
            case Color.Green:
                envManaScript.currentGreen -= attack.colorCost;
                break;
            case Color.Blue:
                envManaScript.currentBlue -= attack.colorCost;
                break;
            case Color.Violet:
                envManaScript.currentViolet -= attack.colorCost;
                break;
        }
    }
    
    
}
