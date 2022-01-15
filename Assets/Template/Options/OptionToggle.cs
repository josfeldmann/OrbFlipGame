using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionToggle : MonoBehaviour
{
    public Toggle toggle;
    public bool defaultValue = false;
    public string key = "";
    public UnityEvent<bool> unityEvent;
    private void Awake() {
        if (!Options.HasBool(key)) Options.SetBool(key, defaultValue); 
        toggle.SetIsOnWithoutNotify(Options.GetBool(key));
        toggle.onValueChanged.AddListener(ChangeProxy);

    }

    public void ChangeProxy(bool b) {
        unityEvent.Invoke(b);
    }

    public void SetVariable(bool b) {
        Options.SetBool(key, b);
    }

}
