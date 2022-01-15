using UnityEngine;

public class OptionsLoader : MonoBehaviour {

    private void Awake() {
        print(Options.initiallyLoadedyet);
        if (!Options.initiallyLoadedyet) {
            print("First");
            Options.LoadOptions();
        }
    }

    
}
