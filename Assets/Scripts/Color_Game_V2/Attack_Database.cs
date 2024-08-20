using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Database : MonoBehaviour
{
    //Neutral Attacks

    //Red Attacks
    public Attack _fireball;

    //Orange Attacks

    //Yellow Attacks

    //Green Attacks

    //Blue Attacks

    //Violet Attacks

    //Axe

    //Bow

    //Hammer

    //Spellbook

    //Sword

    //Wand




    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        _fireball = CreateAttack("Fireball", 10, 75, 10, 10, 1, AttackType.Physical, Hue.Red, AttackBehavior.Burn);        
    }

    private Attack CreateAttack(string attackName, int attackPower, int attackAccuracy, int colorCost, int staminaCost, int numOfHits,
                    AttackType attackType, Hue attackColor, AttackBehavior attackBehavior)
    {
        var attack = new Attack(attackName, attackPower, attackAccuracy, colorCost, staminaCost, numOfHits, attackType, attackColor, attackBehavior);

        return attack;
    }
}
