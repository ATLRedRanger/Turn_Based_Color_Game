using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    private GameObject clone;
    public Animator redSlash;
    public Animator bubble;
    public Animator vertical_Slash;
    public GameObject vert_Slash;
    public GameObject yellow_Splash;

    private Unit_Spawner unitSpawnerScript;

    // Start is called before the first frame update
    void Start()
    {
        unitSpawnerScript = FindObjectOfType<Unit_Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PlayAnimation(Attack attack)
    {
        //Instantiates a clone of the GameObject with the desired animation
        //Based off the name of the attack
        //Destroys the clone when the animation is done. 
        switch (attack.attackName)
        {
            case "Fireball":
                redSlash.Play("Base Layer."+attack.animationName); 
                
                break;
            case "Yellow Splash":
                //bubble.Play("Base Layer.Bubble");
                clone = Instantiate(yellow_Splash, unitSpawnerScript.enemyOne.transform.position, Quaternion.identity);
                Destroy(clone, 2);
                break;
            case "Chop":
                clone = Instantiate(vert_Slash, unitSpawnerScript.enemyOne.transform.position, Quaternion.identity);
                Destroy(clone, 1);
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