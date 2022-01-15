using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FLIPSTATE { ONE, TWO, THREE, BALL }


public class FlipCard : MonoBehaviour
{
    public int x, y;
    public Image backImage;
    public Image spriteImage;
    public FLIPSTATE state;
    public bool facingUp = false;
    public FlipController controller;
    public bool flipping;
    public static float flipSpeed = 6f;

    public GameObject BallMarker, oneMarker, twoMarker, threeMarker;

    public void SetSprite(Sprite s) {
        spriteImage.rectTransform.sizeDelta = new Vector2(s.rect.width * 2, s.rect.height * 2);
        spriteImage.sprite = s;
    }


    public void HideAllMarkers() {
        BallMarker.SetActive(false);
        oneMarker.SetActive(false);
        twoMarker.SetActive(false);
        threeMarker.SetActive(false);
    }

    public void InstantlyFlipBack() {
        facingUp = false;
        transform.localScale = Vector3.one;
        backImage.sprite = SpriteDataBase.CARDBACK;
        spriteImage.gameObject.SetActive(false);
    }

    public void Hover() {
        
        controller.Hover(this);
    }

    public void UnHover() {
        controller.ExitHover(this);
    }

    public void Click() {
        if (facingUp) return;
        controller.Click(this);
    }

    public void SetFlipState(FLIPSTATE state) {
        this.state = state;
        switch (state) {
            case FLIPSTATE.ONE:
                SetSprite(SpriteDataBase.ONE);
                break;
            case FLIPSTATE.TWO:
                SetSprite(SpriteDataBase.TWO);
                break;
            case FLIPSTATE.THREE:
                SetSprite(SpriteDataBase.THREE);
                break;
            case FLIPSTATE.BALL:
                SetSprite(SpriteDataBase.BALL);
                break;
        }
    }

    internal void InstantlyFlipFront() {
        facingUp = true;
        backImage.sprite = SpriteDataBase.CARDFRONT;
        spriteImage.gameObject.SetActive(true);
    }

    internal void Flip() {
        flipping = true;
        StartCoroutine(FlipIEnum());
    }

    public IEnumerator FlipIEnum() {

        transform.localScale = Vector3.one;

        while (transform.localScale.x != 0) {
            transform.localScale = new Vector3(Mathf.MoveTowards(transform.localScale.x, 0, flipSpeed * Time.deltaTime), 1, 1);
            yield return null;
        }
        backImage.sprite = SpriteDataBase.CARDFRONT;
        spriteImage.gameObject.SetActive(true);
        facingUp = true;
        HideAllMarkers();
        while (transform.localScale.x != 1) {
            transform.localScale = new Vector3(Mathf.MoveTowards(transform.localScale.x, 1, flipSpeed * Time.deltaTime), 1, 1);
            yield return null;
        }
        flipping = false;

    }

    public void ToggleMarkers() {
        foreach (FLIPSTATE s in MarkerButton.markedStates) {
            switch (s) {
                case FLIPSTATE.ONE:
                    oneMarker.SetActive(!oneMarker.activeInHierarchy);
                    break;
                case FLIPSTATE.TWO:
                    twoMarker.SetActive(!twoMarker.activeInHierarchy);
                    break;
                case FLIPSTATE.THREE:
                    threeMarker.SetActive(!threeMarker.activeInHierarchy);
                    break;
                case FLIPSTATE.BALL:
                    BallMarker.SetActive(!BallMarker.activeInHierarchy);
                    break;
            }
        }
    }

    internal int GetValue() {
        switch (state) {
            case FLIPSTATE.ONE:
                return 1;
                break;
            case FLIPSTATE.TWO:
                return 2;
                break;
            case FLIPSTATE.THREE:
                return 3;
                break;
            case FLIPSTATE.BALL:
                break;
        }
        return 0;
    }
}
