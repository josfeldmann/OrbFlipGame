using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAchievement : MonoBehaviour
{

    public AchievementTracker tracker;
    public AchievementObject objectToCheck;
    public AchievementObject objectToCheck2;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.K)) {
            tracker.saveFile.savedBools.Add("Test", true);
            tracker.CheckAllAchievements();
        }
    }
}
