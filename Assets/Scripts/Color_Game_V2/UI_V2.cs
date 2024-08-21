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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        _redBar.fillAmount = (float)(currentRedAmount / maxRedAmount);
        _redBarText.text = "Red: " + currentRedAmount.ToString() + " / " + maxRedAmount.ToString(); 
        _orangeBar.fillAmount = (float)(currentOrangeAmount / maxOrangeAmount);
        _orangeBarText.text = "Orange: " + currentOrangeAmount.ToString() + " / " + maxOrangeAmount.ToString();
        _yellowBar.fillAmount = (float)(currentYellowAmount / maxYellowAmount);
        _yellowBarText.text = "Yellow: " + currentYellowAmount.ToString() + " / " + maxYellowAmount.ToString();
        _greenBar.fillAmount = (float)(currentGreenAmount / maxGreenAmount);
        _greenBarText.text = "Green: " + currentGreenAmount.ToString() + " / " + maxGreenAmount.ToString();
        _blueBar.fillAmount = (float)(currentBlueAmount / maxBlueAmount);
        _blueBarText.text = "Blue: " + currentBlueAmount.ToString() + " / " + maxBlueAmount.ToString();
        _violetBar.fillAmount = (float)(currentVioletAmount / maxVioletAmount);
        _violetBarText.text = "Violet: " + currentVioletAmount.ToString() + " / " + maxVioletAmount.ToString();


    }
}
