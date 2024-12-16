using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Database : MonoBehaviour
{
    //Neutral Attacks
    public Attack _basicAttack;
    public Attack _basicSlimeAttack;

    //Red Attacks
    public Attack _fireball;

    //Orange Attacks
    public Attack _orangeAttackOne;

    //Yellow Attacks

    //Green Attacks

    //Blue Attacks

    //Violet Attacks

    //Axe
    public Attack _basicAxeAttack;

    //Bow
    public Attack _basicBowAttack;

    //Hammer
    public Attack _basicHammerAttack;

    //Spellbook
    public Attack _basicSpellbookAttack;

    //Sword
    public Attack _basicSwordAttack;

    //Wand
    public Attack _basicStaffAttack;


    private StatusEffectsDatabase_V2 _statusEffectsDatabase;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void Awake()
    {
        _statusEffectsDatabase = FindObjectOfType<StatusEffectsDatabase_V2>();
        //string attackName, int attackPower, int attackAccuracy, int attackBonus, int critRoll, int colorCost, int numOfHits, int statusBuildUpAmount, AttackType attackType, Hue attackColor, AttackBehavior attackBehavior, bool isSingleTarget

        //Red
        _fireball = CreateAttack("Fireball", 1, 100, 0, 20, 5, 1, 2, AttackType.Special, Hue.Red, AttackBehavior.Burn, true);

        //Orange
        _orangeAttackOne = CreateAttack("Orange Attack", 2, 20, 0, 20, 6, 2, 4, AttackType.Special, Hue.Orange, AttackBehavior.None, true);

        //Yellow

        //Green

        //Blue

        //Violet

        //Neutral

        _basicAttack = CreateAttack("Attack", 1, 100, 0, 20, 1, 1, 0, AttackType.Physical, Hue.Neutral, AttackBehavior.FutureSight, true);
        _basicAxeAttack = CreateAttack("Attack", 2, 90, 0, 20, 1, 1, 0, AttackType.Physical, Hue.Neutral, AttackBehavior.None, true);
        _basicBowAttack = CreateAttack("Attack", 2, 90, 0, 20, 1, 1, 0, AttackType.Physical, Hue.Neutral, AttackBehavior.None, true);
        _basicHammerAttack = CreateAttack("Attack", 2, 90, 0, 20, 1, 1, 0, AttackType.Physical, Hue.Neutral, AttackBehavior.None, true);
        _basicSpellbookAttack = CreateAttack("Attack", 1, 90, 0, 20, 1, 1, 0, AttackType.Special, Hue.Neutral, AttackBehavior.None, false);
        _basicStaffAttack = CreateAttack("Attack", 2, 90, 0, 20, 1, 1, 0, AttackType.Special, Hue.Neutral, AttackBehavior.None, false);
        _basicSwordAttack = CreateAttack("Attack", 2, 90, 0, 20, 1, 1, 0, AttackType.Physical, Hue.Neutral, AttackBehavior.None, true);


        //EnemySpecific Attacks
        _basicSlimeAttack = CreateAttack("Basic Slime Attack", 1, 100, 0, 20, 1, 1, 0, AttackType.Physical, Hue.Neutral, AttackBehavior.None, true);



        //Adding statuses to attacks
        //_fireball.SetAttackDebuff(_statusEffectsDatabase.deBuffington);
        
    }

    private Attack CreateAttack(string attackName, int attackPower, int attackAccuracy, int attackBonus, int critRoll, int colorCost, int numOfHits,
                    int statusBuildUpAmount, AttackType attackType, Hue attackColor, AttackBehavior attackBehavior, bool isSingleTarget)
    {
        var attack = new Attack(attackName, attackPower, attackAccuracy, attackBonus, critRoll, colorCost, numOfHits, statusBuildUpAmount, attackType, attackColor, attackBehavior, isSingleTarget);

        return attack;
    }
}
