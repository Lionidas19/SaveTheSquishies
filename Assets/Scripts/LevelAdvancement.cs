using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelAdvancement : MonoBehaviour, IPointerClickHandler
{
    public Button button;

    private LevelManager lm;

    void Start()
    {

    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            if (gameObject.name == "PlayButton")
            {
                ActiveButtons.goButton = true;
                button.interactable = false;
                //Destroy(gameObject);
            }
            if(gameObject.name == "ResetButton")
            {
                ActiveButtons.blue = false;
                ActiveButtons.red = false;
                ActiveButtons.yellow = false;
                ActiveButtons.goButton = false;
                ActiveButtons.resetButton = true;
                ActiveButtons.advancebutton = false;
            }
            if(gameObject.name == "AdvanceButton")
            {
                Debug.Log("pressed");
                if (ActiveButtons.advancebutton == false)
                {
                    Debug.Log("not yet");
                }
                else
                {
                    ActiveButtons.blue = false;
                    ActiveButtons.red = false;
                    ActiveButtons.yellow = false;
                    ActiveButtons.goButton = false;
                    ActiveButtons.resetButton = false;

                    if (SceneManager.GetActiveScene().name == "Level1")
                    {
                        if (ClearedLevelTracker.Level_1Cleared == 0)
                        {
                            PlayerPrefs.SetInt("Level1Cleared", 1);
                        }
                    }
                    else if (SceneManager.GetActiveScene().name == "Level2")
                    {
                        if (ClearedLevelTracker.Level_2Cleared == 0)
                        {
                            PlayerPrefs.SetInt("Level2Cleared", 1);
                        }
                    }
                    else if (SceneManager.GetActiveScene().name == "Level3")
                    {
                        if (ClearedLevelTracker.Level_3Cleared == 0)
                        {
                            PlayerPrefs.SetInt("Level3Cleared", 1);
                        }
                    }
                    else if (SceneManager.GetActiveScene().name == "Level4")
                    {
                        if (ClearedLevelTracker.Level_4Cleared == 0)
                        {
                            PlayerPrefs.SetInt("Level4Cleared", 1);
                        }
                    }
                    else if (SceneManager.GetActiveScene().name == "Level5")
                    {
                        if (ClearedLevelTracker.Level_5Cleared == 0)
                        {
                            PlayerPrefs.SetInt("Level5Cleared", 1);
                        }
                    }
                    else if (SceneManager.GetActiveScene().name == "Level6")
                    {
                        if (ClearedLevelTracker.Level_6Cleared == 0)
                        {
                            PlayerPrefs.SetInt("Level6Cleared", 1);
                        }
                    }
                    else if (SceneManager.GetActiveScene().name == "Level7")
                    {
                        if (ClearedLevelTracker.Level_7Cleared == 0)
                        {
                            PlayerPrefs.SetInt("Level7Cleared", 1);
                        }
                    }
                    else if (SceneManager.GetActiveScene().name == "Level8")
                    {
                        if (ClearedLevelTracker.Level_8Cleared == 0)
                        {
                            PlayerPrefs.SetInt("Level8Cleared", 1);
                        }
                    }
                    else if (SceneManager.GetActiveScene().name == "Level9")
                    {
                        if (ClearedLevelTracker.Level_9Cleared == 0)
                        {
                            PlayerPrefs.SetInt("Level9Cleared", 1);
                        }
                    }
                    else if (SceneManager.GetActiveScene().name == "Level10")
                    {
                        if (ClearedLevelTracker.Level_10Cleared == 0)
                        {
                            PlayerPrefs.SetInt("Level10Cleared", 1);
                        }
                    }
                    else if (SceneManager.GetActiveScene().name == "Level11")
                    {
                        if (ClearedLevelTracker.Level_11Cleared == 0)
                        {
                            PlayerPrefs.SetInt("Level11Cleared", 1);
                        }
                    }
                    else if (SceneManager.GetActiveScene().name == "Level12")
                    {
                        if (ClearedLevelTracker.Level_12Cleared == 0)
                        {
                            PlayerPrefs.SetInt("Level12Cleared", 1);
                        }
                    }
                    else if (SceneManager.GetActiveScene().name == "Level13")
                    {
                        if (ClearedLevelTracker.Level_13Cleared == 0)
                        {
                            PlayerPrefs.SetInt("Level13Cleared", 1);
                        }
                    }
                    else if (SceneManager.GetActiveScene().name == "Level14")
                    {
                        if (ClearedLevelTracker.Level_14Cleared == 0)
                        {
                            PlayerPrefs.SetInt("Level14Cleared", 1);
                        }
                    }

                    ActiveButtons.advancebutton = false;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }
    }
}