using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberImageDisplayer : MonoBehaviour
{
    public List<Image> numbers;

    public void SetInt(int i) {

        for (int x = 0; x < numbers.Count; x++) {
            int result = 0;
            if (x == 0) {
                result = i % 10;
                
            } else {
                result = (int)((i / Mathf.Pow(10, x))%10);
            }
            numbers[x].sprite = SpriteDataBase.GetNumberSprite(result);
            numbers[x].rectTransform.sizeDelta = new Vector2(numbers[x].sprite.rect.width * 2, numbers[x].sprite.rect.height * 2);
        }


    }



}
