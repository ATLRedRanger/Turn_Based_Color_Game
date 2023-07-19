using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Attack
{
    public string attackName;

    public int attackDamage;

    public int staminaCost;

    public int attackAccuracy;

    public int colorCost;

    
    

    public Color attackColor;
  
    //These are different ways to *initialize* the attack


    //Using this method, I'd have to make the new attack then declare all of the variables. 
    //Seems way more time consuming because I'd have to do it over for each new attack 
    //that I wanted to create.
    public Attack()
    {
        //Slice = new Attack();
        //Slice.attackName = "Slice";
        //Slice.attackDamage = 1;
        //Slice.staminaCost = 1;

        //I'd have to write all that out every time I wanted a new attack using this method.
        //Also feels like it's a lot to remember because I'd have to remember I needed to do all of this.
    }

    //Using this method, I can do things once. 
    public Attack(string attackName, int attackDamage, int staminaCost, int colorCost, int attackAccuracy, Color attackColor)
    {
        this.attackName = attackName;
        this.attackDamage = attackDamage;
        this.staminaCost = staminaCost;
        this.colorCost = colorCost;
        this.attackAccuracy = attackAccuracy;
        this.attackColor = attackColor;
        

            
        //Creating an attack in this way would look like
        //Slice = new Attack("Slice", 2, 2);
        //I think this is the way I'll remember most of the time because this is how I orginally
        //learned to initialize an object in a class
    }



}