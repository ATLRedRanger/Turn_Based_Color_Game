using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_V2 : MonoBehaviour
{
    private int currentRedAmount;
    private int maxRedAmount;
    private int currentOrangeAmount;
    private int maxOrangeAmount;
    private int currentYellowAmount;
    private int maxYellowAmount;
    private int currentGreenAmount;
    private int maxGreenAmount;
    private int currentBlueAmount;
    private int maxBlueAmount;
    private int currentVioletAmount;
    private int maxVioletAmount;

    //Player
    public Text playerName;
    public Text playerHealthText;
    public Text playerStaminaText;
    public Image playerHealthBar;
    public Image playerStaminaBar;
    public Text playerDamageNumberText;

    //Enemy One
    public Text enemyOneName;
    public Text enemyOneHealthText;
    public Text enemyOneStaminaText;
    public Image enemyOneHealthBar;
    public Image enemyOneStaminaBar;
    public Text enemyOneDamageNumberText;


    //Enemy Two
    public Text enemyTwoName;
    public Text enemyTwoHealthText;
    public Text enemyTwoStaminaText;
    public Image enemyTwoHealthBar;
    public Image enemyTwoStaminaBar;
    public Text enemyTwoDamageNumberText;


    //Environment Bars
    public Image _redBar;
    public Text _redBarText;
    public Image _orangeBar;
    public Text _orangeBarText;
    public Image _yellowBar;
    public Text _yellowBarText;
    public Image _greenBar;
    public Text _greenBarText;
    public Image _blueBar;
    public Text _blueBarText;
    public Image _violetBar;
    public Text _violetBarText;

    //Attack Description Panel
    public Text _attackDescriptionText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerHealthAndStamina(Unit_V2 player)
    {
        playerName.text = player.unitName;
        playerHealthText.text = player.GetCurrentHp().ToString() + " / " + player.GetMaxHp().ToString();
        playerStaminaText.text = player.GetCurrentStamina().ToString() + " / " + player.GetMaxStamina().ToString();
        playerHealthBar.fillAmount = (float)(float)(player.GetCurrentHp() / (float)(player.GetMaxHp()));
        playerStaminaBar.fillAmount = (float)(float)(player.GetCurrentStamina() / (float)(player.GetMaxStamina()));
    }

    public void SetEnemeyOneHealthAndStamina(Unit_V2 enemyOne)
    {
        if(enemyOne == null)
        {

        }
        else
        {
            enemyOneName.text = enemyOne.unitName;
            enemyOneHealthText.text = enemyOne.GetCurrentHp().ToString() + " / " + enemyOne.GetMaxHp().ToString();
            enemyOneStaminaText.text = enemyOne.GetCurrentStamina().ToString() + " / " + enemyOne.GetMaxStamina().ToString();
            enemyOneHealthBar.fillAmount = (float)(float)(enemyOne.GetCurrentHp() / (float)(enemyOne.GetMaxHp()));
            enemyOneStaminaBar.fillAmount = (float)(float)(enemyOne.GetCurrentStamina() / (float)(enemyOne.GetMaxStamina()));
        }
        
    }

    public void SetEnemeyTwoHealthAndStamina(Unit_V2 enemyTwo)
    {
        if (enemyTwo == null)
        {

        }
        else
        {
            enemyTwoName.text = enemyTwo.unitName;
            enemyTwoHealthText.text = enemyTwo.GetCurrentHp().ToString() + " / " + enemyTwo.GetMaxHp().ToString();
            enemyTwoStaminaText.text = enemyTwo.GetCurrentStamina().ToString() + " / " + enemyTwo.GetMaxStamina().ToString();
            enemyTwoHealthBar.fillAmount = (float)(float)(enemyTwo.GetCurrentHp() / (float)(enemyTwo.GetMaxHp()));
            enemyTwoStaminaBar.fillAmount = (float)(float)(enemyTwo.GetCurrentStamina() / (float)(enemyTwo.GetMaxStamina()));
        }

    }

    public void SetMaxColorAmounts(int maxRed, int maxOrange, int maxYellow, int maxGreen, int maxBlue, int maxViolet)
    {
        maxRedAmount = maxRed;
        maxOrangeAmount = maxOrange;
        maxYellowAmount = maxYellow;
        maxGreenAmount = maxGreen;
        maxBlueAmount = maxBlue;
        maxVioletAmount = maxViolet;
    }

    public void UpdateEnvironmentColors(int currentRed, int currentOrange, int currentYellow, int currentGreen, int currentBlue, int currentViolet)
    {
        
        currentRedAmount = currentRed;
        currentOrangeAmount = currentOrange;
        currentYellowAmount = currentYellow;
        currentGreenAmount = currentGreen;
        currentBlueAmount = currentBlue;
        currentVioletAmount = currentViolet;

        UpdateEnvironmentBars();
    }

    public void UpdateEnvironmentBars()
    {
        Debug.Log($"CurrentRed: {currentRedAmount}");
        _redBar.fillAmount = (float)((float)currentRedAmount / (float)maxRedAmount);
        _redBarText.text = "Red: " + currentRedAmount.ToString() + " / " + maxRedAmount.ToString(); 
        _orangeBar.fillAmount = (float)(float)(currentOrangeAmount / (float)maxOrangeAmount);
        _orangeBarText.text = "Orange: " + currentOrangeAmount.ToString() + " / " + maxOrangeAmount.ToString();
        _yellowBar.fillAmount = (float)(float)(currentYellowAmount / (float)maxYellowAmount);
        _yellowBarText.text = "Yellow: " + currentYellowAmount.ToString() + " / " + maxYellowAmount.ToString();
        _greenBar.fillAmount = (float)(float)(currentGreenAmount / (float)maxGreenAmount);
        _greenBarText.text = "Green: " + currentGreenAmount.ToString() + " / " + maxGreenAmount.ToString();
        _blueBar.fillAmount = (float)(float)(currentBlueAmount / (float)maxBlueAmount);
        _blueBarText.text = "Blue: " + currentBlueAmount.ToString() + " / " + maxBlueAmount.ToString();
        _violetBar.fillAmount = (float)(float)(currentVioletAmount / (float)maxVioletAmount);
        _violetBarText.text = "Violet: " + currentVioletAmount.ToString() + " / " + maxVioletAmount.ToString();


    }

    public void SetAttackDescriptionText(Attack attack, Unit_V2 attacker, Unit_V2 defender, string source)
    {
        _attackDescriptionText.text = "";

        string description = " " + attacker.unitName + " has dealt " + source + " damage to " + defender.unitName + ".";
        
        _attackDescriptionText.text = description;
    }

    public void SetStatusDescriptionText(Unit_V2 subject, int damage, string source)
    {
        _attackDescriptionText.text = "";

        string description = " " + subject.unitName + " takes " + damage + " from " + source + ".";

        _attackDescriptionText.text = description;
    }

    public void DisplayDamageNumber()
    {

    }
}
