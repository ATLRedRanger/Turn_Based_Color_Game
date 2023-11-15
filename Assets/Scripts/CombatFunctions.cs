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

    private int potentialAttackDamage;

    private int damageAfterReductions;

    public Unit chosenEnemy;


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
    public void Combat(Attack attack, Unit attacker, Unit defender)
    {
        //Chosen Attack: The chosen attack is dictated by the UI script. 
        //Chosen Enemy: The chosen enemy is dictated by the UI script. 
        //Do I have enough stamina for the attack?
        if (DidAttackHit(attack, attacker) == true)
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
        attack.AttackFunction();
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

        dieRoll = Random.Range(currentAccuracy, 101);

        return dieRoll;
    }
    public bool DidAttackHit(Attack attack, Unit unit)
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
            Debug.Log("ATTACK HITS!");
            hit = true;
        }
        //Debug.Log("Final Accuracy is " + finalAccuracy + "Attack Accuracy is " + attack.attackAccuracy);

        return hit;

    }
    public void UseSpecialAttack(Attack attack, Unit player, Unit chosenEnemy)
    {
        PotentialDamage(attack, player);
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
                    Debug.Log(unit.unitName + " HAS " + unit.physicalAttack + " AND IS EQUIPPED WITH " + unit.equippedWeapon.itemName + "THAT HAS " + unit.equippedWeapon.weaponDamage + "DAMAGE AND THE ATTACK HAS" + attack.attackDamage + "DAMAGE");
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
        //Thinking about putting a for loop here so that I can have multi-attacking attacks
        //If I ever have multiple enemies I could use a for loop here too and loop through the targets
        //for(int i = 0; i < attack.attackInstances; i++)
        {
            //If the defender is not defending, deal full damage
            if (defender.isDefending != true)
            {
                Debug.Log("DEFENDER IS NOT DEFENDING");
                switch (uiScript.chosenAttack.attackType)
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
                Debug.Log("DEFENDER IS DEFENDING");
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
                    Debug.Log("DEFENDER IS DEFENDING AND ATTACKER ISN'T EQUIPPED");
                    Debug.Log(defender.name + " HAS " + defender.currentHealth + " HEALTH!");
                    Debug.Log("DAMAGE = " + damage);
                    defender.currentHealth -= (int)(damage * .5);
                    Debug.Log(defender.name + " HAS " + defender.currentHealth + " HEALTH!");


                }

            }
        }
        

    }
    public void ReduceColorFromEnv(Attack attack)
    {
        
        //Debug.Log("CHOSEN ATTACK COLOR " + attack.attackColor);
        
        switch (attack.attackColor)
        {
            case Hue.Red:
                
                envManaScript.currentRed -= attack.colorCost;
                
                break;
            case Hue.Orange:
                envManaScript.currentOrange -= attack.colorCost;
                break;
            case Hue.Yellow:
                envManaScript.currentYellow -= attack.colorCost;
                break;
            case Hue.Green:
                envManaScript.currentGreen -= attack.colorCost;
                break;
            case Hue.Blue:
                envManaScript.currentBlue -= attack.colorCost;
                break;
            case Hue.Violet:
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
            case Hue.Red:
                envManaScript.currentOrange += attack.colorCost;
                if (envManaScript.currentOrange > envManaScript.maxOrange)
                {
                    envManaScript.maxOrange = envManaScript.currentOrange;
                }
                break;
            case Hue.Orange:
                envManaScript.currentYellow += attack.colorCost;
                if (envManaScript.currentYellow > envManaScript.maxYellow)
                {
                    envManaScript.maxYellow = envManaScript.currentYellow;
                }
                break;
            case Hue.Yellow:
                envManaScript.currentGreen += attack.colorCost;
                if (envManaScript.currentGreen > envManaScript.maxGreen)
                {
                    envManaScript.maxGreen = envManaScript.currentGreen;
                }
                break;
            case Hue.Green:
                envManaScript.currentBlue += attack.colorCost;
                if (envManaScript.currentBlue > envManaScript.maxBlue)
                {
                    envManaScript.maxBlue = envManaScript.currentBlue;
                }
                break;
            case Hue.Blue:
                envManaScript.currentViolet += attack.colorCost;
                if (envManaScript.currentViolet > envManaScript.maxViolet)
                {
                    envManaScript.maxViolet = envManaScript.currentViolet;
                }
                break;
            case Hue.Violet:
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

    public void CombatSteps(Attack attack, Unit attacker, Unit defender)
    {
        
        if(DidAttackHit(attack, attacker) == true)
        {
            CheckForSpecialWeaponProperties(attacker);
            PotentialDamage(attack, attacker);
            CheckForCrit(attacker);
            DamageAfterArmorandRes(attack, defender);
            ReduceHealthAndStaminaOfDefender(attack, attacker, defender);
            ReduceStamina(attack, attacker);
            ReduceColorFromEnv(attack);
            ColorReturn(attack);
        }
        attacker.hadATurn = true;
    }

    public void CheckForSpecialWeaponProperties(Unit attacker)
    {
        if(attacker.equippedWeapon != null)
        {
            attacker.equippedWeapon.SpecialProperty();
        }
        
    }

    public int PotentialDamage(Attack attack, Unit attacker)
    {
        potentialAttackDamage = 0;
        switch (attack.attackType)
        {
            case AttackType.Special:
                if (IsAttackerEquipped(attacker) == true)
                {
                    Debug.Log("ATTACKER IS EQUIPPED");
                    if (attacker.equippedWeapon.weaponType == WeaponType.Staff)
                    {
                        Debug.Log("STAFF EQUIPPED");
                        Staff equippedStaff = player.equippedWeapon as Staff;
                        if (equippedStaff.affinity == attack.attackColor)
                        {
                            int damageToBeBoosted = (int)((attack.attackDamage + attacker.equippedWeapon.weaponDamage) * 1.3);
                            Debug.Log($"POTENTIAL ATTACK DAMAGE (Staff Equipped + Affinity): {potentialAttackDamage} = {damageToBeBoosted} + {attacker.magicAttack}");
                            
                            potentialAttackDamage = damageToBeBoosted + attacker.magicAttack;
                            
                        }
                        else
                        {
                            Debug.Log($"POTENTIAL ATTACK DAMAGE (Staff Equipped w/No Affinity): {potentialAttackDamage} = {attack.attackDamage} + {attacker.magicAttack} + {attacker.equippedWeapon.weaponDamage}");
                            potentialAttackDamage = attack.attackDamage + attacker.magicAttack + attacker.equippedWeapon.weaponDamage;
                        }

                    }
                    else
                    {
                        Debug.Log($"POTENTIAL S.ATTACK DAMAGE: {potentialAttackDamage} = {attack.attackDamage} + {attacker.magicAttack} + {attacker.equippedWeapon.weaponDamage}");
                        potentialAttackDamage = attack.attackDamage + attacker.magicAttack + attacker.equippedWeapon.weaponDamage;
                    }
                    
                }
                else
                {
                    Debug.Log($"POTENTIAL S.ATTACK DAMAGE: {potentialAttackDamage} = {attack.attackDamage} + {attacker.magicAttack}");
                    potentialAttackDamage = attack.attackDamage + attacker.magicAttack;
                }
                break;
            default:
                if (IsAttackerEquipped(attacker) == true)
                {
                    Debug.Log($"POTENTIAL P.ATTACK DAMAGE (Weapon Equipped): {potentialAttackDamage} = {attack.attackDamage} + {attacker.physicalAttack} + {attacker.equippedWeapon.weaponDamage}");
                    potentialAttackDamage = attack.attackDamage + attacker.physicalAttack + attacker.equippedWeapon.weaponDamage;
                }
                else
                {
                    Debug.Log($"POTENTIAL ATTACK DAMAGE: {potentialAttackDamage} = {attack.attackDamage} + {attacker.physicalAttack}");
                    potentialAttackDamage = attack.attackDamage + attacker.physicalAttack;
                }
                break;
        }

        Debug.Log($"FINAL POTENTIAL DAMAGE: {potentialAttackDamage}");
        return potentialAttackDamage;
    }
    
    private bool IsAttackerEquipped(Unit attacker)
    {

        bool isEquipped = true;
        Debug.Log("IS ATTACKER EQUIPPED" + attacker.equippedWeapon);
        if(attacker.equippedWeapon == null)
        {

            isEquipped = false;
        }
        return isEquipped;
    }

    public void CheckForCrit(Unit attacker)
    {
        //The thought process behind this is:
        //The higher your base accuracy, the more likely you are to crit
        //(The better you are at hitting your target, the more likely you are to hit critical points)
        //Having a high base accuracy should reward you with easier crits
        //Stamina management should also reward/punish your crits
        //If you're in the midst of battle and you're dying, you might get lucky, but not overly so. 
        //I want crits to feel rewarding, but shouldn't really decide the battle

        

        bool crit = false;
        int dieRoll = Random.Range(0, 101);
        int critChance = (int)(100 - (attacker.baseAccuracy * .10));
        int critCalc = (int)(dieRoll + (attacker.baseAccuracy * .10));

        StaminaLevels critThreshold = StaminaConversion(attacker);

        switch (critThreshold)
        {
            case StaminaLevels.Full:
                if((critCalc * 1.25) > critChance)
                {
                    crit = true;
                }
                break;
            case StaminaLevels.ThreeQuarters:
                if ((critCalc * 1.15) > critChance)
                {
                    crit = true;
                }
                break;
            case StaminaLevels.Half:
                if ((critCalc * 1.05) > critChance)
                {
                    crit = true;
                }
                break;
            case StaminaLevels.OneQuarter:
                if ((critCalc * 1) > critChance)
                {
                    crit = true;
                }
                break;
        }
        if(crit)
        {
            potentialAttackDamage = (int)(potentialAttackDamage * 1.25);
            Debug.Log("YOU'VE LANDED A CRITICAL HIT!");
        }
        
    }

    public void DamageAfterArmorandRes(Attack attack, Unit defender)
    {
        int defenderMagicDefense = defender.magicDefense;
        int defenderPhysicalDefense = defender.physicalDefense;

        

        //Thought process behind this:
        //I want the player to manage stamina on both sides of the battle
        //If the player is being very aggressive and 
        StaminaLevels defenderStamina = StaminaConversion(defender);
        if(attack.attackType == AttackType.Special)
        {
            
            switch (defenderStamina)
            {
                case StaminaLevels.Full:
                    defenderMagicDefense = (int)(defender.magicDefense * 1.75);
                    break;
                case StaminaLevels.ThreeQuarters:
                    defenderMagicDefense = (int)(defender.magicDefense * 1.5);
                    break;
                case StaminaLevels.Half:
                    defenderMagicDefense = (int)(defender.magicDefense * 1.25);
                    break;
                case StaminaLevels.OneQuarter:
                    defenderMagicDefense = (int)(defender.magicDefense * 1.15);
                    break;
                default:
                    defenderMagicDefense = defender.magicDefense * 1;
                    break;
            }

            damageAfterReductions = potentialAttackDamage - defenderMagicDefense;
        }
        else
        {
            switch (defenderStamina)
            {
                case StaminaLevels.Full:
                    defenderPhysicalDefense = (int)(defender.physicalDefense * 1.75);
                    break;
                case StaminaLevels.ThreeQuarters:
                    defenderPhysicalDefense = (int)(defender.physicalDefense * 1.5);
                    break;
                case StaminaLevels.Half:
                    defenderPhysicalDefense = (int)(defender.physicalDefense * 1.25);
                    break;
                case StaminaLevels.OneQuarter:
                    defenderPhysicalDefense = (int)(defender.physicalDefense * 1.15);
                    break;
                default:
                    defenderPhysicalDefense = defender.physicalDefense * 1;
                    break;
            }

            damageAfterReductions = potentialAttackDamage - defender.physicalDefense;
        }
        

        
    }

    public void ReduceHealthAndStaminaOfDefender(Attack attack, Unit attacker, Unit defender)
    {

        //If the defender is defending and the attacker has a weapon
        //Reduce the health of the defender by the damage * the weaponModifier
        //If the the defender is defending and the attacker is weaponless
        //Reduce the health of the defender by half
        //If the defender is not defending, defender takes full damage 

        if (defender.isDefending && attacker.equippedWeapon != null)
        {
            Debug.Log("DEFENDER IS DEFENDING AND ATTACKER HAS A WEAPON");
            defender.currentHealth -= (int)(damageAfterReductions * attacker.equippedWeapon.weaponHealthModifier);
            defender.currentStamina -= (int)(damageAfterReductions * attacker.equippedWeapon.weaponStaminaModifier);
            if(defender.currentStamina < 1)
            {
                defender.currentStamina = 0;
            }
        }
        else if(defender.isDefending)
        {
            Debug.Log("DEFENDER IS DEFENDING");
            defender.currentHealth -= damageAfterReductions * 1/2;
        }
        else
        {
            Debug.Log("DEFENDER ISN'T DEFENDING");
            defender.currentHealth -= damageAfterReductions;
        }
         
    }

    private void WeaponEffectOnDefenderStamina(Unit attacker)
    {
        if(attacker.equippedWeapon != null)
        {
            switch (attacker.equippedWeapon.weaponType)
            {
                case WeaponType.Axe:
                    break;
                case WeaponType.Bow:
                    break;
                case WeaponType.Staff:
                    break;
                case WeaponType.Sword:
                    break;
                case WeaponType.Hammer:
                    break;
                case WeaponType.Spellbook:
                    break;
            }
        }
    }
}
//TODO: Accuracy? Should it play a part in criticals?
//TODO: Restructure the way combat is handled so that way it happens in a step by step way
//Combat In Steps:
// 1) How many times does the chosen attack hit if it hits?
// 2) Roll for accuracy on each attempt at a hit
// 3) If an attack is successful, how much damage is it potentially doing?
//    -Check for crits in this stage
//    -Check for weapon special abilities or modifiers
// 4) How much damage is the attack doing after defenses and resistances?
// 5) How much health damage does the defender take?
// 6) How much stamina damage does the defender take?
// 7) Are there any secondary effects of the attack?

//TODO: Come up with a damage calculation that takes into account Attack Damage, Weapon Damage, Critials, Base Defenses, Armor Defenses and other miscellaneous values. 

