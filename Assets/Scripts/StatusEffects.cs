using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Profiling;
using UnityEditorInternal;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;


public class StatusEffects : MonoBehaviour
{
    private Turn_Manager turnManager_Script;

    private CombatFunctions combatFunctions_Script;

    private UI ui_Script;

    public int burnDamage;
    
    private void Start()
    {
        turnManager_Script = FindObjectOfType<Turn_Manager>();
        combatFunctions_Script = FindObjectOfType<CombatFunctions>();
        ui_Script = FindObjectOfType<UI>();
    }

    private StaminaLevels StaminaConversion(Unit unit)
    {
        //The higher the stamina, the better the accuracy the unit will have

        if (unit.currentStamina <= (unit.OgStamina * 1 / 4))
        {
            return StaminaLevels.OneQuarter;
        }
        if (unit.currentStamina > (unit.OgStamina * 1 / 4) && unit.currentStamina <= (unit.OgStamina * (1 / 2)))
        {
            return StaminaLevels.Half;
        }
        if ((unit.currentStamina > (1 / 2) && unit.currentStamina <= (unit.OgStamina * 3 / 4)))
        {
            return StaminaLevels.ThreeQuarters;
        }
        if (unit.currentStamina > (unit.OgStamina * 3 / 4))
        {
            return StaminaLevels.Full;
        }
        return StaminaLevels.Broken;
    }

    public void Burning()
    {
        
        burnDamage = (int)(Mathf.Round(turnManager_Script.unitReferences[turnManager_Script.turnIndex].maxHealth / 16));

        if (turnManager_Script.unitReferences[turnManager_Script.turnIndex].isBurning)
        {
            //Debug.Log("PLAYER IS BURNING!");
            turnManager_Script.unitReferences[turnManager_Script.turnIndex].currentHealth -= burnDamage;

            ui_Script.MiscellaneousFloatingNumbers(turnManager_Script.unitReferences[turnManager_Script.turnIndex], burnDamage);

            turnManager_Script.unitReferences[turnManager_Script.turnIndex].burnTimer -= 1;
        }
        if (turnManager_Script.unitReferences[turnManager_Script.turnIndex].burnTimer < 1)
        {

            turnManager_Script.unitReferences[turnManager_Script.turnIndex].isBurning = false;
        }
        
    }

    public void Stunned()
    {

        //Debug.Log($"{turnManager_Script.unitReferences[turnManager_Script.turnIndex].currentStamina} / {turnManager_Script.unitReferences[turnManager_Script.turnIndex].maxStamina}");
            
        if (turnManager_Script.unitReferences[turnManager_Script.turnIndex].isStunned)
        {
            turnManager_Script.unitReferences[turnManager_Script.turnIndex].maxStamina = turnManager_Script.unitReferences[turnManager_Script.turnIndex].stunnedMaxStamina;

            if (turnManager_Script.unitReferences[turnManager_Script.turnIndex].currentStamina > turnManager_Script.unitReferences[turnManager_Script.turnIndex].maxStamina)
            {
                //Debug.Log("IF STUNNED AN CURRENT > MAX");
                turnManager_Script.unitReferences[turnManager_Script.turnIndex].currentStamina = turnManager_Script.unitReferences[turnManager_Script.turnIndex].maxStamina;
            }
                
        }
            
        if (turnManager_Script.unitReferences[turnManager_Script.turnIndex].stunnedTimer < 1)
        {
            turnManager_Script.unitReferences[turnManager_Script.turnIndex].maxStamina = turnManager_Script.unitReferences[turnManager_Script.turnIndex].OgStamina;
            turnManager_Script.unitReferences[turnManager_Script.turnIndex].isStunned = false;
                
        }
            
            
        turnManager_Script.unitReferences[turnManager_Script.turnIndex].stunnedTimer -= 1;

        //Debug.Log($"{turnManager_Script.unitReferences[turnManager_Script.turnIndex].currentStamina} / {turnManager_Script.unitReferences[turnManager_Script.turnIndex].maxStamina}");
    }

    public void Vampped(Unit attacker)
    {
        //Heals the attacker half of the damage dealt 
        if (attacker.isVampped)
        {
            attacker.currentHealth += Mathf.RoundToInt(combatFunctions_Script.damageAfterReductions * 1 / 2);
            
            attacker.isVampped = false;
        }
        

    }
}

//Something to consider:
/*
 * Reassess Stamina Mechanics:
    Influence status effects or ability cooldowns.

 * Ways to implement Stamina Mechanics into Status Effects
 * Duration
 * Damage
 * Increased stamina costs for actions
 * Resistances
 */