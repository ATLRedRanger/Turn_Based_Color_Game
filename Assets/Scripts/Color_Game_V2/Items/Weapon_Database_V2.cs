using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Database_V2 : MonoBehaviour
{
    public Weapon_Axe basicAxe;
    public Weapon_Spellbook basicSpellbook;
    
    void Awake()
    {
        basicAxe = CreateAxe("Basic Axe", "A basic axe.", ItemIDMaker(), 1, WeaponType.Axe, 1);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public Weapon_Axe CreateAxe(string itemName = "", string itemDescription = "", string itemID = "", int itemAmount = 0, WeaponType weaponType = WeaponType.Axe, int weaponDamage = 0)
    {
        var axe = new Weapon_Axe(itemName, itemDescription, itemID, itemAmount, weaponType = WeaponType.Axe, weaponDamage);

        return axe;
    }

    public Weapon_Spellbook CreateSpellbook(string itemName = "", string itemDescription = "", string itemID = "", int itemAmount = 0, WeaponType weaponType = WeaponType.Spellbook, int baseDamage = 0, int spellbookTier = 1)
    {
        var spellbook = new Weapon_Spellbook(itemName, itemDescription, itemID, itemAmount, WeaponType.Spellbook, baseDamage, spellbookTier);

        return spellbook;
    }
    private string ItemIDMaker()
    {
        List<string> alphabet = new List<string>
        {
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"
        };

        string itemID = "";

        for (int i = 0; i < 7; i++)
        {
            int a = Random.Range(0, 36);
            itemID = itemID + alphabet[a];
        }

        return itemID;
    }
}
