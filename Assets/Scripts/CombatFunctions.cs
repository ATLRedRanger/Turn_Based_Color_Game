using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CombatFunctions : MonoBehaviour
{

    public AttacksDatabase attackDatabase;

    public bool inCombat;

    //public Unit unit;

    public ENV_Mana envManaScript;

    public Unit_Spawner unitSpawnerScript;

    public Inventory inventoryScript;

    public Turn_Manager turnManagerScript;

    public UI uiScript;

    public Unit player;

    public int attackDamage;

    private int potentialAttackDamage;

    public int damageAfterReductions;

    public Unit chosenEnemy;

    public int healthLost;

    public bool crit;

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
        //Stamina stunned problem isn't here
        int staminaRegend = ((unit.maxStamina * 1 / 10)*unit.staminaRegenModifier);

        unit.currentStamina += staminaRegend;


        if(unit.currentStamina >= unit.maxStamina)
        {
            
            unit.currentStamina = unit.maxStamina;
        }

        
    }
    /*public void UseHealthPotion()
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
        
        
    }*/
    public void IsDefending(Unit unit)
    {
        unit.isDefending = true;
        unit.hadATurn = true;
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
            
            hit = true;
        }
        //Debug.Log("Final Accuracy is " + finalAccuracy + "Attack Accuracy is " + attack.attackAccuracy);

        return hit;

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
    public IEnumerator CombatStepsTwo(Attack attack, Unit attacker, Unit defender)
    {
        inCombat = true;
        for (int i = 0; i < attack.numOfAttacks; i++)
        {
            yield return new WaitForSeconds(.8f);
            
            if (DidAttackHit(attack, attacker) == true)
            {
                potentialAttackDamage = 0;
                CheckForSpecialWeaponProperties(attack, attacker);
                CheckForAttackAbilities(attack, defender);
                PotentialDamage(attack, attacker);
                CheckForCrit(attacker);
                DamageAfterArmorandRes(attack, defender);
                DamageAfterStatusCheck(defender);
                ReduceHealthAndStaminaOfDefender(attack, attacker, defender);
                uiScript.FloatingNumbersText(defender, attack);
                ReduceStamina(attack, attacker);
                ReduceColorFromEnv(attack);
                ColorReturn(attack);
                yield return new WaitForSeconds(.7f);
                uiScript.UpdateUI();
            }
            if(defender.currentHealth < 1)
            {
                break;
            }
        }

        attacker.hadATurn = true;
        inCombat = false;
    }
    public void CombatSteps(Attack attack, Unit attacker, Unit defender)
    {
        for (int i = 0; i < attack.numOfAttacks; i++)
        {
            
            Debug.Log($"NumOfAttacks = {attack.numOfAttacks}");
            if (DidAttackHit(attack, attacker) == true)
            {
                potentialAttackDamage = 0;
                CheckForSpecialWeaponProperties(attack, attacker);
                CheckForAttackAbilities(attack, defender);
                PotentialDamage(attack, attacker);
                CheckForCrit(attacker);
                DamageAfterArmorandRes(attack, defender);
                DamageAfterStatusCheck(defender);
                ReduceHealthAndStaminaOfDefender(attack, attacker, defender);
                uiScript.FloatingNumbersText(defender, attack);
                ReduceStamina(attack, attacker);
                ReduceColorFromEnv(attack);
                ColorReturn(attack);
                
            }
            
        }
            
        attacker.hadATurn = true;
    }

    public void CheckForSpecialWeaponProperties(Attack attack, Unit attacker)
    {
        //Each weapon should feel different from each other
        //Hammer: Gets bonus damage from the attacker current stamina level and always deals stamina damage
        if (attacker.equippedWeapon != null)
        {
            switch (attacker.equippedWeapon.weaponType)
            {
                case WeaponType.Axe:
                    break;
                case WeaponType.Bow:
                    break;
                case WeaponType.Hammer:
                    if(attack.attackType != AttackType.Special)
                    {
                        potentialAttackDamage = attacker.equippedWeapon.Hammer(attacker);
                    }
                    break;
                case WeaponType.Spellbook:
                    break;
                case WeaponType.Staff:
                    break;
                case WeaponType.Sword:
                    break;
                default:
                    break;
            }

        }
        else
        {
            potentialAttackDamage = 0;
        }
        
        Debug.Log($"Potential Attack Damage = {potentialAttackDamage}");
    }

    public int PotentialDamage(Attack attack, Unit attacker)
    {
        
        switch (attack.attackType)
        {
            case AttackType.Special:
                if (IsAttackerEquipped(attacker) == true && (attacker.equippedWeapon.weaponType == WeaponType.Staff || attacker.equippedWeapon.weaponType == WeaponType.Spellbook))
                //(IsAttackerEquipped(attacker) == true && (attacker.equippedWeapon.weaponType == WeaponType.Staff || true))
                //(IsAttackerEquipped(attacker) == true && (false || true))
                {
                    Debug.Log("ATTACKER IS EQUIPPED");
                    if (attacker.equippedWeapon.weaponType == WeaponType.Staff)
                    {
                        Debug.Log("STAFF EQUIPPED");
                        Staff equippedStaff = player.equippedWeapon as Staff;
                        if (equippedStaff.affinity == attack.attackColor)
                        {
                            int damageToBeBoosted = Mathf.RoundToInt((float)((attack.attackDamage + attacker.equippedWeapon.weaponDamage) * 1.3));
                            
                            
                            potentialAttackDamage += damageToBeBoosted + attacker.magicAttack;
                            Debug.Log($"POTENTIAL ATTACK DAMAGE (Staff Equipped + Affinity): {potentialAttackDamage} = {damageToBeBoosted} + {attacker.magicAttack}");
                        }
                        else
                        {
                            
                            potentialAttackDamage += attack.attackDamage + attacker.magicAttack + attacker.equippedWeapon.weaponDamage;
                            Debug.Log($"POTENTIAL ATTACK DAMAGE (Staff Equipped w/No Affinity): {potentialAttackDamage} = {attack.attackDamage} + {attacker.magicAttack} + {attacker.equippedWeapon.weaponDamage}");
                        }

                    }
                    else
                    {
                        
                        potentialAttackDamage += attack.attackDamage + attacker.magicAttack + attacker.equippedWeapon.weaponDamage;
                        Debug.Log($"POTENTIAL S.ATTACK DAMAGE: {potentialAttackDamage} = {attack.attackDamage} + {attacker.magicAttack} + {attacker.equippedWeapon.weaponDamage}");
                    }
                    
                }
                else
                {
                    
                    potentialAttackDamage += attack.attackDamage + attacker.magicAttack;
                    Debug.Log($"POTENTIAL S.ATTACK DAMAGE: {potentialAttackDamage} = {attack.attackDamage} + {attacker.magicAttack}");
                }
                break;
            default:
                if (IsAttackerEquipped(attacker) == true)
                {
                    
                    potentialAttackDamage += attack.attackDamage + attacker.physicalAttack + attacker.equippedWeapon.weaponDamage;
                    Debug.Log($"POTENTIAL P.ATTACK DAMAGE (Weapon Equipped): {potentialAttackDamage} = {attack.attackDamage} + {attacker.physicalAttack} + {attacker.equippedWeapon.weaponDamage}");
                }
                else
                {
                    
                    potentialAttackDamage += attack.attackDamage + attacker.physicalAttack;
                    Debug.Log($"POTENTIAL ATTACK DAMAGE: {potentialAttackDamage} = {attack.attackDamage} + {attacker.physicalAttack}");
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


        crit = false;
        
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
            if(attacker.equippedWeapon != null)
            {
                potentialAttackDamage = Mathf.RoundToInt((float)(potentialAttackDamage * attacker.equippedWeapon.weaponCritModifier));
            }
            else
            {
                potentialAttackDamage = Mathf.RoundToInt((float)(potentialAttackDamage * 1.25f));
            }
            Debug.Log($"YOU'VE LANDED A CRITICAL HIT! {potentialAttackDamage}");
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
                    defenderMagicDefense = Mathf.RoundToInt((float)(defender.magicDefense * 1.75));
                    break;
                case StaminaLevels.ThreeQuarters:
                    defenderMagicDefense = Mathf.RoundToInt((float)(defender.magicDefense * 1.5));
                    break;
                case StaminaLevels.Half:
                    defenderMagicDefense = Mathf.RoundToInt((float)(defender.magicDefense * 1.25));
                    break;
                case StaminaLevels.OneQuarter:
                    defenderMagicDefense = Mathf.RoundToInt((float)(defender.magicDefense * 1.15));
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
                    defenderPhysicalDefense = Mathf.RoundToInt((float)(defender.physicalDefense * 1.75));
                    break;
                case StaminaLevels.ThreeQuarters:
                    defenderPhysicalDefense = Mathf.RoundToInt((float)(defender.physicalDefense * 1.5));
                    break;
                case StaminaLevels.Half:
                    defenderPhysicalDefense = Mathf.RoundToInt((float)(defender.physicalDefense * 1.25));
                    break;
                case StaminaLevels.OneQuarter:
                    defenderPhysicalDefense = Mathf.RoundToInt((float)(defender.physicalDefense * 1.15));
                    break;
                default:
                    defenderPhysicalDefense = defender.physicalDefense * 1;
                    break;
            }

            damageAfterReductions = potentialAttackDamage - defender.physicalDefense;
        }
        

        
    }

    public void DamageAfterStatusCheck(Unit defender)
    {
        Debug.Log($"DAMAGE AFTER REDUCTIONS{damageAfterReductions} : {defender.isExhausted}");

        if (defender.isExhausted)
        {
            damageAfterReductions = (int)(damageAfterReductions * 1.5);
        }
        Debug.Log($"DAMAGE AFTER REDUCTIONS{damageAfterReductions}");
    }

    public void ReduceHealthAndStaminaOfDefender(Attack attack, Unit attacker, Unit defender)
    {

        
        int healthPreDamage = defender.currentHealth;

        //If the attacker has a weapon and it's a hammer, 
        //The defender takes stamina damage equal to half of the damage received.
        if (attacker.equippedWeapon != null)
        {
            if (attacker.equippedWeapon.weaponType == WeaponType.Hammer)
            {

                defender.currentStamina -= (int)(damageAfterReductions * .5);
                if (defender.currentStamina < 1)
                {
                    defender.currentStamina = 0;
                }
            }
        }
        
        //If the defender is defending, they take half damage after reductions
        //If the defender is not defending, they take full damage

        if (defender.isDefending)
        {
            defender.currentHealth -= damageAfterReductions * 1 / 2;
        }
        else
        {
            defender.currentHealth -= damageAfterReductions;
        }
        

        healthLost = healthPreDamage - defender.currentHealth;
        
    }

    public void CheckForAttackAbilities(Attack attack, Unit defender)
    {
        attack.AttackFunction(defender);
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

    IEnumerator WaitForTime(float time)
    {
        yield return new WaitForSeconds(time);
        //Debug.Log("Waiting");
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
//TODO: Make sure the damage calcs always make the defender lose health NOT GAIN IT if the defender has more defenses than the attacker has offense. 
//TODO: Go through all the numbers

