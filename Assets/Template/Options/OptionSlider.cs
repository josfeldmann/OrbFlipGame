using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionSlider : MonoBehaviour
{
    public string key = "";
    public float defaultValue = 1;
    public Slider slider;
    public string endString, stringFormat;
    public float multiple = 1;
    public TextMeshProUGUI value;
    public UnityEvent<float> unityEvent;

    private void Awake() {
        if (!Options.HasFloat(key)) Options.SetFloat(key, defaultValue);
        slider.SetValueWithoutNotify(Options.GetFloat(key));
        SetText(Options.GetFloat(key));
        slider.onValueChanged.AddListener(ChangeProxy);
       
    }
    
    public void ChangeProxy(float f) {
        unityEvent.Invoke(f);
    }

    public void SetText(float f) {
        value.SetText((slider.value * multiple).ToString(stringFormat) + endString);
    }

    public void SetVariable(float f) {
        Options.SetFloat(key, f);
        SetText(f);
    }

    public void SetMusic(float f) {
        AudioManager.instance.SetMusic(f);
        SetText(f);
    }

    public void SetSFX(float f) {
        AudioManager.instance.SetSFX(f);
        SetText(f);
    }


}
