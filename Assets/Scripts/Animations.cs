using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    private GameObject clone;
    
    public Animator bubble;
    public Animator vertical_Slash;
    public GameObject vert_Slash;
    public GameObject yellow_Splash;
    public GameObject fireball;
    public GameObject orange_Spike;
    public GameObject violetBall;
    public GameObject blueCrush;
    public GameObject greenPunch;
    public GameObject redSlash;

    private Unit_Spawner unitSpawnerScript;
    private AttacksDatabase attacksScript;

    // Start is called before the first frame update
    void Start()
    {
        unitSpawnerScript = FindObjectOfType<Unit_Spawner>();
        attacksScript = FindObjectOfType<AttacksDatabase>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PlayAnimation(Attack attack, Unit defender)
    {
        //Instantiates a clone of the GameObject with the desired animation
        //Based off the name of the attack
        //Destroys the clone when the animation is done.

        switch (attack.attackName)
        {
            case "Fireball":
                clone = Instantiate(fireball, defender.transform.position, Quaternion.identity);
                Destroy(clone, attacksScript._fireBall.animTimeLength);
                break;
            case "Yellow Splash":
                //bubble.Play("Base Layer.Bubble");
                clone = Instantiate(yellow_Splash, defender.transform.position, Quaternion.identity);
                Destroy(clone, attacksScript._yellowSplash.animTimeLength);
                break;
            case "Orange Spike":
                clone = Instantiate(orange_Spike, defender.transform.position, Quaternion.identity);
                Destroy(clone, attacksScript._orangeSpike.animTimeLength);
                break;
            case "Green Punch":
                clone = Instantiate(greenPunch, defender.transform.position, Quaternion.identity);
                Destroy(clone, attacksScript._greenPunch.animTimeLength);
                break;
            case "Blue Crush":
                clone = Instantiate(blueCrush, defender.transform.position, Quaternion.identity);
                Destroy(clone, attacksScript._blueCrush.animTimeLength);
                break;
            case "Violet Ball":
                clone = Instantiate(violetBall, defender.transform.position, Quaternion.identity);
                Destroy(clone, attacksScript._violetBall.animTimeLength);
                break;
            case "Chop":
                clone = Instantiate(vert_Slash, defender.transform.position, Quaternion.identity);
                Destroy(clone, attacksScript._chop.animTimeLength);
                break;
            case "Red's Slash":
                clone = Instantiate(redSlash, defender.transform.position, Quaternion.identity);
                Destroy(clone, attacksScript._redSlash.animTimeLength);
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
 *Slash
 *Violet Ball
 *QuickShot
 *Slam
 */