using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCaller : MonoBehaviour
{
    private void Start()
    {
        /*PlayerPrefs.SetInt("Level1Cleared", 0);
        PlayerPrefs.SetInt("Level2Cleared", 0);
        PlayerPrefs.SetInt("Level3Cleared", 0);*/
        if (PlayerPrefs.HasKey("Level1Cleared"))
        {
            ClearedLevelTracker.levelsCleared = PlayerPrefs.GetInt("LevelCleared");
            ClearedLevelTracker.Level_1Cleared = PlayerPrefs.GetInt("Level1Cleared");
            ClearedLevelTracker.Level_2Cleared = PlayerPrefs.GetInt("Level2Cleared");
            ClearedLevelTracker.Level_3Cleared = PlayerPrefs.GetInt("Level3Cleared");
            ClearedLevelTracker.Level_4Cleared = PlayerPrefs.GetInt("Level4Cleared");
            ClearedLevelTracker.Level_5Cleared = PlayerPrefs.GetInt("Level5Cleared");
            ClearedLevelTracker.Level_6Cleared = PlayerPrefs.GetInt("Level6Cleared");
            ClearedLevelTracker.Level_7Cleared = PlayerPrefs.GetInt("Level7Cleared");
            ClearedLevelTracker.Level_8Cleared = PlayerPrefs.GetInt("Level8Cleared");
            ClearedLevelTracker.Level_9Cleared = PlayerPrefs.GetInt("Level9Cleared");
            ClearedLevelTracker.Level_10Cleared = PlayerPrefs.GetInt("Level10Cleared");
            ClearedLevelTracker.Level_11Cleared = PlayerPrefs.GetInt("Level11Cleared");
            ClearedLevelTracker.Level_12Cleared = PlayerPrefs.GetInt("Level12Cleared");
            ClearedLevelTracker.Level_13Cleared = PlayerPrefs.GetInt("Level13Cleared");
            ClearedLevelTracker.Level_14Cleared = PlayerPrefs.GetInt("Level14Cleared");
            Debug.Log("Game data loaded!");
        }
        else
            Debug.LogError("There is no save data!");
    }
}
