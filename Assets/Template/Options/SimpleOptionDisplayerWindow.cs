using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimpleOptionDisplayerWindow : MonoBehaviour
{

    public OptionButton optionButton;
    public TextMeshProUGUI prompt;
    public Transform optionButtonTransform;
    public List<OptionButton> optionButtons = new List<OptionButton>();
    public bool isDone = false;
    public int result = 0;
    public void SetOptions(string prompt, List<string> options) {
        isDone = false;
        int toAdd = options.Count - optionButtons.Count;
        this.prompt.SetText(prompt);
        for (int i = 0; i < toAdd; i++) {
            OptionButton op = Instantiate(optionButton, optionButtonTransform);
            op.gameObject.SetActive(false);
            optionButtons.Add(op);
        }

        foreach (OptionButton b in optionButtons) {
            b.gameObject.SetActive(false);
        }

        for (int i = 0; i < options.Count; i++) {
            optionButtons[i].gameObject.SetActive(true);
            optionButtons[i].Set(options[i], i, this);
        }

    }

    public void Click(OptionButton b) {
        isDone = true;
        result = b.option;
    }


}
