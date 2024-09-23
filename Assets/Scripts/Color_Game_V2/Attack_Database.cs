using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Database : MonoBehaviour
{
    //Neutral Attacks
    public Attack _basicAttack;

    //Red Attacks
    public Attack _fireball;

    //Orange Attacks

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
        //string attackName, int attackPower, int attackAccuracy, int colorCost, int staminaCost, int numOfHits, AttackType attackType, Hue attackColor, AttackBehavior attackBehavior)
        _fireball = CreateAttack("Fireball", 1, 100, 1, 1, 1, AttackType.Special, Hue.Red, AttackBehavior.Burn);
        _basicAttack = CreateAttack("Attack", 1, 100, 0, 1, 1, AttackType.Physical, Hue.Neutral, AttackBehavior.None);
        _basicAxeAttack = CreateAttack("Attack", 2, 90, 0, 1, 1, AttackType.Physical, Hue.Neutral, AttackBehavior.None);
        _basicBowAttack = CreateAttack("Attack", 2, 90, 0, 1, 1, AttackType.Physical, Hue.Neutral, AttackBehavior.None);
        _basicHammerAttack = CreateAttack("Attack", 2, 90, 0, 1, 1, AttackType.Physical, Hue.Neutral, AttackBehavior.None);
        _basicSpellbookAttack = CreateAttack("Attack", 2, 90, 0, 1, 1, AttackType.Special, Hue.Neutral, AttackBehavior.None);
        _basicStaffAttack = CreateAttack("Attack", 2, 90, 0, 1, 1, AttackType.Special, Hue.Neutral, AttackBehavior.None);
        _basicSwordAttack = CreateAttack("Attack", 2, 90, 0, 1, 1, AttackType.Physical, Hue.Neutral, AttackBehavior.None);




        //Adding statuses to attacks
        _fireball.SetAttackDebuff(_statusEffectsDatabase.deBuffington);
        
    }

    private Attack CreateAttack(string attackName, int attackPower, int attackAccuracy, int colorCost, int staminaCost, int numOfHits,
                    AttackType attackType, Hue attackColor, AttackBehavior attackBehavior)
    {
        var attack = new Attack(attackName, attackPower, attackAccuracy, colorCost, staminaCost, numOfHits, attackType, attackColor, attackBehavior);

        return attack;
    }
}
