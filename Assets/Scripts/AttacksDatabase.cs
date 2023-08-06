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
    //Magic Defensive
    

    // Start is called before the first frame update
    void Start()
    {
        //I can make attacks by using the name of the attack, then making a new attack with the parameters included
        _punch = new Attack("Punch", 1, 1, 0, 100, Color.Neutral, WeaponType.Neutral);

        _fireBall = new Attack("Fireball", 5, 12, 4, 95, Color.Red, WeaponType.Neutral) ;


        //I can make private weapons in this way using the createAttack function
        _kick = createAttack("Kick", 3, 3, 0, 100, Color.Neutral, WeaponType.Neutral);



        //Axe 
        _chop = createAttack("Chop", 2, 5, 0, 95, Color.Neutral, WeaponType.Axe);

        //Staff
        _violetBall = new Attack("Violet Ball", 2, 5, 5, 85, Color.Violet, WeaponType.Staff);

        //Sword
        _slash = new Attack("Slash", 2, 4, 0, 95, Color.Neutral, WeaponType.Sword);

        //Hammer
        _slam = new Attack("Slam", 2, 4, 0, 95, Color.Neutral, WeaponType.Hammer);

        //Bow
        _quickShot = new Attack("Quick Shot", 2, 4, 0, 90, Color.Neutral, WeaponType.Bow);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //This is another way of creating an item. I can make new items in this script using this function that will 
    //return a new attack.
    private Attack createAttack(string attackName, int attackDamage, int staminaCost, int colorCost, int attackAccuracy, Color attackColor, WeaponType attackType)
    {
        var attack = new Attack(attackName, attackDamage, staminaCost, colorCost, attackAccuracy, attackColor, attackType);

        return attack;
    }

    

}
