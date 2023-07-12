using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public Item _healthPotion;

    // Start is called before the first frame update
    void Start()
    {
        _healthPotion = CreateItem("Health Potion");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Item CreateItem(string itemName)
    {
        var item = new Item(itemName);

        return item;
    }
}
