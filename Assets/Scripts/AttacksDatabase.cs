using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttacksDatabase : MonoBehaviour
{
    public Attack _punch;

    public Attack _kick;

    public Attack _chop;

    public Attack _fireBall;

    // Start is called before the first frame update
    void Start()
    {
        //I can make attacks by using the name of the attack, then making a new attack with the parameters included
        _punch = new Attack("Punch", 1, 1, 0, 100, Color.Neutral);

        _fireBall = new Attack("Fireball", 5, 12, 4, 95, Color.Red);


        //I can make private weapons in this way using the createAttack function
        _kick = createAttack("Kick", 1, 1, 0, 100, Color.Neutral);

        _chop = createAttack("Chop", 2, 2, 0, 95, Color.Neutral);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //This is another way of creating an item. I can make new items in this script using this function that will 
    //return a new attack.
    private Attack createAttack(string attackName, int attackDamage, int staminaCost, int colorCost, int attackAccuracy, Color attackColor)
    {
        var attack = new Attack(attackName, attackDamage, staminaCost, colorCost, attackAccuracy, attackColor);

        return attack;
    }

    

}
