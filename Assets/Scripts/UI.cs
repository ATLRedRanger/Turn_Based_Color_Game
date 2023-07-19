using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI : MonoBehaviour
{

    public Text enemyOneName;

    public Text enemyOneHealth;

    public Text enemyOneStamina;

    public Text enemyOneButtonText;

    public Text playerName;

    public Text playerHealth;

    public Text playerStamina;

    public Text environmentRed;

    public Text backButtonText;

    public Unit_Spawner unitSpawnerScript;

    public Turn_Manager turnManagerScript;

    public ENV_Mana envManaScript;

    public List<GameObject> buttonList;

    public GameObject gameOrganizer;

    public CombatFunctions combatFunctions;

    public AttacksDatabase attacksDatabase;

    private Unit player;

    public Button _punchButton;

    public GameObject _enemyOneButton;

    public GameObject _backButton;

    public GameObject _enemyPanel;

    public GameObject _attacksPanel;

    public GameObject _fightButton;

    public GameObject _fireBallButton;

    

    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(StartStuff());
        _backButton.SetActive(false);
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
        enemyOneButtonText.text = unitSpawnerScript.enemyOne.unitName;
        backButtonText.text = "B";
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

    public void BackButton()
    {
        //This button is to allow the player to go back to a previous menu
        if (_enemyPanel.activeSelf == true)
        {
            _enemyPanel.SetActive(false);
            _fightButton.SetActive(true);
        }
        if (_attacksPanel.activeSelf == true)
        {
            _attacksPanel.SetActive(false);
            _enemyPanel.SetActive(true);
        }
        if (_attacksPanel.activeSelf == false && _enemyPanel.activeSelf == false)
        {
            _backButton.SetActive(false);
        }
        
    }

    public void EnemyOneButton()
    {
        unitSpawnerScript.chosenEnemy = unitSpawnerScript.enemyOne;
        _enemyPanel.SetActive(false);
        OpenFightPanel();
        
    }

    public void OnPunchClick()
    {
        //Function for calculating total damage by the player using this attack.
        combatFunctions.DamageFromAttack(player.unitAttackDictionary["Punch"], unitSpawnerScript.player);
        
        //Function for reducing the stamina of the player but the stamina cost of the attack.
        combatFunctions.ReduceStamina(attacksDatabase._punch, unitSpawnerScript.player);

        //This is where we put the function for enemies taking damage.
        combatFunctions.ReduceHealth(combatFunctions.attackDamage, unitSpawnerScript.chosenEnemy);

        UpdateUI();
        player.hadATurn = true;

        SetFalse();

        turnManagerScript.EndTurn();
    }

    public void OnFireballClick()
    {
        

        if(combatFunctions.HitorMiss(attacksDatabase._fireBall, unitSpawnerScript.player) == true)
        {
            //Function for calculating total damage by the player using this attack.
            combatFunctions.DamageFromAttack(attacksDatabase._fireBall, unitSpawnerScript.player);

            //Function for reducing the stamina of the player but the stamina cost of the attack.
            combatFunctions.ReduceStamina(attacksDatabase._fireBall, unitSpawnerScript.player);

            //This is where we put the function for enemies taking damage.
            combatFunctions.ReduceHealth(combatFunctions.attackDamage, unitSpawnerScript.enemyOne);

            //This is where we put the function to reduce the color in the environment.
            combatFunctions.ReduceColorFromEnv(attacksDatabase._fireBall);

            player.hadATurn = true;

            SetFalse();

            turnManagerScript.EndTurn();
        }
        else
        {
            //UpdateUI();
            player.hadATurn = true;

            SetFalse();

            turnManagerScript.EndTurn();
        }
        
    }

    public void OpenEnemyPanel()
    {
        
        //Function to open and close the panel that houses all of the attack options
        if (_enemyPanel != null)
        {
            //Sets a bool for the panel
            bool isActive = _enemyPanel.activeSelf;
            //Whatever the bool is for the panel, this will make it the opposite
            _enemyPanel.SetActive(!isActive);
        }
        if(_enemyPanel.activeSelf == true)
        {
            _fightButton.SetActive(false);
            _backButton.SetActive(true);
        }
    }

    public void OpenFightPanel()
    {
        
        //Function to open and close the panel that houses all of the attack options
        if (_attacksPanel != null)
        {
            //Sets a bool for the panel
            bool isActive = _attacksPanel.activeSelf;
            //Whatever the bool is for the panel, this will make it the opposite
            _attacksPanel.SetActive(!isActive);
        }
        if (_enemyPanel.activeSelf == true)
        {
            _backButton.SetActive(true);
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
    private void SetFalse()
    {
        _backButton.SetActive(false);
        _attacksPanel.SetActive(false);
    }
}
//KEYBOARD SHORTCUTS
//HIGHLIGHT INSTANCES - SHIFT+ALT+. (Highlights instances of a word)