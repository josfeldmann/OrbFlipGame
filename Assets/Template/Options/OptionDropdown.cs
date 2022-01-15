using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

public class OptionDropdown : MonoBehaviour
{
    public TMP_Dropdown dropDown;
    public int defaultValue = 0;
    public string key = "";
    public UnityEvent<int> unityEvent;
    private void Awake() {
        if (!Options.HasInt(key)) Options.SetInt(key, defaultValue);
        dropDown.SetValueWithoutNotify(Options.GetInt(key));
        dropDown.onValueChanged.AddListener(ChangeProxy);

    }

    public void ChangeProxy(int i) {
        unityEvent.Invoke(i);
    }

    public void SetVariable(int i) {
        Options.SetInt(key, i);
    }
}
