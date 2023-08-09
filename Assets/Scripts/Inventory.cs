using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    
    public Unit player;

    private Unit_Spawner unitSpawnerScript;

    private ItemDatabase itemScript;
    
    public List<Weapon> playerWeaponList = new List<Weapon>();

    public List<Consumable> playerConsumableList = new List<Consumable>();

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
        player.equippedWeapon = playerWeaponList[0];
        player.isWeaponEquipped = true;

    }
    

    IEnumerator LoadingScripts()
    {
        yield return new WaitForSeconds(.2f);

        unitSpawnerScript = FindObjectOfType<Unit_Spawner>();

        itemScript = FindObjectOfType<ItemDatabase>();

        player = unitSpawnerScript.player;

        //playerWeaponList = player.weaponList;

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
