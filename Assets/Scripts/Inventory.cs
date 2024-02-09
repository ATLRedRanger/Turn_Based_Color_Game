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

    private Item itemBeingPressed;

    private UI ui_Script;
    
    //public List<Weapon> playerWeaponList = new List<Weapon>();

    //public List<Consumable> playerConsumableList = new List<Consumable>();

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
        
        //playerWeaponList.Add(itemScript._basicAxe);
        //playerConsumableList.Add(itemScript._healthPotion);
        player.equippedWeapon = itemScript._basicSpellbook;
        player.isWeaponEquipped = true;
        //playerInventory.Add(itemScript._basicHammer);
        playerInventory.Add(itemScript._healthPotion);
        playerInventory.Add(itemScript._basicSpellbook);
        playerInventory.Add(itemScript._basicStaff);
        playerInventory.Add(itemScript._basicBow);
        playerInventory.Add(itemScript._redsDarkGreatsword);

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

    public void EquipWeapon()
    {
      
        Weapon equippableWeapon = itemBeingPressed as Weapon;
        
        if (player.equippedWeapon == null || CheckIfSpellbook() || equippableWeapon.weaponType == WeaponType.Staff)
        {
            player.equippedWeapon = equippableWeapon;
            player.isWeaponEquipped = true;
            Debug.Log($"Player equipped weapon is {player.equippedWeapon.itemName} and it's ID is {player.equippedWeapon.itemID}");
        }
        else
        {
            // Swap weapons if user already has one equipped
            player.equippedWeapon = equippableWeapon;
            Debug.Log($"Player swapped weapon to {player.equippedWeapon.itemName} and it's ID is {player.equippedWeapon.itemID}");
        }
        
        if(player.equippedWeapon.itemID == itemBeingPressed.itemID && player.isWeaponEquipped == true)
        {
           
        }
    }
    public void UnequipWeapon()
    {
        
        player.isWeaponEquipped = false;
        player.equippedWeapon = null;
        Debug.Log($"Is player equipped? {player.isWeaponEquipped}");
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
            
            buttonList[i].gameObject.SetActive(true);
            buttonTextList[i].text = playerInventory[i].itemName;
               
        }
       
    }
    
    public bool CheckIfSpellbook()
    {
        
        if (itemBeingPressed.itemName.Contains("SpellBook"))
        {
            
            return true;
        }

        return false;
    }

    public void UseItem()
    { 

        if(itemBeingPressed.itemType == ItemType.Weapon)
        {
            Weapon selectedWeapon = itemBeingPressed as Weapon;
            ui_Script.chosenAttack = selectedWeapon.weaponAttack;
            ui_Script.OpenEnemiesPanel();
        }
        else
        {
            itemBeingPressed.Use(player);
            ui_Script.UpdateUI();
            ui_Script.ClosePanels();
            if (itemBeingPressed.itemAmount < 1)
            {

                playerInventory.Remove(itemBeingPressed);

            }
        }
    }
    
    public void OpenSpellBook()
    {
        ui_Script.OpenSpellBookPanel();
        ui_Script.ClosePanels();
        ui_Script._fightPanel.SetActive(true);


    }

    public void ItemBeingInteractedWith()
    {
        //This function is to decipher which item in the inventory is being interacted with
        //It first disables the UI buttons
        //Then it checks the button being clicked using the event system
        //Then it parses through the button name for the last 2 characters (which are numbers)
        //Then it assigns the inventory slot at that position to itemBeingPressed
        //If the item being pressed is not a weapon, it turns on the Use button
        //If the item being pressed is a weapon, it turns on the Equip button
        //If the weapon has an attack that can be used, it turns on the Use button

        ui_Script._useButton.SetActive(false);
        ui_Script._equipButton.SetActive(false);
        ui_Script._unequipButton.SetActive(false);
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        int stringButtonNum = int.Parse(buttonName.Substring(buttonName.Length - 2));
        itemBeingPressed = playerInventory[stringButtonNum - 1];

        if (itemBeingPressed.itemType != ItemType.Weapon)
        {
            ui_Script._useButton.SetActive(true);

        }
        else
        {
            Weapon selectedWeapon = itemBeingPressed as Weapon;
            if (player.isWeaponEquipped == false || player.equippedWeapon.itemID != itemBeingPressed.itemID)
            {
                ui_Script._equipButton.SetActive(true);
            }
            else
            {
                ui_Script._unequipButton.SetActive(true);
            }
            
            if (selectedWeapon.weaponAttack != null && ui_Script.IsAttackUsable(selectedWeapon.weaponAttack))
            {
                ui_Script._useButton.SetActive(true);
            }
        }
        
        /*if(player.equippedWeapon.itemID == itemBeingPressed.itemID)
        {
            ui_Script._equipButton.SetActive(false);
            ui_Script._unequipButton.SetActive(true);
        }*/
    }
}
//TODO: Make an inventory panel that has all the items in the player inventory
//TODO: Make it so that the inventory updates so that if the inventory doesn't have an item it doesn't show up in the UI
//Actually use the Use() function on the items instead of what i'm doing in CombatFunctions

//Equipping an Item:
//Press the button corresponding to the Item you want to equip
//If you are not equipped with an item, bring up the Equip button
//When you press the Equip button, the item is equipped