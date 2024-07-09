using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public Consumable _healthPotion;

    public Consumable _redTintPotion;

    public Weapon _basicHammer;

    public Weapon _basicSword;

    public Weapon _basicAxe;

    public Weapon _basicBow;

    public Weapon _basicStaff;

    public Weapon _redsDarkGreatsword;

    public Spellbook _basicSpellbook;

    public GameObject itemDatabase;

    public AttacksDatabase attackDatabase;

    //Item Sprites
    public Sprite healthPotionSprite;

    public Sprite basicSwordSprite;

    public Sprite basicAxeSprite;

    // Start is called before the first frame update
    void Awake()
    {
       

        _healthPotion = new Consumable("Health Potion", ItemType.Consumable, healthPotionSprite, "Heals a small amount of health.", 5, ConsumableType.Health, 10, Hue.Neutral);

        _basicSword = new Weapon("Basic Sword", ItemType.Weapon, basicSwordSprite, "A basic sword", 1, 2, 0, 0, 0 , ItemIDMaker(), WeaponType.Sword, null);

        _basicHammer = new Weapon("Basic Hammer", ItemType.Weapon, null, "A basic hammer", 1, 5, 0, 0, 0, ItemIDMaker(), WeaponType.Hammer, null);

        _basicAxe = new Weapon("Basic Axe", ItemType.Weapon, basicAxeSprite, "A basic axe", 1, 2, 0, 0, 0, ItemIDMaker(), WeaponType.Axe, null);

        _basicBow = CreateWeapon("Basic Bow", ItemType.Weapon, null, "A basic bow", 1, 2, ItemIDMaker(), WeaponType.Bow, null);

        _basicStaff = new Staff("Basic Staff", ItemType.Weapon, "A basic staff", 1, 2, ItemIDMaker(), WeaponType.Staff, 1, Hue.Red);

        _basicSpellbook = new Spellbook(_basicStaff, ItemType.Weapon, 1, 1, "Basic SpellBook", "A basic spelbook", ItemIDMaker(), WeaponType.Spellbook, Hue.Neutral);

        _redTintPotion = CreateConsumable("Red Potion", ItemType.Consumable, null, "Tints the user Red.", 2, ConsumableType.Tint, 0, Hue.Red);
        
        
        
    }

    void Start()
    {
        attackDatabase = FindObjectOfType<AttacksDatabase>();
        _basicSpellbook.AddSpellToSpellbook(attackDatabase._fireBall);
        _basicSpellbook.AddSpellToSpellbook(attackDatabase._violetBall);
        _redsDarkGreatsword = CreateWeapon("Red's Dark Greatsword", ItemType.Weapon, null, "Sword Red used to slaughter many!", 1, 10, ItemIDMaker(), WeaponType.Sword, attackDatabase._redSlash);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private Weapon CreateWeapon(string itemName, ItemType itemType, Sprite itemSprite, string itemDescription, int weaponLevelRequirement, int weaponBaseDamage, string itemID, WeaponType weaponType, Attack weaponAttack)
    {
        var weapon = new Weapon(itemName, itemType, itemSprite, itemDescription, weaponLevelRequirement, weaponBaseDamage, 0, 0, 0, itemID, weaponType, weaponAttack);
        return weapon;
    }

    private Item CreateItem(string itemName, ItemType itemType, Sprite itemSprite, string itemDescription, int itemAmount, string itemID)
    {
        var item = new Item(itemName, itemType, itemSprite, itemDescription, itemAmount, ItemIDMaker());
        return item;
    }

    private Consumable CreateConsumable(string itemName, ItemType itemType, Sprite itemSprite, string itemDescription, int itemAmount, ConsumableType consumableType, int refillAmount, Hue tint)
    {
        var consumable = new Consumable(itemName, itemType, itemSprite, itemDescription, itemAmount, consumableType, refillAmount, tint);
        return consumable;
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
