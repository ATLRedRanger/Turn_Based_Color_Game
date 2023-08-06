using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    
    public Unit player;

    private Unit_Spawner unitSpawnerScript;

    private ItemDatabase itemScript;
    
    public List<Item> playerItemList = new List<Item>();


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadingScripts());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddToInventory()
    {
        
        playerItemList.Add(itemScript._basicSword);
        playerItemList.Add(itemScript._healthPotion);
        playerItemList[1].itemAmount += 1;
        Debug.Log(player.itemList.Count);
        Debug.Log(playerItemList[0].itemName);
    }
    

    IEnumerator LoadingScripts()
    {
        yield return new WaitForSeconds(.5f);

        unitSpawnerScript = FindObjectOfType<Unit_Spawner>();

        itemScript = FindObjectOfType<ItemDatabase>();

        player = unitSpawnerScript.player;

        playerItemList = player.itemList;

        AddToInventory();
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
            Debug.Log("You have " + player.equippedWeapon.itemName + " equipped. Would you like to unequip?");
        }

    }


}
