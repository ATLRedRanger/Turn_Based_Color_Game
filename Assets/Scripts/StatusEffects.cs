using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StatusEffects : MonoBehaviour
{
    //What is a delegate?
    public delegate void Burned();
    //What is an event?
    public static event Burned isBurned;

    
    public void BurningCondition()
    {
        
        isBurned();
        
    }

}
