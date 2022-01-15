using TMPro;
using UnityEngine;

public class WinScreen : MonoBehaviour {

    public TextMeshProUGUI winLoseText;
    public NumberImageDisplayer numberDisplayer, highScore;

    int tCoins = 0;
    bool iswon = false;
    public void Set(int totalCoints, bool isWon) {
        tCoins = totalCoints;
        iswon = isWon;
        numberDisplayer.SetInt(totalCoints);
        if (iswon) {
            winLoseText.SetText("Win");
        } else {
            winLoseText.SetText("Lose");
        }
        
        if (Options.GetInt(Options.HIGHSCORE) < totalCoints) {
            Options.SetInt(Options.HIGHSCORE, totalCoints);
        }

        highScore.SetInt(Options.GetInt(Options.HIGHSCORE));


    }


}
