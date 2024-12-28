using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Database : MonoBehaviour
{
    //Consumable Items
    public Item_Consumable healthPotion;
    public Item_Consumable burnHeal;

    //Weapons
    public Weapon basicAxe;
    public Weapon basicBow;
    public Weapon_Spellbook basicSpellbook;
    public Weapon_Spellbook redSpellbook;

    public Weapon_Staff basicStaff;

    private Attack_Database attackDatabaseScript;


    void Awake()
    {
        attackDatabaseScript = FindObjectOfType<Attack_Database>();
        //Consumables
        healthPotion = CreateConsumable("Health Potion", "Heals 20hp.", ItemIDMaker(), 1, ConsumableType.Health, 20);
        burnHeal = CreateConsumable("Burn Heal", "Heals burn", ItemIDMaker(), 1, ConsumableType.BurnHeal, 0);

        //Weapons:

        //Axes
        basicAxe = CreateWeapon("Basic Axe", "A basic axe.", ItemIDMaker(), WeaponType.Axe, 1, 0, null);


        //Bows
        basicBow = CreateWeapon("Basic Bow", "A basic bow.", ItemIDMaker(), WeaponType.Bow, 1, 0, null);


        //Hammers


        //Spellbooks
        basicSpellbook = CreateSpellbook("Basic Spellbook", "A basic spellbook.", ItemIDMaker(), 1, WeaponType.Spellbook, 1, 1);
        redSpellbook = CreateSpellbook("Red Spellbook", "A spellbook with a light red cover.", ItemIDMaker(), 1, WeaponType.Spellbook, 1, 1);

        //Staves
        basicStaff = CreateStaff("Basic Staff", "A basic staff.", ItemIDMaker(), 1, WeaponType.Staff, 1, 0, null, Hue.Neutral);

        //Swords



    }


    // Start is called before the first frame update
    void Start()
    {
        basicSpellbook.AddAttackToSpellbook(attackDatabaseScript._fireball);
        basicSpellbook.AddAttackToSpellbook(attackDatabaseScript._basicSpellbookAttack);
        redSpellbook.AddAttackToSpellbook(attackDatabaseScript._orangeAttackOne);
        redSpellbook.AddAttackToSpellbook(attackDatabaseScript._fireball);
    }

    public Item_Consumable CreateConsumable(string itemName = "", string itemDescription = "", string itemID = "", int itemAmount = 0, ConsumableType consumableType = ConsumableType.Null, int amountToIncrease = 0)
    {
        Item_Consumable consumable = new Item_Consumable(itemName, itemDescription, itemID, itemAmount, consumableType, amountToIncrease);
        
        return consumable;
    }

    public Weapon CreateWeapon(string name, string itemDescription, string itemID, WeaponType weaponType, int baseDamage, int bonusModifier, Attack weaponAttack)
    {
        Weapon weapon = new Weapon(name, itemDescription, itemID, 1, weaponType, baseDamage, bonusModifier, weaponAttack);

        return weapon;
    }

    public Weapon_Spellbook CreateSpellbook(string itemName = "", string itemDescription = "", string itemID = "", int itemAmount = 0, WeaponType weaponType = WeaponType.Spellbook, int baseDamage = 0, int spellbookTier = 1)
    {
        var spellbook = new Weapon_Spellbook(itemName, itemDescription, itemID, itemAmount, WeaponType.Spellbook, baseDamage, spellbookTier);

        return spellbook;
    }

    public Weapon_Staff CreateStaff(string itemName = "", string itemDescription = "", string itemID = "", int itemAmount = 0, WeaponType weaponType = WeaponType.Spellbook, int baseDamage = 0, int bonusModifier = 0, Attack weaponAttack = null, Hue affinity = Hue.Neutral)
    {
        Weapon_Staff staff = new Weapon_Staff(itemName, itemDescription, itemID, itemAmount, weaponType, baseDamage, bonusModifier, weaponAttack, affinity);

        return staff;
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
