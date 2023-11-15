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
        player.equippedWeapon = itemScript._basicStaff;
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
        
        for(int i = 0; i < playerInventory.Count; i++)
        {
            buttonList[i].gameObject.SetActive(true);
            buttonTextList[i].text = playerInventory[i].itemName;
        }
        
    }

    public void UseItem()
    {
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        int stringButtonNum = int.Parse(buttonName.Substring(buttonName.Length - 2));

        playerInventory[stringButtonNum - 1].Use(player);

        playerInventory[stringButtonNum - 1].itemAmount -= 1;
        if (playerInventory[stringButtonNum - 1].itemAmount < 1)
        {
            playerInventory.Remove(playerInventory[stringButtonNum - 1]);
        }

        UpdateInventoryUI();
        ui_Script.UpdateUI();
        ui_Script.ClosePanels();
    }
}
//TODO: Make an inventory panel that has all the items in the player inventory
//Actually use the Use() function on the items instead of what i'm doing in CombatFunctions
