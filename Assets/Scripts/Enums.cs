using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public enum Color
{
    Red,
    Orange,
    Yellow,
    Green,
    Blue,
    Violet,
    Neutral
}

public enum StaminaLevels
{
    Full,
    ThreeQuarters,
    Half,
    OneQuarter,
    Broken
}

public enum WeaponType
{
    Axe,
    Staff,
    Sword,
    Hammer,
    Bow,
    Neutral
    
}

public enum ItemType
{
    Consumable, 
    Equipment
}

public enum ConsumableType
{
    Health,Stamina
}

public struct LocationMana
{
    
    public LocationMana(int currentAmount, int colorMax)
    {
        this.currentAmount = currentAmount;
        this.colorMax = colorMax;
    }

    public int currentAmount { get; }
    public int colorMax { get; }

}