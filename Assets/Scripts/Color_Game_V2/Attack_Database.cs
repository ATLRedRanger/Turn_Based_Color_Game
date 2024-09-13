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


    private StatusEffectsDatabase_V2 _statusEffectsDatabase;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void Awake()
    {
        _statusEffectsDatabase = FindObjectOfType<StatusEffectsDatabase_V2>();
        //string attackName, int attackPower, int attackAccuracy, int colorCost, int staminaCost, int numOfHits, AttackType attackType, Hue attackColor, AttackBehavior attackBehavior)
        _fireball = CreateAttack("Fireball", 1, 100, 1, 1, 1, AttackType.Physical, Hue.Red, AttackBehavior.Burn);
        _fireball.SetAttackDebuff(_statusEffectsDatabase.deBuffington);
    }

    private Attack CreateAttack(string attackName, int attackPower, int attackAccuracy, int colorCost, int staminaCost, int numOfHits,
                    AttackType attackType, Hue attackColor, AttackBehavior attackBehavior)
    {
        var attack = new Attack(attackName, attackPower, attackAccuracy, colorCost, staminaCost, numOfHits, attackType, attackColor, attackBehavior);

        return attack;
    }
}
