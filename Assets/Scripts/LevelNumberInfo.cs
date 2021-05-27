using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelNumberInfo : MonoBehaviour
{
    /*int numberOfClearedLevels;
    // Start is called before the first frame update
    void Start()
    {
        int numberOfClearedLevels = ClearedLevelTracker.levelsCleared;
    }*/

    public void SaveGame()
    {
        PlayerPrefs.SetInt("LevelCleared", ClearedLevelTracker.levelsCleared);
        PlayerPrefs.SetInt("Level1Cleared", ClearedLevelTracker.Level_1Cleared);
        PlayerPrefs.SetInt("Level2Cleared", ClearedLevelTracker.Level_2Cleared);
        PlayerPrefs.SetInt("Level3Cleared", ClearedLevelTracker.Level_3Cleared);
        PlayerPrefs.SetInt("Level4Cleared", ClearedLevelTracker.Level_4Cleared);
        PlayerPrefs.SetInt("Level5Cleared", ClearedLevelTracker.Level_5Cleared);
        PlayerPrefs.SetInt("Level6Cleared", ClearedLevelTracker.Level_6Cleared);
        PlayerPrefs.SetInt("Level7Cleared", ClearedLevelTracker.Level_7Cleared);
        PlayerPrefs.SetInt("Level8Cleared", ClearedLevelTracker.Level_8Cleared);
        PlayerPrefs.SetInt("Level9Cleared", ClearedLevelTracker.Level_9Cleared);
        PlayerPrefs.SetInt("Level10Cleared", ClearedLevelTracker.Level_10Cleared);
        PlayerPrefs.SetInt("Level11Cleared", ClearedLevelTracker.Level_11Cleared);
        PlayerPrefs.SetInt("Level12Cleared", ClearedLevelTracker.Level_12Cleared);
        PlayerPrefs.SetInt("Level13Cleared", ClearedLevelTracker.Level_13Cleared);
        PlayerPrefs.SetInt("Level14Cleared", ClearedLevelTracker.Level_14Cleared);
        PlayerPrefs.Save();
        Debug.Log("Game data saved!");
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("LevelCleared"))
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
