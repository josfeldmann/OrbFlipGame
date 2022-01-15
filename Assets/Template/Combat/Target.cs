using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public bool isPlayer = false;

    public static List<Target> playertargets = new List<Target>(); 

    private void Awake() {
        if (isPlayer) {
            playertargets.Add(this);
        }

    }

    private void OnDestroy() {
        if (isPlayer) {
            playertargets.Remove(this);
        }
    }



}
