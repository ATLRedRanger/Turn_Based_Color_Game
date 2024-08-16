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
    public Attack _redSlash;


    //Magic Defensive
    void Awake()
    {
        //I can make attacks by using the name of the attack, then making a new attack with the parameters included
        _punch = new Attack("Punch", "Punch", 2, 5, 0, 100, 1, 2, Hue.Neutral, AttackType.Physical, WeaponType.Neutral, AttackBehavior.None);

        _fireBall = new Attack("Fireball", "Nothing", 1, 1, 1, 75, 1, 2, Hue.Red, AttackType.Special, WeaponType.Neutral, AttackBehavior.Burn);
        
        _greenPunch = new Attack("Green Punch", "Green_Punch", 5, 20, 7, 75, 1, 1.3f, Hue.Green, AttackType.Special, WeaponType.Neutral, AttackBehavior.Vamp);

        _orangeSpike = new Attack("Orange Spike", "Orange_Spike", 5, 20, 4, 75, 1, 2, Hue.Orange, AttackType.Special, WeaponType.Neutral, AttackBehavior.None);

        _blueCrush = new Attack("Blue Crush", "Blue_Crush", 1, 20, 1, 75, 1, 2, Hue.Blue, AttackType.Special, WeaponType.Neutral, AttackBehavior.Stun);

        _yellowSplash = new Attack("Yellow Splash", "Yellow_Splash", 6, 20, 4, 75, 1, 2, Hue.Yellow, AttackType.Special, WeaponType.Neutral, AttackBehavior.None);

        _redSlash = new Attack("Red's Slash", "Reds_Slash", 20, 75, 1, 50, 1, 2, Hue.Red, AttackType.Special, WeaponType.Neutral, AttackBehavior.Burn);

        //I can make private weapons in this way using the createAttack function
        _kick = createAttack("Kick", "Kick", 3, 3, 0, 100, 1, 2, Hue.Neutral, AttackType.Physical, WeaponType.Neutral, AttackBehavior.None);



        //Axe 
        _chop = createAttack("Chop", "Vertical_Slash", 2, 5, 0, 95, 1, 1, Hue.Neutral, AttackType.Physical, WeaponType.Axe, AttackBehavior.None);

        //Staff
        _violetBall = new Attack("Violet Ball", "Violet_Ball", 2, 50, 5, 85, 1, 5, Hue.Violet, AttackType.Special, WeaponType.Staff, AttackBehavior.None);

        //Sword
        _slash = new Attack("Slash", "Slash", 2, 4, 0, 95, 1, 2, Hue.Neutral, AttackType.Physical, WeaponType.Sword, AttackBehavior.None);

        //Hammer
        _slam = new Attack("Slam", "Slam", 7, 4, 0, 95, 1, 2, Hue.Neutral, AttackType.Physical, WeaponType.Hammer, AttackBehavior.None);

        //Bow
        _quickShot = new Attack("Quick Shot", "Quick_Shot", 1, 15, 0, 90, 2, 2, Hue.Neutral, AttackType.Physical, WeaponType.Bow, AttackBehavior.None);
    }

    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //This is another way of creating an item. I can make new items in this script using this function that will 
    //return a new attack.
    private Attack createAttack(string attackName, string animationName, int attackDamage, int staminaCost, int colorCost, int attackAccuracy, int numOfAttacks, float animTimeLength, Hue attackColor, AttackType attackType, WeaponType weaponType, AttackBehavior attackBehavior)
    {
        var attack = new Attack(attackName, animationName, attackDamage, staminaCost, colorCost, attackAccuracy, numOfAttacks, animTimeLength, attackColor, attackType, weaponType, attackBehavior);

        return attack;
    }

    

}
