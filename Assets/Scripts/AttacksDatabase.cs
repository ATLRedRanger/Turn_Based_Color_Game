using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class AttacksDatabase : MonoBehaviour
{
    public Attack _punch;

    public Attack _kick;

    //Axe
    public Attack _chop;
    //Staff
    public Attack _violetBall;
    //Sword
    public Attack _slash;
    //Hammer
    public Attack _slam;
    //Bow
    public Attack _quickShot;

    //Magic Offensive
    public Attack _fireBall;
    public Attack _greenPunch;
    public Attack _orangeSpike;
    public Attack _yellowSplash;
    public Attack _blueCrush;


    //Magic Defensive
    

    // Start is called before the first frame update
    void Start()
    {
        //I can make attacks by using the name of the attack, then making a new attack with the parameters included
        _punch = new Attack("Punch", "Punch", 1, 1, 0, 100, Hue.Neutral, AttackType.Physical, WeaponType.Neutral);

        _fireBall = new Attack("Fireball", "Red_Slash", 5, 12, 5, 75, Hue.Red, AttackType.Special, WeaponType.Neutral);

        _greenPunch = new Attack("Green Punch", "Green_Punch", 5, 7, 7, 75, Hue.Green, AttackType.Physical, WeaponType.Neutral);

        _orangeSpike = new Attack("Orange Spike", "Orange_Spike", 5, 6, 4, 75, Hue.Orange, AttackType.Special, WeaponType.Neutral);

        _blueCrush = new Attack("Blue Crush", "Blue_Crush", 3, 3, 8, 50, Hue.Blue, AttackType.Special, WeaponType.Neutral);

        _yellowSplash = new Attack("Yellow Splash", "Yellow_Splash", 6, 12, 4, 85, Hue.Yellow, AttackType.Special, WeaponType.Neutral);

        //I can make private weapons in this way using the createAttack function
        _kick = createAttack("Kick", "Kick", 3, 3, 0, 100, Hue.Neutral, AttackType.Physical, WeaponType.Neutral);



        //Axe 
        _chop = new Attack("Chop", "Vertical_Slash", 2, 5, 0, 95, Hue.Neutral, AttackType.Physical, WeaponType.Axe);

        //Staff
        _violetBall = new Attack("Violet Ball", "Violet_Ball", 2, 5, 5, 85, Hue.Violet, AttackType.Special, WeaponType.Staff);

        //Sword
        _slash = new Attack("Slash", "Slash", 2, 4, 0, 95, Hue.Neutral, AttackType.Physical, WeaponType.Sword);

        //Hammer
        _slam = new Attack("Slam", "Slam", 2, 4, 0, 95, Hue.Neutral, AttackType.Physical, WeaponType.Hammer);

        //Bow
        _quickShot = new Attack("Quick Shot", "Quick_Shot", 2, 4, 0, 90, Hue.Neutral, AttackType.Physical, WeaponType.Bow);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //This is another way of creating an item. I can make new items in this script using this function that will 
    //return a new attack.
    private Attack createAttack(string attackName, string animationName, int attackDamage, int staminaCost, int colorCost, int attackAccuracy, Hue attackColor, AttackType attackType, WeaponType weaponType)
    {
        var attack = new Attack(attackName, animationName, attackDamage, staminaCost, colorCost, attackAccuracy, attackColor, attackType, weaponType);

        return attack;
    }

    

}
