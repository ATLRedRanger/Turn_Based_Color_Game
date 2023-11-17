using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StatusEffects : MonoBehaviour
{
    private Turn_Manager turnManager_Script;

    private void Start()
    {
        turnManager_Script = FindObjectOfType<Turn_Manager>();
    }
   
    
    public void Burning()
    {

        int burnDamage = (int)(Mathf.Round(turnManager_Script.unitReferences[turnManager_Script.turnIndex].maxHealth / 5));

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


}
