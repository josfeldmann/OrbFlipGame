using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipMarker : MonoBehaviour
{
    public Image pointOne, pointTwo;
    public Image orbOne, orbTwo;
    public bool isRow;
    public int index;

    public void SetImage(Image i, Sprite s) {
        i.rectTransform.sizeDelta = new Vector2(s.rect.width * 2, s.rect.height * 2);
        i.sprite = s;
    }

    public void SetPoints(int amount) {
        SetImage(pointOne, SpriteDataBase.GetNumberSpriteSmall(amount / 10));
        SetImage(pointTwo, SpriteDataBase.GetNumberSpriteSmall(amount % 10));
    }

    public void SetOrbs(int amount) {
        SetImage(orbOne, SpriteDataBase.GetNumberSpriteSmall(amount / 10));
        SetImage(orbTwo, SpriteDataBase.GetNumberSpriteSmall(amount % 10));
    }


}
