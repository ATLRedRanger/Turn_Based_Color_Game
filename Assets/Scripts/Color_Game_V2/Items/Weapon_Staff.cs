using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Staff : Weapon
{
    public Hue affinity = Hue.Neutral;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Weapon_Staff(string itemName = "", string itemDescription = "", string itemID = "", int itemAmount = 0, WeaponType weaponType = WeaponType.Spellbook, int baseDamage = 0, int bonusModifier = 0, Attack weaponAttack = null, Hue affinity = Hue.Neutral)
    {
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemID = itemID;
        this.itemAmount = itemAmount;
        this.weaponType = weaponType;
        this.baseDamage = baseDamage;
        this.bonusModifier = bonusModifier;
        this.weaponAttack = weaponAttack;
        this.affinity = affinity;
    }
}
