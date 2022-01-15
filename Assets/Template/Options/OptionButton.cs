using TMPro;
using UnityEngine;

public class OptionButton : MonoBehaviour {
    public TextMeshProUGUI text;
    public int option;
    [HideInInspector] public SimpleOptionDisplayerWindow window;

    public void Set(string s, int o, SimpleOptionDisplayerWindow window) {
        text.SetText(s);
        option = 0;
        this.window = window;
    }

    public void Click() {
        window.Click(this);
    }

}
