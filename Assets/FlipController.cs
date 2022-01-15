using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class FlipLevelInfo {
    public int two = 2, three = 2, orb = 6;
}

[System.Serializable]
public class FlipLevels {
    public List<FlipLevelInfo> levels = new List<FlipLevelInfo>();
}

public class FlipController : MonoBehaviour
{

    public NumberImageDisplayer currentpointDisplayer, totalPointsDisplayer;
    public int currentLevel;
    public StateMachine<FlipController> controller;
    public List<FlipCard> flipCards = new List<FlipCard>();
    public List<FlipMarker> rowMarker = new List<FlipMarker>();
    public List<FlipMarker> columnMarker = new List<FlipMarker>();
    public FlipCard[,] flipCardGrid;
    public SpriteDataBase database;
    public Transform cursor;
    public FlipCard currentCard;
    public FlipLevelCollection flipLevels;
    public InputManager manager;
    public int currentpoints = 0, totalPoints;
    public WinScreen winScreen;
    public List<MarkerButton> markers = new List<MarkerButton>();


    [Header("Audio")]
    public AudioPlayer clickSound;
    public AudioPlayer flipSound;
    public AudioPlayer winSound;

    private void Awake() {
        controller = new StateMachine<FlipController>(new IdleFlipControllerState(), this);
    }

    public void Update() {
        controller.Update();
    }

    public static List<FlipCard> checkCards = new List<FlipCard>();
    
    public void UpdateCurrentPoints() {
        int twoScore = 1;
        int threeScore = 1;
        if (twoCount > 0) {
            twoScore = (int)Mathf.Pow(2, twoCount);
        }
        if (threeCount > 0) {
            threeScore = (int)Mathf.Pow(3, threeCount);
        }
        currentpoints = twoScore * threeScore;
        if (currentpoints == 1) {
            currentpoints = 0;
        }
        currentpointDisplayer.SetInt(currentpoints);
    }

    public void UpdateTotalPoints() {
        totalPointsDisplayer.SetInt(totalPoints);
    }

    public void Setup() {
        
        if (controller == null) controller = new StateMachine<FlipController>(new IdleFlipControllerState(), this);
        currentpoints = 0;
        totalPoints = 0;
        totalPointsDisplayer.SetInt(totalPoints);
        checkCards = new List<FlipCard>();
        flipCardGrid = new FlipCard[5, 5];
        database.SetSprites();
        cursor.gameObject.SetActive(false);
        foreach (FlipCard c in flipCards) {
            flipCardGrid[c.x, c.y] = c;
            c.InstantlyFlipBack();
            c.controller = this;
            c.HideAllMarkers();
        }
        foreach(MarkerButton m in markers) {
            m.markedBool = false;
            m.Set();
        }

        SetUpRound();
    }


    public void SetUpRound() {
        twoCount = 0;
        threeCount = 0;
        FlipLevelInfo f = flipLevels.levels[currentLevel].levels.PickRandom();
        winScreen.gameObject.SetActive(false);
        List<FlipCard> flips = new List<FlipCard>(flipCards);
        textPanel.SetActive(false);

        for (int i = 0; i < f.orb; i++) {
            FlipCard card = flips.PickRandom();
            card.SetFlipState(FLIPSTATE.BALL);
            flips.Remove(card);
        }
        for (int i = 0; i < f.two; i++) {
            FlipCard card = flips.PickRandom();
            card.SetFlipState(FLIPSTATE.TWO);
            flips.Remove(card);
            checkCards.Add(card);
        }
        for (int i = 0; i < f.three; i++) {
            FlipCard card = flips.PickRandom();
            card.SetFlipState(FLIPSTATE.THREE);
            flips.Remove(card);
            checkCards.Add(card);
        }
        foreach (FlipCard ff in flips) {
            ff.SetFlipState(FLIPSTATE.ONE);
        }


        for (int y = 0; y < 5; y++) {
            int total = 0;
            int orb = 0;
            for (int x = 0; x < 5; x++) {
                switch (flipCardGrid[x, y].state) {
                    case FLIPSTATE.ONE:
                        total += 1;
                        break;
                    case FLIPSTATE.TWO:
                        total += 2;
                        break;
                    case FLIPSTATE.THREE:
                        total += 3;
                        break;
                    case FLIPSTATE.BALL:
                        orb += 1;
                        break;
                }
            }
            rowMarker[y].SetOrbs(orb);
            rowMarker[y].SetPoints(total);
        }

        for (int x = 0; x < 5; x++) {

            int total = 0;
            int orb = 0;
            for (int y = 0; y < 5; y++) {
                switch (flipCardGrid[x, y].state) {
                    case FLIPSTATE.ONE:
                        total += 1;
                        break;
                    case FLIPSTATE.TWO:
                        total += 2;
                        break;
                    case FLIPSTATE.THREE:
                        total += 3;
                        break;
                    case FLIPSTATE.BALL:
                        orb += 1;
                        break;
                }
            }
            columnMarker[x].SetOrbs(orb);
            columnMarker[x].SetPoints(total);
        }
        controller.ChangeState(new IdleFlipControllerState());
        UpdateCurrentPoints();
    }

