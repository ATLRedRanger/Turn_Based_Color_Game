﻿using System.Collections;
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

public enum WhoseTurn
{
    Player,
    EnemyOne,
    EnemyTwo,
    Nobody
}
public enum CombatState
{
    Won,
    Lost,
    Active
}
public enum Hue
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
    Empty,
    Broken
}

public enum WeaponType
{
    Axe,
    Staff,
    Spellbook,
    Sword,
    Hammer,
    Bow,
    Neutral
    
}

public enum ItemType
{
    Consumable, 
    Weapon,
    Null
}

public enum ConsumableType
{
    Health,
    Tint,
    BurnHeal,
    Null
}

public enum AttackType
{
    Physical,
    Special
}

public enum AttackBehavior
{
    Burn,
    Stun,
    Vamp, 
    FutureSight,
    None
}

public enum Statuses
{
    Burned,
    Blinded,
    Exhausted,
    Stunned

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