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

    public string location;

    //Forest
    public List<int> forestReds = new List<int>();
    public List<int> forestOranges = new List<int>();
    public List<int> forestYellows = new List<int>();
    public List<int> forestGreens = new List<int>();
    public List<int> forestBlues = new List<int>();
    public List<int> forestViolets = new List<int>();


    //Dictionary inside of a dictionary 
    public Dictionary<string, Dictionary<Color, LocationMana>> Locations = new Dictionary<string, Dictionary<Color, LocationMana>>();

    // Start is called before the first frame update
    void Start()
    {
        

        //StartingLocation();
        //Locations is a Dictionary that has the key of a string and the value of another Dictionary

        //Locations["Forest"] is making a new Dictionary with the key of a Color and the struct LocationMana
        Locations["Forest"] = new Dictionary<Color, LocationMana>();

        Locations["Forest"][Color.Red] = LocationColors(forestReds);
        Locations["Forest"][Color.Orange] = LocationColors(forestOranges);
        Locations["Forest"][Color.Yellow] = LocationColors(forestYellows);
        Locations["Forest"][Color.Green] = LocationColors(forestGreens);
        Locations["Forest"][Color.Blue] = LocationColors(forestBlues);
        Locations["Forest"][Color.Violet] = LocationColors(forestViolets);


        StartingLocation();

    }

    private LocationMana LocationColors(List<int> locationValues)
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartingLocation()
    {

        if (location != null)
        {
            switch(location)
            {
                case "Forest":
                    currentRed = Locations["Forest"][Color.Red].currentAmount;
                    maxRed = Locations["Forest"][Color.Red].colorMax;
                    currentOrange = Locations["Forest"][Color.Orange].currentAmount;
                    maxOrange = Locations["Forest"][Color.Orange].colorMax;
                    currentYellow = Locations["Forest"][Color.Yellow].currentAmount;
                    maxYellow = Locations["Forest"][Color.Yellow].colorMax;
                    currentGreen = Locations["Forest"][Color.Green].currentAmount;
                    maxGreen = Locations["Forest"][Color.Green].colorMax;
                    currentBlue = Locations["Forest"][Color.Blue].currentAmount;
                    maxBlue = Locations["Forest"][Color.Blue].colorMax;
                    currentViolet = Locations["Forest"][Color.Violet].currentAmount;
                    maxViolet = Locations["Forest"][Color.Violet].colorMax;
                    break;
                default:
                    break;
            }
        }
    }

    


}
//TODO: Make a ENV_Color regen system