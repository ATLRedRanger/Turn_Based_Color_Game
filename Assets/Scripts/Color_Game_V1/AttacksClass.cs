﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

[System.Serializable]
public class Attack
{
    public string attackName;

    public string animationName;

    public int attackDamage;

    public int staminaCost;

    public int attackAccuracy;

    public int colorCost;

    public int numOfAttacks;

    public float animTimeLength;

    public Hue attackColor;

    public AttackType attackType;

    public WeaponType weaponType;

    private AttackBehavior attackBehavior;
    
  
    //These are different ways to *initialize* the attack


    //Using this method, I'd have to make the new attack then declare all of the variables. 
    //Seems way more time consuming because I'd have to do it over for each new attack 
    //that I wanted to create.
    public Attack()
    {
        //Slice = new Attack();
        //Slice.attackName = "Slice";
        //Slice.attackDamage = 1;
        //Slice.staminaCost = 1;

        //I'd have to write all that out every time I wanted a new attack using this method.
        //Also feels like it's a lot to remember because I'd have to remember I needed to do all of this.
    }

    //Using this method, I can do things once. 
    public Attack(string attackName, string animationName, int attackDamage, int staminaCost, int colorCost, int attackAccuracy, int numOfAttacks, float animTimeLength, Hue attackColor, AttackType attackType ,WeaponType weaponType, AttackBehavior attackBehavior)
    {
        this.attackName = attackName;
        this.animationName = animationName;
        this.attackDamage = attackDamage;
        this.staminaCost = staminaCost;
        this.colorCost = colorCost;
        this.attackAccuracy = attackAccuracy;
        this.numOfAttacks = numOfAttacks;
        this.animTimeLength = animTimeLength;
        this.attackColor = attackColor;
        this.attackType = attackType;
        this.weaponType = weaponType;  
        this.attackBehavior = attackBehavior;

            
        //Creating an attack in this way would look like
        //Slice = new Attack("Slice", 2, 2);
        //I think this is the way I'll remember most of the time because this is how I orginally
        //learned to initialize an object in a class
    }

    public void AttackFunction(Unit defender)
    {
        
        
    }

    public void AttackStatusBehavior(Unit attacker, Unit defender)
    {
        switch (attackBehavior)
        {
            case AttackBehavior.Burn:
                if(defender.burnAmount < 100)
                {
                    defender.burnAmount += 45;
                    if (defender.burnAmount >= 100)
                    {
                        defender.isBurning = true;
                    }
                }
                break;
            case AttackBehavior.Stun:
                if (defender.stunAmount < 100)
                {
                    defender.stunAmount += 40;
                    if (defender.stunAmount >= 100)
                    {
                        defender.isStunned = true;
                    }
                }
                break;
            case AttackBehavior.Vamp:
                attacker.isVampped = true;
                break;
        } 
    }
}

//TODO: Decide what/if attacks should have special functionality 
//TODO: and what should they do?
//TODO: Figure out how to make an attack have its own functionality
//TODO: IE: How to make fireball burn on its own