    public static FlipCard bufferCursor;


    public void Hover(FlipCard flipCard) {
        if (!canMoveCursor) {
            bufferCursor = flipCard;
            return;
        }
        cursor.gameObject.SetActive(true);
        cursor.position = flipCard.transform.position;
        currentCard = flipCard;
    }

    public void ExitHover(FlipCard flipCard) {
        if (!canMoveCursor) {
            bufferCursor = null;
            return;
        }
        currentCard = null;
        cursor.gameObject.SetActive(false);
    }

    public void Click(FlipCard card) {
        if (!canMoveCursor) return;
        if (card.facingUp) return;

        if (MarkerButton.markedStates.Count > 0 && !currentCard.facingUp) {
            currentCard.ToggleMarkers();
        } else {
            controller.ChangeState(new CardFlipControllerState(currentCard));
        }
    }

    public bool canMoveCursor = false;

    public GameObject textPanel;
    public TextMeshProUGUI textDisplayer;


    public void FlashText(string s, float time) {
        StartCoroutine(FlashTextNum(s, time));
    }


    public IEnumerator FlashTextNum(string s, float time) {

        textDisplayer.SetText(s);
        yield return null;
        textPanel.SetActive(true);

        yield return new WaitForSeconds(time);

        textPanel.SetActive(false);


    }
    public int twoCount = 0;
    public int threeCount = 0;
    public IEnumerator WinNumerator() {

        yield return new WaitForSeconds(0.5f);

        foreach (FlipCard c in flipCards) {
            if (!c.facingUp) {
                c.Flip();
            }
        }

        yield return new WaitForSeconds(0.25f);
       




        UpdateCurrentPoints();

        yield return FlashTextNum("Win", 1);
        totalPoints += currentpoints;
        Options.AddInt(Options.TOTALCOINS, currentpoints);
        currentpoints = 0;
        UpdateTotalPoints();
        currentLevel++;
        foreach (FlipCard c in flipCards) {
            c.InstantlyFlipBack();
        }

        if (currentLevel >= flipLevels.levels.Count) {
            winScreen.gameObject.SetActive(true);
            winScreen.Set(totalPoints, true);
        } else {
            SetUpRound();

        }




    }

    public IEnumerator LoseNumerator() {
        yield return new WaitForSeconds(0.25f);
        winScreen.gameObject.SetActive(true);
        totalPoints += currentpoints;
        Options.AddInt(Options.TOTALCOINS, totalPoints);
        winScreen.Set(totalPoints, false);
    }
}


public class IdleFlipControllerState : State<FlipController> {

    public override void Enter(StateMachine<FlipController> obj) {
        obj.target.canMoveCursor = true;
        if (FlipController.bufferCursor != null) {
            obj.target.Hover(FlipController.bufferCursor);
        } else {
            obj.target.cursor.gameObject.SetActive(false);
        }
    }
    public override void Update(StateMachine<FlipController> obj) {
        if (obj.target.manager.clickDown) {
            if (obj.target.currentCard != null) obj.target.Click(obj.target.currentCard);
        }
    }
}

public class CardFlipControllerState : State<FlipController> {
    FlipCard card;
    public CardFlipControllerState(FlipCard cardToWatch) {
        card = cardToWatch;
    }

    public override void Enter(StateMachine<FlipController> obj) {
        card.Flip();
        obj.target.Hover(obj.target.currentCard);
        obj.target.flipSound.Play();
        obj.target.canMoveCursor = false;
    }

    

    public override void Update(StateMachine<FlipController> obj) {
        if (card.flipping == false) {
            if (FlipController.checkCards.Contains(card)) FlipController.checkCards.Remove(card);

            if (obj.target.currentCard.GetValue() == 2) {
                obj.target.twoCount++;
                Options.AddInt(Options.TWOSFLIPPPED, 1);
            } else if (obj.target.currentCard.GetValue() == 3) {
                obj.target.threeCount++;
                Options.AddInt(Options.THREESFLIPPPED, 1);
            } else if (obj.target.currentCard.state == FLIPSTATE.BALL) {
                Options.AddInt(Options.ORBSFLIPPPED, 1);
            }

            obj.target.UpdateCurrentPoints();
            if (obj.target.currentCard.state == FLIPSTATE.BALL) {
                obj.ChangeState(new LoseLevelState());
            } else if (FlipController.checkCards.Count == 0) {
                
                obj.ChangeState(new WinLevelFlipState());
            } else {
                
                obj.ChangeState(new IdleFlipControllerState());
            }
        }
    }
}

public class WinLevelFlipState : State<FlipController> {
    public override void Enter(StateMachine<FlipController> obj) {
        Options.SaveOptions();
        obj.target.currentCard = null;
        obj.target.winSound.Play();
        obj.target.StartCoroutine(obj.target.WinNumerator());
    }
} 


public class LoseLevelState : State<FlipController> {
    public override void Enter(StateMachine<FlipController> obj) {
        Options.SaveOptions();
        obj.target.StartCoroutine(obj.target.LoseNumerator());
    }
}