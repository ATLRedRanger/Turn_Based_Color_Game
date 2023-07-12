using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENV_Mana : MonoBehaviour
{
    
    public int currentRed;
    public int maxRed;
    public int currentOrange;
    public int maxOrange;
    public int currentYellow;
    public int maxYellow;
    public int currentGreen;
    public int maxGreen;
    public int currentBlue;
    public int maxBlue;
    public int currentViolet;
    public int maxViolet;
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentRed = maxRed;
        currentOrange = maxOrange;
        currentYellow = maxYellow;
        currentGreen = maxGreen;
        currentBlue = maxBlue;
        currentViolet = maxViolet;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
