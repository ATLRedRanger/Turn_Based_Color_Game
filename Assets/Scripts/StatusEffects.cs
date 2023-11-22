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

    
    
    private void Start()
    {
        turnManager_Script = FindObjectOfType<Turn_Manager>();

        
    }
    
     

    public void Burning()
    {

        int burnDamage = (int)(Mathf.Round(turnManager_Script.unitReferences[turnManager_Script.turnIndex].maxHealth / 16));

        if (turnManager_Script.unitReferences[turnManager_Script.turnIndex].isBurning)
        {
            //Debug.Log("PLAYER IS BURNING!");
            turnManager_Script.unitReferences[turnManager_Script.turnIndex].currentHealth -= burnDamage;

            turnManager_Script.unitReferences[turnManager_Script.turnIndex].burnTimer -= 1;
        }
        if (turnManager_Script.unitReferences[turnManager_Script.turnIndex].burnTimer < 1)
        {

            turnManager_Script.unitReferences[turnManager_Script.turnIndex].isBurning = false;
        }

    }

    public void Stunned()
    {

        Debug.Log($"{turnManager_Script.unitReferences[turnManager_Script.turnIndex].currentStamina} / {turnManager_Script.unitReferences[turnManager_Script.turnIndex].maxStamina}");
            
        if (turnManager_Script.unitReferences[turnManager_Script.turnIndex].isStunned)
        {
            turnManager_Script.unitReferences[turnManager_Script.turnIndex].maxStamina = turnManager_Script.unitReferences[turnManager_Script.turnIndex].stunnedMaxStamina;

            if (turnManager_Script.unitReferences[turnManager_Script.turnIndex].currentStamina > turnManager_Script.unitReferences[turnManager_Script.turnIndex].maxStamina)
            {
                Debug.Log("IF STUNNED AN CURRENT > MAX");
                turnManager_Script.unitReferences[turnManager_Script.turnIndex].currentStamina = turnManager_Script.unitReferences[turnManager_Script.turnIndex].maxStamina;
            }
                
        }
            
        if (turnManager_Script.unitReferences[turnManager_Script.turnIndex].stunnedTimer < 1)
        {
            turnManager_Script.unitReferences[turnManager_Script.turnIndex].maxStamina = turnManager_Script.unitReferences[turnManager_Script.turnIndex].OgStamina;
            turnManager_Script.unitReferences[turnManager_Script.turnIndex].isStunned = false;
                
        }
            
            
        turnManager_Script.unitReferences[turnManager_Script.turnIndex].stunnedTimer -= 1;

        Debug.Log($"{turnManager_Script.unitReferences[turnManager_Script.turnIndex].currentStamina} / {turnManager_Script.unitReferences[turnManager_Script.turnIndex].maxStamina}");
    }
}
