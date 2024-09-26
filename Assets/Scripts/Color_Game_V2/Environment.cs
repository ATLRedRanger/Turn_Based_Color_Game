using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public int currentRed;
    public int previousRed;
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


    //Forest
    public List<int> forestReds = new List<int>();
    public List<int> forestOranges = new List<int>();
    public List<int> forestYellows = new List<int>();
    public List<int> forestGreens = new List<int>();
    public List<int> forestBlues = new List<int>();
    public List<int> forestViolets = new List<int>();

    //Cave
    public List<int> caveReds = new List<int>();
    public List<int> caveOranges = new List<int>();
    public List<int> caveYellows = new List<int>();
    public List<int> caveGreens = new List<int>();
    public List<int> caveBlues = new List<int>();
    public List<int> caveViolets = new List<int>();


    //Dictionary inside of a dictionary 
    public Dictionary<string, Dictionary<Hue, LocationMana>> Locations = new Dictionary<string, Dictionary<Hue, LocationMana>>();
    private Dictionary<Hue, int> environmentColorDictionary = new Dictionary<Hue, int>();


    // Start is called before the first frame update
    void Start()
    {


        //StartingLocation();
        //Locations is a Dictionary that has the key of a string and the value of another Dictionary

        //Locations["Forest"] is Dictionary with the key of a Dictionary (Color) and the value is the struct LocationMana
        Locations["Forest"] = new Dictionary<Hue, LocationMana>();

        Locations["Forest"][Hue.Red] = LocationColors(forestReds);
        Locations["Forest"][Hue.Orange] = LocationColors(forestOranges);
        Locations["Forest"][Hue.Yellow] = LocationColors(forestYellows);
        Locations["Forest"][Hue.Green] = LocationColors(forestGreens);
        Locations["Forest"][Hue.Blue] = LocationColors(forestBlues);
        Locations["Forest"][Hue.Violet] = LocationColors(forestViolets);

        Locations["Cave"] = new Dictionary<Hue, LocationMana>();

        Locations["Cave"][Hue.Red] = LocationColors(caveReds);
        Locations["Cave"][Hue.Orange] = LocationColors(caveOranges);
        Locations["Cave"][Hue.Yellow] = LocationColors(caveYellows);
        Locations["Cave"][Hue.Green] = LocationColors(caveGreens);
        Locations["Cave"][Hue.Blue] = LocationColors(caveBlues);
        Locations["Cave"][Hue.Violet] = LocationColors(caveViolets);
        //StartingLocation();

        previousRed = maxRed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private LocationMana LocationColors(List<int> locationValues)
    {
        //This is a struct that takes in a List of values
        //It then rolls a random number between 0 and the number of values in the list
        //It then assigns that value at the List[roll] to the min and max variables
        //If the min is greater than the max, it sets the min to the max
        //Then it returns those values
        int roll;


        roll = Random.Range(0, locationValues.Count);
        int locationMinAmount = locationValues[roll];

        roll = Random.Range(0, locationValues.Count);
        int locationMaxAmount = locationValues[roll];

        if (locationMinAmount > locationMaxAmount)
        {
            locationMinAmount = locationMaxAmount;
        }

        LocationMana returnLocationMana = new LocationMana(locationMinAmount, locationMaxAmount);

        return returnLocationMana;
    }

    public void StartingLocation()
    {
        //This uses the location to set the min and max color values for that location
        //Using a switch statement and assigning the variables of min and max colors
        //To the appropriate values based on the dictionaries created earlier
        //SO, if the location is Forest, we are setting the currentRed equal to the
        //random number in the list of numbers in the Dictionary Locations
        //with a key of "Forest" and a second key Color.Red and using the value
        //at that location to set the min (current) red. 
        /*
        if (location != null)
        {
            switch (location)
            {
                case "Forest":
                    
                    break;
                default:
                    break;
            }
        }*/
    }

    public void GenerateEnvironment(string location)
    {
        switch (location)
        {
            case "Forest":
                currentRed = Locations["Forest"][Hue.Red].currentAmount;
                maxRed = Locations["Forest"][Hue.Red].colorMax;
                currentOrange = Locations["Forest"][Hue.Orange].currentAmount;
                maxOrange = Locations["Forest"][Hue.Orange].colorMax;
                currentYellow = Locations["Forest"][Hue.Yellow].currentAmount;
                maxYellow = Locations["Forest"][Hue.Yellow].colorMax;
                currentGreen = Locations["Forest"][Hue.Green].currentAmount;
                maxGreen = Locations["Forest"][Hue.Green].colorMax;
                currentBlue = Locations["Forest"][Hue.Blue].currentAmount;
                maxBlue = Locations["Forest"][Hue.Blue].colorMax;
                currentViolet = Locations["Forest"][Hue.Violet].currentAmount;
                maxViolet = Locations["Forest"][Hue.Violet].colorMax;
                break;
            case "Cave":
                currentRed = Locations["Cave"][Hue.Red].currentAmount;
                maxRed = Locations["Cave"][Hue.Red].colorMax;
                currentOrange = Locations["Cave"][Hue.Orange].currentAmount;
                maxOrange = Locations["Cave"][Hue.Orange].colorMax;
                currentYellow = Locations["Cave"][Hue.Yellow].currentAmount;
                maxYellow = Locations["Cave"][Hue.Yellow].colorMax;
                currentGreen = Locations["Cave"][Hue.Green].currentAmount;
                maxGreen = Locations["Cave"][Hue.Green].colorMax;
                currentBlue = Locations["Cave"][Hue.Blue].currentAmount;
                maxBlue = Locations["Cave"][Hue.Blue].colorMax;
                currentViolet = Locations["Cave"][Hue.Violet].currentAmount;
                maxViolet = Locations["Cave"][Hue.Violet].colorMax;
                break;

        }
    }
    public void RedChangeHappened()
    {

    }

    public Dictionary<Hue, int> GetCurrentColorDictionary()
    {
        return environmentColorDictionary; 
    }

    public void UpdateEnvironmentColorDictionary()
    {
        environmentColorDictionary[Hue.Red] = currentRed;
        environmentColorDictionary[Hue.Orange] = currentOrange;
        environmentColorDictionary[Hue.Yellow] = currentYellow;
        environmentColorDictionary[Hue.Green] = currentGreen;
        environmentColorDictionary[Hue.Blue] = currentBlue;
        environmentColorDictionary[Hue.Violet] = currentViolet;
    }

    public void RegenEnvColors()
    {
        int maxAmount = 0;
        
        List<int> maxColors = new List<int>(){maxRed, maxOrange, maxYellow, maxGreen, maxBlue, maxViolet};

        foreach (int color in maxColors)
        {
            if(color > maxAmount)
            {
                maxAmount = color;
            }
        }

        int amountToRegen = maxAmount / 5;
        GainRed(amountToRegen);
        GainOrange(amountToRegen);
        GainYellow(amountToRegen);
        GainGreen(amountToRegen);
        GainBlue(amountToRegen);
        GainViolet(amountToRegen);


    }


    public void LoseRed(int amount)
    {
        currentRed -= amount;
        if(currentRed < 0)
        {
            currentRed = 0;
        }

    }
    public void GainRed(int amount)
    {
        currentRed += amount;
        if (currentRed > maxRed)
        {
            currentRed = maxRed;
        }

    }
    public void LoseOrange(int amount)
    {
        currentOrange-= amount;
        if (currentOrange < 0)
        {
            currentOrange = 0;
        }
    }
    public void GainOrange(int amount)
    {
        currentOrange += amount;
        if (currentOrange > maxOrange)
        {
            currentOrange = maxOrange;
        }
    }
    public void LoseYellow(int amount)
    {
        currentYellow -= amount;
        if (currentYellow < 0)
        {
            currentYellow = 0;
        }
    }
    public void GainYellow(int amount)
    {
        currentYellow += amount;
        if (currentYellow > maxYellow)
        {
            currentYellow = maxYellow;
        }
    }
    public void LoseGreen(int amount)
    {
        currentGreen -= amount;
        if (currentGreen < 0)
        {
            currentGreen = 0;
        }
    }
    public void GainGreen(int amount)
    {
        currentGreen += amount;
        if (currentGreen > maxGreen)
        {
            currentGreen = maxGreen;
        }
    }
    public void LoseBlue(int amount)
    {
        currentBlue -= amount;
        if (currentBlue < 0)
        {
            currentBlue = 0;
        }
    }
    public void GainBlue(int amount)
    {
        currentBlue += amount;
        if (currentBlue > maxBlue)
        {
            currentBlue = maxBlue;
        }
    }
    public void LoseViolet(int amount)
    {
        currentViolet -= amount;
        if (currentViolet < 0)
        {
            currentViolet = 0;
        }
    }
    public void GainViolet(int amount)
    {
        currentViolet += amount;
        if (currentViolet > maxViolet)
        {
            currentViolet = maxViolet;
        }
    }
}
