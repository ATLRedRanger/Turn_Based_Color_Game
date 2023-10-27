using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    public Animator redSlash;
    public Animator bubble;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAnimation(Attack attack)
    {
        switch (attack.attackName)
        {
            case "Fireball":
                redSlash.Play("Base Layer."+attack.animationName); 
                break;
            case "Yellow Splash":
                bubble.Play("Base Layer.Bubble");
                break;
        }
    }
}

//TODO: Make animations for the attacks.
//TODO: Decide what the colors will do/look like and get better names. 
//I don't think I like the idea of "Green Punch" or "Orange Spike"
//Attacks needing animations:
/*
 * Fireball - 
 *Blue Crush - 
 *Green Punch
 *Orange Spike
 *Yellow Splash
 *Chop
 *Slash
 *Violet Ball
 *QuickShot
 *Slam
 */