using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Profiling;
using UnityEditorInternal;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

//In a SoulsLike, the status builds up, then procs. I need to replicate that. 
//What should happen is:
// 1) An attack should build up the status effect
// 2) If the units threshold is met or exceeded, the status procs
// 3) After a certain amount of time, the build up should reset to 0

public class StatusEffects : MonoBehaviour
{
    private Turn_Manager turnManager_Script;

    private CombatFunctions combatFunctions_Script;

    private UI ui_Script;

    public int burnDamage;

    public int healthGained;
    
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
            turnManager_Script.unitReferences[turnManager_Script.turnIndex].LoseHealth(burnDamage);
            turnManager_Script.unitReferences[turnManager_Script.turnIndex].burnAmount = 0;
            //ui_Script.MiscellaneousFloatingNumbers(turnManager_Script.unitReferences[turnManager_Script.turnIndex], burnDamage, "-");

            turnManager_Script.unitReferences[turnManager_Script.turnIndex].burnTimer -= 1;
        }
        if (turnManager_Script.unitReferences[turnManager_Script.turnIndex].burnTimer < 1)
        {
            
            turnManager_Script.unitReferences[turnManager_Script.turnIndex].isBurning = false;
            
            turnManager_Script.unitReferences[turnManager_Script.turnIndex].SetBurnTimer();
        }

        Event_Manager.StartPrintEvent();
    }

    public void Stunned()
    {

        //Debug.Log($"{turnManager_Script.unitReferences[turnManager_Script.turnIndex].currentStamina} / {turnManager_Script.unitReferences[turnManager_Script.turnIndex].maxStamina}");
            
        if (turnManager_Script.unitReferences[turnManager_Script.turnIndex].isStunned)
        {
            turnManager_Script.unitReferences[turnManager_Script.turnIndex].maxStamina = turnManager_Script.unitReferences[turnManager_Script.turnIndex].stunnedMaxStamina;
            turnManager_Script.unitReferences[turnManager_Script.turnIndex].stunAmount = 0;
            if (turnManager_Script.unitReferences[turnManager_Script.turnIndex].currentStamina > turnManager_Script.unitReferences[turnManager_Script.turnIndex].maxStamina)
            {
                //Debug.Log("IF STUNNED AN CURRENT > MAX");
                turnManager_Script.unitReferences[turnManager_Script.turnIndex].currentStamina = turnManager_Script.unitReferences[turnManager_Script.turnIndex].maxStamina;
            }

            turnManager_Script.unitReferences[turnManager_Script.turnIndex].stunnedTimer -= 1;
        }

        if (turnManager_Script.unitReferences[turnManager_Script.turnIndex].stunnedTimer < 1)
        {
            turnManager_Script.unitReferences[turnManager_Script.turnIndex].maxStamina = turnManager_Script.unitReferences[turnManager_Script.turnIndex].OgStamina;
            turnManager_Script.unitReferences[turnManager_Script.turnIndex].isStunned = false;
            turnManager_Script.unitReferences[turnManager_Script.turnIndex].SetStunnedTimer();
        }
            
            
       
        //Debug.Log($"{turnManager_Script.unitReferences[turnManager_Script.turnIndex].currentStamina} / {turnManager_Script.unitReferences[turnManager_Script.turnIndex].maxStamina}");
    }

    public void Vampped(Unit attacker)
    {
        healthGained = Mathf.RoundToInt(combatFunctions_Script.damageAfterReductions * 1 / 2);
        //Heals the attacker half of the damage dealt 
        if (attacker.isVampped)
        {
            attacker.currentHealth += healthGained;
            ui_Script.MiscellaneousFloatingNumbers(attacker, healthGained, "+");
            if(attacker.currentHealth >= attacker.maxHealth)
            {
                attacker.currentHealth = attacker.maxHealth;
            }
            
            attacker.isVampped = false;
        }
        
    }

    public void Healing(Unit unit)
    {
        unit.isHealing = false;
    }

    public void Tinted()
    {
        
        if(turnManager_Script.unitReferences[turnManager_Script.turnIndex].tintTimer == 0) 
        {
            turnManager_Script.unitReferences[turnManager_Script.turnIndex].isTinted = false;
            turnManager_Script.unitReferences[turnManager_Script.turnIndex].SetTintToOriginal();
        }
        else
        {
            turnManager_Script.unitReferences[turnManager_Script.turnIndex].tintTimer -= 1;
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
