
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit_V2 : Unit_V2
{
    [SerializeField]
    private Hue sensitiveColor = Hue.Red;
    [SerializeField]
    private Hue tolerantColor = Hue.Blue;
    private int baseAttackBonusModifier = 0;
    private int armorClassBonusModifier = 0;
    private int dcBonousModifier = 0;
    [SerializeField]
    private int postiveBonus = 0;
    [SerializeField]
    private int negativeBonus = 0;
    private List<Attack> attackList = new List<Attack>();
    private Item commonDrop = null;
    private Item uncommonDrop = null;
    private Item rareDrop = null;
    private Item superRareDrop = null;
    [SerializeField]
    private int expDropped;
    [SerializeField]
    private int moneyDropped;
    public Animator unitAnimator;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        unitAnimator = GetComponent<Animator>();
        //AddAttackToDictionary(attackDatabaseScript._fireball);
        AddAttackToDictionary(attackDatabaseScript._basicSlimeAttack);
        AddAttackToDictionary(attackDatabaseScript._basicAttack);
        rareDrop = itemDatabaseScript.burnHeal;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public virtual void UnitColorBehavior(Dictionary<Hue, int> envColors)
    {
        //Debug.Log("PINEAPPLE");
        if(sensitiveColor != Hue.Neutral && tolerantColor != Hue.Neutral)
        {
            Debug.Log($"Pos Color: {envColors[tolerantColor]} vs Neg Color: {envColors[sensitiveColor]}");
            if (envColors[tolerantColor] > envColors[sensitiveColor])
            {
                //Debug.Log("KIWI");
                AddAttackToDictionary(attackDatabaseScript._fireball);
                unitAnimator.SetBool("isRaging", true);
            }
            else
            {
                unitAnimator.SetBool("isRaging", false);
            }

            if (unitAnimator.GetBool("isRaging") == false && unitAttackDictionary.ContainsKey("Fireball"))
            {
                unitAttackDictionary.Remove("Fireball");
            }
        }
        
    }

    private bool IsEnemyAttackUseable(Attack attack, Environment env)
    {
        if (attack.attackColor == Hue.Neutral && this.GetCurrentStamina() >= attack.staminaCost) 
        {
            return true;
        }
        else if (this.GetCurrentStamina() >= attack.staminaCost && env.GetCurrentColorDictionary()[attack.attackColor] >= attack.colorCost)
        {
            return true;
        }

        return false;
    }

    public Attack EnemyAttackDecision(Environment env)
    {
       
        foreach(var kvp in unitAttackDictionary)
        {
            attackList.Add(kvp.Value);
        }

        foreach (Attack attack in attackList)
        {
            if (IsEnemyAttackUseable(attack, env))
            {
                return attack;
            }
        }

        return null;
    }

    public Item DroppedItem()
    {
        int roll = Random.Range(1, 101);

        Debug.Log($"DroppedItem() Roll: {roll}");

        if(roll == 1)
        {
            return superRareDrop;
        }
        else if(roll <= 11)
        {
            return rareDrop;
        }
        else if(roll <= 30)
        {
            return uncommonDrop;
        }
        else
        {
            return commonDrop;
        }   
    }

    public int GetExpDropped()
    {
        Debug.Log($"ENEMY HAS {expDropped} TO GIVE!");
        return expDropped;
    }

    public int GetMoneyDropped()
    {
        return moneyDropped;
    }

    public Hue GetSensitiveColor()
    {
        return sensitiveColor;
    }

    public Hue GetTolerantColor()
    {
        return tolerantColor;
    }

    public int GetACBonusModifier()
    {
        return armorClassBonusModifier;
    }

    public int GetBABBonusModifier()
    {
        return baseAttackBonusModifier;
    }

    public int GetDCBonusModifier()
    {
        return dcBonousModifier;
    }
    public override int GetCombatBAB()
    {
        return GetBAB() + GetBABBonusModifier();
    }

    public override int GetCombatAC()
    {
        return base.GetCombatAC() + GetACBonusModifier();

    }

    public override int GetCombatDC()
    {
        return base.GetCombatDC() + GetDCBonusModifier();
    }
}
