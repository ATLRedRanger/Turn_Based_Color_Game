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

    // Start is called before the first frame update
    void Awake()
    {
       

        _healthPotion = new Consumable("Health Potion", "Heals a small amount of health.", 5, ConsumableType.Health, 10);

        _basicSword = new Weapon("Basic Sword", "A basic sword", 1, 2, 1, 1, WeaponType.Sword);

        _basicHammer = new Weapon("Basic Hammer", "A basic hammer", 1, 2, 0.4f, 0.6f, WeaponType.Hammer);

        _basicAxe = new Weapon("Basic Axe", "A basic axe", 1, 2, 0.6f, 0.4f, WeaponType.Axe);

        _basicBow = new Weapon("Basic Bow", "A basic bow", 1, 2, 1, 1, WeaponType.Bow);

        _basicStaff = new Weapon("Basic Staff", "A basic staff", 1, 2, 1, 1, WeaponType.Staff);

        _basicSpellbook = new Spellbook(_basicAxe, 1, 1, "Basic Spellbook", WeaponType.Spellbook);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Item CreateItem(string itemName, string itemDescription, int itemAmount)
    {
        var item = new Item(itemName, itemDescription, itemAmount);
        return item;
    }
}
