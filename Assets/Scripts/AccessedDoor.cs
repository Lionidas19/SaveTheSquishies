using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessedDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Avatar")
        {
            ActiveButtons.advancebutton = true;
            Destroy(other.gameObject);
        }
    }
}
