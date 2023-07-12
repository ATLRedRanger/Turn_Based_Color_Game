using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI : MonoBehaviour
{

    public Text enemyOneName;

    public Text enemyOneHealth;

    public Text enemyOneStamina;

    public Text playerName;

    public Text playerHealth;

    public Text playerStamina;

    public Text environmentRed;

    public Unit_Spawner unitSpawnerScript;

    public Turn_Manager turnManagerScript;

    public ENV_Mana envManaScript;

    public List<GameObject> buttonList;

    public GameObject gameOrganizer;

    public CombatFunctions combatFunctions;

    public AttacksDatabase attacksDatabase;

    private Unit player;

    public Button _punchButton;

    public GameObject _fightPanel;

    public GameObject _fightButton;

    public GameObject _fireBallButton;

    

    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(StartStuff());
       
       //Debug.Log("APPLE "+unitSpawnerScript.player.playerAttackDictionary["Punch"].attackDamage);

       

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator StartStuff()
    {
        yield return new WaitForSeconds(1);
        unitSpawnerScript = gameOrganizer.GetComponent<Unit_Spawner>();
        turnManagerScript = gameOrganizer.GetComponent<Turn_Manager>();
        envManaScript = FindObjectOfType<ENV_Mana>();
        player = unitSpawnerScript.player;
        StartCoroutine(NamesText());
        StartCoroutine(HealthText());
        StartCoroutine(StaminaText());
        StartCoroutine(EnvironmentText());
    }
    IEnumerator NamesText()
    {
        yield return new WaitForSeconds(.02f);
        enemyOneName.text = unitSpawnerScript.listOfCombatants[1].unitName;
        playerName.text = unitSpawnerScript.player.unitName;
    }

    IEnumerator HealthText()
    {
        yield return new WaitForSeconds(.02f);
        playerHealth.text = "Health:" + unitSpawnerScript.player.currentHealth.ToString() + "/" + unitSpawnerScript.player.maxHealth.ToString();
        enemyOneHealth.text = "Health:" + unitSpawnerScript.enemyOne.currentHealth.ToString() + "/" + unitSpawnerScript.enemyOne.maxHealth.ToString();
    }

    IEnumerator StaminaText()
    {
        yield return new WaitForSeconds(.02f);
        playerStamina.text = "Stamina:" + unitSpawnerScript.player.currentStamina.ToString() + "/" + unitSpawnerScript.player.maxStamina.ToString();
        enemyOneStamina.text = "Stamina:" + unitSpawnerScript.enemyOne.currentStamina.ToString() + "/" + unitSpawnerScript.enemyOne.maxStamina.ToString();
    }

    IEnumerator EnvironmentText()
    {
        yield return new WaitForSeconds(.02f);
        environmentRed.text = "Red:" + envManaScript.currentRed.ToString() + "/" + envManaScript.maxRed.ToString();
    }
    public void UpdateUI()
    {
        //Enemy One UI stuff
        enemyOneName.text = unitSpawnerScript.enemyOne.unitName;
        enemyOneHealth.text = "Health:" + unitSpawnerScript.enemyOne.currentHealth.ToString() + "/" + unitSpawnerScript.enemyOne.maxHealth.ToString();
        enemyOneStamina.text = "Stamina:" + unitSpawnerScript.enemyOne.currentStamina.ToString() + "/" + unitSpawnerScript.enemyOne.maxStamina.ToString();

        //Players UI stuff
        playerName.text = unitSpawnerScript.player.unitName;
        playerHealth.text = "Health:" + unitSpawnerScript.player.currentHealth.ToString() + "/" + unitSpawnerScript.player.maxHealth.ToString();
        playerStamina.text = "Stamina:" + unitSpawnerScript.player.currentStamina.ToString() + "/" + unitSpawnerScript.player.maxStamina.ToString();

        //Update Environment Stuff
        environmentRed.text = "Red:" + envManaScript.currentRed.ToString() + "/" + envManaScript.maxRed.ToString();
    }
    public void MenuVisibile()
    {
        
        if (_fightButton != null && unitSpawnerScript.player.myTurn == true)
        {
            _fightButton.SetActive(true);
            AvailableAttacks();
        }
        else
        {
            _fightButton.SetActive(false);
        }
        
        
    }

    //Buttons

    public void OnPunchClick()
    {
        //Function for calculating total damage by the player using this attack.
        combatFunctions.DamageFromAttack(player.playerAttackDictionary["Punch"], unitSpawnerScript.player);
        
        //Function for reducing the stamina of the player but the stamina cost of the attack.
        combatFunctions.ReduceStamina(attacksDatabase._punch, unitSpawnerScript.player);

        //This is where we put the function for enemies taking damage.
        combatFunctions.ReduceHealth(combatFunctions.attackDamage, unitSpawnerScript.enemyOne);

        UpdateUI();
        player.hadATurn = true;

        _fightPanel.SetActive(false);

        turnManagerScript.EndTurn();
    }

    public void OnFireballClick()
    {

        //Function for calculating total damage by the player using this attack.
        combatFunctions.DamageFromAttack(attacksDatabase._fireBall, unitSpawnerScript.player);

        //Function for reducing the stamina of the player but the stamina cost of the attack.
        combatFunctions.ReduceStamina(attacksDatabase._fireBall, unitSpawnerScript.player);

        //This is where we put the function for enemies taking damage.
        combatFunctions.ReduceHealth(combatFunctions.attackDamage, unitSpawnerScript.enemyOne);

        //This is where we put the function to reduce the color in the environment.
        combatFunctions.ReduceColorFromEnv(attacksDatabase._fireBall);

        UpdateUI();
        player.hadATurn = true;

        _fightPanel.SetActive(false);

        turnManagerScript.EndTurn();
    }

    public void OpenFightPanel()
    {
        
        //Function to open and close the panel that houses all of the attack options
        if(_fightPanel != null)
        {
            //Sets a bool for the panel
            bool isActive = _fightPanel.activeSelf;
            //Whatever the bool is for the panel, this will make it the opposite
            _fightPanel.SetActive(!isActive);
        }
    }

    public void AvailableAttacks()
    {
        _fireBallButton.SetActive(false);

        if(envManaScript.currentRed >= attacksDatabase._fireBall.colorCost && _fireBallButton != null)
        {
            _fireBallButton.SetActive(true);
        }
    }
}
