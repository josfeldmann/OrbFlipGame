using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementObject : ScriptableObject
{
    [SerializeField] protected string id;
    [SerializeField] protected string achievementName;
    [SerializeField] protected string achivementDescription;
    public Sprite achievementSprite;
    public string GetID() {
        return id;
    }

    public string GetDescription() {
        return achivementDescription;
    }

    public string GetName() {
        return achievementName;
    }

    public virtual bool IsFulfilled(AchievementTracker tracker) {
        return false;
    }

}


[System.Serializable]
public class AchievementTrackerSaveFile {
    public HashSet<string> completedAchievements = new HashSet<string>();
    public Dictionary<string, bool> savedBools = new Dictionary<string, bool>();
    public Dictionary<string, int> savedInts = new Dictionary<string, int>();
    public Dictionary<string, float> savedFloats = new Dictionary<string, float>();
    public Dictionary<string, string> savedStrings = new Dictionary<string, string>();
}

public enum AchievementTab {
    IN = 0, OUT = 1, WAIT = 2
}
