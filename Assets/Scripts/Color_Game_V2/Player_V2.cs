using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_V2 : Unit_V2
{
    private int currentLevel = 0;

    private int currentExp = 0;

    private int expNeededToLevel = 50;

    private Weapon equippedWeapon = null;

    private List<Item> inventory = new List<Item>();

    //Weapon Mastery
    private int axeMastery = 0;
    private int bowMastery = 0;
    private int hammerMastery = 0;
    private int spellBookMastery = 0;
    private int staffMastery = 0;
    private int swordMastery = 0;

    //Color Mastery
    private int redMastery = 0;
    private int orangeMastery = 0;
    private int yellowMastery = 0;
    private int greenMastery = 0;
    private int blueMastery = 0;
    private int violetMastery = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
