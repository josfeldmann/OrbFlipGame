using System.Collections.Generic;
using UnityEngine;

public class AchievementTracker : MonoBehaviour {
    
    public AchievementTrackerSaveFile saveFile = null;
    public List<AchievementObject> toDisplayAchievements = new List<AchievementObject>();
    public AchievementDisplayerTab displayerTab;
    public bool displayingAchievement = false;
    public AchievementTab achievementState;
    public RectTransform inPosition, outPosition;
    public float tabSpeed = 200f;
    public float waitTime = 1.5f;
    private float timer;
    public List<AchievementObject> achievements = new List<AchievementObject>();
    private Dictionary<string, AchievementObject> achievementDict = new Dictionary<string, AchievementObject>();

    private void Awake() {
        Setup();
    }

    public void CheckAllAchievements() {
        foreach (AchievementObject a in achievements) {
            CheckAchievement(a);
        }
    }

    public void Setup() {
        if (saveFile == null) saveFile = new AchievementTrackerSaveFile();
        achievementDict = new Dictionary<string, AchievementObject>();
        foreach (AchievementObject a in achievements) {
            achievementDict.Add(a.GetID(), a);
        }
        if (!displayingAchievement) displayerTab.rect.anchoredPosition = outPosition.anchoredPosition;
    }

    public void Load(AchievementTrackerSaveFile save) {
        saveFile = save;
    }

    public void CheckAchievement(AchievementObject achievement) {
        string id = achievement.GetID();
        if (saveFile.completedAchievements.Contains(id)) {
            return;
        }

        if (achievement.IsFulfilled(this)) {
            saveFile.completedAchievements.Add(id);
            toDisplayAchievements.Add(achievement);
            StartShowingAchievement();
        }
    }

    public void StartShowingAchievement() {
        if (displayingAchievement) {

        } else if (toDisplayAchievements.Count > 0) {
            displayingAchievement = true;
            displayerTab.SetAchievement(toDisplayAchievements[0]);
            displayerTab.rect.anchoredPosition = outPosition.anchoredPosition;
            achievementState = AchievementTab.IN;
        }
    }

    private void Update() {
        if (displayingAchievement) {
            switch (achievementState) {
                case AchievementTab.IN:
                    if (displayerTab.rect.anchoredPosition != inPosition.anchoredPosition) {
                        displayerTab.rect.anchoredPosition = Vector2.MoveTowards(displayerTab.rect.anchoredPosition, inPosition.anchoredPosition, tabSpeed * Time.deltaTime);
                    } else {
                        achievementState = AchievementTab.WAIT;
                        timer = Time.time + waitTime;
                    }
                    break;
                case AchievementTab.OUT:
                    if (displayerTab.rect.anchoredPosition != outPosition.anchoredPosition) {
                        displayerTab.rect.anchoredPosition = Vector2.MoveTowards(displayerTab.rect.anchoredPosition, outPosition.anchoredPosition, tabSpeed * Time.deltaTime);
                    } else {
                        displayingAchievement = false;
                        toDisplayAchievements.RemoveAt(0);
                        if (toDisplayAchievements.Count > 0) StartShowingAchievement();
                    }
                    break;
                case AchievementTab.WAIT:
                    if (timer < Time.time) {
                        achievementState = AchievementTab.OUT;
                    }
                    break;
            }
        }
    }




}
