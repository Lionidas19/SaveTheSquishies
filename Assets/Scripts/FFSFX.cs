using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FFSFX : MonoBehaviour
{
    public AudioSource audioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioSource.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(gameObject.tag == "BluePlat")
        {
            audioSource.Play();
        }
    }
}
