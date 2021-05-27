using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour, IPointerClickHandler
{
    public Button button;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            if(gameObject.name == "Level 1")
            {
                SceneManager.LoadScene("Level1_Tutorial");
            }
            else if (gameObject.name == "Level 2")
            {
                if(ClearedLevelTracker.Level_1Cleared == 1)
                {
                    SceneManager.LoadScene("Level2_Tutorial");
                }
            }
            else if (gameObject.name == "Level 3")
            {
                if (ClearedLevelTracker.Level_2Cleared == 1)
                {
                    SceneManager.LoadScene("Level3_Tutorial");
                }
            }
            else if (gameObject.name == "Level 4")
            {
                if (ClearedLevelTracker.Level_3Cleared == 1)
                {
                    SceneManager.LoadScene("Level4");
                }
            }
            else if (gameObject.name == "Level 5")
            {
                if (ClearedLevelTracker.Level_4Cleared == 1)
                {
                    SceneManager.LoadScene("Level5");
                }
            }
            else if (gameObject.name == "Level 6")
            {
                if (ClearedLevelTracker.Level_5Cleared == 1)
                {
                    SceneManager.LoadScene("Level6");
                }
            }
            else if (gameObject.name == "Level 7")
            {
                if (ClearedLevelTracker.Level_6Cleared == 1)
                {
                    SceneManager.LoadScene("Level7");
                }
            }
            else if (gameObject.name == "Level 8")
            {
                if (ClearedLevelTracker.Level_7Cleared == 1)
                {
                    SceneManager.LoadScene("Level8");
                }
            }
            else if (gameObject.name == "Level 9")
            {
                if (ClearedLevelTracker.Level_8Cleared == 1)
                {
                    SceneManager.LoadScene("Level9");
                }
            }
            else if (gameObject.name == "Level 10")
            {
                if (ClearedLevelTracker.Level_9Cleared == 1)
                {
                    SceneManager.LoadScene("Level10");
                }
            }
            else if (gameObject.name == "Level 11")
            {
                if (ClearedLevelTracker.Level_10Cleared == 1)
                {
                    SceneManager.LoadScene("Level11");
                }
            }
            else if (gameObject.name == "Level 12")
            {
                if (ClearedLevelTracker.Level_11Cleared == 1)
                {
                    SceneManager.LoadScene("Level12");
                }
            }
            else if (gameObject.name == "Level 13")
            {
                if (ClearedLevelTracker.Level_12Cleared == 1)
                {
                    SceneManager.LoadScene("Level13");
                }
            }
            else if (gameObject.name == "Level 14")
            {
                if (ClearedLevelTracker.Level_13Cleared == 1)
                {
                    SceneManager.LoadScene("Level14");
                }
            }
            else if (gameObject.name == "Level 15")
            {
                if (ClearedLevelTracker.Level_14Cleared == 1)
                {
                    SceneManager.LoadScene("Level15");
                }
            }
        }
    }
}
