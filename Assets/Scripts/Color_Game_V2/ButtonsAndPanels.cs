using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonsAndPanels : MonoBehaviour
{

    public Attack chosenAttack = null;

    public Button _fireBallButton;

    //Panel Opening Buttons
    public Button _abilitiesButton;
    public Button _itemsButton;
    public Button _spellsButton;

    //Panels
    public GameObject _FightPanel;
    public GameObject _SpellsPanel;
    public GameObject _AbilitiesPanel;
    public GameObject _ItemsPanel;

    // Start is called before the first frame update
    void Start()
    {
        
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
        bool isActive = _AbilitiesPanel.activeSelf;

        if(_AbilitiesPanel != null)
        {
            _AbilitiesPanel.SetActive(!isActive);
        }
    }

    public void ToggleSpellsPanel()
    {
        bool isActive = _SpellsPanel.activeSelf;

        if (_SpellsPanel != null)
        {
            _SpellsPanel.SetActive(!isActive);
        }
    }

    public void ToggleItemPanel()
    {
        bool isActive = _ItemsPanel.activeSelf;

        if (_ItemsPanel != null)
        {
            _ItemsPanel.SetActive(!isActive);
        }
    }

    public void _FireballClick()
    {
        
    }
}
