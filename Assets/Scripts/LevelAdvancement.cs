using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelAdvancement : MonoBehaviour, IPointerClickHandler
{
    public Button button;

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
                /*SceneManager.LoadScene(SceneManager.GetActiveScene().name);*/
            }
            if(gameObject.name == "AdvanceButton")
            {
                if(ActiveButtons.advancebutton == false)
                {
                    Debug.Log("not yet");
                }
                else
                {
                    Debug.Log("next level");
                }
            }
        }
    }
}