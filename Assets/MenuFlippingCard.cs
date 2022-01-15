using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuFlippingCard : MonoBehaviour
{
    public Image card;

    public bool facingup = false;


    public List<Image> images = new List<Image>();
    public SpriteDataBase database;
    public float CardSpeed = 1f;
    public float scaleTarget = 0;
    public float linger = 0.25f;

    float lingertime = 0;

    private void Start() {
        lingertime = Time.time + linger;
        database.SetSprites();
        ShowBack();
    }

    public void ShowFront() {
        facingup = true;
        card.sprite = SpriteDataBase.CARDFRONT;
        foreach (Image i in images) {
            i.gameObject.SetActive(false);
        }
        images.PickRandom().gameObject.SetActive(true);
    }

    public void ShowBack() {
        facingup = false;
        card.sprite = SpriteDataBase.CARDBACK;
        foreach (Image i in images) {
            i.gameObject.SetActive(false);
        }
    }


    private void Update() {
        if (Time.time < lingertime) {
        } else {
            if (transform.localScale.x != scaleTarget) {
                transform.localScale = new Vector3(Mathf.MoveTowards(transform.localScale.x, scaleTarget, CardSpeed * Time.deltaTime), 1, 1);
            } else {
                if (scaleTarget == 0) scaleTarget = 1;
                else scaleTarget = 0;

                if (scaleTarget == 1)
                    FlipToggle();
                else
                    lingertime = Time.time + linger;
            }
        }
    }

    public void FlipToggle() {
        facingup = !facingup;
        if (facingup) {
            ShowFront();
        } else {
            ShowBack();
        }
        
    }


}
