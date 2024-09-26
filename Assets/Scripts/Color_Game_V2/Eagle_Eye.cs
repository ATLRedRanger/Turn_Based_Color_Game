
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UnityLinker;
using UnityEngine;

public class Eagle_Eye : MonoBehaviour
{
    private List<List<object>> statusDamageQue = new List<List<object>>();
    private List<Unit_V2> listOfCombatants = new List<Unit_V2>();
    private CombatState theCombatState = CombatState.Active;
    private WhoseTurn whoseTurnIsIt = WhoseTurn.Nobody;
    [SerializeField]
    private Unit_V2 player;
    private Unit_V2 enemyOne;
    private Unit_V2 enemyTwo;
    private Unit_V2 currentPC;
    private int numOfEnemies;
    private int turnsInRound;
    private string currentLocation = "Forest";

    private Attack chosenAttack = null;
    private Unit_V2 chosenPCTarget = null;
    private Unit_V2 chosenEnemyTarget = null;
    private bool playerTurnIsDone = false;


    //Scripts
    private Unit_Spawner unitSpawnerScript;
    private Environment envManaScript;
    private StatusEffectsDatabase_V2 statusEffectScript;
    private Attack_Database attackDatabaseScript;
    private ButtonsAndPanels buttonsAndPanelsScript;
    private UI_V2 uiScript;
    private Weapon_Database_V2 weaponDatabaseScript;
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(LoadScripts());

        
    }

    public void Test()
    {
        
        /*
        CheckAttack_StatusBuildupRelationship(player.GetAttackDictionary()["Fireball"], enemyOne);
        CheckStatusTimes(listOfCombatants);
        EndOfRoundStatusDamage();
        uiScript.SetPlayerHealthAndStamina(player);
        uiScript.SetEnemeyOneHealthAndStamina(enemyOne);
        */

        GenerateEnemies();
        listOfCombatants.Add(player);
        
        currentPC = player;
        GenerateEnvironment();
        SetMaxColorAmountsForUI();
        
    }

    public void Test_2()
    {
        
        StartCoroutine(Combat());
        
    }

    public void Test_3()
    {
        player.equippedWeapon = weaponDatabaseScript.basicAxe;

    }
    IEnumerator LoadScripts()
    {
        yield return new WaitForSeconds(1);

        unitSpawnerScript = FindObjectOfType<Unit_Spawner>();
        envManaScript = FindObjectOfType<Environment>();
        statusEffectScript = FindObjectOfType<StatusEffectsDatabase_V2>();
        attackDatabaseScript = FindObjectOfType<Attack_Database>();
        buttonsAndPanelsScript = FindObjectOfType<ButtonsAndPanels>();
        uiScript = FindObjectOfType<UI_V2>();
        weaponDatabaseScript = FindObjectOfType<Weapon_Database_V2>();

        player = unitSpawnerScript.SpawnPlayer();
        
        //Debug.Log("Finished Loading");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void SortCombatants(List<Unit_V2> listOfCombatants)
    {
        listOfCombatants.Sort((y, x) => x.GetCurrentSpeed().CompareTo(y.GetCurrentSpeed()));
    }

    private void GenerateEnemies()
    {
        player.gameObject.SetActive(true);
        int enemiesToGenerate = 1;//Random.Range(1, 3);
        //Debug.Log($"Generated Enemies: {enemiesToGenerate}");
        for(int i = 0; i < enemiesToGenerate; i++) 
        {
            switch (i)
            {
                case 0:
                    enemyOne = unitSpawnerScript.GenerateEnemy(0, currentLocation);
                    Debug.Log($"EnemyOne is {enemyOne.unitName}");
                    listOfCombatants.Add(enemyOne);

                    break;
                case 1:
                    enemyTwo = unitSpawnerScript.GenerateEnemy(1, currentLocation);
                    Debug.Log($"EnemyTwo is {enemyTwo.unitName}");
                    listOfCombatants.Add(enemyTwo);

                    break;
                default:
                    break;
            }
        }
        
    }

    private void CombatUIUpdates()
    {
        uiScript.SetPlayerHealthAndStamina(currentPC);
        uiScript.SetEnemeyOneHealthAndStamina(enemyOne);
        uiScript.SetEnemeyTwoHealthAndStamina(enemyTwo);

        UpdateEnvironmentColorsForUI();
    }

    IEnumerator Combat()
    {
        
        List<Unit_V2> deadUnits = new List<Unit_V2>();
        theCombatState = CombatState.Active;
        
        
        //Debug.Log($"List Of Combatants: {listOfCombatants.Count}");
        int currentRound = 0;
        yield return new WaitForSeconds(1f);
        while (theCombatState == CombatState.Active)
        {
            /*
             * Beginning of Turn Operations:
             * Tell the Buttons and Panels script which enemies are still alive.
             * 
            */
            buttonsAndPanelsScript.ToggleEnemyButtons(enemyOne, enemyTwo);
            UpdateEnvironmentColors();
            CombatUIUpdates();

            currentRound++;
            Debug.Log($"Current Round: {currentRound}");

            SortCombatants(listOfCombatants);
            yield return new WaitForSeconds(1f);
            foreach (Unit_V2 unit in listOfCombatants)
            {
                //Debug.Log($"Current Unit: {unit.unitName}");
                if (unit is Player_V2)
                {

                    currentPC = unit;
                    buttonsAndPanelsScript.ToggleFightPanel();
                    PlayerTurn();
                    yield return new WaitUntil(PlayerTurnIsDone);
                    if (IsPlayerAlive(player))
                    {
                        Debug.Log("Player is alive");
                    }
                    else
                    {
                        theCombatState = CombatState.Lost;
                        break;
                    }
                }
                if (unit is EnemyUnit_V2)
                {
                    if (unit.GetCurrentHp() < 1)
                    {
                        Debug.Log($"Adding {unit} to DeadUnitList");
                        deadUnits.Add(unit);
                        
                    }
                    else
                    {
                        EnemyTurn(unit as EnemyUnit_V2);
                        if (IsPlayerAlive(player))
                        {
                            //Debug.Log("Player is alive");
                        }
                        else
                        {
                            theCombatState = CombatState.Lost;
                            break;
                        }
                    }
                    
                }
            }
            //End of turn stuff
            yield return new WaitForSeconds (.5f);
            CheckStatusTimes(listOfCombatants);
            yield return new WaitForSeconds(.5f);
            CheckBuffsAndDebuffs(listOfCombatants);
            yield return new WaitForSeconds(.5f);
            EndOfRoundStatusDamage();
            yield return new WaitForSeconds(.5f);
            //TODO: Add environment color regen.
            if (currentRound % 3 ==  0)
            {
                envManaScript.RegenEnvColors();
            }
            
            
            foreach (Unit_V2 unit in listOfCombatants)
            {
                if ( unit is EnemyUnit_V2 && !deadUnits.Contains(unit) && unit.GetCurrentHp() < 1)
                {
                   deadUnits.Add(unit);
                   //Debug.Log($"Dead Units Count: {deadUnits.Count}");
                }
            }

            foreach (EnemyUnit_V2 enemy in deadUnits)
            {
                //Debug.Log($"DeadUnit: {enemy.unitName}");
                if (enemy == enemyOne)
                {  
                    listOfCombatants.Remove(enemy);
                }
                else if(enemy == enemyTwo)
                {
                    listOfCombatants.Remove(enemy); 
                }
            }

            CombatUIUpdates();

            if (listOfCombatants.Count == 1 && listOfCombatants[0] is Player_V2)
            {
                theCombatState = CombatState.Won;
                break;
            }
            
            if (IsPlayerAlive(player))
            {
                Debug.Log("Player is alive");
            }
            else
            {
                theCombatState = CombatState.Lost;
                break;
            }
            CombatUIUpdates();
        }
        yield return new WaitForSeconds(1f);
        if (theCombatState == CombatState.Won)
        {
            PlayerWon();
        }
        else
        {
            PlayerLost();
        }
    }
    
    private bool IsPlayerAlive(Unit_V2 player)
    {
        if (player.GetCurrentHp() > 1)
        {
            return true;
        }

        return false;
    }

    private void PlayerTurn()
    {
        playerTurnIsDone = false;
        currentPC.isDefending = false;
        chosenAttack = null;
        chosenEnemyTarget = null;
        Debug.Log("Player Turn");
        StartCoroutine(WaitForPlayerDecisions());
    }

    private bool PlayerTurnIsDone()
    {
        if (playerTurnIsDone)
        {
            return true;
        }
        return false;
    }

    // Helper function to calculate damage multiplier (replace with your implementation for critical hits, etc.)
    private float CalculateDamageMultiplier()
    {
        float roll = Random.Range(1f, 5f);

        switch ((int)roll) // Cast roll to int for case matching
        {
            case 1:
                return 1.25f;
            case 2:
                return 1.5f;
            case 3:
                return 1.75f;
            case 4:
                return 2.0f;
            default:
                return 1.0f;
        }
    }

    private bool DoesAttackCrit(Unit_V2 attacker)
    {
        //TODO: Attacks only crit if there's a weapon equipped
        //Is this what I want?
        //Matt Suggestion: The first time a color is at full each combat, that color effect is critical. 
        //Vincent Respons: Maybe a higher chance so that it's not a guarantee.
        int roll = Random.Range(1, 101);
        switch (attacker.StaminaLevelConversion())
        {
            case StaminaLevels.Full:
                break;
            case StaminaLevels.ThreeQuarters: 
                break;
            case StaminaLevels.Half: 
                break;
            case StaminaLevels.OneQuarter: 
                break;
            case StaminaLevels.Empty: 
                break;
            case StaminaLevels.Broken:
                break;
        }
        
        return false;
    }

    private int CalcAttackDamage(Attack attack, Unit_V2 attacker, Unit_V2 defender)
    {
        float baseDamage = attack.attackPower;
        int damageBeforeDefenses = 0;
        int damageAfterDefenses = 0;
        float damageMultiplier = CalculateDamageMultiplier(); // Helper function

        // Calculate base damage with potential random variation
        if(attacker.equippedWeapon != null)
        {
            switch (attack.attackType)
            {
                case AttackType.Physical:
                    if (attacker.equippedWeapon.weaponType == WeaponType.Axe || attacker.equippedWeapon.weaponType == WeaponType.Bow || attacker.equippedWeapon.weaponType == WeaponType.Hammer || attacker.equippedWeapon.weaponType == WeaponType.Sword)
                    {
                        baseDamage = attack.attackPower + attacker.GetCurrentAttack() + attacker.equippedWeapon.GetWeaponBaseDamage();
                        Debug.Log($"Physical Attack with a physical weapon equipped: ({baseDamage}) = {attack.attackPower} + {attacker.GetCurrentAttack()}");
                    }

                    break;
                case AttackType.Special:
                    if (attacker.equippedWeapon.weaponType == WeaponType.Spellbook || attacker.equippedWeapon.weaponType == WeaponType.Staff)
                    {
                        baseDamage = attack.attackPower + attacker.GetCurrentAttack() + attacker.equippedWeapon.GetWeaponBaseDamage();
                        Debug.Log($"Special Attack with a special weapon equipped: ({baseDamage}) = {attack.attackPower} + {attacker.GetCurrentAttack()}");
                    }

                    break;
            }
        }
        else
        {
            baseDamage = attack.attackPower + attacker.GetCurrentAttack();
            Debug.Log($"Base Damage: ({attack.attackPower}) + ({attacker.GetCurrentAttack()})");
        }
        

        // Apply damage multiplier for critical hits, etc.
        damageBeforeDefenses = Mathf.RoundToInt(baseDamage * damageMultiplier);
        Debug.Log($"DamageBeforeDefenses ({damageBeforeDefenses}) = {baseDamage} * {damageMultiplier}");

        // Apply damage after critical hit.
        if (DoesAttackCrit(attacker))
        {
            damageBeforeDefenses *= Mathf.RoundToInt(damageBeforeDefenses * 1.5f);
        }

        // Apply color resistances based on attack type and color
        damageAfterDefenses = ApplyColorAndWeaponResistances(attack.attackColor, damageBeforeDefenses, attacker, defender);
        Debug.Log($"DamageAfterDefenses: {damageAfterDefenses}");


        if (defender.isDefending)
        {
            damageAfterDefenses = Mathf.RoundToInt(damageAfterDefenses / 2);
        }
        return damageAfterDefenses;
    }

    

    private int ApplyColorAndWeaponResistances(Hue attackColor, int damage, Unit_V2 attacker, Unit_V2 defender)
    {
        float combinedResistances = 0;

        if (attacker.equippedWeapon != null)
        {
            combinedResistances = defender.GetWeaponResistances()[attacker.equippedWeapon.weaponType];
        }

        combinedResistances += defender.GetColorResistances()[attackColor];

        return damage - Mathf.RoundToInt(damage * combinedResistances);
        
    }

    private void CheckAttack_StatusBuildupRelationship(Attack attack, Unit_V2 defender)
    {
        switch(attack.attackBehavior)
        {
            case AttackBehavior.Burn:
                if (defender.DoesStatusExist(statusEffectScript.burn))
                {
                    foreach (StatusEffect_V2 status in defender.unitStatusEffects)
                    {
                        if (status.GetStatusName() == "Burn")
                        {
                            status.effectStack += 1;
                            //Debug.Log($"{defender.unitName}'s burn stack: {status.effectStack}.");
                        }
                    }
                }
                else if (defender.GetBurnAmount() >= defender.GetBurnThreshhold())
                {
                    //Debug.Log($"{defender.unitName} is now burning!");
                    defender.AddStatus(statusEffectScript.burn);
                    defender.SetBurnAmountToZero();
                }
                else
                {
                    defender.AddToBurnAmount(1);
                }
                
                break;
        }
    }

    private void CheckAttack_Buff_DebuffBuildupRelationship(Attack attack, Unit_V2 chosenTarget)
    {
        if(attack.attackBuff != null)
        {
            foreach (Buffs buff in chosenTarget.GetListOfBuffs())
            {
                if (chosenTarget.DoesStatusExist(buff))
                {
                    continue;
                }
                else
                {
                    buff.ApplyBuff(chosenTarget);
                }
            }
            
        }

        if(attack.attackDebuff != null)
        {
            //Debug.Log("Attack Debuff != Null");
            if (chosenTarget.DoesStatusExist(attack.attackDebuff))
            {
                //Debug.Log($"{attack.attackDebuff.GetStatusName()} is on the {chosenTarget.unitName}");
                
            }
            else
            {

                attack.attackDebuff.ApplyDebuff(chosenTarget);
            }
           
        }
        
        
        
    }

    private void CheckStatusTimes(List<Unit_V2> listOfCombatants)
    {
        List<StatusEffect_V2> removeStatus = new List<StatusEffect_V2>();

        foreach (Unit_V2 unit in listOfCombatants)
        {
            foreach (StatusEffect_V2 status in unit.unitStatusEffects)
            {
                //Debug.Log("First Foreach");
                switch (status.GetStatusName())
                {
                    case "Burn":
                        //Debug.Log($"{unit.unitName}'s burnTimer: {unit.GetBurnTimer()}");
                        if (unit.GetBurnTimer() >= status.GetEffectLength())
                        {
                            Debug.Log("Setting Burn Timer To Zero");
                            unit.SetBurnTimerToZero();
                            removeStatus.Add(status);
                        }
                        else
                        {
                            
                            statusDamageQue.Add(new List<object> {unit, status.GetStatusDamage(), status.timeNeededInQue});
                            //Debug.Log("Burn Damage: " + status.GetStatusDamage());
                            unit.AddToBurnTimer(1);
                            
                        }
                        break;
                }

            }
            foreach (StatusEffect_V2 status in removeStatus)
            {
                
                //Debug.Log($"Trying to remove {status.GetStatusName()}");
                if (unit.DoesStatusExist(status))
                {
                    Debug.Log($"{status.GetStatusName()} has been removed.");
                    unit.unitStatusEffects.Remove(status);
                }
            }
            removeStatus.Clear();
        }
    }

    private void CheckBuffsAndDebuffs(List<Unit_V2> listOfCombatants)
    {
        List<Buffs> removeBuff = new List<Buffs>();
        List<Debuffs> removeDebuff = new List<Debuffs>();
        foreach (Unit_V2 unit in listOfCombatants)
        {
            //Debug.Log($"{unit.unitName} BuffsCount: {unit.GetListOfBuffs().Count}");
            //Debug.Log($"{unit.unitName} DebuffsCount: {unit.GetListOfDebuffs().Count}");
            foreach (Buffs buff in unit.GetListOfBuffs())
            {
                if (buff.GetTimeActive() < 1)
                {
                    buff.ActivateBuffEffect(unit);
                }
                
                if (buff.GetTimeActive() > buff.GetEffectLength())
                {
                    removeBuff.Add(buff);
                }
                else
                {
                    buff.SetTimeActive(1);
                }
            }

            foreach (Debuffs debuff in unit.GetListOfDebuffs())
            {

                if (debuff.GetTimeActive() < 1)
                {
                    debuff.ActivateDebuffEffect(unit);
                }
                if(debuff.GetTimeActive() >= debuff.GetEffectLength())
                {
                    removeDebuff.Add(debuff);
                }
                else
                {
                    debuff.SetTimeActive(1);
                }
            }

            if (unit.GetListOfBuffs().Count > 0)
            {
                foreach (Buffs buff in removeBuff)
                {
                    
                    if (unit.DoesStatusExist(buff))
                    {
                        //Debug.Log($"{buff.GetStatusName()} has been removed.");
                        buff.RevertBuffEffect(unit);
                        unit.GetListOfBuffs().Remove(buff);
                    }
                }
            }

            
            
            foreach (Debuffs debuff in removeDebuff)
            {
                
                if (unit.DoesStatusExist(debuff))
                {
                    unit.GetListOfDebuffs().Remove(debuff);
                   
                    debuff.RevertDebuffEffect(unit);
                }
                
            }
            

            

            

            removeBuff.Clear();
            removeDebuff.Clear();
        }
    }

    public void EndOfRoundStatusDamage()
    {
        //This function is for timed damage effects to go off
        //The timeInQue is so that statusEffects can sit in the que to "cook"
        //Then when the timer ticks down to 0, the statusEffect goes off
        //This system was intended for effects like Pokemon's Future Sight
        //The way it's supposed to work is that you look at each object in the que,
        //you then iterate over it looking for whichUnit is being affected,
        //how much damage are they going to take and then when is the damage supposed to happen.
        //Then when the timeInQue is NOT < 1, you add it to the blank list, clear the status list, 
        //then put it back in the Que.

        List <List<object>> blankList = new List<List<object>>();
        Unit_V2 unit = null;
        int damage = 0;
        int timeInQue;

        if (statusDamageQue.Count != 0)
        {
            for (int i = 0; i < statusDamageQue.Count; i++)
            {
                unit = statusDamageQue[i][0] as Unit_V2;
                if (statusDamageQue[i][1] is int)
                {
                    damage = (int)(statusDamageQue[i][1]);
                }
                if (statusDamageQue[i][2] is int)
                {
                    timeInQue = (int)statusDamageQue[i][2];
                    if (timeInQue < 1)
                    {
                        unit.TakeDamage(damage);
                        
                    }
                    else
                    {
                        statusDamageQue[i][2] = timeInQue - 1;
                        blankList.Add(statusDamageQue[i]);
                    }
                }
            }
        }

        statusDamageQue.Clear();
        statusDamageQue = blankList;
    }

    IEnumerator WaitForPlayerDecisions()
    {
        
        //buttonsAndPanelsScript.ToggleFightPanel();
        yield return new WaitUntil(PlayerChoiceIsMade);
        //Player is attacking single target
        if (AttackIsChosen() && EnemyIsChosen())
        {
            //buttonsAndPanelsScript.ToggleFightPanel();
            PayAttackCost(currentPC, chosenAttack);
            Debug.Log($"Chosen Attack: {chosenAttack.attackName}");
            Debug.Log($"Chosen Attack Target: {chosenEnemyTarget.unitName}");
            yield return new WaitForSeconds(1);

            for (int i = 0; i < chosenAttack.numOfHits; i++)
            {
                if (chosenAttack.DoesAttackHit(currentPC))
                {
                    Debug.Log("Attack Hits");
                    int damage = CalcAttackDamage(chosenAttack, currentPC, chosenEnemyTarget);
                    int staminaDamage = 0;
                    CheckAttack_StatusBuildupRelationship(chosenAttack, chosenEnemyTarget);
                    //Debug.Log(currentPC.equippedWeapon.itemName);
                    if (currentPC.equippedWeapon != null)
                    {
                        switch (currentPC.equippedWeapon)
                        {
                            case Weapon_Axe axe:
                                damage = Mathf.RoundToInt(damage * axe.healthPercent);
                                staminaDamage = Mathf.RoundToInt(damage * axe.staminaPercent);
                                Debug.Log($"STAMINA DAMAGE: {staminaDamage}");
                                break;
                            case Weapon_Hammer hammer:
                                damage = Mathf.RoundToInt(damage * hammer.healthPercent);
                                staminaDamage = Mathf.RoundToInt(damage * hammer.staminaPercent);
                                Debug.Log($"STAMINA DAMAGE: {staminaDamage}");
                                break;
                            default:
                                break;
                        }
                    }
                    chosenEnemyTarget.TakeDamage(damage);
                    chosenEnemyTarget.ReduceStamina(Mathf.Clamp(staminaDamage, 1, staminaDamage));
                    CheckAttack_Buff_DebuffBuildupRelationship(chosenAttack, chosenEnemyTarget);
                    
                }
                else
                {
                    Debug.Log("Attack Doesn't Hit");
                }
            }
        }
        
        Debug.Log("PLAYER TURN HAS FINISHED!");
        
        playerTurnIsDone = true;
    }

    public List<string> IsPlayerAttackUseable()
    {
        List<string> useableAttacks = new List<string>();

        foreach(var kvp in currentPC.GetAttackDictionary())
        {
            if (currentPC.GetCurrentStamina() >= kvp.Value.staminaCost && envManaScript.GetCurrentColorDictionary()[kvp.Value.attackColor] >= kvp.Value.colorCost)
            {
               useableAttacks.Add(kvp.Key);
            }
        }
        return useableAttacks;
    }

    private void PayAttackCost(Unit_V2 attacker, Attack attack)
    {
        attacker.ReduceStamina(attack.staminaCost);
        switch (attack.attackColor)
        {
            case Hue.Red:
                envManaScript.LoseRed(attack.colorCost);
                break;
            case Hue.Orange:
                envManaScript.LoseOrange(attack.colorCost);
                break;
            case Hue.Yellow:
                envManaScript.LoseYellow(attack.colorCost);
                break;
            case Hue.Green:
                envManaScript.LoseGreen(attack.colorCost);
                break;
            case Hue.Blue:
                envManaScript.LoseBlue(attack.colorCost);
                break;
            case Hue.Violet:
                envManaScript.LoseViolet(attack.colorCost);
                break;
            default:
                break;
        }
        
            
        
        
    }


    

    private void EnemyTurn(EnemyUnit_V2 unit)
    {
        Unit_V2 enemyChosenTarget = player;
        Debug.Log(enemyChosenTarget.unitName);
        Attack enemyChosenAttack = unit.EnemyAttackDecision(envManaScript);
        if(enemyChosenTarget != null)
        {
            PayAttackCost(unit, enemyChosenAttack);
            for(int i = 0; i < enemyChosenAttack.numOfHits; i++)
            {
                if (enemyChosenAttack.DoesAttackHit(unit))
                {
                    int damage = CalcAttackDamage(enemyChosenAttack, unit, enemyChosenTarget);
                    int staminaDamage = Mathf.Clamp(damage / 3, 1, damage/3);
                    CheckAttack_StatusBuildupRelationship(enemyChosenAttack, enemyChosenTarget);
                    enemyChosenTarget.TakeDamage(damage);
                    Debug.Log($"Stamina Damage: {staminaDamage}");
                    enemyChosenTarget.ReduceStamina(staminaDamage);
                    CheckAttack_Buff_DebuffBuildupRelationship(enemyChosenAttack, enemyChosenTarget);
                }
            }
            
        }
        /*
        Debug.Log($"{unit.unitName}'s Turn");
        if (player != null)
        {
            player.TakeDamage(5);
            
        }*/
    }

    private void PlayerWon()
    {
        Debug.Log("Player Won");
    }
    private void PlayerLost()
    {
        Debug.Log("Player Lost");
    }

    private void GenerateEnvironment()
    {
        currentLocation = "Forest";
        envManaScript.GenerateEnvironment(currentLocation);
    }

    private void SetMaxColorAmountsForUI()
    {
        uiScript.SetMaxColorAmounts(envManaScript.maxRed, envManaScript.maxOrange, envManaScript.maxYellow,
                                        envManaScript.maxGreen, envManaScript.maxBlue, envManaScript.maxViolet);
    }

    private void UpdateEnvironmentColors()
    {
        envManaScript.UpdateEnvironmentColorDictionary();
    }

    private void UpdateEnvironmentColorsForUI()
    {
        uiScript.UpdateEnvironmentColors(envManaScript.currentRed, envManaScript.currentOrange, envManaScript.currentYellow,
                                        envManaScript.currentGreen, envManaScript.currentBlue, envManaScript.currentViolet);
    }

    public void AttackChangeNotification(string attack)
    {
        switch(attack)
        {
            case "Fireball":
                chosenAttack = attackDatabaseScript._fireball;
                break;
            case "Attack":
                if(currentPC.equippedWeapon != null)
                {
                    switch (currentPC.equippedWeapon.weaponType)
                    {
                        case WeaponType.Axe:
                            chosenAttack = attackDatabaseScript._basicAxeAttack;
                            break;
                        case WeaponType.Bow:
                            chosenAttack = attackDatabaseScript._basicBowAttack;
                            break;
                        case WeaponType.Hammer:
                            chosenAttack = attackDatabaseScript._basicHammerAttack;
                            break;
                        case WeaponType.Spellbook:
                            chosenAttack = attackDatabaseScript._basicSpellbookAttack;
                            break;
                        case WeaponType.Staff:
                            chosenAttack = attackDatabaseScript._basicStaffAttack;
                            break;
                        case WeaponType.Sword:
                            chosenAttack = attackDatabaseScript._basicSwordAttack;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    chosenAttack = attackDatabaseScript._basicAttack;
                }
                
                break;
        }
    }

    private bool AttackIsChosen()
    {
        if(chosenAttack != null)
        {
            return true;
        }
        return false;
    }

    private bool EnemyIsChosen()
    {
        if (chosenEnemyTarget != null)
        {
            return true;
        }
        return false;
    }

    public void DefendIsChosen()
    {
        currentPC.isDefending = true;
        Debug.Log($"{currentPC.unitName} is Defending!");
    }
    private bool PlayerChoiceIsMade()
    {
        
        if (AttackIsChosen() && EnemyIsChosen())
        {
            return true;
        }else if (currentPC.isDefending)
        {
            return true;
        }
        return false;
    }
    public void SetAttackTarget(string enemy)
    {
        switch (enemy)
        {
            case "EnemyOne":
                chosenEnemyTarget = enemyOne;
                Debug.Log($"{enemyOne.unitName} is the chosen target!");
                break;
            case "EnemyTwo":
                chosenEnemyTarget = enemyTwo;
                Debug.Log($"{enemyTwo.unitName} is the chosen target!");
                break;
            default:
                //chosenEnemyTarget = enemyOne;
                break;
        }
        
    }

    public void ResetAttackAndEnemyTargets()
    {
        chosenAttack = null;
        chosenEnemyTarget = null;
    }
}
