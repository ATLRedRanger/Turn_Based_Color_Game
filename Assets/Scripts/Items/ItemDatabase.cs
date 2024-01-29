using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public Consumable _healthPotion;

    public Weapon _basicHammer;

    public Weapon _basicSword;

    public Weapon _basicAxe;

    public Weapon _basicBow;

    public Weapon _basicStaff;

    public Weapon _redsDarkGreatsword;

    public Spellbook _basicSpellbook;

    public GameObject itemDatabase;

    public AttacksDatabase attackDatabase;

    // Start is called before the first frame update
    void Awake()
    {
       

        _healthPotion = new Consumable("Health Potion", ItemType.Consumable, "Heals a small amount of health.", 5, ConsumableType.Health, 10);

        _basicSword = new Weapon("Basic Sword", ItemType.Weapon, "A basic sword", 1, 2, 0.6f, 0.4f, 1.10f, ItemIDMaker(), WeaponType.Sword, null);

        _basicHammer = new Weapon("Basic Hammer", ItemType.Weapon, "A basic hammer", 1, 5, 0.2f, 0.8f, 1.10f, ItemIDMaker(), WeaponType.Hammer, null);

        _basicAxe = new Weapon("Basic Axe", ItemType.Weapon, "A basic axe", 1, 2, 0.8f, 0.2f, 1.10f, ItemIDMaker(), WeaponType.Axe, null);

        _basicBow = CreateWeapon("Basic Bow", ItemType.Weapon, "A basic bow", 1, 2, 1, 1, 1.5f, ItemIDMaker(), WeaponType.Bow, null);

        _basicStaff = new Staff("Basic Staff", ItemType.Weapon, "A basic staff", 1, 2, 1, 1, 1.10f, ItemIDMaker(), WeaponType.Staff, 1, Hue.Red);

        _basicSpellbook = new Spellbook(_basicStaff, ItemType.Weapon, 1, 1, "Basic SpellBook", "A basic spelbook", ItemIDMaker(), WeaponType.Spellbook, Hue.Neutral);

        //_redsDarkGreatsword = new Weapon("Red's Dark Greatsword", ItemType.Weapon, "Sword Red used to slaughter many!", 1, 10, 0.6f, 0.4f, 1.10f, WeaponType.Sword, null);

        
        
    }

    void Start()
    {
        attackDatabase = FindObjectOfType<AttacksDatabase>();
        _basicSpellbook.AddSpellToSpellbook(attackDatabase._fireBall);
        _basicSpellbook.AddSpellToSpellbook(attackDatabase._violetBall);
        //_redsDarkGreatsword.weaponAttack = attackDatabase._redSlash;
        _redsDarkGreatsword = CreateWeapon("Red's Dark Greatsword", ItemType.Weapon, "Sword Red used to slaughter many!", 1, 10, 0.6f, 0.4f, 1.10f, ItemIDMaker(), WeaponType.Sword, attackDatabase._redSlash);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Weapon CreateWeapon(string itemName, ItemType itemType, string itemDescription, int weaponLevelRequirement, int weaponDamage, float weaponHealthModifier, float weaponStaminaModifier, float weaponCritModifier, string itemID, WeaponType weaponType, Attack weaponAttack)
    {
        var weapon = new Weapon(itemName, itemType, itemDescription, weaponLevelRequirement, weaponDamage, weaponHealthModifier, weaponStaminaModifier, weaponCritModifier, itemID, weaponType, weaponAttack);
        return weapon;
    }

    private Item CreateItem(string itemName, ItemType itemType, string itemDescription, int itemAmount, string itemID)
    {
        var item = new Item(itemName, itemType, itemDescription, itemAmount, ItemIDMaker());
        return item;
    }

    private string ItemIDMaker()
    {
        List<string> alphabet = new List<string>
        {
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"
        };

        string itemID = "";

        for(int i = 0; i < 7; i++)
        {
            int a = Random.Range(0, 36);
            itemID = itemID + alphabet[a];
        }

        return itemID;
    }
}
