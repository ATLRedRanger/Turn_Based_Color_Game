using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;

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

    public TMP_Text experienceGained;

    public TMP_Text weaponExperienceGained;

    public TMP_Text moneyGained;

    public TMP_Text victoryText;

    public TMP_Text defeatText;


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

    public GameObject _newBattleButton;


    //UI Abilities Buttons
    public GameObject _punchButton;
    public Button _chopButton;
    public Button _violetBallButton;
    public Button _slashButton;
    public Button _slamButton;
    public Button _quickShotButton;




    //UI Item Buttons
    public GameObject _healthPotion;

    
    //UI Spells Buttons
    public Button _fireBallButton;
    public Button _yellowSplash;
    public Button _orangeSpike;
    public Button _blueCrush;
    public Button _greenPunch;

    //UI Enemies Buttons
    public GameObject _enemyOneButton;

    public TMP_Text enemyOneText;

    public TMP_Text enemyOneButtonNameText;

    public GameObject _enemyOneButtonTwo;

    public TMP_Text enemyOneTextTwo;


    //UI Panels
    public GameObject _enemiesPanel;

    public GameObject _enemiesTwoPanel;

    public GameObject _abilitiesPanel;

    public GameObject _itemPanel;

    public GameObject _spellsPanel;

    public GameObject _fightPanel;

    public GameObject _endBattlePanel;

    //UI Bars
    public Image player_HealthBar;
    public Image player_StaminaBar;
    public Image player_StunnedBar;
    public Image enemy_One_HealthBar;
    public Image enemy_One_StaminaBar;
    public Image enemy_One_StunnedBar;
    public Image redBar;
    public Image orangeBar;
    public Image yellowBar;
    public Image greenBar;
    public Image blueBar;
    public Image violetBar;

    //Status Effect Icons
    public Image player_Burn_Sprite;
    public Image enemyOne_Burn_Sprite;
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

    public Unit enemyOne;

    public StatusEffects statusEffectsScript;

    private Animations animationScript;

    //Abilities List
    public List<Button> abilitiesList = new List<Button>();

    //Attack
    public Attack chosenAttack;

    public float fillSmoothness = 0.1f;

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
        yield return new WaitForSeconds(.5f);
        unitSpawnerScript = gameOrganizer.GetComponent<Unit_Spawner>();
        turnManagerScript = gameOrganizer.GetComponent<Turn_Manager>();
        envManaScript = FindObjectOfType<ENV_Mana>();
        player = unitSpawnerScript.player;
        enemyOne = unitSpawnerScript.enemyOne;
        inventoryScript = FindObjectOfType<Inventory>();
        animationScript = FindObjectOfType<Animations>();
        
        UpdateUI();
       
    }
    private void NamesText()
    {
        
        playerName.text = unitSpawnerScript.player.unitName;
        enemyOneName.text = unitSpawnerScript.enemyOne.unitName;
        enemyOneTextTwo.text = enemyOneText.text;
        
        
    }

    private void HealthText()
    {
        playerHealth.text = "Health:" + unitSpawnerScript.player.currentHealth.ToString() + "/" + unitSpawnerScript.player.maxHealth.ToString();
        enemyOneHealth.text = "Health:" + unitSpawnerScript.enemyOne.currentHealth.ToString() + "/" + unitSpawnerScript.enemyOne.maxHealth.ToString();

    }

    private void StaminaText()
    {
        
        playerStamina.text = "Stamina:" + unitSpawnerScript.player.currentStamina.ToString() + "/" + unitSpawnerScript.player.maxStamina.ToString();
        enemyOneStamina.text = "Stamina:" + unitSpawnerScript.enemyOne.currentStamina.ToString() + "/" + unitSpawnerScript.enemyOne.maxStamina.ToString();
        
        
    }

    private void EnvironmentText()
    {
        
        environmentRed.text = "Red:" + envManaScript.currentRed.ToString() + "/" + envManaScript.maxRed.ToString();
        environmentOrange.text = "Orange:" + envManaScript.currentOrange.ToString() + "/" + envManaScript.maxOrange.ToString();
        environmentYellow.text = "Yellow:" + envManaScript.currentYellow.ToString() + "/" + envManaScript.maxYellow.ToString();
        environmentGreen.text = "Green:" + envManaScript.currentGreen.ToString() + "/" + envManaScript.maxGreen.ToString();
        environmentBlue.text = "Blue:" + envManaScript.currentBlue.ToString() + "/" + envManaScript.maxBlue.ToString();
        environmentViolet.text = "Violet:" + envManaScript.currentViolet.ToString() + "/" + envManaScript.maxViolet.ToString();
    }
    public void UpdateUI()
    {
        NamesText();
        HealthText();
        StaminaText();
        EnvironmentText();
        GraphicalBars();
        StatusEffectSprites();
        
    }

  

    private void GraphicalBars()
    {
        //Player
        player_HealthBar.fillAmount = (float)(float)unitSpawnerScript.player.currentHealth / (float)unitSpawnerScript.player.maxHealth;
        player_StaminaBar.fillAmount = (float)(float)unitSpawnerScript.player.currentStamina / (float)unitSpawnerScript.player.maxStamina;

        //EnemyOne
        enemy_One_HealthBar.fillAmount = (float)(float)unitSpawnerScript.enemyOne.currentHealth / (float)unitSpawnerScript.enemyOne.maxHealth;
        enemy_One_StaminaBar.fillAmount = (float)(float)unitSpawnerScript.enemyOne.currentStamina / (float)unitSpawnerScript.enemyOne.maxStamina;

        //Environment
        redBar.fillAmount = (float)((float)envManaScript.currentRed / (float)envManaScript.maxRed);
        orangeBar.fillAmount = (float)((float)envManaScript.currentOrange / (float)envManaScript.maxOrange);
        yellowBar.fillAmount = (float)((float)envManaScript.currentYellow / (float)envManaScript.maxYellow);
        greenBar.fillAmount = (float)((float)envManaScript.currentGreen / (float)envManaScript.maxGreen);
        blueBar.fillAmount = (float)((float)envManaScript.currentBlue / (float)envManaScript.maxBlue);
        violetBar.fillAmount = (float)((float)envManaScript.currentViolet / (float)envManaScript.maxViolet);
        
    }

    private void StatusEffectSprites()
    {
        
        //Burning
        if (unitSpawnerScript.player.isBurning == true)
        {
            player_Burn_Sprite.gameObject.SetActive(true);
        }
        else
        {
            player_Burn_Sprite.gameObject.SetActive(false);
        }
        if (unitSpawnerScript.enemyOne.isBurning == true)
        {
            enemyOne_Burn_Sprite.gameObject.SetActive(true);
        }
        else
        {
            enemyOne_Burn_Sprite.gameObject.SetActive(false);
        }
        //Stunned
        if(unitSpawnerScript.player.isStunned  == true)
        {
            player_StunnedBar.gameObject.SetActive(true);
        }
        else
        {
            player_StunnedBar.gameObject.SetActive (false);
        }
        if (unitSpawnerScript.enemyOne.isStunned)
        {
            enemy_One_StunnedBar.gameObject.SetActive(true);
        }
        else
        {
            enemy_One_StunnedBar.gameObject.SetActive(false);
        }
    }

    public void EndBattleUI()
    {
        //This function controls all of the text that happens at the end of a battle
        //Experience, weapon experience, money 
        //TODO: Ask the player if they'd like to continue or go back to town
        enemyOneHealth.enabled = false;
        enemyOneName.enabled = false;
        enemyOneStamina.enabled = false;

        _endBattlePanel.SetActive(true);
        if (turnManagerScript.state == BattleState.WON)
        {
            
           
            _newBattleButton.SetActive(true);
            
           
            victoryText.text = "BATTLE ENDED!";
            defeatText.gameObject.SetActive(false);
            experienceGained.text = "Player gains " + unitSpawnerScript.enemyOne.expGiven + " experience.";
            weaponExperienceGained.text = "Player gains " + 15 + " " + player.equippedWeapon.weaponType + " experience.";
            moneyGained.text = "Player gains " + unitSpawnerScript.enemyOne.moneyGiven + " money.";
        }

        if (turnManagerScript.state == BattleState.LOST)
        {
            //enemyOne.gameObject.SetActive(false);
            _newBattleButton.SetActive(false);

            defeatText.text = "GAME OVER!";
            defeatText.gameObject.SetActive(true);
            victoryText.gameObject.SetActive(false);
            experienceGained.gameObject.SetActive(false);
            weaponExperienceGained.gameObject.SetActive(false);
            moneyGained.gameObject.SetActive(false);
        }

        
    }

    public void MenuVisibile()
    {
        
        if (_fightButton != null && turnManagerScript.state == BattleState.PLAYERTURN)
        {
            _fightButton.SetActive(true);
            
        }
        else
        {
            _fightButton.SetActive(false);
        }
        
        
    }

    //Buttons

    public void OnNewBattleClick()
    {
        StartCoroutine(StartStuff());
        CloseEndBattle();
        
        turnManagerScript.NewBattle();
        

        enemyOneHealth.enabled = true;
        enemyOneName.enabled = true;
        enemyOneStamina.enabled = true;
        
        
        UpdateUI();
    }

    public void OnDefendClick()
    {
        combatFunctions.IsDefending(player);
        
        ClosePanels();
    }

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
        combatFunctions.CombatSteps(chosenAttack, player, enemyOne);
        
        ClosePanels();

        //Calls the function to play animations from the animation script. 
        animationScript.PlayAnimation(chosenAttack);
    }
    
    //Color.Neutral Attack Buttons
    public void OnAttackClick()
    {
        
        chosenAttack = attacksDatabase._punch;
        OpenEnemiesPanel();

    }
   
    //Color.Red Attack Buttons
    public void OnFireballClick()
    {
        chosenAttack = player.unitAttackDictionary["Fireball"];
        
        OpenEnemiesPanel();
        
    }

    //Color.Orange Attack Buttons
    public void OnOrangeSpikeClick()
    {
        chosenAttack = player.unitAttackDictionary["Orange Spike"];
        OpenEnemiesPanel();
    }

    //Color.Yellow Attack Buttons
    public void OnYellowSplashClick()
    {
        chosenAttack = player.unitAttackDictionary["Yellow Splash"];
        OpenEnemiesPanel();
    }
    
    //Color.Green Attack Buttons
    public void OnGreenPunchClick()
    {
        chosenAttack = player.unitAttackDictionary["Green Punch"];
        OpenEnemiesPanel();
    }

    //Color.Blue Attack Buttons
    public void OnBlueCrushClick()
    {
        chosenAttack = player.unitAttackDictionary["Blue Crush"];
        OpenEnemiesPanel();
    }
    
    //Color.Violet Attack Buttons
    

    //Axe Buttons
    public void OnChopClick()
    {
        chosenAttack = player.unitAttackDictionary["Chop"];
        _abilitiesPanel.SetActive(false);
        OpenEnemiesPanel();
    }
    //Staff Buttons
    public void OnVioletBallClick()
    {
        chosenAttack = player.unitAttackDictionary["Violet Ball"];
        OpenEnemiesPanel();
    }
    //Sword Buttons
    public void OnSlashClick()
    {
        chosenAttack = player.unitAttackDictionary["Slash"];
        OpenEnemiesPanel();
    }
    //Hammer Buttons
    public void OnSlamClick()
    {
        chosenAttack = player.unitAttackDictionary["Slam"];
        OpenEnemiesPanel();
    }
    //Bow Buttons
    public void OnQuickShotClick()
    {
        chosenAttack = player.unitAttackDictionary["Quick Shot"];
        OpenEnemiesPanel();
    }
    //Use Items 
    public void UseHealthPotion()
    {
        combatFunctions.UseHealthPotion();
        UpdateUI();
        StartCoroutine(WaitForTime(1));
        ClosePanels();
        player.hadATurn = true;
        
    }

    //Attack Functions
    public bool CheckForAttackAvailability(Attack attack)
    {
        bool isUseable = false;
        //Checks to see if the attack has a color component
        if (attack.attackColor != Hue.Neutral)
        {
            //if it does, checks to see which color so can take from the environment
            switch(attack.attackColor)
            {
                case Hue.Red:
                    if(attack.colorCost <= envManaScript.currentRed && player.currentStamina >= attack.staminaCost)
                    {
                        isUseable = true;
                    }
                    break;
                case Hue.Orange:
                    if (attack.colorCost <= envManaScript.currentOrange && player.currentStamina >= attack.staminaCost)
                    {
                        isUseable = true;
                    }
                    break;
                case Hue.Yellow:
                    if (attack.colorCost <= envManaScript.currentYellow && player.currentStamina >= attack.staminaCost)
                    {
                        isUseable = true;
                    }
                    break;
                case Hue.Green:
                    if (attack.colorCost <= envManaScript.currentGreen && player.currentStamina >= attack.staminaCost)
                    {
                        isUseable = true;
                    }
                    break;
                case Hue.Blue:
                    if (attack.colorCost <= envManaScript.currentBlue && player.currentStamina >= attack.staminaCost)
                    {
                        isUseable = true;
                    }
                    break;
                case Hue.Violet:
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
                case Hue.Red:
                    if(player.currentStamina >= kvp.Value.staminaCost && kvp.Value.colorCost <= envManaScript.currentRed)
                    {
                        _fireBallButton.interactable = true;
                    }
                    break;
                case Hue.Orange:
                    if (player.currentStamina >= kvp.Value.staminaCost && kvp.Value.colorCost <= envManaScript.currentOrange)
                    {
                        _orangeSpike.interactable = true;
                    }
                    break;
                case Hue.Yellow:
                    if (player.currentStamina >= kvp.Value.staminaCost && kvp.Value.colorCost <= envManaScript.currentYellow)
                    {
                        _yellowSplash.interactable = true;
                    }
                    break;
                case Hue.Green:
                    if (player.currentStamina >= kvp.Value.staminaCost && kvp.Value.colorCost <= envManaScript.currentGreen)
                    {
                        _greenPunch.interactable = true;
                    }
                    break;
                case Hue.Blue:
                    if (player.currentStamina >= kvp.Value.staminaCost && kvp.Value.colorCost <= envManaScript.currentBlue)
                    {
                        _blueCrush.interactable = true;
                    }
                    break;
                case Hue.Violet:
                    break;
                default:
                    break;
            }
        }

        
    }

    public void AvailableAbilities()
    {
        SetAbilitiesFalse();
        
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
        EnemyButtonNames();

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
        _spellsPanel.SetActive(false);
        _enemiesPanel.SetActive(false);
        _itemPanel.SetActive(false);

        if (_enemiesPanel != null)
        {

            _enemiesPanel.SetActive(!isActive);
        }

    }

    public void OpenEnemiesTwoPanel()
    {
        bool isActive = _enemiesTwoPanel.activeSelf;

        if (_enemiesTwoPanel != null)
        {

            _enemiesTwoPanel.SetActive(!isActive);

        }

        _itemPanel.SetActive(false);
        _spellsPanel.SetActive(false);
        _enemiesPanel.SetActive(false);
    }

    public void ClosePanels()
    {
        _fightPanel.SetActive(false);
        _abilitiesPanel.SetActive(false);
        _spellsPanel.SetActive(false);
        _itemPanel.SetActive(false);
        _enemiesPanel.SetActive(false);
        _enemiesTwoPanel.SetActive(false);
        
    }

    public void CloseEndBattle()
    {
        _endBattlePanel.SetActive(false);
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
        for(int i = 0; i < abilitiesList.Count; i++)
        {
            abilitiesList[i].interactable = false;
        }
        /*
        _chopButton.interactable = false;
        _slamButton.interactable = false;
        _slashButton.interactable = false;
        _quickShotButton.interactable = false;
        _violetBallButton.interactable = false;
        */
    }

    IEnumerator WaitForTime(float time)
    {
        yield return new WaitForSeconds(time);
        //Debug.Log("Waiting");
    }

    private void EnemyButtonNames()
    {
       enemyOneButtonNameText.text = enemyOne.unitName;
    }

    public void NewBattleStuff()
    {
        enemyOneHealth.enabled = true;
        enemyOneName.enabled = true;
        enemyOneStamina.enabled = true;
        enemyOneText.gameObject.SetActive(true);
        UpdateUI();
        
    }

}
//KEYBOARD SHORTCUTS
//HIGHLIGHT INSTANCES - SHIFT+ALT+. (Highlights instances of a word)

//TODO: Better button feedback 
//TODO: Fix the reason why I can't click the Fight button too soon
//TODO: Figure out a better way to use items. 