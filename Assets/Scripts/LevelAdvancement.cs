using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelAdvancement : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            if (gameObject.name == "Go")
            {
                ActiveColors.goButton = true;
                Destroy(gameObject);
            }
            if(gameObject.name == "Repeat")
            {
                ActiveColors.blue = false;
                ActiveColors.red = false;
                ActiveColors.yellow = false;
                ActiveColors.goButton = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}