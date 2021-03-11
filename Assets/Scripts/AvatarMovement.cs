using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarMovement : MonoBehaviour
{
    public float movementSpeed;

    // Update is called once per frame
    void Update()
    {
        if(ActiveColors.goButton == true)
        transform.Translate(Vector2.right * Time.deltaTime * movementSpeed);
    }
}
