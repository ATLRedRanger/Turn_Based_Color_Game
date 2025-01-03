
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle_Eye : MonoBehaviour
{
    private List<List<object>> statusDamageQue = new List<List<object>>();
    [SerializeField]
    private List<Unit_V2> listOfCombatants = new List<Unit_V2>();
    private CombatState theCombatState = CombatState.Active;
    private WhoseTurn whoseTurnIsIt = WhoseTurn.Nobody;
    [SerializeField]
    private Player_V2 player;
    [SerializeField]
    private Unit_V2 enemyOne;
    [SerializeField]
    private Unit_V2 enemyTwo;
    private Unit_V2 currentPC;
    private int numOfEnemies;
    private int turnsInRound;
    [SerializeField]
    private string currentLocation = "";
    private SubLocation subLocation = SubLocation.beginning;

    private Attack chosenAttack = null;
    private Item_Consumable chosenConsumable = null;
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
    //private Weapon_Database_V2 weaponDatabaseScript;
    private Item_Database itemDatabaseScript;
    private Inventory_V2 inventoryScript;




    private string lootDrop1 = null;
    private string lootDrop2 = null;


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

        StartCombat();

    }

    public void Test_2()
    {
        //EndCombat();
        //buttonsAndPanelsScript.ToggleLocationsPanel();
    }

    public void Test_3()
    {

        //inventoryScript.AddToInventory(weaponDatabaseScript.basicAxe);
        inventoryScript.AddToInventory(itemDatabaseScript.burnHeal);
        player.equippedWeapon = itemDatabaseScript.basicStaff;
        //player.equippedWeapon = itemDatabaseScript.basicSpellbook;
        
    }

    public void Test_4()
    {
        
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
        //weaponDatabaseScript = FindObjectOfType<Weapon_Database_V2>();
        itemDatabaseScript = FindObjectOfType<Item_Database>();
        inventoryScript = FindObjectOfType<Inventory_V2>();

        player = unitSpawnerScript.SpawnPlayer();
        player.gameObject.SetActive(false);

        //Debug.Log("Finished Loading");

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void StartCombat()
    {
        player.gameObject.SetActive(true);
        GenerateEnemies();
        listOfCombatants.Add(player);

        currentPC = player;
        GenerateEnvironment();
        SetMaxColorAmountsForUI();
        StartCoroutine(Combat());
        buttonsAndPanelsScript.ToggleCombatPanel();
    }

    private void EndCombat()
    {
        player.gameObject.SetActive(false);
        buttonsAndPanelsScript.ToggleCombatPanel();
        if(enemyOne != null)
        {
            Destroy(enemyOne.gameObject);
            enemyOne = null;
        }
        if (enemyTwo != null)
        {
            Destroy(enemyTwo.gameObject);
            enemyTwo = null;
        }

        
        
        listOfCombatants.Clear();
    }

    //Sorts combatants from fastest to slowest
    private void SortCombatants(List<Unit_V2> listOfCombatants)
    {
        listOfCombatants.Sort((y, x) => x.GetCurrentSpeed().CompareTo(y.GetCurrentSpeed()));
    }

    private void GenerateEnemies()
    {
        
        int enemiesToGenerate = Random.Range(1, 3);
        //Debug.Log($"Generated Enemies: {enemiesToGenerate}");
        if(subLocation != SubLocation.subLocation_5)
        {
            for (int i = 0; i < enemiesToGenerate; i++)
            {
                switch (i)
                {
                    case 0:
                        enemyOne = unitSpawnerScript.GenerateEnemy(0, currentLocation);
                        
                        listOfCombatants.Add(enemyOne);

                        break;
                    case 1:
                        enemyTwo = unitSpawnerScript.GenerateEnemy(1, currentLocation);
                        
                        listOfCombatants.Add(enemyTwo);

                        break;
                    default:
                        break;
                }
            }
        }
        else
        {
            switch (currentLocation)
            {
                case "Forest":
                    enemyOne = unitSpawnerScript.GenerateEnemy(0, "Forest_Boss");
                    listOfCombatants.Add(enemyOne);
                    break;
            }
        }
        

    }

    private void CombatUIUpdates()
    {
        UpdateEnvironmentColors();
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
        yield return new WaitForSeconds(.1f);
        while (theCombatState == CombatState.Active)
        {
            /*
             * Beginning of Turn Operations:
             * Tell the Buttons and Panels script which enemies are still alive.
             * 
            */
            if (currentRound % 3 == 0)
            {
                envManaScript.RegenEnvColors();
            }
            buttonsAndPanelsScript.ToggleEnemyButtons(enemyOne, enemyTwo);
            UpdateEnvironmentColors();
            CombatUIUpdates();

            currentRound++;
            //Debug.Log($"Current Round: {currentRound}");

            SortCombatants(listOfCombatants);
            yield return new WaitForSeconds(1);
            foreach (Unit_V2 unit in listOfCombatants)
            {
                CombatUIUpdates();
                //Debug.Log($"Current Unit: {unit.unitName}");
                if (unit is Player_V2)
                {

                    currentPC = unit;
                    
                    if (IsPlayerAlive(player))
                    {
                        yield return new WaitForSeconds(1);
                        PlayerTurn();
                        yield return new WaitUntil(PlayerTurnIsDone);
                        //Debug.Log("Player is alive");
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
                        //Debug.Log($"Adding {unit} to DeadUnitList");
                        deadUnits.Add(unit);

                    }
                    else
                    {

                        if (IsPlayerAlive(player))
                        {
                            yield return new WaitForSeconds(1);
                            StartCoroutine(EnemyTurn(unit as EnemyUnit_V2));
                            //Debug.Log("Player is alive");
                        }
                        else
                        {
                            theCombatState = CombatState.Lost;
                            break;
                        }
                    }
                    yield return new WaitForSeconds(1);
                }
                CombatUIUpdates();
            }
            //End of turn stuff

            yield return new WaitForSeconds(.5f);
            CheckStatusTimes(listOfCombatants);
            yield return new WaitForSeconds(.5f);
            CheckBuffsAndDebuffs(listOfCombatants);
            yield return new WaitForSeconds(.5f);
            EndOfRoundStatusDamage();
            //yield return new WaitForSeconds(.5f);
            yield return new WaitForSeconds(.5f);
            CombatUIUpdates();

            foreach (Unit_V2 unit in listOfCombatants)
            {
                if (unit is EnemyUnit_V2 && !deadUnits.Contains(unit) && unit.GetCurrentHp() < 1)
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
                else if (enemy == enemyTwo)
                {
                    listOfCombatants.Remove(enemy);
                }
            }

            if (listOfCombatants.Count == 1 && listOfCombatants[0] is Player_V2)
            {
                theCombatState = CombatState.Won;
                break;
            }

            if (IsPlayerAlive(player))
            {
                //Debug.Log("Player is alive");
            }
            else
            {
                theCombatState = CombatState.Lost;
                break;
            }
            
            foreach (Unit_V2 unit in listOfCombatants)
            {
                if (unit is EnemyUnit_V2)
                {
                    EnemyUnit_V2 enemy = unit as EnemyUnit_V2;
                    enemy.UnitColorBehavior(envManaScript.GetCurrentColorDictionary());
                }
            }

            yield return new WaitForSeconds(1);

        }
        //yield return new WaitForSeconds(.5f);

        if (theCombatState == CombatState.Won)
        {
            PlayerWon(deadUnits);
        }
        else
        {
            PlayerLost(deadUnits);
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
        currentPC.isDefending = false;
        currentPC.usedItem = false;
        buttonsAndPanelsScript._fightButton.gameObject.SetActive(true);
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


    private float CalculateDamageMultiplier()
    {
        int roll = Random.Range(1, 5);

        switch (roll) // Cast roll to int for case matching
        {
            case 1:
                return 1.05f;
            case 2:
                return 1.1f;
            case 3:
                return 1.2f;
            case 4:
                return 1.3f;
            default:
                return 1.0f;
        }
    }

    private bool DoesAttackCrit(Attack attack)
    {
        //TODO: Attacks only crit if there's a weapon equipped
        //Is this what I want?
        //Matt Suggestion: The first time a color is at full each combat, that color effect is critical. 
        //Vincent Responds: Maybe a higher chance so that it's not a guarantee.
        int roll = Random.Range(1, 21);
        if (envManaScript.GreatestColorInEnvironment(attack.attackColor))
        {
            Debug.Log("CRIT ROLL + 2");
            roll += 2;
        }

        Debug.Log($"CRIT ROLL: {roll}");

        if (roll >= attack.critRoll)
        {
            Debug.Log("CRITICAL HIT!");
            return true;
        }

        return false;
    }

    private int CalcAttackDamage(Attack attack, Unit_V2 attacker, Unit_V2 defender)
    {

        float baseDamage = attack.attackPower + attacker.GetUnitDamage();
        //Debug.Log($"{attack.attackName}'s base damage is {baseDamage}");
        int damageBeforeDefenses = 0;
        int damageAfterDefenses = 0;
        float damageMultiplier = CalculateDamageMultiplier(); // Helper function

        CheckAttackBehavior(attack);

        Debug.Log($"Base Damage: {baseDamage} = Attack Power: {attack.attackPower} + Attacker Unit Damage: {attacker.GetUnitDamage()}");
        // Calculate base damage with potential random variation
        if (attacker.equippedWeapon != null)
        {
            switch (attack.attackType)
            {
                case AttackType.Physical:
                    if (attacker.equippedWeapon.weaponType == WeaponType.Axe || attacker.equippedWeapon.weaponType == WeaponType.Bow || attacker.equippedWeapon.weaponType == WeaponType.Hammer || attacker.equippedWeapon.weaponType == WeaponType.Sword)
                    {
                        
                    }
                    else
                    {
                        baseDamage -= attacker.equippedWeapon.GetWeaponBaseDamage();
                    }

                    break;
                case AttackType.Special:
                    if (attacker.equippedWeapon.weaponType == WeaponType.Spellbook)
                    {
                        
                    }
                    else if(attacker.equippedWeapon.weaponType == WeaponType.Staff)
                    {
                        Weapon_Staff staff = attacker.equippedWeapon as Weapon_Staff;
                        if(staff.affinity != Hue.Neutral && staff.affinity == attack.attackColor)
                        {
                            Debug.Log($"Staff Affinity: {staff.affinity} and Attack Color: {attack.attackColor}");
                            baseDamage = Mathf.RoundToInt(baseDamage * 1.2f);
                        }
                    }
                    else
                    {
                        baseDamage -= attacker.equippedWeapon.GetWeaponBaseDamage();
                    }

                    break;
            }
        }
        //Debug.Log($"Base Damage: {baseDamage}");

        // Apply damage multiplier for critical hits, etc.
        //Debug.Log($"DamageBeforeDefenses ({damageBeforeDefenses}) = {baseDamage} * {damageMultiplier}");
        damageBeforeDefenses = Mathf.RoundToInt(baseDamage * damageMultiplier);
        

        // Apply damage after critical hit.
        if (DoesAttackCrit(attack))
        {
            damageBeforeDefenses = Mathf.RoundToInt(damageBeforeDefenses * 2f);
        }

        // Apply color resistances based on attack type and color
        damageAfterDefenses = ApplyColorAndWeaponResistances(attack, damageBeforeDefenses, defender);
        //Debug.Log($"DamageAfterDefenses: {damageAfterDefenses}");

        
        if (defender.isDefending)
        {
            damageAfterDefenses = Mathf.RoundToInt(damageAfterDefenses / 2);
        }

        //Keeps the damage from being negative.
        damageAfterDefenses = Mathf.Clamp(damageAfterDefenses, 0, damageAfterDefenses);

        return damageAfterDefenses;
    }



    private int ApplyColorAndWeaponResistances(Attack attack, int damage, Unit_V2 defender)
    {
        float combinedResistances = 0;

        combinedResistances += defender.GetColorResistances()[attack.attackColor];

        Debug.Log($"Damage: {damage} - Mathf.RoundToInt(damage({damage} * combinedResistances({combinedResistances})");

        return damage - Mathf.RoundToInt(damage * combinedResistances);

    }

    private void CheckAttack_StatusBuildupRelationship(Attack attack, Unit_V2 defender)
    {
        switch (attack.attackBehavior)
        {
            case AttackBehavior.Burn:
                //If the unit is already burning, we want to add stacks to their burn status -> Deals more burn damage.
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
                //If they aren't burning, we want to add to their burn amount
                else
                {
                    defender.AddToBurnAmount(attack.GetStatusBuildUpAmount());
                }

                //If the burn amount is greater than or equal to the unit's threshhold -> The unit starts burning
                //The turn it reaches that threshold -> Unit should start burning. 
                if (defender.GetBurnAmount() >= defender.GetBurnThreshhold() && defender.DoesStatusExist(statusEffectScript.burn) == false)
                {
                    //Debug.Log($"{defender.unitName} is now burning!");

                    defender.AddStatus(statusEffectScript.burn.DeepCopy());
                    defender.SetBurnAmountToZero();
                    foreach (StatusEffect_V2 status in defender.unitStatusEffects)
                    {
                        if (status.GetStatusName() == "Burn")
                        {
                            status.SetStatusDamage(defender);
                            //Debug.Log(status.GetStatusDamage());
                        }
                    }
                }
                break;
            case AttackBehavior.FutureSight:
                if (defender.DoesStatusExist(statusEffectScript.futureSight) == false)
                {
                    defender.AddStatus(statusEffectScript.futureSight.DeepCopy());
                }
                break;


        }
    }

    private void CheckAttack_Buff_DebuffBuildupRelationship(Attack attack, Unit_V2 chosenTarget)
    {
        if (attack.attackBuff != null)
        {

            if (chosenTarget.DoesStatusExist(attack.attackBuff))
            {

            }
            else
            {
                attack.attackBuff.ApplyBuff(chosenTarget);
            }

        }

        if (attack.attackDebuff != null)
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
                switch (status.GetStatusName())
                {
                    case "Burn":

                        //Debug.Log($"{unit.unitName}'s burnStack: {status.effectStack}.");
                        if (unit.GetBurnTimer() >= status.GetEffectLength())
                        {
                            Debug.Log("Setting Burn Timer To Zero");
                            unit.SetBurnTimerToZero();
                            removeStatus.Add(status);
                        }
                        else
                        {

                            statusDamageQue.Add(new List<object> { unit, status.GetStatusDamage(), status.timeNeededInQue, status });
                            //Debug.Log("Burn Damage: " + status.GetStatusDamage());
                            unit.AddToBurnTimer(1);

                        }
                        break;
                    case "Future Sight":
                        if (status.timeNeededInQue >= status.GetEffectLength())
                        {
                            statusDamageQue.Add(new List<object> { unit, status.GetStatusDamage(), status.timeNeededInQue, status });
                            removeStatus.Add(status);
                        }
                        else
                        {
                            status.effectLength += 1;
                        }
                        break;
                }

            }
            foreach (StatusEffect_V2 status in removeStatus)
            {

                Debug.Log($"Trying to remove {status.GetStatusName()}");
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
                if (debuff.GetTimeActive() >= debuff.GetEffectLength())
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


            //Debug.Log($"{unit.unitName} has {unit.GetListOfDebuffs().Count} active Debuffs.");
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


    private void EndOfRoundStatusDamage()
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

        List<List<object>> blankList = new List<List<object>>();
        Unit_V2 unit = null;
        int damage = 0;
        int timeInQue;

        if (statusDamageQue.Count != 0)
        {
            for (int i = 0; i < statusDamageQue.Count; i++)
            {
                unit = statusDamageQue[i][0] as Unit_V2;
                if (unit.GetCurrentHp() > 0)
                {
                    if (statusDamageQue[i][1] is int)
                    {
                        damage = (int)(statusDamageQue[i][1]);
                    }
                    if (statusDamageQue[i][2] is int)
                    {
                        timeInQue = (int)statusDamageQue[i][2];
                        if (timeInQue < 1)
                        {
                            //buttonsAndPanelsScript.ToggleAttackDescriptionPanel();
                            uiScript.SetStatusDescriptionText(unit, damage, statusDamageQue[i][3].ToString());
                            unit.TakeDamage(damage); 
                            //buttonsAndPanelsScript.ToggleAttackDescriptionPanel();
                        }
                        else
                        {
                            statusDamageQue[i][2] = timeInQue - 1;
                            blankList.Add(statusDamageQue[i]);
                        }
                    }
                }

            }
        }
        statusDamageQue.Clear();
        statusDamageQue = blankList;
    }

    
    IEnumerator WaitForPlayerDecisions()
    {

        yield return new WaitUntil(PlayerChoiceIsMade);
        buttonsAndPanelsScript._fightButton.gameObject.SetActive(false);

        //Player is attacking single target
        if (AttackIsChosen() && chosenAttack.isSingleTarget == true)
        {
            
            if (EnemyIsChosen())
            {
                //buttonsAndPanelsScript.ToggleFightPanel();
                PayAttackCost(currentPC, chosenAttack);
                yield return new WaitForSeconds(1);

                for (int i = 0; i < chosenAttack.numOfHits; i++)
                {
                    if (chosenAttack.DoesAttackHit(currentPC, chosenEnemyTarget, envManaScript.GreatestColorInEnvironment(chosenAttack.attackColor)))
                    {
                        if(chosenEnemyTarget.GetCurrentHp() > 0)
                        {
                            Debug.Log("Attack Hits");
                            int damage = CalcAttackDamage(chosenAttack, currentPC, chosenEnemyTarget);

                            CheckAttack_StatusBuildupRelationship(chosenAttack, chosenEnemyTarget);

                            chosenEnemyTarget.TakeDamage(damage);

                            CheckAttack_Buff_DebuffBuildupRelationship(chosenAttack, chosenEnemyTarget);

                            yield return new WaitForSeconds(1);

                            //buttonsAndPanelsScript.ToggleAttackDescriptionPanel();

                            uiScript.SetAttackDescriptionText(chosenAttack, player, chosenEnemyTarget, damage.ToString());

                            yield return new WaitForSeconds(2);

                            //buttonsAndPanelsScript.ToggleAttackDescriptionPanel();
                        }

                    }
                    else
                    {
                        Debug.Log("Attack Doesn't Hit");
                    }

                    CombatUIUpdates();
                }

            }
        }
        else if (AttackIsChosen() && chosenAttack.isSingleTarget == false)
        {
            PayAttackCost(currentPC, chosenAttack);
            foreach (Unit_V2 enemy in listOfCombatants)
            {
                if (enemy is EnemyUnit_V2)
                {
                    yield return new WaitForSeconds(1);

                    for (int i = 0; i < chosenAttack.numOfHits; i++)
                    {
                        if (chosenAttack.DoesAttackHit(currentPC, enemy, envManaScript.GreatestColorInEnvironment(chosenAttack.attackColor)))
                        {
                            if(enemy.GetCurrentHp() > 0)
                            {
                                Debug.Log("Attack Hits");
                                int damage = CalcAttackDamage(chosenAttack, currentPC, enemy);
                                
                                CheckAttack_StatusBuildupRelationship(chosenAttack, enemy);
                                enemy.TakeDamage(damage);
                                
                                CheckAttack_Buff_DebuffBuildupRelationship(chosenAttack, enemy);
                                yield return new WaitForSeconds(1);
                                //buttonsAndPanelsScript.ToggleAttackDescriptionPanel();
                                uiScript.SetAttackDescriptionText(chosenAttack, player, enemy, damage.ToString());
                                yield return new WaitForSeconds(1);
                                
                            }

                        }
                        //buttonsAndPanelsScript.ToggleAttackDescriptionPanel();
                        CombatUIUpdates();

                    }
                }
            }
        }
        else if (ConsumableIsChosen() && PlayerIsChosen())
        {
            UseConsumable();
            inventoryScript.RemoveFromInventory(chosenConsumable);
            chosenPCTarget = null;
            chosenConsumable = null;
        }

        Debug.Log("PLAYER TURN HAS FINISHED!");
        playerTurnIsDone = true;
    }

    public bool IsPlayerAttackUseable(Attack attack)
    {

        if (attack.attackColor != Hue.Neutral && attack.weaponReq == WeaponType.Neutral)
        {
            if(attack.colorCost <= envManaScript.GetCurrentColorDictionary()[attack.attackColor])
            {
                return true;
            }
        }
        else if(attack.attackColor != Hue.Neutral && attack.weaponReq != WeaponType.Neutral && currentPC.equippedWeapon != null)
        {
            if(currentPC.equippedWeapon.weaponType == attack.weaponReq && attack.colorCost <= envManaScript.GetCurrentColorDictionary()[attack.attackColor])
            {
                return true;
            }
        }
        else if(attack.attackColor == Hue.Neutral && attack.weaponReq == WeaponType.Neutral)
        {
            if (envManaScript.IsThereEnoughColor(attack.colorCost))
            {
                return true;
            }
        }else if (attack.attackColor == Hue.Neutral && attack.weaponReq != WeaponType.Neutral && currentPC.equippedWeapon != null)
        {
            if(envManaScript.IsThereEnoughColor(attack.colorCost) && attack.weaponReq == currentPC.equippedWeapon.weaponType)
            {
                return true;
            }
        }
        return false;
    }

    private void PayAttackCost(Unit_V2 attacker, Attack attack)
    {

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
                envManaScript.LoseRandomColor(attack.colorCost);
                break;
        }



    }




    IEnumerator EnemyTurn(EnemyUnit_V2 unit)
    {
        Unit_V2 enemyChosenTarget = player;
        
        Attack enemyChosenAttack = unit.EnemyAttackDecision(envManaScript);
        if(enemyChosenAttack == null)
        {
            Debug.Log("ATTACK IS NULL");
        }
        if (enemyChosenTarget != null)
        {
            PayAttackCost(unit, enemyChosenAttack);
            for (int i = 0; i < enemyChosenAttack.numOfHits; i++)
            {
                if (enemyChosenAttack.DoesAttackHit(unit, enemyChosenTarget, envManaScript.GreatestColorInEnvironment(enemyChosenAttack.attackColor)))
                {
                    int damage = CalcAttackDamage(enemyChosenAttack, unit, enemyChosenTarget);                
                    CheckAttack_StatusBuildupRelationship(enemyChosenAttack, enemyChosenTarget);
                    enemyChosenTarget.TakeDamage(damage);
                    CheckAttack_Buff_DebuffBuildupRelationship(enemyChosenAttack, enemyChosenTarget);
                    yield return new WaitForSeconds(1);
                    //buttonsAndPanelsScript.ToggleAttackDescriptionPanel();
                    uiScript.SetAttackDescriptionText(enemyChosenAttack, unit, enemyChosenTarget, damage.ToString());
                    yield return new WaitForSeconds(1);
                    //buttonsAndPanelsScript.ToggleAttackDescriptionPanel();
                }
            }

        }
    }
    
    public void PlayerWon(List<Unit_V2> deadEnemies)
    {
        lootDrop1 = null;
        lootDrop2 = null;
        int experience = GainExperience(deadEnemies);
        int money = EnemyMoneyAmount(deadEnemies);
        Debug.Log("Player Won");
        EndCombat();
        
        LootDrops(deadEnemies);
        player.GainExp(GainExperience(deadEnemies));
        if(player.equippedWeapon != null && player.equippedWeapon.weaponType == WeaponType.Spellbook)
        {
            Weapon_Spellbook spellbook = player.equippedWeapon as Weapon_Spellbook;
            spellbook.GainExp(experience);
        }
        inventoryScript.GainMoney(money);
        uiScript.SetVictoryScreenText(experience, money, lootDrop1, lootDrop2);
        buttonsAndPanelsScript.ToggleEndOfBattlePanel();
        PlayerLocationAfterBattle();
    }

    public void PlayerLost(List<Unit_V2> deadEnemies)
    {
        int money = EnemyMoneyAmount(deadEnemies);
        EndCombat();
        Debug.Log("Player Lost");
        buttonsAndPanelsScript.ToggleEndOfBattlePanel();
        inventoryScript.LoseMoney(money);
        uiScript.SetDefeatScreenText(money);
        PlayerLocationAfterBattle();
    }

    private void LootDrops(List<Unit_V2> deadEnemies)
    {
        int count = 0;

        foreach (Unit_V2 unit in deadEnemies)
        {
            if (unit is EnemyUnit_V2)
            {
                EnemyUnit_V2 enemy = unit as EnemyUnit_V2;
                Item item = enemy.DroppedItem();
                if (item != null)
                {
                    count++;
                    Debug.Log($"Enemy Dropped: {item.itemName}");
                    inventoryScript.AddToInventory(item);
                    if (count == 1)
                    {
                        lootDrop1 = item.itemName;
                    }
                    if (count == 2)
                    {
                        lootDrop2 = item.itemName;
                    }
                    
                    
                }
                
            }

        }
    }

    private int GainExperience(List<Unit_V2> deadEnemies)
    {
        int expGained = 0;
        foreach (Unit_V2 unit in deadEnemies)
        {
            if (unit is EnemyUnit_V2)
            {
                EnemyUnit_V2 enemy = unit as EnemyUnit_V2;

                expGained += enemy.GetExpDropped();

            }
        } 
        return expGained;
    }

    private int EnemyMoneyAmount(List<Unit_V2> deadEnemies)
    {
        int moneyDropped = 0;
        foreach (Unit_V2 unit in deadEnemies)
        {
            if (unit is EnemyUnit_V2)
            {
                EnemyUnit_V2 enemy = unit as EnemyUnit_V2;

                moneyDropped += enemy.GetMoneyDropped();

            }
        }
        return moneyDropped;
    }

    private void GenerateEnvironment()
    {
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

    public void AttackChangeNotification(Attack attack)
    {

        if(attack.isSingleTarget == true)
        {
            buttonsAndPanelsScript.ToggleEnemiesPanel();
        }
        else
        {
            if(buttonsAndPanelsScript._SpellsPanel.activeSelf == true)
            {
                buttonsAndPanelsScript.ToggleSpellsPanel();
            }
        }
        chosenAttack = attack;
    }

    public void ConsumableChangeNotification(Item_Consumable consumable)
    {
        buttonsAndPanelsScript.TogglePCsPanel();
        chosenConsumable = consumable;

    }
    private bool AttackIsChosen()
    {
        if (chosenAttack != null)
        {
            return true;
        }
        return false;
    }

    private bool EnemyIsChosen()
    {
        if (chosenEnemyTarget != null || chosenAttack.isSingleTarget == false)
        {
            return true;
        }

        return false;
    }

    private bool ConsumableIsChosen()
    {
        if(buttonsAndPanelsScript.itemBeingPressed != null && buttonsAndPanelsScript.itemBeingPressed is Item_Consumable)
        {
            return true;
        }
        return false;
    }

    private bool PlayerIsChosen()
    {
        if(chosenPCTarget != null)
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
            buttonsAndPanelsScript.ToggleFightPanel();
            return true;

        }
        else if (ConsumableIsChosen() && PlayerIsChosen())
        {
            buttonsAndPanelsScript.ToggleFightPanel();
            return true;
        }
        else if (currentPC.isDefending)
        {
            return true;
        }
        else if (currentPC.usedItem)
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
                break;
        }

    }

    public void SetPlayerTarget(string playerName)
    {
        switch (playerName)
        {
            case "Player":
                chosenPCTarget = player;
                Debug.Log($"{player.unitName} is the chosen target!");
                break;
            case "PlayerTwo":
                
                break;
            default:
                break;
        }

    }
    public void ResetAttackAndEnemyTargets()
    {
        chosenAttack = null;
        chosenEnemyTarget = null;
    }































































    public void UseConsumable()
    {
        chosenConsumable.Use(chosenPCTarget);
    }



























































    //Reimagining my combat to fit the new AC/DC scheme
    //Does the attack hit? Check to see if the attack has any environment conditions for boosting accuracy.
    //Roll the "die" (random roll from 1-attack's accuracy + attacker's combatBAB). If it's >= defender DC, then it hits. 
    //If it hits -> Check to see if the attack has any environment conditions for boosting damage. 
    //Deal damage
    
    private void CheckAttackBehavior(Attack attack)
    {
        attack.envBehavior(envManaScript.GreatestColorInEnvironment(attack.attackColor));
    }

















    //Locations
    public void SetLocation(string location)
    {
        currentLocation = location;
    }

    public string GetLocation()
    {
        return currentLocation;
    }

    public void SetSubLocation(string location)
    {
        int roll = Random.Range(0, 2);
        switch(location)
        {
            case "subLocation_1":
                subLocation = SubLocation.subLocation_1;
                if(roll == 1)
                {
                    buttonsAndPanelsScript.ToggleLocationsPanel();
                    StartCombat();
                }
                else
                {
                    subLocation = SubLocation.subLocation_2;
                    buttonsAndPanelsScript.RefreshSubLocations();
                }
                break;
            case "subLocation_2":
                subLocation = SubLocation.subLocation_2;
                if (roll == 1)
                {
                    buttonsAndPanelsScript.ToggleLocationsPanel();
                    StartCombat();
                }
                else
                {
                    subLocation = SubLocation.subLocation_3;
                    buttonsAndPanelsScript.RefreshSubLocations();
                }
                break;
            case "subLocation_3":
                subLocation = SubLocation.subLocation_3;
                if (roll == 1)
                {
                    buttonsAndPanelsScript.ToggleLocationsPanel();
                    StartCombat();
                }
                else
                {
                    subLocation = SubLocation.subLocation_4;
                    buttonsAndPanelsScript.RefreshSubLocations();
                }
                break;
            case "subLocation_4":
                subLocation = SubLocation.subLocation_4;
                if (roll == 1)
                {
                    buttonsAndPanelsScript.ToggleLocationsPanel();
                    StartCombat();
                }
                else
                {
                    subLocation = SubLocation.subLocation_5;
                    buttonsAndPanelsScript.RefreshSubLocations();
                }
                break;
            case "subLocation_5":
                subLocation = SubLocation.subLocation_5;
                buttonsAndPanelsScript.ToggleLocationsPanel();
                StartCombat();
                break;
        }

        
    }

    public SubLocation GetSubLocation()
    {
        return subLocation;
    }

    private void PlayerLocationAfterBattle()
    {
        switch(theCombatState)
        {
            case CombatState.Won:
                if(subLocation == SubLocation.subLocation_1)
                {
                    subLocation = SubLocation.subLocation_2;
                }else if(subLocation == SubLocation.subLocation_2)
                {
                    subLocation = SubLocation.subLocation_3;
                }else if(subLocation == SubLocation.subLocation_3)
                {
                    subLocation = SubLocation.subLocation_4;
                }else if (subLocation == SubLocation.subLocation_4)
                {
                    subLocation = SubLocation.subLocation_5;
                }else if (subLocation == SubLocation.subLocation_5)
                {
                    subLocation = SubLocation.beginning;
                }
                break;
            case CombatState.Lost:
                
                if (subLocation == SubLocation.subLocation_2)
                {
                    subLocation = SubLocation.subLocation_1;
                }
                else if (subLocation == SubLocation.subLocation_3)
                {
                    subLocation = SubLocation.subLocation_2;
                }
                else if (subLocation == SubLocation.subLocation_4)
                {
                    subLocation = SubLocation.subLocation_3;
                }
                else if(subLocation == SubLocation.subLocation_5)
                {
                    subLocation = SubLocation.subLocation_4;
                }
                break;
        }
    }


































    public Unit_V2 GetCurrentPC()
    {
        if (currentPC.equippedWeapon != null && currentPC.equippedWeapon.weaponType == WeaponType.Spellbook)
        {
            Weapon_Spellbook spellbook = currentPC.equippedWeapon as Weapon_Spellbook;
            //Debug.Log(currentPC.equippedWeapon.itemName);
            buttonsAndPanelsScript.playerSpellbook = spellbook;
            buttonsAndPanelsScript.playerSpellbook.spellbookAttacks = spellbook.spellbookAttacks;
        }
        else
        {

        }
        return currentPC;
    }

    public Player_V2 GetPlayer()
    {
        return player;
    }

    public CombatState GetCombatState()
    {
        return theCombatState;
    }
}