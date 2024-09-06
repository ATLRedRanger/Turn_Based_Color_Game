using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;

public class ButtonsAndPanels : MonoBehaviour
{
    private Eagle_Eye eagleScript;

    public Attack chosenAttack = null;

    public Button _fireBallButton;

    //Panel Opening Buttons
    public Button _abilitiesButton;
    public Button _itemsButton;
    public Button _spellsButton;
    public Button _defendButton;

    public Button _spellPanelCycleButton;

    //Panels
    public GameObject _FightPanel;
    public GameObject _SpellsPanel;
    public GameObject _SpellsPanel2;
    public GameObject _AbilitiesPanel;
    public GameObject _ItemsPanel;
    public GameObject _EnemiesPanel;

    //TargetEnemyButtons
    public GameObject _enemyOneButton;
    public TMP_Text _enemyOneButtonText;
    public GameObject _enemyTwoButton;
    public TMP_Text _enemyTwoButtonText;

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
        bool isActive = _FightPanel.activeSelf;

        if (_FightPanel != null)
        {
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
        }
    }

    public void _FireballClick()
    {
        eagleScript.AttackChangeNotification("Fireball");
        ToggleSpellsPanel();
        ToggleEnemiesPanel();
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


    public void ToggleEnemyButtons(Unit_V2 enemyOne = null, Unit_V2 enemyTwo = null)
    {
        _enemyOneButton.SetActive(false);
        _enemyTwoButton.SetActive(false);

        if (enemyOne != null)
        {
            _enemyOneButton.SetActive(true);
            SetEnemyOneButtonName(enemyOne.unitName);
        }
        if (enemyTwo != null)
        {
            _enemyTwoButton.SetActive(true);
            SetEnemyTwoButtonName(enemyTwo.unitName);
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
}
