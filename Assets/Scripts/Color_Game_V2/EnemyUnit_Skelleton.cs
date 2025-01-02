using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit_Skelleton : EnemyUnit_V2
{
    
    private List<Attack> attackList = new List<Attack>();
    private Item commonDrop = null;
    private Item uncommonDrop = null;
    private Item rareDrop = null;
    private Item superRareDrop = null;
    

    // Start is called before the first frame update
    public override void Start()
    {
        AddAttackToDictionary(attackDatabaseScript._fireball);
        commonDrop = itemDatabaseScript.healthPotion;
        uncommonDrop = itemDatabaseScript.basicBow;
        rareDrop = itemDatabaseScript.basicAxe;
        superRareDrop = itemDatabaseScript.redSpellbook;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        
    }

    public override void UnitColorBehavior(Dictionary<Hue, int> envColors)
    {
        if (GetSensitiveColor() != Hue.Neutral && GetTolerantColor() != Hue.Neutral)
        {
            if (envColors[GetTolerantColor()] > envColors[GetSensitiveColor()])
            {
                unitSpriteRenderer = differentSprite;
            }
        }

    }
}

//TESTING TESTING TESTING TESTING