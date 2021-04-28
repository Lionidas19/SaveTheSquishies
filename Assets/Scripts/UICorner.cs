using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICorner : MonoBehaviour
{
    public float velocity;

    private Vector2 TopLeftCornerStart = new Vector2(2.5f, -2.5f);
    private Vector2 TopLeftCornerEnd = new Vector2(-27f, 13f);

    private Vector2 BottomRightCornerStart = new Vector2(-2.5f, 2.5f);
    private Vector2 BottomRightCornerEnd = new Vector2(27f, -13f);

    private bool TopLeftCornerArrived = false;
    private bool BottomRightCornerArrived = false;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name == "Panel_1")
            gameObject.transform.position = TopLeftCornerStart;


        if (gameObject.name == "Panel_2")
            gameObject.transform.position = BottomRightCornerStart;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.name == "Panel_1")
        {
            if (TopLeftCornerArrived == false)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, TopLeftCornerEnd, velocity);
            }
            if(gameObject.transform.position.x == TopLeftCornerEnd.x)
            {
                TopLeftCornerArrived = true;
            }
        }
        
        if (gameObject.name == "Panel_2")
        {
            if (BottomRightCornerArrived == false)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, BottomRightCornerEnd, velocity);
            }
            if (gameObject.transform.position.x == BottomRightCornerEnd.x)
            {
                BottomRightCornerArrived = true;
            }
        }
    }
}
