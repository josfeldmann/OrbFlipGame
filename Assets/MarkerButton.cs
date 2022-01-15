using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerButton : MonoBehaviour
{
    public bool markedBool = false;
    public Image marked, unmarked;
    public FLIPSTATE markState;

    public static List<FLIPSTATE> markedStates = new List<FLIPSTATE>();

    public void Set() {
        marked.gameObject.SetActive(markedBool);
        unmarked.gameObject.SetActive(!markedBool);
        if (markedBool) {
            if (!markedStates.Contains(markState)) markedStates.Add(markState);
        } else {
            if (markedStates.Contains(markState)) markedStates.Remove(markState);
        }
    }

    public void Click() {
        markedBool = !markedBool;
        Set();
    }

    private void Update() {
        if( Input.GetKeyDown(KeyCode.Alpha1) && markState == FLIPSTATE.ONE) {
            Click();
        } else if (Input.GetKeyDown(KeyCode.Alpha2) && markState == FLIPSTATE.TWO) {
            Click();
        } else if (Input.GetKeyDown(KeyCode.Alpha3) && markState == FLIPSTATE.THREE) {
            Click();
        }
    }

}
