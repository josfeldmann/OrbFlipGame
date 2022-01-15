using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/BoolAchievement")]
public class BoolAchievementObject : AchievementObject {

    public List<string> boolIds = new List<string>();

    public override bool IsFulfilled(AchievementTracker tracker) {
        foreach (string s in boolIds) {
            if (tracker.saveFile.savedBools.ContainsKey(s)) {
                if (tracker.saveFile.savedBools[s] == false) {
                    return false;
                }
            } else {
                return false;
            }
        }
        return true;
    }
}
