using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementDisplayerTab : MonoBehaviour {
    public AchievementObject currentAchivementObject;
    public RectTransform rect;
    public Image image;
    public TextMeshProUGUI nameText, descriptionText;

    public void SetAchievement(AchievementObject o) {
        currentAchivementObject = o;
        image.sprite = o.achievementSprite;
        nameText.text = o.GetName();
        descriptionText.text = o.GetDescription();
    }

}