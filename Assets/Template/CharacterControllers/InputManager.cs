using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float horizontal, vertical;
    public bool aDown, bDown;
    public bool clickDown;
    public bool clickUp;
    public bool rightClickDown;


    private void Update() {
        clickDown = Input.GetMouseButtonDown(0);
        clickUp = Input.GetMouseButtonUp(0);
        rightClickDown = Input.GetMouseButtonDown(1);
    }
}



