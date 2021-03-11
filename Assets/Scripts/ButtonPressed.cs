using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPressed : MonoBehaviour, IPointerClickHandler
{
    public GameObject image;

    // Start is called before the first frame update
    void Start()
    {
        image.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            if(gameObject.name == "Yellow")
            {
                image.SetActive(!image.activeSelf);
                ActiveColors.yellow = image.activeSelf;
            }
            else if (gameObject.name == "Blue")
            {
                image.SetActive(!image.activeSelf);
                ActiveColors.blue = image.activeSelf;
            }
            else if (gameObject.name == "Red")
            {
                image.SetActive(!image.activeSelf);
                ActiveColors.red = image.activeSelf;
            }
        }
    }
}
