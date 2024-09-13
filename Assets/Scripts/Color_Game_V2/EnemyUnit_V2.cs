using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit_V2 : Unit_V2
{

    private Hue sensitiveColor = Hue.Green;
    private Hue tolerantColor = Hue.Blue;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnitColorBehavior(Dictionary<Hue, int> envColors)
    {
        if (envColors[tolerantColor] > envColors[sensitiveColor])
        {
            //SetSpeedTier(1);
        }
    }

}
