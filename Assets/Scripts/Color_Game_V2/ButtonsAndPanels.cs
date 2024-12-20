using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonsAndPanels : MonoBehaviour
{
    private Eagle_Eye eagleScript;
    private Attack_Database attackDatabaseScript;
    private Inventory_V2 inventoryScript;

    public Attack chosenAttack = null;

    public Button _attackButton;
    public Button _fireBallButton;


    //Panel Opening Buttons
    public GameObject _fightButton;
    public Button _abilitiesButton;
    public Button _itemsButton;
    public Button _spellsButton;
    public Button _defendButton;
    public Button _spellbookButton;
    public Button _spellPanelCycleButton;

    //Panels
    public GameObject _FightPanel;
    public GameObject _SpellsPanel;
    public GameObject _SpellsPanel2;
    public GameObject _AbilitiesPanel;
    public GameObject _ItemsPanel;
    public GameObject _EnemiesPanel;
    public GameObject _PCsPanel;
    public GameObject _AttackDescriptionPanel;
    public GameObject _SpellbookPanel;
    public GameObject _EndOfBattlePanel;

    //Inventory Buttons
    public GameObject _inventoryButton1;
    public TMP_Text _inventoryButton1Text;
    public GameObject _inventoryButton2;
    public TMP_Text _inventoryButton2Text;
    public GameObject _inventoryButton3;
    public TMP_Text _inventoryButton3Text;
    public GameObject _inventoryButton4;
    public TMP_Text _inventoryButton4Text;
    public GameObject _inventoryButton5;
    public TMP_Text _inventoryButton5Text;
    public GameObject _inventoryButton6;
    public TMP_Text _inventoryButton6Text;
    public GameObject _inventoryButton7;
    public TMP_Text _inventoryButton7Text;
    public GameObject _inventoryButton8;
    public TMP_Text _inventoryButton8Text;
    private List<GameObject> inventoryButtons = new List<GameObject>();
    private List<TMP_Text> inventoryButtonsText = new List<TMP_Text>();


    //TargetPlayerButtons
    public GameObject _playerOneButton;
    public TMP_Text _playerOneButtonText;

    //TargetEnemyButtons
    public GameObject _enemyOneButton;
    public TMP_Text _enemyOneButtonText;
    public GameObject _enemyTwoButton;
    public TMP_Text _enemyTwoButtonText;


    //Spellbook Buttons
    public Button _spellbookButtonOne;
    public TMP_Text _spellbookButton01Text;
    public Button _spellbookButtonTwo;
    public TMP_Text _spellbookButton02Text;
    public Button _spellbookButtonThree;
    public TMP_Text _spellbookButton03Text;
    public Button _spellbookButtonFour;
    public TMP_Text _spellbookButton04Text;

    public Item itemBeingPressed;
    public Weapon_Spellbook playerSpellbook;



    // Start is called before the first frame update
    void Start()
    {
        eagleScript = FindObjectOfType<Eagle_Eye>();
        attackDatabaseScript = FindObjectOfType<Attack_Database>();
        inventoryScript = FindObjectOfType<Inventory_V2>();
        inventoryButtons.Add(_inventoryButton1);
        inventoryButtons.Add(_inventoryButton2);
        inventoryButtons.Add(_inventoryButton3);
        inventoryButtons.Add(_inventoryButton4);
        inventoryButtons.Add(_inventoryButton5);
        inventoryButtons.Add(_inventoryButton6);
        inventoryButtons.Add(_inventoryButton7);
        inventoryButtons.Add(_inventoryButton8);

        inventoryButtonsText.Add(_inventoryButton1Text);
        inventoryButtonsText.Add(_inventoryButton2Text);
        inventoryButtonsText.Add(_inventoryButton3Text);
        inventoryButtonsText.Add(_inventoryButton4Text);
        inventoryButtonsText.Add(_inventoryButton5Text);
        inventoryButtonsText.Add(_inventoryButton6Text);
        inventoryButtonsText.Add(_inventoryButton7Text);
        inventoryButtonsText.Add(_inventoryButton8Text);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleFightPanel()
    {
        _spellbookButton.gameObject.SetActive(false);
        eagleScript.GetCurrentPC();
        //Debug.Log("Fight Panel");
        bool isActive = _FightPanel.activeSelf;

        if (_FightPanel != null)
        {
            if (playerSpellbook != null)
            {
                _spellbookButton.gameObject.SetActive(true);
            }
            _FightPanel.SetActive(!isActive);
        }

        _EnemiesPanel.SetActive(false);
        _ItemsPanel.SetActive(false);
        _SpellbookPanel.SetActive(false);
    }

    public void ToggleAbilitiesPanel()
    {
        InteractableAttackButtons();
        bool isActive = _AbilitiesPanel.activeSelf;

        if(_AbilitiesPanel != null)
        {
            _AbilitiesPanel.SetActive(!isActive);
        }

        if (_AbilitiesPanel.activeSelf)
        {
            _ItemsPanel.SetActive(false);
            _EnemiesPanel.SetActive(false);
            _SpellsPanel.SetActive(false);
            _SpellbookPanel.SetActive(false);
        }
    }

    public void ToggleSpellsPanel()
    {
        InteractableAttackButtons();
        bool isActive = _SpellsPanel.activeSelf;

        if (_SpellsPanel != null)
        {
            _SpellsPanel.SetActive(!isActive);
        }

        if (_SpellsPanel.activeSelf)
        {
            _ItemsPanel.SetActive(false);
            _AbilitiesPanel.SetActive(false);
            _EnemiesPanel.SetActive(false);
            _SpellbookPanel.SetActive(false);
        }
    }

    public void ToggleItemPanel()
    {
        eagleScript.ResetAttackAndEnemyTargets();

        bool isActive = _ItemsPanel.activeSelf;

        if (_ItemsPanel != null)
        {
            _ItemsPanel.SetActive(!isActive);
        }

        for(int i = 0; i < inventoryButtons.Count; i++)
        {
            inventoryButtons[i].SetActive(false);
        }

        
        for(int i = 0; i < inventoryScript.GetInventory().Count; i++)
        {
            inventoryButtons[i].SetActive(true);
            inventoryButtonsText[i].text = inventoryScript.GetInventory()[i].itemName;

        }



        if (_ItemsPanel.activeSelf)
        {
            _AbilitiesPanel.SetActive(false);
            _EnemiesPanel.SetActive(false);
            _SpellsPanel.SetActive(false);
            _SpellbookPanel.SetActive(false);
        }
    }

    public void TogglePCsPanel()
    {
        
        bool isActive = _PCsPanel.activeSelf;
        
        if (_PCsPanel != null)
        {
            _PCsPanel.SetActive(!isActive);
            TogglePCsButtons();
        }
    }


    public void ToggleEnemiesPanel()
    {
        
        bool isActive = _EnemiesPanel.activeSelf;

        if (_EnemiesPanel != null)
        {
            Debug.Log("PEAR");
            _EnemiesPanel.SetActive(!isActive);
        }

        
        if (_EnemiesPanel.activeSelf)
        {
            _AbilitiesPanel.SetActive(false);
            _ItemsPanel.SetActive(false);
            _SpellsPanel.SetActive(false);
            _SpellbookPanel.SetActive(false);
        }
    }

    public void ToggleSpellbookPanel()
    {
        _spellbookButtonOne.interactable = false;
        _spellbookButtonTwo.interactable = false;
        _spellbookButtonThree.interactable = false;
        _spellbookButtonFour.interactable = false;
        _spellbookButton01Text.text = "";
        _spellbookButton02Text.text = "";
        _spellbookButton03Text.text = "";
        _spellbookButton04Text.text = "";

        bool isActive = _SpellbookPanel.activeSelf;
        

        if (_SpellbookPanel != null)
        {
            _SpellbookPanel.SetActive(!isActive);
        }
        
        if (playerSpellbook.numOfAttacks > 3)
        {
            _spellbookButton04Text.text = playerSpellbook.spellbookAttacks[3].attackName;

            if (eagleScript.IsAttackUseable(playerSpellbook.spellbookAttacks[3]))
            {
                _spellbookButtonFour.interactable = true;
            }
        }

        if (playerSpellbook.numOfAttacks > 2)
        {
            _spellbookButton03Text.text = playerSpellbook.spellbookAttacks[2].attackName;

            if (eagleScript.IsAttackUseable(playerSpellbook.spellbookAttacks[2]))
            {
                _spellbookButtonThree.interactable = true;
            }
        }

        if (playerSpellbook.numOfAttacks > 1)
        {

            _spellbookButton02Text.text = playerSpellbook.spellbookAttacks[1].attackName;

            if (eagleScript.IsAttackUseable(playerSpellbook.spellbookAttacks[1]))
            {
                _spellbookButtonTwo.interactable = true;
            }
        }

        if (playerSpellbook.numOfAttacks > 0)
        {
            _spellbookButton01Text.text = playerSpellbook.spellbookAttacks[0].attackName;

            if (eagleScript.IsAttackUseable(playerSpellbook.spellbookAttacks[0]))
            {
                _spellbookButtonOne.interactable = true;
            }
        }
        

        if (_SpellbookPanel.activeSelf)
        {
            _AbilitiesPanel.SetActive(false);
            _ItemsPanel.SetActive(false);
            _SpellsPanel.SetActive(false);
            _EnemiesPanel.SetActive(false);
        }
    }

    public void _SpellPanelCycle()
    {
        bool isActive = _SpellsPanel.activeSelf;

        if(_SpellsPanel != null && _SpellsPanel2 != null)
        {
            _SpellsPanel2.SetActive(isActive);
            _SpellsPanel.SetActive(!isActive);
        }

    }

    public void ToggleAttackDescriptionPanel()
    {
       //_AttackDescriptionPanel.SetActive(true);
        
        bool isActive = _AttackDescriptionPanel.activeSelf;
        
        if (_AttackDescriptionPanel != null)
        {
            _AttackDescriptionPanel.SetActive(!isActive);
        }
    }


    public void TogglePCsButtons()
    {
        if(eagleScript.GetPlayer() != null && eagleScript.GetPlayer().GetCurrentHp() > 0)
        {
            _playerOneButton.SetActive(true);
            _playerOneButtonText.text = eagleScript.GetPlayer().unitName;
        }
    }

    public void ToggleEnemyButtons(Unit_V2 enemyOne, Unit_V2 enemyTwo)
    {
        //_enemyOneButton.SetActive(false);
        //_enemyTwoButton.SetActive(false);
        
        if (enemyOne != null && enemyOne.GetCurrentHp() > 0)
        {
            //Debug.Log($"ButtonPanel Test Name: {enemyOne.unitName}");
            _enemyOneButton.SetActive(true);
            SetEnemyOneButtonName(enemyOne.unitName);
        }
        else
        {
            
            _enemyOneButton.SetActive(false);
            SetEnemyOneButtonName("");
        }
        if (enemyTwo != null && enemyTwo.GetCurrentHp() > 0)
        {
            _enemyTwoButton.SetActive(true);
            SetEnemyTwoButtonName(enemyTwo.unitName);
        }
        else
        {
            _enemyTwoButton.SetActive(false);
        }

        
    }

    public void SetEnemyOneButtonName(string name)
    {
        _enemyOneButtonText.text = name;
    }

    public void OnPlayerOneButtonClick()
    {
        eagleScript.SetPlayerTarget("Player");
        TogglePCsPanel();
        ToggleItemPanel();
    }

    public void OnEnemeyOneButtonClick()
    {
        eagleScript.SetAttackTarget("EnemyOne");
    }

    public void SetEnemyTwoButtonName(string name)
    {
        
        _enemyTwoButtonText.text = name;
    }

    public void OnEnemyTwoButtonClick()
    {
        eagleScript.SetAttackTarget("EnemyTwo"); 
    }
    public void OnDefendButtonClick()
    {
        eagleScript.DefendIsChosen();

        _AbilitiesPanel.SetActive(false);
        _EnemiesPanel.SetActive(false);
        _ItemsPanel.SetActive(false);
        _SpellsPanel.SetActive(false);
        _SpellbookPanel.SetActive(false);
        _PCsPanel.SetActive(false);
        ToggleFightPanel();
    }

    public void InteractableAttackButtons()
    {
        SetButtonsToUninteractable();

        if (eagleScript.IsAttackUseable(attackDatabaseScript._fireball))
        {
            _fireBallButton.enabled = true;
        }
    }

    public void SetButtonsToUninteractable()
    {
        _fireBallButton.interactable = false;
    }






    //Attack Buttons
    public void _FireballClick()
    {
        eagleScript.AttackChangeNotification(attackDatabaseScript._fireball);
        ToggleSpellsPanel();


    }

    public void _OrangeAttackClick()
    {
        eagleScript.AttackChangeNotification(attackDatabaseScript._orangeAttackOne);
        ToggleSpellsPanel();
    }

    public void _AttackClick()
    {
        if(eagleScript.GetCurrentPC().equippedWeapon != null)
        {
            switch (eagleScript.GetCurrentPC().equippedWeapon.weaponType)
            {
                case WeaponType.Axe:
                    eagleScript.AttackChangeNotification(attackDatabaseScript._basicAxeAttack);
                    break;
                case WeaponType.Bow:
                    eagleScript.AttackChangeNotification(attackDatabaseScript._basicBowAttack);
                    break;
                case WeaponType.Hammer:
                    eagleScript.AttackChangeNotification(attackDatabaseScript._basicHammerAttack);
                    break;
                case WeaponType.Spellbook:
                    eagleScript.AttackChangeNotification(attackDatabaseScript._basicSpellbookAttack);
                    break;
                case WeaponType.Staff:
                    eagleScript.AttackChangeNotification(attackDatabaseScript._basicStaffAttack);
                    break;
                case WeaponType.Sword:
                    eagleScript.AttackChangeNotification(attackDatabaseScript._basicSwordAttack);
                    break;
            }
        }
        else
        {
            eagleScript.AttackChangeNotification(attackDatabaseScript._basicAttack);
        }
        
    }


    //Item Buttons
    public void _HealthPotionClick()
    {

    }



































    public void SpellbookButtonBeingPressed()
    {
        //buttonName is the name of the GameObject Button being pressed.
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        //buttonNum is the last 2 digits at the end of the GameObject Button name.
        int buttonNum = int.Parse(buttonName.Substring(buttonName.Length - 2)) - 1;

        Debug.Log(playerSpellbook.spellbookAttacks[buttonNum].attackName);
        
        eagleScript.AttackChangeNotification(playerSpellbook.spellbookAttacks[buttonNum]);
        
        //ToggleSpellbookPanel();
    }



    public void ItemBeingPressed()
    {
        //buttonName is the name of the GameObject Button being pressed.
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        //Debug.Log(buttonName + "is being pressed.");
        //buttonNum is the last 2 digits at the end of the GameObject Button name.
        int buttonNum = int.Parse(buttonName.Substring(buttonName.Length - 2));
        //Debug.Log(buttonNum + "num of button.");
        itemBeingPressed = inventoryScript.GetInventory()[buttonNum - 1];
        
        if(itemBeingPressed is Item_Consumable)
        {
            Item_Consumable consumable = itemBeingPressed as Item_Consumable;
            eagleScript.ConsumableChangeNotification(consumable);
        }
    }




    public void UseItemButton()
    {
        
    }



























    public void EndOfBattlePanel(CombatState combatState)
    {
        _EndOfBattlePanel.SetActive(true);

        if(combatState == CombatState.Won)
        {

        }
        else
        {

        }
    }
}
