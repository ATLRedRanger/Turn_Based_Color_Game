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

    public Spellbook _basicSpellbook;

    public GameObject itemDatabase;

    public AttacksDatabase attackDatabase;

    // Start is called before the first frame update
    void Awake()
    {
       

        _healthPotion = new Consumable("Health Potion", ItemType.Consumable, "Heals a small amount of health.", 5, ConsumableType.Health, 10);

        _basicSword = new Weapon("Basic Sword", ItemType.Weapon, "A basic sword", 1, 2, 0.6f, 0.4f, WeaponType.Sword);

        _basicHammer = new Weapon("Basic Hammer", ItemType.Weapon, "A basic hammer", 1, 2, 0.2f, 0.8f, WeaponType.Hammer);

        _basicAxe = new Weapon("Basic Axe", ItemType.Weapon, "A basic axe", 1, 2, 0.8f, 0.2f, WeaponType.Axe);

        _basicBow = new Weapon("Basic Bow", ItemType.Weapon, "A basic bow", 1, 2, 1, 1, WeaponType.Bow);

        _basicStaff = new Staff("Basic Staff", ItemType.Weapon, "A basic staff", 1, 2, 1, 1, WeaponType.Staff, 1, Hue.Red);

        _basicSpellbook = new Spellbook(_basicStaff, ItemType.Weapon, 1, 1, "Basic SpellBook", "A basic spelbook", WeaponType.Spellbook, Hue.Neutral);
        
        

        
    }

    void Start()
    {
        attackDatabase = FindObjectOfType<AttacksDatabase>();
        Debug.Log("FIREBALL " + attackDatabase._fireBall.attackName);
        _basicSpellbook.AddSpellToSpellbook(attackDatabase._fireBall);
        _basicSpellbook.spellBookAttackList.Add(attackDatabase._violetBall);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Item CreateItem(string itemName, ItemType itemType, string itemDescription, int itemAmount)
    {
        var item = new Item(itemName, itemType, itemDescription, itemAmount);
        return item;
    }
}
