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

    public Attack chosenAttack = null;

    public Button _attackButton;
    public Button _fireBallButton;
    

    //Panel Opening Buttons
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
    public GameObject _AttackDescriptionPanel;
    public GameObject _SpellbookPanel;


    //TargetEnemyButtons
    public GameObject _enemyOneButton;
    public TMP_Text _enemyOneButtonText;
    public GameObject _enemyTwoButton;
    public TMP_Text _enemyTwoButtonText;


    public Item itemBeingPressed;

    // Start is called before the first frame update
    void Start()
    {
        eagleScript = FindObjectOfType<Eagle_Eye>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleFightPanel()
    {
        _spellbookButton.gameObject.SetActive(false);
        //Debug.Log("Fight Panel");
        bool isActive = _FightPanel.activeSelf;

        if (_FightPanel != null)
        {
            if (eagleScript.GetCurrentPC().equippedWeapon != null && eagleScript.GetCurrentPC().equippedWeapon.weaponType == WeaponType.Spellbook)
            {
                _spellbookButton.gameObject.SetActive(true);
            }
            _FightPanel.SetActive(!isActive);
        }

        
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

        if (_ItemsPanel.activeSelf)
        {
            _AbilitiesPanel.SetActive(false);
            _EnemiesPanel.SetActive(false);
            _SpellsPanel.SetActive(false);
            _SpellbookPanel.SetActive(false);
        };
    }

    public void ToggleEnemiesPanel()
    {
        bool isActive = _EnemiesPanel.activeSelf;

        if (_EnemiesPanel != null)
        {
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
        bool isActive = _SpellbookPanel.activeSelf;

        if (_SpellbookPanel != null)
        {
            _SpellbookPanel.SetActive(!isActive);
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
        if (enemyTwo != null)
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

    public void OnEnemeyOneButtonClick()
    {
        eagleScript.SetAttackTarget("EnemyOne");
        ToggleEnemiesPanel();
    }

    public void SetEnemyTwoButtonName(string name)
    {
        
        _enemyTwoButtonText.text = name;
    }

    public void OnEnemyTwoButtonClick()
    {
        eagleScript.SetAttackTarget("EnemyTwo");
        ToggleEnemiesPanel();
    }
    public void OnDefendButtonClick()
    {
        eagleScript.DefendIsChosen();
        ToggleFightPanel();
    }

    public void InteractableAttackButtons()
    {
        SetButtonsToUninteractable();

        List<string> useableAttacks = eagleScript.IsPlayerAttackUseable();

        foreach(string attack in useableAttacks)
        {
            switch (attack)
            {
                case "Fireball":
                    _fireBallButton.interactable = true;
                    break;
            }
        }
    }

    public void SetButtonsToUninteractable()
    {
        _fireBallButton.interactable = false;
    }






    //Attack Buttons
    public void _FireballClick()
    {
        eagleScript.AttackChangeNotification("Fireball");
        ToggleSpellsPanel();
        //ToggleEnemiesPanel();
    }

    public void _AttackClick()
    {
        eagleScript.AttackChangeNotification("Attack");
        //ToggleSpellsPanel();
        ToggleEnemiesPanel();
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
        int buttonNum = int.Parse(buttonName.Substring(buttonName.Length - 2));
        Debug.Log(eagleScript.GetCurrentPC().equippedWeapon.itemName);
        if (eagleScript.GetCurrentPC().equippedWeapon.weaponType == WeaponType.Spellbook)
        {
            Debug.Log(buttonNum + "num of button.");
            Weapon_Spellbook spellbook = eagleScript.GetCurrentPC().equippedWeapon as Weapon_Spellbook;
            eagleScript.AttackChangeNotification(spellbook.spellbookAttacks[buttonNum - 1].attackName);
        }
        
        Debug.Log(buttonName + "is being pressed.");
        
        ToggleSpellbookPanel();
    }



    public void ItemBeingPressed()
    {
        //buttonName is the name of the GameObject Button being pressed.
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        //buttonNum is the last 2 digits at the end of the GameObject Button name.
        int buttonNum = int.Parse(buttonName.Substring(buttonName.Length - 2));

        //itemBeingPressed = playerInventory[stringButtonNum - 1];
        Debug.Log(buttonName + "is being pressed.");
        Debug.Log(buttonNum + "num of button.");

    }
}
