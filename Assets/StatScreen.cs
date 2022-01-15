using System;
using TMPro;
using UnityEngine;

public class StatScreen : MonoBehaviour {

    public TextMeshProUGUI twosflipped, threesflipped, orbsflipped, totalcoins, highScore;
    
    
    
    public void SetUp() {
        totalcoins.SetText("Total Coins: " + Options.GetInt(Options.TOTALCOINS).ToString());
        twosflipped.SetText("2's Flipped: " + Options.GetInt(Options.TWOSFLIPPPED).ToString()); ;
        threesflipped.SetText("3's Flipped: " + Options.GetInt(Options.THREESFLIPPPED).ToString());
        orbsflipped.SetText("Orbs Flipped: " + Options.GetInt(Options.ORBSFLIPPPED).ToString());
        highScore.SetText("High Score: " + Options.GetInt(Options.HIGHSCORE).ToString());
    }
}

