using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public Consumable _healthPotion;

    public Weapon _basicSword;

    // Start is called before the first frame update
    void Start()
    {
        _healthPotion = new Consumable("Health Potion", "Heals a small amount of health.", 0, ConsumableType.Health, 10);

        _basicSword = new Weapon("Basic Sword", "A basic sword", 1, 2, WeaponType.Sword);

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
