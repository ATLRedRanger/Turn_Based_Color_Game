using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI : MonoBehaviour
{
    //UI Text
    public Text enemyOneName;

    public Text enemyOneHealth;

    public Text enemyOneStamina;

    public Text enemyOneButtonText;

    public Text playerName;

    public Text playerHealth;

    public Text playerStamina;

    public Text environmentRed;

    public Text backButtonText;


    //UI Menu Buttons
    public GameObject _itemButton;

    public GameObject _spellsButton;

    public GameObject _attackButton;

    public GameObject _defendButton;

    public GameObject _abilitiesButton;

    public GameObject _fightButton;

    public GameObject _backButton;

    //UI Abilities Buttons
    public GameObject _punchButton;


    //UI Item Buttons
    public GameObject _healthPotion;


    //UI Spells Buttons
    public GameObject _fireBallButton;


    //UI Enemies Buttons
    public GameObject _enemyOneButton;

    public GameObject _enemyTwoButton;

    

    

    

    //UI Panels
    public GameObject _enemiesPanel;

    public GameObject _abilitiesPanel;

    public GameObject _itemPanel;

    public GameObject _spellsPanel;

    public GameObject _fightPanel;


    //Scripts
    public ENV_Mana envManaScript;

    public GameObject gameOrganizer;

    public Unit_Spawner unitSpawnerScript;

    public Turn_Manager turnManagerScript;

    public CombatFunctions combatFunctions;

    public AttacksDatabase attacksDatabase;

    public Inventory inventoryScript;

    //Player
    private Unit player;

    

    

    

    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(StartStuff());
        _backButton.SetActive(false);
       
        
       

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
        inventoryScript = FindObjectOfType<Inventory>();
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
        if (_enemiesPanel.activeSelf == true)
        {
            _enemiesPanel.SetActive(false);
            _fightButton.SetActive(true);
        }
        if (_abilitiesPanel.activeSelf == true)
        {
            _abilitiesPanel.SetActive(false);
            _enemiesPanel.SetActive(true);
        }
        if (_abilitiesPanel.activeSelf == false && _enemiesPanel.activeSelf == false)
        {
            _backButton.SetActive(false);
        }
        
    }

    public void EnemyOneButton()
    {
        unitSpawnerScript.chosenEnemy = unitSpawnerScript.enemyOne;
        _enemiesPanel.SetActive(false);
        OpenAbilitiesPanel();
        
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

            //combatFunctions.FireBall();
            combatFunctions.UseAttack(attacksDatabase._fireBall, player, unitSpawnerScript.chosenEnemy);

            player.hadATurn = true;

            SetFalse();

            turnManagerScript.EndTurn();
        }
        else
        {
            
            player.hadATurn = true;

            SetFalse();

            turnManagerScript.EndTurn();
        }
        
    }

    public void OpenFightPanel()
    {
        bool isActive = _fightPanel.activeSelf;

        if(_fightPanel != null)
        {
            
            _fightPanel.SetActive(!isActive);
        }

        
    }

    public void OpenItemsPanel()
    {
        bool isActive = _itemPanel.activeSelf;

        if(_itemPanel != null)
        {
            
            _itemPanel.SetActive(!isActive);
        }

        if(_itemPanel != null)
        {
            if(_itemPanel.activeSelf == true)
            {
                _spellsPanel.SetActive(false);
                _abilitiesPanel.SetActive(false);
                
            }
        }
    }

    public void OpenSpellsPanel()
    {
        bool isActive = _spellsPanel.activeSelf;

        if (_spellsPanel != null)
        {

            _spellsPanel.SetActive(!isActive);
        }

        if (_spellsPanel != null)
        {
            if (_spellsPanel.activeSelf == true)
            {
                _itemPanel.SetActive(false);
                _abilitiesPanel.SetActive(false);

            }
        }
    }

    public void OpenAbilitiesPanel()
    {
        bool isActive = _abilitiesPanel.activeSelf;

        if (_abilitiesPanel != null)
        {

            _abilitiesPanel.SetActive(!isActive);
        }

        if (_abilitiesPanel != null)
        {
            if (_abilitiesPanel.activeSelf == true)
            {
                _itemPanel.SetActive(false);
                _spellsPanel.SetActive(false);

            }
        }
    }

    public void OpenEnemiesPanel()
    {
        bool isActive = _enemiesPanel.activeSelf;

        if (_enemiesPanel != null)
        {

            _enemiesPanel.SetActive(!isActive);
        }

    }

    /*public void OpenAbilitiesPanel()
    {
        
        //Function to open and close the panel that houses all of the attack options
        if (_abilitiesPanel != null)
        {
            //Sets a bool for the panel
            bool isActive = _abilitiesPanel.activeSelf;
            //Whatever the bool is for the panel, this will make it the opposite
            _abilitiesPanel.SetActive(!isActive);
        }
        if (_enemiesPanel.activeSelf == true)
        {
            _backButton.SetActive(true);
        }
    }*/

    public void UseHealthPotion()
    {
           foreach(Item item in inventoryScript.playerItemList)
        {
            if(item.itemName == "Health Potion" && item.itemAmount > 0)
            {
                Consumable healthPotion = item.GetComponent<Consumable>();
                player.currentHealth += healthPotion.refillAmount;
                item.itemAmount -= 1;
                if(item.itemAmount < 1)
                {
                    inventoryScript.playerItemList.Remove(item);
                }
            }
            
        }
    }

    public void AvailableAttacks()
    {
        _fireBallButton.SetActive(false);

        if(envManaScript.currentRed >= attacksDatabase._fireBall.colorCost && _fireBallButton != null && unitSpawnerScript.player.currentStamina >= attacksDatabase._fireBall.staminaCost)
        {
            _fireBallButton.SetActive(true);
        }
    }
    private void SetFalse()
    {
        _backButton.SetActive(false);
        _abilitiesPanel.SetActive(false);
    }
}
//KEYBOARD SHORTCUTS
//HIGHLIGHT INSTANCES - SHIFT+ALT+. (Highlights instances of a word)

//TODO: Finish UI panel setup. Do I want to select the enemy before or after selecting an attack?
//TODO: Upload new version to git
//TODO: Fix UseHealthPotion()