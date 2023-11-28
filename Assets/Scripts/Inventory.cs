using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class Inventory : MonoBehaviour
{
    
    public Unit player;

    public GameObject _inventoryPanel;

    private Unit_Spawner unitSpawnerScript;

    private ItemDatabase itemScript;

    private UI ui_Script;
    
    public List<Weapon> playerWeaponList = new List<Weapon>();

    public List<Consumable> playerConsumableList = new List<Consumable>();

    public List<Item> playerInventory = new List<Item>();

    public List<Button> buttonList = new List<Button>();

    public List<TMP_Text> buttonTextList = new List<TMP_Text>();

    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadingScripts());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToInventory()
    {
        
        playerWeaponList.Add(itemScript._basicAxe);
        playerConsumableList.Add(itemScript._healthPotion);
        player.equippedWeapon = itemScript._basicSpellbook;
        player.isWeaponEquipped = true;
        playerInventory.Add(itemScript._basicHammer);
        playerInventory.Add(itemScript._healthPotion);
        playerInventory.Add(itemScript._basicSpellbook);
        playerInventory.Add(itemScript._basicStaff);

        Debug.Log(player.equippedWeapon);
    }
    

    IEnumerator LoadingScripts()
    {
        yield return new WaitForSeconds(.2f);

        unitSpawnerScript = FindObjectOfType<Unit_Spawner>();

        itemScript = FindObjectOfType<ItemDatabase>();

        ui_Script = FindObjectOfType<UI>();

        player = unitSpawnerScript.player;

        //playerWeaponList = player.weaponList;

        AddToInventory();

        UpdateInventoryUI();
    }

    public void EquipWeapon(Weapon weapon)
    {
        if(player.equippedWeapon == null)
        {
            switch (weapon.weaponType)
            {
                case WeaponType.Axe:
                    if (weapon.weaponLevelRequirement >= player.axeMastery)
                    {
                        player.equippedWeapon = weapon;
                    }
                    break;
                case WeaponType.Staff:
                    if (weapon.weaponLevelRequirement >= player.staffMastery)
                    {
                        player.equippedWeapon = weapon;
                    }
                    break;
                case WeaponType.Sword:
                    if (weapon.weaponLevelRequirement >= player.swordMastery)
                    {
                        player.equippedWeapon = weapon;
                    }
                    break;
                case WeaponType.Hammer:
                    if (weapon.weaponLevelRequirement >= player.hammerMastery)
                    {
                        player.equippedWeapon = weapon;
                    }
                    break;
                case WeaponType.Bow:
                    if (weapon.weaponLevelRequirement >= player.bowMastery)
                    {
                        player.equippedWeapon = weapon;
                    }
                    break;
            }
        }
        else
        {
            //Debug.Log("You have " + player.equippedWeapon.itemName + " equipped. Would you like to unequip?");
        }

    }

    public void UpdateInventoryUI()
    {

        for(int i = 0; i < buttonList.Count; i++)
        {
            buttonList[i].gameObject.SetActive(false);
            
        }
        
        //Loops through the buttonList to set buttons active to the number of items in the inventory
        //Sets any extra buttons to false if they're out of the inventory
        for(int i = 0; i < playerInventory.Count; i++)
        {
            //Checks to see if the button has "SpellBook" in its name 
            //If it does, the player can use the spellbook to open the panel
            //If it doesn't, the button is uninteractable
            if (buttonTextList[i].text.Contains("SpellBook") && player.equippedWeapon.weaponType != WeaponType.Spellbook)
            {
                buttonList[i].interactable = false;
            }
            buttonList[i].gameObject.SetActive(true);
            buttonTextList[i].text = playerInventory[i].itemName;
            
            
        }
        
        

    }
    
    public bool CheckIfSpellbook()
    {
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        int stringButtonNum = int.Parse(buttonName.Substring(buttonName.Length - 2));
        Debug.Log(playerInventory[stringButtonNum - 1].itemName);
        if (playerInventory[stringButtonNum - 1].itemName.Contains("SpellBook"))
        {
            
            return true;
        }

        return false;
    }

    public void UseItem()
    {
        //TODO: Fix the closePanels timing so that the spellbook panel stays open
        //      until you cast the spell. 

        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        int stringButtonNum = int.Parse(buttonName.Substring(buttonName.Length - 2));

        if (CheckIfSpellbook())
        {
            OpenSpellBook();
        }
        else
        {
            playerInventory[stringButtonNum - 1].Use(player);


            if (playerInventory[stringButtonNum - 1].itemAmount < 1)
            {

                playerInventory.Remove(playerInventory[stringButtonNum - 1]);

            }
        }
        

        

        
        ui_Script.UpdateUI();
        ui_Script.ClosePanels();
    }

    public void OpenSpellBook()
    {
        ui_Script.OpenSpellBookPanel();
        ui_Script.ClosePanels();
        ui_Script._fightPanel.SetActive(true);


    }
}
//TODO: Make an inventory panel that has all the items in the player inventory
//TODO: Make it so that the inventory updates so that if the inventory doesn't have an item it doesn't show up in the UI
//Actually use the Use() function on the items instead of what i'm doing in CombatFunctions
