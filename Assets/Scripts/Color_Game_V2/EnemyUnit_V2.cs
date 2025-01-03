
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class EnemyUnit_V2 : Unit_V2
{
    public SpriteRenderer sprite;
    [SerializeField]
    private Hue tolerantColor = Hue.Blue;
    [SerializeField]
    private Hue sensitiveColor = Hue.Red;
    
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
        //unitAnimator = GetComponent<Animator>();
        AddAttackToDictionary(attackDatabaseScript._basicSlimeAttack);
        AddAttackToDictionary(attackDatabaseScript._basicAttack);
        rareDrop = itemDatabaseScript.burnHeal;

        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public virtual void UnitColorBehavior(Dictionary<Hue, int> envColors)
    {
        //Debug.Log("PINEAPPLE");
        //if(sensitiveColor != Hue.Neutral && tolerantColor != Hue.Neutral)
        //{
        //    Debug.Log($"Pos Color: {envColors[tolerantColor]} vs Neg Color: {envColors[sensitiveColor]}");
        //    if (envColors[tolerantColor] > envColors[sensitiveColor])
        //    {
        //        //Debug.Log("KIWI");
        //        AddAttackToDictionary(attackDatabaseScript._fireball);
        //        unitAnimator.SetBool("isRaging", true);
        //    }
        //    else
        //    {
        //        unitAnimator.SetBool("isRaging", false);
        //    }

        //    if (unitAnimator.GetBool("isRaging") == false && unitAttackDictionary.ContainsKey("Fireball"))
        //    {
        //        unitAttackDictionary.Remove("Fireball");
        //    }
        //}
        if (GetSensitiveColor() != Hue.Neutral && GetTolerantColor() != Hue.Neutral)
        {
            if (envColors[GetTolerantColor()] > envColors[GetSensitiveColor()])
            {
                sprite.color = Color.red;
            }
            else
            {
                sprite.color = Color.white;
            }
        }
    }

    private bool IsEnemyAttackUseable(Attack attack, Environment env)
    {
        
        if (attack.attackColor == Hue.Neutral) 
        {
            return true;
        }
        else if (env.GetCurrentColorDictionary()[attack.attackColor] >= attack.colorCost)
        {
            return true;
        }

        return false;
    }

    public virtual Attack EnemyAttackDecision(Environment env)
    {
        attackList.Clear();

        foreach (var kvp in unitAttackDictionary)
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

    
    public override int GetCombatBAB()
    {
        return GetBAB() + GetBABBonusModifier();
    }

    public override int GetCombatAC()
    {
        return GetAC() + GetACBonusModifier();

    }

    public override int GetCombatDC()
    {
        return GetDC() + GetDCBonusModifier();
    }
}
