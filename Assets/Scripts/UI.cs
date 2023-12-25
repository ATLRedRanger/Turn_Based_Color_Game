using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine.EventSystems;



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

    public GameObject floatingDamageText;

    public GameObject enemyOneDamagePosition;

    public GameObject playerDamagePosition;

    public GameObject redBarPosition;

    public GameObject orangeBarPosition;

    public GameObject yellowBarPosition;

    public GameObject greenBarPosition;

    public GameObject blueBarPosition;

    public GameObject violetBarPosition;

    //private GameObject floatingDamageTextClone;

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


    //SpellBook Related
    private Attack currentSpellBookSpell;
    public GameObject _spellBookButtonMain;
    public Button _spellBookButton1;
    public TMP_Text _spellBookButton1Text;
    public Button _spellBookButton2;
    public TMP_Text _spellBookButton2Text;
    public Button _spellBookButton3;
    public TMP_Text _spellBookButton3Text;
    public Button _spellBookButton4;
    public TMP_Text _spellBookButton4Text;

    //UI Item Buttons
    public GameObject _healthPotion;
    public GameObject _equipButton;
    public GameObject _useButton;

    
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

    public GameObject _spellBookPanel;

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

    public CombatFunctions combatFunctionsScript;

    private AttacksDatabase attacksDatabase;

    private Inventory inventoryScript;

    //Player
    public StatusEffects statusEffectsScript;

    private Animations animationScript;

    //Abilities List
    public List<Button> abilitiesList = new List<Button>();

    //Attack
    public Attack chosenAttack;



    //Damage Popup
    
    

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
        unitSpawnerScript = FindObjectOfType<Unit_Spawner>();
        turnManagerScript = FindObjectOfType<Turn_Manager>();
        envManaScript = FindObjectOfType<ENV_Mana>();
        inventoryScript = FindObjectOfType<Inventory>();
        animationScript = FindObjectOfType<Animations>();
        combatFunctionsScript = FindObjectOfType<CombatFunctions>();
        UpdateUI();
        Debug.Log("UNIT SPAWNER SCRIPT" + unitSpawnerScript.player.maxStamina);
    }
    private void NamesText()
    {
        
        playerName.text = unitSpawnerScript.player.unitName;
        enemyOneName.text = unitSpawnerScript.enemyOne.unitName;
        enemyOneTextTwo.text = enemyOneText.text;
        
        
    }

    public void FloatingNumbersText(Unit unit, Attack attack)
    {


        StartCoroutine(DamageNumbers(unit));
        StartCoroutine(EnvironmentManaNumbers(attack));
        
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
        //TODO: Ask theunitSpawnerScript.player if they'd like to continue or go back to town
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
            weaponExperienceGained.text = "Player gains " + 15 + " " + unitSpawnerScript.player.equippedWeapon.weaponType + " experience.";
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
        combatFunctionsScript.IsDefending(unitSpawnerScript.player);
        
        ClosePanels();
    }

    /*public void BackButton()
    {
        //This button is to allow theunitSpawnerScript.player to go back to a previous menu
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
        
        _spellBookPanel.gameObject.SetActive(false);
        

        StartCoroutine(combatFunctionsScript.CombatStepsTwo(chosenAttack,unitSpawnerScript.player, unitSpawnerScript.enemyOne));
        
        if(chosenAttack == currentSpellBookSpell)
        {
            Spellbook spellbook = unitSpawnerScript.player.equippedWeapon as Spellbook;
            spellbook.GainExperience(15);
            Debug.Log($"SpellBook Experience{spellbook.spellBookExperience}");
        }

        //Calls the function to play animations from the animation script. 
        PlayAttackAnimation(chosenAttack, unitSpawnerScript.enemyOne);

        _fightButton.SetActive(false);
        ClosePanels();
    }
    public void PlayAttackAnimation(Attack chosenAttack, Unit defender)
    {
        animationScript.PlayAnimation(chosenAttack, defender);
    }
    //Color.Neutral Attack Buttons
    public void OnAttackClick()
    {
        
        chosenAttack = unitSpawnerScript.player.unitAttackDictionary["Punch"];
        OpenEnemiesPanel();

    }
   
    //Color.Red Attack Buttons
    public void OnFireballClick()
    {
        chosenAttack =unitSpawnerScript.player.unitAttackDictionary["Fireball"];
        
        OpenEnemiesPanel();
        
    }

    //Color.Orange Attack Buttons
    public void OnOrangeSpikeClick()
    {
        chosenAttack =unitSpawnerScript.player.unitAttackDictionary["Orange Spike"];
        OpenEnemiesPanel();
    }

    //Color.Yellow Attack Buttons
    public void OnYellowSplashClick()
    {
        chosenAttack =unitSpawnerScript.player.unitAttackDictionary["Yellow Splash"];
        OpenEnemiesPanel();
    }
    
    //Color.Green Attack Buttons
    public void OnGreenPunchClick()
    {
        chosenAttack =unitSpawnerScript.player.unitAttackDictionary["Green Punch"];
        OpenEnemiesPanel();
    }

    //Color.Blue Attack Buttons
    public void OnBlueCrushClick()
    {
        chosenAttack =unitSpawnerScript.player.unitAttackDictionary["Blue Crush"];
        OpenEnemiesPanel();
    }
    
    //Color.Violet Attack Buttons
    

    //Axe Buttons
    public void OnChopClick()
    {
        chosenAttack =unitSpawnerScript.player.unitAttackDictionary["Chop"];
        _abilitiesPanel.SetActive(false);
        OpenEnemiesPanel();
    }
    //Staff Buttons
    public void OnVioletBallClick()
    {
        chosenAttack =unitSpawnerScript.player.unitAttackDictionary["Violet Ball"];
        OpenEnemiesPanel();
    }
    //Sword Buttons
    public void OnSlashClick()
    {
        chosenAttack =unitSpawnerScript.player.unitAttackDictionary["Slash"];
        OpenEnemiesPanel();
    }
    //Hammer Buttons
    public void OnSlamClick()
    {
        chosenAttack =unitSpawnerScript.player.unitAttackDictionary["Slam"];
        OpenEnemiesPanel();
    }
    //Bow Buttons
    public void OnQuickShotClick()
    {
        chosenAttack =unitSpawnerScript.player.unitAttackDictionary["Quick Shot"];
        OpenEnemiesPanel();
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
                    if(attack.colorCost <= envManaScript.currentRed &&unitSpawnerScript.player.currentStamina >= attack.staminaCost)
                    {
                        isUseable = true;
                    }
                    break;
                case Hue.Orange:
                    if (attack.colorCost <= envManaScript.currentOrange &&unitSpawnerScript.player.currentStamina >= attack.staminaCost)
                    {
                        isUseable = true;
                    }
                    break;
                case Hue.Yellow:
                    if (attack.colorCost <= envManaScript.currentYellow &&unitSpawnerScript.player.currentStamina >= attack.staminaCost)
                    {
                        isUseable = true;
                    }
                    break;
                case Hue.Green:
                    if (attack.colorCost <= envManaScript.currentGreen &&unitSpawnerScript.player.currentStamina >= attack.staminaCost)
                    {
                        isUseable = true;
                    }
                    break;
                case Hue.Blue:
                    if (attack.colorCost <= envManaScript.currentBlue &&unitSpawnerScript.player.currentStamina >= attack.staminaCost)
                    {
                        isUseable = true;
                    }
                    break;
                case Hue.Violet:
                    if (attack.colorCost <= envManaScript.currentViolet &&unitSpawnerScript.player.currentStamina >= attack.staminaCost)
                    {
                        isUseable = true;
                    }
                    break;
            }
        }
        else
        {
            if(unitSpawnerScript.player.currentStamina >= attack.staminaCost)
            {
                isUseable = true;
            }
        }
        
        return isUseable;
        
    }

    public void AvailableSpells()
    {

        SetSpellsFalse();
        foreach(var kvp in unitSpawnerScript.player.unitAttackDictionary)
        {
            switch(kvp.Value.attackColor)
            {
                case Hue.Red:
                    if(unitSpawnerScript.player.currentStamina >= kvp.Value.staminaCost && kvp.Value.colorCost <= envManaScript.currentRed)
                    {
                        _fireBallButton.interactable = true;
                    }
                    break;
                case Hue.Orange:
                    if (unitSpawnerScript.player.currentStamina >= kvp.Value.staminaCost && kvp.Value.colorCost <= envManaScript.currentOrange)
                    {
                        _orangeSpike.interactable = true;
                    }
                    break;
                case Hue.Yellow:
                    if (unitSpawnerScript.player.currentStamina >= kvp.Value.staminaCost && kvp.Value.colorCost <= envManaScript.currentYellow)
                    {
                        _yellowSplash.interactable = true;
                    }
                    break;
                case Hue.Green:
                    if (unitSpawnerScript.player.currentStamina >= kvp.Value.staminaCost && kvp.Value.colorCost <= envManaScript.currentGreen)
                    {
                        _greenPunch.interactable = true;
                    }
                    break;
                case Hue.Blue:
                    if (unitSpawnerScript.player.currentStamina >= kvp.Value.staminaCost && kvp.Value.colorCost <= envManaScript.currentBlue)
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
        
        switch(unitSpawnerScript.player.equippedWeapon.weaponType)
        {
            case WeaponType.Axe:
                switch (unitSpawnerScript.player.axeMastery)
                {
                    case 1:
                        if (CheckForAttackAvailability(unitSpawnerScript.player.unitAttackDictionary["Chop"]))
                        {
                            _chopButton.interactable = true;
                        }
                        
                        break;
                    case 3:
                        break;
                    
                }
                break;
            case WeaponType.Staff:
                switch (unitSpawnerScript.player.staffMastery)
                {
                    case 1:
                        _violetBallButton.interactable = true; 
                        break;
                }
                break;
            case WeaponType.Sword:
                switch (unitSpawnerScript.player.swordMastery)
                {
                    case 1:
                        if (CheckForAttackAvailability(unitSpawnerScript.player.unitAttackDictionary["Slash"]))
                        {
                            _slashButton.interactable = true;
                        }
                        //_slashButton.interactable = true;
                        break;
                }
                break;
            case WeaponType.Hammer:
                switch (unitSpawnerScript.player.hammerMastery)
                {
                    case 1:
                        if (CheckForAttackAvailability(unitSpawnerScript.player.unitAttackDictionary["Slam"]))
                        {
                            _slamButton.interactable = true;
                        }
                        
                        break;
                }
                break;
            case WeaponType.Bow:
                switch (unitSpawnerScript.player.bowMastery)
                {
                    case 1:
                        if (CheckForAttackAvailability(unitSpawnerScript.player.unitAttackDictionary["Quick Shot"]))
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
        if(unitSpawnerScript.player.equippedWeapon.weaponType == WeaponType.Spellbook)
        {
            _spellBookButtonMain.gameObject.SetActive(true);
        }
        else
        {
            _spellBookButtonMain.gameObject.SetActive(false);
        }
        bool isActive = _fightPanel.activeSelf;

        if (_fightPanel != null)
        {

            _fightPanel.SetActive(!isActive);
            _spellBookPanel.SetActive(false);
        }

        if (_fightPanel.activeSelf == false)
        {
            ClosePanels();
            _spellBookPanel.SetActive(false);
        }
    }

    public void OpenItemsPanel()
    {
        bool isActive = _itemPanel.activeSelf;

        _equipButton.gameObject.SetActive(false);
        _useButton.gameObject.SetActive(false);
        
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
                _spellBookPanel.SetActive(false);
                inventoryScript.UpdateInventoryUI();
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
                _spellBookPanel.SetActive(false);
            }
        }
        if(_spellsPanel.activeSelf == false)
        {
            _abilitiesPanel.SetActive(false);
            _itemPanel.SetActive(false);
            _enemiesPanel.SetActive(false);
            _spellBookPanel.SetActive(false);
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
                _spellBookPanel.SetActive(false);
            }
        }
        if(_abilitiesPanel.activeSelf == false)
        {
            
            _spellsPanel.SetActive(false);
            _itemPanel.SetActive(false);
            _enemiesPanel.SetActive(false);
            _spellBookPanel.SetActive(false);
        }

    }

    public void OpenEnemiesPanel()
    {
        bool isActive = _enemiesPanel.activeSelf;
        _spellsPanel.SetActive(false);
        _enemiesPanel.SetActive(false);
        _itemPanel.SetActive(false);
        _spellBookPanel.SetActive(false);
        _abilitiesPanel.SetActive(false);

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
        _spellBookPanel.SetActive(false);
        _abilitiesPanel.SetActive(false);
    }
    
    public void OpenSpellBookPanel()
    {
        _spellBookButton1.gameObject.SetActive(false);
        _spellBookButton2.gameObject.SetActive(false);
        _spellBookButton3.gameObject.SetActive(false);
        _spellBookButton4.gameObject.SetActive(false);
        _spellBookButton1.interactable = false;
        _spellBookButton2.interactable = false;
        _spellBookButton3.interactable = false;
        _spellBookButton4.interactable = false;

        _spellBookPanel.SetActive(true);

        Spellbook spellbook = unitSpawnerScript.player.equippedWeapon as Spellbook;
        
        //If the button is true, set the text of the button to the spell name
        if (spellbook.spellBookAttackList.Count >= 1)
        {
            _spellBookButton1.gameObject.SetActive(true);
            _spellBookButton1Text.text = spellbook.spellBookAttackList[0].attackName;
            if (IsAttackUsable(spellbook.spellBookAttackList[0]))
            {
                _spellBookButton1.interactable = true;
            }
        }
        if (spellbook.spellBookAttackList.Count >= 2)
        {
            _spellBookButton2.gameObject.SetActive(true);
            _spellBookButton2Text.text = spellbook.spellBookAttackList[1].attackName;
            
            if (IsAttackUsable(spellbook.spellBookAttackList[1]))
            {
                
                _spellBookButton2.interactable = true;
            }
        }
        if (spellbook.spellBookAttackList.Count >= 3)
        {
            _spellBookButton3.gameObject.SetActive(true);
            _spellBookButton3Text.text = spellbook.spellBookAttackList[2].attackName;
            if (IsAttackUsable(spellbook.spellBookAttackList[2]))
            {
                _spellBookButton3.interactable = true;
            }
        }
        if (spellbook.spellBookAttackList.Count >= 4)
        {
            _spellBookButton4.gameObject.SetActive(true);
            _spellBookButton4Text.text = spellbook.spellBookAttackList[3].attackName;
            if (IsAttackUsable(spellbook.spellBookAttackList[3]))
            {
                _spellBookButton4.interactable = true;
            }
        }

        _spellsPanel.SetActive(false);
        _enemiesPanel.SetActive(false);
        _itemPanel.SetActive(false);
        _abilitiesPanel.SetActive(false);
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
        yield return new WaitForSeconds(2);

    }
    IEnumerator DamageNumbers(Unit unit)
    {
        enemyOneText.faceColor = new Color32(241, 160, 118, 255);
        //Instantiates the GameObject that has the animations for the damage text
        yield return new WaitForSeconds(.75f);

        if (unit.isPlayer)
        {
            
            //Instantiates a copy of the gameObject at the specified position
            //Grabs the TMP_Text component off the child of the GameObject
            //Destroys the GameObject after a second
            GameObject floatingDamageTextClone = Instantiate(floatingDamageText, playerDamagePosition.transform.position, Quaternion.identity);
            floatingDamageTextClone.transform.GetChild(0).GetComponent<TMP_Text>().text = "-" + combatFunctionsScript.healthLost.ToString();
            if (combatFunctionsScript.crit)
            {
                floatingDamageTextClone.transform.GetChild(0).GetComponent<TMP_Text>().faceColor = new Color32(255, 255, 0, 255);
            }
            else
            {
                floatingDamageTextClone.transform.GetChild(0).GetComponent<TMP_Text>().faceColor = new Color32(255, 255, 255, 255);
            }
            
            Destroy(floatingDamageTextClone, 1);
        }
        if (unit.unitName == unitSpawnerScript.enemyOne.unitName)
        {
            GameObject floatingDamageTextClone = Instantiate(floatingDamageText, enemyOneDamagePosition.transform.position, Quaternion.identity);
            floatingDamageTextClone.transform.GetChild(0).GetComponent<TMP_Text>().text = "-" + combatFunctionsScript.healthLost.ToString();
            if (combatFunctionsScript.crit)
            {
                floatingDamageTextClone.transform.GetChild(0).GetComponent<TMP_Text>().faceColor = new Color32(255, 255, 0, 255);
            }
            else
            {
                floatingDamageTextClone.transform.GetChild(0).GetComponent<TMP_Text>().faceColor = new Color32(255, 255, 255, 255);
            }
            
            Destroy(floatingDamageTextClone, 1);
        }
        
        
    }
    IEnumerator EnvironmentManaNumbers(Attack attack)
    {
        float textDelay = 1.5f;
        yield return new WaitForSeconds(1);
        switch(attack.attackColor)
        {
            case Hue.Red:
                GameObject floatingDamageTextClone6 = Instantiate(floatingDamageText, redBarPosition.transform.position, Quaternion.identity);
                floatingDamageTextClone6.transform.GetChild(0).GetComponent<TMP_Text>().text = "-" + attack.colorCost.ToString(); 
                floatingDamageTextClone6.transform.GetChild(0).GetComponent<TMP_Text>().faceColor = new Color32(255, 0, 0, 255);
                Destroy(floatingDamageTextClone6, textDelay);
                break;
            case Hue.Orange:
                GameObject floatingDamageTextClone1 = Instantiate(floatingDamageText, orangeBarPosition.transform.position, Quaternion.identity);
                floatingDamageTextClone1.transform.GetChild(0).GetComponent<TMP_Text>().text = "-" + attack.colorCost.ToString();
                floatingDamageTextClone1.transform.GetChild(0).GetComponent<TMP_Text>().faceColor = new Color32(255, 92, 0, 255);
                Destroy(floatingDamageTextClone1, textDelay);
                break;
            case Hue.Yellow:
                GameObject floatingDamageTextClone2 = Instantiate(floatingDamageText, yellowBarPosition.transform.position, Quaternion.identity);
                floatingDamageTextClone2.transform.GetChild(0).GetComponent<TMP_Text>().text = "-" + attack.colorCost.ToString();
                floatingDamageTextClone2.transform.GetChild(0).GetComponent<TMP_Text>().faceColor = new Color32(202, 161, 0, 255);
                Destroy(floatingDamageTextClone2, textDelay);
                break;
            case Hue.Green:
                GameObject floatingDamageTextClone3 = Instantiate(floatingDamageText, greenBarPosition.transform.position, Quaternion.identity);
                floatingDamageTextClone3.transform.GetChild(0).GetComponent<TMP_Text>().text = "-" + attack.colorCost.ToString();
                floatingDamageTextClone3.transform.GetChild(0).GetComponent<TMP_Text>().faceColor = new Color32(0, 161, 13, 255);
                Destroy(floatingDamageTextClone3, textDelay);
                break;
            case Hue.Blue:
                GameObject floatingDamageTextClone4 = Instantiate(floatingDamageText, blueBarPosition.transform.position, Quaternion.identity);
                floatingDamageTextClone4.transform.GetChild(0).GetComponent<TMP_Text>().text = "-" + attack.colorCost.ToString();
                floatingDamageTextClone4.transform.GetChild(0).GetComponent<TMP_Text>().faceColor = new Color32(0, 38, 255, 255);
                Destroy(floatingDamageTextClone4, textDelay);
                break;
            case Hue.Violet:
                GameObject floatingDamageTextClone5 = Instantiate(floatingDamageText, violetBarPosition.transform.position, Quaternion.identity);
                floatingDamageTextClone5.transform.GetChild(0).GetComponent<TMP_Text>().text = "-" + attack.colorCost.ToString();
                floatingDamageTextClone5.transform.GetChild(0).GetComponent<TMP_Text>().faceColor = new Color32(179, 0, 255, 255);
                Destroy(floatingDamageTextClone5, textDelay);
                break;
            
        }
        

    }
    public void MiscellaneousFloatingNumbers(Unit unit, int damage)
    {
        
        if (unit.isPlayer)
        {
            //Instantiates a copy of the gameObject at the specified position
            //Grabs the TMP_Text component off the child of the GameObject
            //Destroys the GameObject after a second
            GameObject floatingDamageTextClone = Instantiate(floatingDamageText, playerDamagePosition.transform.position, Quaternion.identity);
            floatingDamageTextClone.transform.GetChild(0).GetComponent<TMP_Text>().text = "-" + damage.ToString();
            floatingDamageTextClone.transform.GetChild(0).GetComponent<TMP_Text>().faceColor = new Color32(255, 255, 255, 255);
            Destroy(floatingDamageTextClone, 1.5f);
        }
        if (unit.unitName == unitSpawnerScript.enemyOne.unitName)
        {
            GameObject floatingDamageTextClone = Instantiate(floatingDamageText, enemyOneDamagePosition.transform.position, Quaternion.identity);
            floatingDamageTextClone.transform.GetChild(0).GetComponent<TMP_Text>().text = "-" + damage.ToString();
            floatingDamageTextClone.transform.GetChild(0).GetComponent<TMP_Text>().color = new Color32(255, 255, 255, 255);
            Destroy(floatingDamageTextClone, 1.5f);
        }
    }
    private void EnemyButtonNames()
    {
       enemyOneButtonNameText.text = unitSpawnerScript.enemyOne.unitName;
    }
    
    public void SpellBookButton1()
    {
        //Finds the button name, so that it matches the list from the spellbook to cast that spell
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        int stringButtonNum = int.Parse(buttonName.Substring(buttonName.Length - 2));

        Spellbook spellbook = unitSpawnerScript.player.equippedWeapon as Spellbook;
        currentSpellBookSpell = spellbook.spellBookAttackList[stringButtonNum - 1];
        chosenAttack = spellbook.spellBookAttackList[stringButtonNum - 1];


        OpenEnemiesPanel();

    }
    
    public void NewBattleStuff()
    {
        enemyOneHealth.enabled = true;
        enemyOneName.enabled = true;
        enemyOneStamina.enabled = true;
        enemyOneText.gameObject.SetActive(true);
        UpdateUI();
        
    }
    public bool IsAttackUsable(Attack attack)
    {
        switch (attack.attackColor)
        {
            case (Hue.Red):
                if (unitSpawnerScript.player.currentStamina >= attack.staminaCost && attack.colorCost <= envManaScript.currentRed)
                {
                    return true;
                }
                break;
            case (Hue.Orange):
                if (unitSpawnerScript.player.currentStamina >= attack.staminaCost && attack.colorCost <= envManaScript.currentOrange)
                {
                    return true;
                }
                break;
            case (Hue.Yellow):
                if (unitSpawnerScript.player.currentStamina >= attack.staminaCost && attack.colorCost <= envManaScript.currentYellow)
                {
                    return true;
                }
                break;
            case (Hue.Green):
                if (unitSpawnerScript.player.currentStamina >= attack.staminaCost && attack.colorCost <= envManaScript.currentGreen)
                {
                    return true;
                }
                break;
            case (Hue.Blue):
                if (unitSpawnerScript.player.currentStamina >= attack.staminaCost && attack.colorCost <= envManaScript.currentBlue)
                {
                    return true;
                }
                break;
            case (Hue.Violet):
                if (unitSpawnerScript.player.currentStamina >= attack.staminaCost && attack.colorCost <= envManaScript.currentViolet)
                {
                    return true;
                }
                break;
            default:
                if (unitSpawnerScript.player.currentStamina >= attack.staminaCost)
                {
                    return true;
                }
                break;
        }

        return false;
    }

    //Inventory Buttons
    public void EquipButton()
    {
        
        inventoryScript.EquipWeapon();
        ClosePanels();
        OpenFightPanel();
    }
    public void UseButton()
    {
        
        inventoryScript.UseItem();
        unitSpawnerScript.player.hadATurn = true;
        ClosePanels();
    }
}
//KEYBOARD SHORTCUTS
//HIGHLIGHT INSTANCES - SHIFT+ALT+. (Highlights instances of a word)

//TODO: Better button feedback 
//TODO: Fix the reason why I can't click the Fight button too soon
//TODO: Figure out a better way to use items. 
//TODO: Make a function called IsCastable to grey out uncastable spells in SpellBook