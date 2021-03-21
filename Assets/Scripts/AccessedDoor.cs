using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessedDoor : MonoBehaviour
{
    public GameObject advanceButton;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Avatar")
        {
            Destroy(advanceButton);
            Destroy(other.gameObject);
        }
    }
}
