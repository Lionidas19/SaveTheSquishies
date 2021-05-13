using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPressed : MonoBehaviour, IPointerClickHandler
{
    public Sprite pressedImage;
    public Sprite unpressedImage;
    public Button button;

    private bool buttonActive;

    // Start is called before the first frame update
    void Start()
    {
        buttonActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            if(gameObject.name == "YellowButton")
            {
                buttonActive = !buttonActive;
                if (buttonActive == true)
                {
                    button.image.sprite = pressedImage;
                }
                else
                {
                    button.image.sprite = unpressedImage;
                }
                ActiveButtons.yellow = buttonActive;
                Debug.Log("Yellow is " + ActiveButtons.yellow);
            }
            else if (gameObject.name == "BlueButton")
            {
                buttonActive = !buttonActive;
                if (buttonActive == true)
                {
                    button.image.sprite = pressedImage;
                }
                else
                {
                    button.image.sprite = unpressedImage;
                }
                ActiveButtons.blue = buttonActive;
                Debug.Log("Blue is " + ActiveButtons.blue);
            }
            else if (gameObject.name == "RedButton")
            {
                buttonActive = !buttonActive;
                if (buttonActive == true)
                {
                    button.image.sprite = pressedImage;
                }
                else
                {
                    button.image.sprite = unpressedImage;
                }
                ActiveButtons.red = buttonActive;
                Debug.Log("Red is " + ActiveButtons.red);
            }
        }
    }
}
