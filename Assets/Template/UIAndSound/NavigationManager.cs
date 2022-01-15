using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class NavigationManager : MonoBehaviour
{

    public static bool navigation = false;
    public static NavigationManager instance;
    public InputManager manager;
    public TextMeshProUGUI debugText;
    public NavigationObject currentNavigationObject;

    public static void InstanceCheck() {
        if (instance == null) {
            instance = FindObjectOfType<NavigationManager>();
        }
    }

    public static void SetSelectedObject(NavigationObject nav) {
        InstanceCheck();
        instance.currentNavigationObject = nav;
        EventSystem.current.SetSelectedGameObject(nav.gameObject);
        navigation = true;
        UpdateText();
        NavigationCooldown();
        instance.currentNavigationObject.OnSelect.Invoke();
    }

    public static void StopNavigation() {
        InstanceCheck();
        navigation = false;
        UpdateText();
        EventSystem.current.SetSelectedGameObject(null);
    }

    public static float NavigationTime = 0.15f, nextNavigationTime = 0;
    public static void NavigationCooldown() {
        nextNavigationTime = Time.time + NavigationTime;
    }

    public static void UpdateText() {
        if (navigation) {
            instance.debugText.SetText("Navigation: true \n" + instance.currentNavigationObject.gameObject.name);
        } else {
            instance.debugText.SetText("Navigation: false");
        }
    }

    private void Update() {
        if (navigation) {
            if (currentNavigationObject == null || !currentNavigationObject.gameObject.activeInHierarchy) {
                navigation = false;
                UpdateText();
            }

            if (manager.aDown) {
                currentNavigationObject.ADown();
            } else if (manager.bDown) {
                currentNavigationObject.BDown();
            } else if (manager.horizontal != 0 || manager.vertical != 0) {
                if (Time.time > nextNavigationTime) {
                    if (manager.horizontal != 0) {
                        if (manager.horizontal > 0) {
                            NavigationObject nav = currentNavigationObject.GetRightObject();
                            if (nav != null && nav != currentNavigationObject) {
                                SetSelectedObject(nav);
                            }
                        } else {
                            NavigationObject nav = currentNavigationObject.GetLeftObject();
                            if (nav != null && nav != currentNavigationObject) {
                                SetSelectedObject(nav);
                            }
                        }
                    } else if (manager.vertical != 0) {
                        if (manager.vertical < 0) {
                            NavigationObject nav = currentNavigationObject.GetDownObject();
                            if (nav != null && nav != currentNavigationObject) {
                                SetSelectedObject(nav);
                            }
                        } else {
                            NavigationObject nav = currentNavigationObject.GetUpObject();
                            if (nav != null && nav != currentNavigationObject) {
                                SetSelectedObject(nav);
                            }
                        }
                    }
                    

                }
            }
        }
    }


}
