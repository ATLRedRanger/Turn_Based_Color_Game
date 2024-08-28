
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
    private Unit_V2 chosenEnemyTarget = null;

    //Scripts
    private Unit_Spawner unitSpawnerScript;
    private Environment envManaScript;
    private StatusEffectsDatabase_V2 statusEffectScript;
    private Attack_Database attackDatabaseScript;
    private ButtonsAndPanels buttonsAndPanelsScript;
    private UI_V2 uiScript;

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
        buttonsAndPanelsScript.SetEnemyOneButtonName(enemyOne.unitName);
        currentPC = player;
        PlayerTurn();
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

        //yield return new WaitForSeconds(1);
        //unitSpawnerScript.SpawnPlayer();
        player = unitSpawnerScript.SpawnPlayer();
        //enemyOne = unitSpawnerScript.GenerateEnemy(0, currentLocation);
        //listOfCombatants.Add(enemyOne);
        Debug.Log("Finished Loading");
        
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
        int enemiesToGenerate = Random.Range(1, 3);
        Debug.Log($"Generated Enemies: {enemiesToGenerate}");
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

    public void Combat()
    {
        GenerateEnemies();
        List<Unit_V2> deadUnits = new List<Unit_V2>();
        theCombatState = CombatState.Active;
        listOfCombatants.Add(player);
        Debug.Log($"List Of Combatants: {listOfCombatants.Count}");
        int currentRound = 0;

        while (theCombatState == CombatState.Active)
        {
            currentRound++;
            Debug.Log($"Current Round: {currentRound}");

            SortCombatants(listOfCombatants);

            foreach (Unit_V2 unit in listOfCombatants)
            {
                Debug.Log($"Current Unit: {unit.unitName}");
                if (unit is Player_V2)
                {
                    whoseTurnIsIt = WhoseTurn.Player;
                    currentPC = unit;
                    buttonsAndPanelsScript.ToggleFightPanel();
                    PlayerTurn();
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
                else if (unit is EnemyUnit_V2)
                {
                    if (unit.GetCurrentHp() < 1)
                    {
                        Debug.Log($"Adding {unit} to DeadUnitList");
                        deadUnits.Add(unit);
                        
                    }
                    else
                    {
                        EnemyTurn(unit);
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
                    
                }
            }
            foreach (Unit_V2 unit in listOfCombatants)
            {
                if ( unit is EnemyUnit_V2 && !deadUnits.Contains(unit) && unit.GetCurrentHp() < 1)
                {
                   deadUnits.Add(unit);
                }
            }
            if (listOfCombatants.Count - deadUnits.Count == 1)
            {
                theCombatState = CombatState.Won;
                break;
            }
            //End of turn stuff
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
        Debug.Log("Player Turn");
        /*
        if (enemyOne != null)
        {
            enemyOne.TakeDamage(5);
            
        }
        if (enemyTwo != null)
        {
            enemyTwo.TakeDamage(5);
            
        }*/
        StartCoroutine(WaitForPlayerDecisions());
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
    private int CalcAttackDamage(Attack attack, Unit_V2 attacker, Unit_V2 defender)
    {
        // Calculate base damage with potential random variation
        float baseDamage = attack.attackPower + attacker.GetCurrentAttack();
        float damageMultiplier = CalculateDamageMultiplier(); // Helper function

        // Apply damage multiplier for critical hits, etc.
        int damageBeforeDefenses = Mathf.RoundToInt(baseDamage * damageMultiplier);


        // Apply color resistances based on attack type and color
        int damageAfterDefenses = ApplyColorAndWeaponResistances(attack.attackColor, damageBeforeDefenses, attacker, defender);

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
                            Debug.Log($"{defender.unitName}'s burn stack: {status.effectStack}.");
                        }
                    }
                }
                else if (defender.GetBurnAmount() >= defender.GetBurnThreshhold())
                {
                    Debug.Log($"{defender.unitName} is now burning!");
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

    private void CheckStatusTimes(List<Unit_V2> listOfCombatants)
    {
        List<StatusEffect_V2> removeStatus = new List<StatusEffect_V2>();

        foreach (Unit_V2 unit in listOfCombatants)
        {
            foreach (StatusEffect_V2 status in unit.unitStatusEffects)
            {
                Debug.Log("First Foreach");
                switch (status.GetStatusName())
                {
                    case "Burn":
                        Debug.Log($"{unit.unitName}'s burnTimer: {unit.GetBurnTimer()}");
                        if (unit.GetBurnTimer() >= status.GetEffectLength())
                        {
                            Debug.Log("Setting Burn Timer To Zero");
                            unit.SetBurnTimerToZero();
                            removeStatus.Add(status);
                        }
                        else
                        {
                            
                            statusDamageQue.Add(new List<object> {unit, status.GetStatusDamage(), status.timeNeededInQue});
                            Debug.Log("Burn Damage: " + status.GetStatusDamage());
                            unit.AddToBurnTimer(1);
                            
                        }
                        break;
                }

            }
            foreach (StatusEffect_V2 status in removeStatus)
            {
                Debug.Log("Second Foreach");
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
        Debug.Log("Start");
        buttonsAndPanelsScript.ToggleFightPanel();
        yield return new WaitUntil(PlayerTurnIsOver);
        if(chosenAttack != null && chosenEnemyTarget != null)
        {
            Debug.Log($"Chosen Attack: {chosenAttack.attackName}");
            Debug.Log($"Chosen Attack Target: {chosenEnemyTarget.unitName}");
        }

        Debug.Log("PLAYER TURN HAS FINISHED!");
        buttonsAndPanelsScript.ToggleFightPanel();
    }
    private void EnemyTurn(Unit_V2 unit)
    {
        Debug.Log($"{unit.unitName}'s Turn");
        if (player != null)
        {
            player.TakeDamage(5);
            
        }
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
        uiScript.SetMaxColorAmounts(envManaScript.currentRed, envManaScript.currentOrange, envManaScript.currentYellow,
                                        envManaScript.currentGreen, envManaScript.currentBlue, envManaScript.currentViolet);
    }

    private void UpdateEnvironmentColros()
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
    private bool PlayerTurnIsOver()
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
            case "EnemeyTwo":
                chosenEnemyTarget = enemyTwo;
                Debug.Log($"{enemyTwo.unitName} is the chosen target!");
                break;
            default:
                chosenEnemyTarget = enemyOne;
                break;
        }
        
    }

    public void ResetAttackAndEnemyTargets()
    {
        chosenAttack = null;
        chosenEnemyTarget = null;
    }
}
