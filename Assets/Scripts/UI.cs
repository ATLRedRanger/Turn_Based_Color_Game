using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UI : MonoBehaviour
{
    //UI Text
    public Text enemyOneName;

    public Text enemyOneHealth;

    public Text enemyOneStamina;

    public Text enemyOneButton;

    public Text playerName;

    public Text playerHealth;

    public Text playerStamina;

    public Text backButtonText;

    //Environment Text
    public Text environmentRed;

    public Text environmentOrange;

    public Text environmentYellow;

    public Text environmentGreen;

    public Text environmentBlue;

    public Text environmentViolet;


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
    public Button _chopButton;
    public Button _violetBallButton;
    public Button _slashButton;
    public Button _slamButton;
    public Button _quickShotButton;
    public Button _yellowSplash;
    public Button _orangeSpike;
    public Button _blueCrush;
    public Button _greenPunch;



    //UI Item Buttons
    public GameObject _healthPotion;


    //UI Spells Buttons
    public Button _fireBallButton;


    //UI Enemies Buttons
    public GameObject _enemyOneButton;

    public TMP_Text enemyOneText;

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

    public StatusEffects statusEffectsScript;

    

    

    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(StartStuff());
        _backButton.SetActive(false);
        statusEffectsScript = FindObjectOfType<StatusEffects>();
       
        
       

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
        enemyOneName.text = unitSpawnerScript.listOfCombatants[0].unitName;
        playerName.text = unitSpawnerScript.player.unitName;
        enemyOneText.text = unitSpawnerScript.enemyOne.unitName;
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
        environmentOrange.text = "Orange:" + envManaScript.currentOrange.ToString() + "/" + envManaScript.maxOrange.ToString();
        environmentYellow.text = "Yellow:" + envManaScript.currentYellow.ToString() + "/" + envManaScript.maxYellow.ToString();
        environmentGreen.text = "Green:" + envManaScript.currentGreen.ToString() + "/" + envManaScript.maxGreen.ToString();
        environmentBlue.text = "Blue:" + envManaScript.currentBlue.ToString() + "/" + envManaScript.maxBlue.ToString();
        environmentViolet.text = "Violet:" + envManaScript.currentViolet.ToString() + "/" + envManaScript.maxViolet.ToString();
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
        environmentOrange.text = "Orange:" + envManaScript.currentOrange.ToString() + "/" + envManaScript.maxOrange.ToString();
        environmentYellow.text = "Yellow:" + envManaScript.currentYellow.ToString() + "/" + envManaScript.maxYellow.ToString();
        environmentGreen.text = "Green:" + envManaScript.currentGreen.ToString() + "/" + envManaScript.maxGreen.ToString();
        environmentBlue.text = "Blue:" + envManaScript.currentBlue.ToString() + "/" + envManaScript.maxBlue.ToString();
        environmentViolet.text = "Violet:" + envManaScript.currentViolet.ToString() + "/" + envManaScript.maxViolet.ToString();
    }
    public void MenuVisibile()
    {
        
        if (_fightButton != null && unitSpawnerScript.player.myTurn == true)
        {
            _fightButton.SetActive(true);
            
            
        }
        else
        {
            _fightButton.SetActive(false);
        }
        
        
    }

    //Buttons

    /*public void BackButton()
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
        
    }*/

    public void EnemyOneButton()
    {
        combatFunctions.chosenEnemy = unitSpawnerScript.enemyOne;

        Unit enemy_One = unitSpawnerScript.enemyOne;

        _enemiesPanel.SetActive(false);

        if (combatFunctions.HitorMiss(combatFunctions.chosenAttack, unitSpawnerScript.player) == true)
        {

            combatFunctions.UseAttack(combatFunctions.chosenAttack, player, enemy_One);

            player.hadATurn = true;

            ClosePanels();

            turnManagerScript.EndTurn();
        }
        else
        {

            player.hadATurn = true;

            ClosePanels();

            turnManagerScript.EndTurn();
        }
        
    }
    
    //Color.Neutral Attack Buttons
    public void OnPunchClick()
    {
        combatFunctions.chosenAttack = player.unitAttackDictionary["Punch"];
        OpenEnemiesPanel();
    }

    //Color.Red Attack Buttons
    public void OnFireballClick()
    {
        combatFunctions.chosenAttack = player.unitAttackDictionary["Fireball"];
        
        player.isBurning = true;
        statusEffectsScript.BurningCondition();
        OpenEnemiesPanel();
    }

    //Color.Orange Attack Buttons
    public void OnOrangeSpikeClick()
    {
        combatFunctions.chosenAttack = player.unitAttackDictionary["Orange Spike"];
        OpenEnemiesPanel();
    }

    //Color.Yellow Attack Buttons
    public void OnYellowSplashClick()
    {
        combatFunctions.chosenAttack = player.unitAttackDictionary["Yellow Splash"];
        OpenEnemiesPanel();
    }
    
    //Color.Green Attack Buttons
    public void OnGreenPunchClick()
    {
        combatFunctions.chosenAttack = player.unitAttackDictionary["Green Punch"];
        OpenEnemiesPanel();
    }

    //Color.Blue Attack Buttons
    public void OnBlueCrushClick()
    {
        combatFunctions.chosenAttack = player.unitAttackDictionary["Blue Crush"];
        OpenEnemiesPanel();
    }
    
    //Color.Violet Attack Buttons


    //Use Items 
    public void UseHealthPotion()
    {
        combatFunctions.UseHealthPotion();
        UpdateUI();
        //StartCoroutine(WaitForTime());
        
        turnManagerScript.EndTurn();
    }

    public bool CheckForAttackAvailability(Attack attack)
    {
        bool isUseable = false;
        //Checks to see if the attack has a color component
        if (attack.attackColor != Color.Neutral)
        {
            //if it does, checks to see which color so can take from the environment
            switch(attack.attackColor)
            {
                case Color.Red:
                    if(attack.colorCost <= envManaScript.currentRed && player.currentStamina >= attack.staminaCost)
                    {
                        isUseable = true;
                    }
                    break;
                case Color.Orange:
                    if (attack.colorCost <= envManaScript.currentOrange && player.currentStamina >= attack.staminaCost)
                    {
                        isUseable = true;
                    }
                    break;
                case Color.Yellow:
                    if (attack.colorCost <= envManaScript.currentYellow && player.currentStamina >= attack.staminaCost)
                    {
                        isUseable = true;
                    }
                    break;
                case Color.Green:
                    if (attack.colorCost <= envManaScript.currentGreen && player.currentStamina >= attack.staminaCost)
                    {
                        isUseable = true;
                    }
                    break;
                case Color.Blue:
                    if (attack.colorCost <= envManaScript.currentBlue && player.currentStamina >= attack.staminaCost)
                    {
                        isUseable = true;
                    }
                    break;
                case Color.Violet:
                    if (attack.colorCost <= envManaScript.currentViolet && player.currentStamina >= attack.staminaCost)
                    {
                        isUseable = true;
                    }
                    break;
            }
        }
        else
        {
            if(player.currentStamina >= attack.staminaCost)
            {
                isUseable = true;
            }
        }
        
        return isUseable;
        
    }

    public void AvailableSpells()
    {

        SetSpellsFalse();
        foreach(var kvp in player.unitAttackDictionary)
        {
            switch(kvp.Value.attackColor)
            {
                case Color.Red:
                    if(player.currentStamina >= kvp.Value.staminaCost && kvp.Value.colorCost <= envManaScript.currentRed)
                    {
                        _fireBallButton.interactable = true;
                    }
                    break;
                case Color.Orange:
                    if (player.currentStamina >= kvp.Value.staminaCost && kvp.Value.colorCost <= envManaScript.currentOrange)
                    {
                        _orangeSpike.interactable = true;
                    }
                    break;
                case Color.Yellow:
                    if (player.currentStamina >= kvp.Value.staminaCost && kvp.Value.colorCost <= envManaScript.currentYellow)
                    {
                        _yellowSplash.interactable = true;
                    }
                    break;
                case Color.Green:
                    if (player.currentStamina >= kvp.Value.staminaCost && kvp.Value.colorCost <= envManaScript.currentGreen)
                    {
                        _greenPunch.interactable = true;
                    }
                    break;
                case Color.Blue:
                    if (player.currentStamina >= kvp.Value.staminaCost && kvp.Value.colorCost <= envManaScript.currentBlue)
                    {
                        _blueCrush.interactable = true;
                    }
                    break;
                case Color.Violet:
                    break;
                default:
                    break;
            }
        }

        
    }

    public void AvailableAbilities()
    {
        SetAbilitiesFalse();
        
        Debug.Log(player.equippedWeapon.itemName);
        switch(player.equippedWeapon.weaponType)
        {
            case WeaponType.Axe:
                switch (player.axeMastery)
                {
                    case 1:
                        if (CheckForAttackAvailability(player.unitAttackDictionary["Chop"]))
                        {
                            _chopButton.interactable = true;
                        }
                        
                        break;
                    case 3:
                        break;
                    
                }
                break;
            case WeaponType.Staff:
                switch (player.staffMastery)
                {
                    case 1:
                        _violetBallButton.interactable = true; 
                        break;
                }
                break;
            case WeaponType.Sword:
                switch (player.swordMastery)
                {
                    case 1:
                        if (CheckForAttackAvailability(player.unitAttackDictionary["Slash"]))
                        {
                            _slashButton.interactable = true;
                        }
                        //_slashButton.interactable = true;
                        break;
                }
                break;
            case WeaponType.Hammer:
                switch (player.hammerMastery)
                {
                    case 1:
                        if (CheckForAttackAvailability(player.unitAttackDictionary["Slam"]))
                        {
                            _slamButton.interactable = true;
                        }
                        
                        break;
                }
                break;
            case WeaponType.Bow:
                switch (player.bowMastery)
                {
                    case 1:
                        if (CheckForAttackAvailability(player.unitAttackDictionary["Quick Shot"]))
                        {
                            _quickShotButton.interactable = true;
                        }
                        
                        break;
                }
                break;
            default:
                break;
        }
    }

    //Open Panels
    public void OpenFightPanel()
    {
        bool isActive = _fightPanel.activeSelf;

        if (_fightPanel != null)
        {

            _fightPanel.SetActive(!isActive);
        }

        if (_fightPanel.activeSelf == false)
        {
            ClosePanels();
        }
    }

    public void OpenItemsPanel()
    {
        bool isActive = _itemPanel.activeSelf;

        if (_itemPanel != null)
        {

            _itemPanel.SetActive(!isActive);
        }

        if (_itemPanel != null)
        {
            if (_itemPanel.activeSelf == true)
            {
                _abilitiesPanel.SetActive(false);
                _spellsPanel.SetActive(false);
                _enemiesPanel.SetActive(false);

            }
        }
        
    }

    public void OpenSpellsPanel()
    {
        bool isActive = _spellsPanel.activeSelf;
        AvailableSpells();
        if (_spellsPanel != null)
        {

            _spellsPanel.SetActive(!isActive);
        }

        if (_spellsPanel != null)
        {
            if (_spellsPanel.activeSelf == true)
            {
                _abilitiesPanel.SetActive(false);
                _itemPanel.SetActive(false);
                _enemiesPanel.SetActive(false);

            }
        }
        if(_spellsPanel.activeSelf == false)
        {
            _abilitiesPanel.SetActive(false);
            _itemPanel.SetActive(false);
            _enemiesPanel.SetActive(false);

        }
    }

    public void OpenAbilitiesPanel()
    {
        bool isActive = _abilitiesPanel.activeSelf;
        AvailableAbilities();
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
                _enemiesPanel.SetActive(false);
            }
        }
        if(_abilitiesPanel.activeSelf == false)
        {
            
            _spellsPanel.SetActive(false);
            _itemPanel.SetActive(false);
            _enemiesPanel.SetActive(false);
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

    private void ClosePanels()
    {
        _fightPanel.SetActive(false);
        _abilitiesPanel.SetActive(false);
        _spellsPanel.SetActive(false);
        _itemPanel.SetActive(false);
        _enemiesPanel.SetActive(false);
    }

    private void SetSpellsFalse()
    {
        _fireBallButton.interactable = false;
        _greenPunch.interactable = false;
        _yellowSplash.interactable = false;
        _blueCrush.interactable = false;
        _orangeSpike.interactable = false;
    }

    private void SetAbilitiesFalse()
    {
        _chopButton.interactable = false;
        _slamButton.interactable = false;
        _slashButton.interactable = false;
        _quickShotButton.interactable = false;
        _violetBallButton.interactable = false;
    }

    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("Waiting");
    }



}
//KEYBOARD SHORTCUTS
//HIGHLIGHT INSTANCES - SHIFT+ALT+. (Highlights instances of a word)

//TODO: Finish UI panel setup. Do I want to select the enemy before or after selecting an attack?
//TODO: Upload new version to git
//TODO: Fix UseHealthPotion()