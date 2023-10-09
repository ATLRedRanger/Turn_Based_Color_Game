using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatFunctions : MonoBehaviour
{

    public AttacksDatabase attackDatabase;

    //public Unit unit;

    public ENV_Mana envManaScript;

    public Unit_Spawner unitSpawnerScript;

    public Inventory inventoryScript;

    public Turn_Manager turnManagerScript;

    public UI uiScript;

    public Unit player;

    public int attackDamage;

    public Unit chosenEnemy;

    public Attack chosenAttack;

    int roll;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadingScripts());
    }

    IEnumerator LoadingScripts()
    {
        yield return new WaitForSeconds(.5f);

        unitSpawnerScript = FindObjectOfType<Unit_Spawner>();
        envManaScript = FindObjectOfType<ENV_Mana>();
        turnManagerScript = FindObjectOfType<Turn_Manager>();  
        inventoryScript = FindObjectOfType<Inventory>();
        uiScript = FindObjectOfType<UI>();
        player = unitSpawnerScript.player;

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegenStamina(Unit unit)
    {
        //Regenerate stamina at a rate of 1/5th of their maximum stanima

        int staminaRegend = ((unit.maxStamina * 1 / 3)*unit.staminaRegenModifier);

        unit.currentStamina += staminaRegend;


        if(unit.currentStamina >= unit.maxStamina)
        {
            unit.currentStamina = unit.maxStamina;
        }

        
    }
    public void UseHealthPotion()
    {
        foreach (Consumable item in inventoryScript.playerConsumableList)
        {

            if (item.itemName == "Health Potion" && item.itemAmount > 0)
            {
                
                player.currentHealth += item.refillAmount;
                if(player.currentHealth >= player.maxHealth)
                {
                    player.currentHealth = player.maxHealth;
                }
                
                item.itemAmount -= 1;
                if (item.itemAmount < 1)
                {
                    inventoryScript.playerConsumableList.Remove(item);
                }
            }

        }
        
        
    }

    /*public bool EnoughStaminaForAttack(Attack attack, Unit unit)
    {
        if(unit.currentStamina >= attack.staminaCost)
        {
            return true;
        }
        else return false;
    }*/

    public void Combat(Attack attack, Unit attacker, Unit defender)
    {
        //Chosen Attack: The chosen attack is dictated by the UI script. 
        //Chosen Enemy: The chosen enemy is dictated by the UI script. 
        //Do I have enough stamina for the attack?
        if (HitorMiss(attack, attacker) == true)
        {
            switch (attack.attackType)
            {
                case AttackType.Physical:
                    UseAttack(attack, attacker, defender);
                    break;
                case AttackType.Special: 
                    UseSpecialAttack(attack, attacker, defender);
                    break;
                default:
                    break;
            }
        }
        attacker.hadATurn = true;
    }
    public void IsDefending(Unit unit)
    {
        unit.isDefending = true;
        unit.hadATurn = true;
    }
    private StaminaLevels StaminaConversion(Unit unit)
    {
        //The higher the stamina, the better the accuracy the unit will have

        if (unit.currentStamina <= (unit.maxStamina * 1 / 4))
        {
            return StaminaLevels.OneQuarter;
        }
        if (unit.currentStamina > (unit.maxStamina * 1 / 4) && unit.currentStamina <= (unit.maxStamina * (1 / 2)))
        {
            return StaminaLevels.Half;
        }
        if ((unit.currentStamina > (1 / 2) && unit.currentStamina <= (unit.maxStamina * 3 / 4)))
        {
            return StaminaLevels.ThreeQuarters;
        }
        if (unit.currentStamina > (unit.maxStamina * 3 / 4))
        {
            return StaminaLevels.Full;
        }
        return StaminaLevels.Broken;
    }
    private int RollForAccuracy(Unit unit, float accuracyMultiple)
    {
        //Takes the unit's base accuracy and multiples it by the multiplier of the stanima value
        int dieRoll;
        int currentAccuracy = (int)(unit.baseAccuracy * accuracyMultiple);

        dieRoll = Random.Range(currentAccuracy, 100);

        return dieRoll;
    }
    public bool HitorMiss(Attack attack, Unit unit)
    {
        //I want stamina to affect accuracy
        //I want to have 4 levels of affecting accuracy
        //The lower the stamina, the less accurate you are

        //Base Accurracy is the minimum random roll
        //Attack Accuracy is the roll + accuracy after stamina
        //Attack Accuracy is out of 100
        //Meet or beat Attack Accuracy to hit



        int finalAccuracy = 0;
        bool hit = false;
        StaminaLevels accuracyLevel = StaminaConversion(unit);

        switch (accuracyLevel)
        {
            case (StaminaLevels.Full):
                roll = RollForAccuracy(unit, 1f);
                finalAccuracy = roll + unit.baseAccuracy;
                break;
            case (StaminaLevels.ThreeQuarters):
                roll = RollForAccuracy(unit, .75f);
                finalAccuracy = roll + unit.baseAccuracy;
                break;
            case (StaminaLevels.Half):
                roll = RollForAccuracy(unit, .5f);
                finalAccuracy = roll + unit.baseAccuracy;
                break;
            case (StaminaLevels.OneQuarter):
                roll = RollForAccuracy(unit, .25f);
                finalAccuracy = roll + unit.baseAccuracy;
                break;
        }
        if (finalAccuracy >= attack.attackAccuracy)
        {

            hit = true;
        }
        //Debug.Log("Final Accuracy is " + finalAccuracy + "Attack Accuracy is " + attack.attackAccuracy);

        return hit;

    }
    public void UseSpecialAttack(Attack attack, Unit player, Unit chosenEnemy)
    {

        DamageFromSpell(attack, player);
        ReduceStamina(attack, player);
        ReduceHealth(attackDamage, chosenEnemy, player);
        ReduceColorFromEnv(attack);
        ColorReturn(attack);

    }
    public void UseAttack(Attack attack, Unit player, Unit chosenEnemy)
    {

        DamageFromAttack(attack, player);
        ReduceStamina(attack, player);
        ReduceHealth(attackDamage, chosenEnemy, player);
        ReduceColorFromEnv(attack);
        ColorReturn(attack);

    }
    public int DamageFromAttack(Attack attack, Unit unit)
    {



        if (unit.isWeaponEquipped != false)
        {
            //Debug.Log("Orange");
            //Debug.Log(unit.equippedWeapon.itemName);
            switch (unit.equippedWeapon.weaponType)
            {
                /*case WeaponType.Axe:
                    
                    break;
                case WeaponType.Staff:
                    
                    break;
                case WeaponType.Sword:
                    
                    break;
                case WeaponType.Hammer:
                    
                    break;
                case WeaponType.Bow:
                    
                    break;*/
                default:
                    attackDamage = unit.physicalAttack + unit.equippedWeapon.weaponDamage + attack.attackDamage;
                    break;
            }
        }
        else
        {
            //Debug.Log("Banana");
            attackDamage = unit.physicalAttack + attack.attackDamage;
        }


        Debug.Log(attackDamage + "is the amount of damage dealt by" + unit.unitName);
        
        return attackDamage;

    }
    public int DamageFromSpell(Attack attack, Unit unit)
    {
        if (unit.isWeaponEquipped != false)
        {
            if (unit.equippedWeapon.weaponType == WeaponType.Staff)
            {
                attackDamage = unit.magicAttack + attack.attackDamage + unit.equippedWeapon.weaponDamage;
            }
            else
            {
                attackDamage = unit.magicAttack + attack.attackDamage;
            }

        }


        return attackDamage;
    }
    public int DamageBeingDealt(Attack chosenAttack, Unit chosenEnemy)
    {
        if(chosenAttack.attackType == AttackType.Physical)
        {
            DamageFromAttack(chosenAttack, chosenEnemy);
        }
        else
        {
            DamageFromSpell(chosenAttack, chosenEnemy);
        }

        return attackDamage;
    }
    public int ReduceStamina(Attack attack, Unit unit)
    {
        unit.currentStamina -= attack.staminaCost;

        if (unit.currentStamina < 1)
        {
            unit.currentStamina = 0;
            unit.isExhausted = true;
        }

        return unit.currentStamina;
    }
    public void ReduceHealth(int damage, Unit defender, Unit attacker)
    {


        //If the defender is not defending, deal full damage
        if (defender.isDefending != true)
        {
            switch (chosenAttack.attackType)
            {
                case AttackType.Physical:
                    defender.currentHealth -= (damage - defender.physicalDefense);

                    break;
                case AttackType.Special:
                    defender.currentHealth -= (damage - defender.magicDefense);

                    //Debug.Log(defender.currentHealth + "CUCUMBER");
                    break;
            }




        }
        else
        {
            //If they are defending and the attacker is using a weapon that modifies damage based on if 
            //they are defending
            if (attacker.isWeaponEquipped != false)
            {
                switch (attacker.equippedWeapon.weaponType)
                {
                    case WeaponType.Axe:
                        //Debug.Log(defender.unitName + " has " + defender.currentHealth);
                        defender.currentHealth -= (int)(damage * attacker.equippedWeapon.weaponHealthModifier);
                        defender.currentStamina -= (int)(damage * attacker.equippedWeapon.weaponStaminaModifier);
                        //Debug.Log(defender.unitName + " has " + defender.currentHealth + "left.");

                        break;
                    case WeaponType.Hammer:
                        //Debug.Log(defender.unitName + " has " + defender.currentHealth);
                        defender.currentHealth -= (int)(damage * attacker.equippedWeapon.weaponHealthModifier);
                        defender.currentStamina -= (int)(damage * attacker.equippedWeapon.weaponStaminaModifier);
                        //Debug.Log(defender.unitName + " has " + defender.currentHealth + "left.");

                        break;
                    default:
                        defender.currentHealth -= (int)(damage * .5);

                        break;
                }
            }
            else
            //If defending, attacker isn't equipped, defender takes half damage
            {

                defender.currentHealth -= (int)(damage * .5);



            }

        }

    }
    public void ReduceColorFromEnv(Attack attack)
    {
        
        //Debug.Log("CHOSEN ATTACK COLOR " + attack.attackColor);
        
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
            default:
                break;
        }
    }
    public void ColorReturn(Attack attack)
    {
        //int roll = Random.Range(0, 5);

        switch (attack.attackColor)
        {
            case Color.Red:
                envManaScript.currentOrange += attack.colorCost;
                if (envManaScript.currentOrange > envManaScript.maxOrange)
                {
                    envManaScript.maxOrange = envManaScript.currentOrange;
                }
                break;
            case Color.Orange:
                envManaScript.currentYellow += attack.colorCost;
                if (envManaScript.currentYellow > envManaScript.maxYellow)
                {
                    envManaScript.maxYellow = envManaScript.currentYellow;
                }
                break;
            case Color.Yellow:
                envManaScript.currentGreen += attack.colorCost;
                if (envManaScript.currentGreen > envManaScript.maxGreen)
                {
                    envManaScript.maxGreen = envManaScript.currentGreen;
                }
                break;
            case Color.Green:
                envManaScript.currentBlue += attack.colorCost;
                if (envManaScript.currentBlue > envManaScript.maxBlue)
                {
                    envManaScript.maxBlue = envManaScript.currentBlue;
                }
                break;
            case Color.Blue:
                envManaScript.currentViolet += attack.colorCost;
                if (envManaScript.currentViolet > envManaScript.maxViolet)
                {
                    envManaScript.maxViolet = envManaScript.currentViolet;
                }
                break;
            case Color.Violet:
                envManaScript.currentRed += attack.colorCost;
                if (envManaScript.currentRed > envManaScript.maxRed)
                {
                    envManaScript.maxRed = envManaScript.currentRed;
                }
                break;
            default:
                break;
        }
    }
}
//TODO: Accuracy? I would like to tie in stamina, health or both into accuracy. Concerned that it could snowball.
//Ie: You are dealing damage to OP, meaning they're accuracy drops and therefor open the door for you to deal more damage.
//Maybe just stamina then. 


//Need to refactor the code in an observer pattern kind of way.
//Untap: 
//Upkeep: The gaining of stamina and status effects should happen here.
//Draw
//MP1
//Combat: 
    //Choose an attack
    //Choose an enemy
    //Accuracy
    //Amount of damage being dealt
    //Health being reduced
//Mp2: Check to see if the opp is still alive
//End:
    //Is everyone still alive?
        //Yes?
            //The end of buffs/debuffs should happen here.
        //No? End of combat happens. Experience, loot, etc. D
