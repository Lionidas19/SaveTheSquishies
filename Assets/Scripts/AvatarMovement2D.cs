using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarMovement2D : MonoBehaviour
{
    /*public float movementSpeed;*/

    public float BounceStrength;
    public float AccelerationStrength;
    public float DecelerationStrength;

    private bool inContactWithPurplePlatform;
    private bool allowedToTeleport;

    private bool soundPlaying;

    private Animator animator;

    public AudioSource footstepSource;

    private void Start()
    {
        BounceStrength = 3;
        AccelerationStrength = 2;
        DecelerationStrength = 2;
        inContactWithPurplePlatform = false;
        allowedToTeleport = true;
        soundPlaying = false;
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        /*Debug.Log("Avatar's horizontal speed" + gameObject.GetComponent<Rigidbody2D>().velocity.x);*/
        /*Debug.Log("Avatar's vertical speed" + gameObject.GetComponent<Rigidbody2D>().velocity.y);*/
        /*if (ActiveColors.goButton == true)
            transform.Translate(Vector2.right * Time.deltaTime * movementSpeed);*/
        if (gameObject.GetComponent<Rigidbody2D>().velocity.y <= -1)
        {
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isFalling", false);
        }

        if (gameObject.GetComponent<Rigidbody2D>().velocity.y >= 1)
        {
            animator.SetBool("isBouncing", true);
        }
        else
        {
            animator.SetBool("isBouncing", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "YellowPlat")
        {
            if (gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, -gameObject.GetComponent<Rigidbody2D>().velocity.y + BounceStrength);
            }/*
            else
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, gameObject.GetComponent<Rigidbody2D>().velocity.y + 5);
            }*/

        }
        else if (other.tag == "GreenPlat")
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x * AccelerationStrength, gameObject.GetComponent<Rigidbody2D>().velocity.y * AccelerationStrength);
            /*Debug.Log("Passed through green");*/
        }
        else if (other.tag == "OrangePlat")
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x / DecelerationStrength, gameObject.GetComponent<Rigidbody2D>().velocity.y / DecelerationStrength);
            /*Debug.Log("Passed through Red");*/
        }
        else if (other.tag == "RedPlat")
        {
            Destroy(gameObject);
        }
        else if (other.tag == "PurplePlat")
        {
            if(inContactWithPurplePlatform == false)
            {
                inContactWithPurplePlatform = true;
                if(allowedToTeleport == true)
                {
                    allowedToTeleport = false;
                    gameObject.transform.position = other.gameObject.GetComponent<PurpleTeleport>().getTeleportPosition();
                }
                else
                {
                    allowedToTeleport = true;
                }
            }
            else
            {
                inContactWithPurplePlatform = false;
                allowedToTeleport = true;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        /*Debug.Log("Collided with " + collision.gameObject.name);*/
        if (collision.collider.tag == "Obstacle" || collision.collider.tag == "BluePlat")
        {
            if (ActiveColors.goButton == true)
            {
                if (gameObject.GetComponent<Rigidbody2D>().velocity.x < 5)
                    gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * 10);
                else
                    gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * 0);
                if (gameObject.GetComponent<Rigidbody2D>().velocity.x > 0.1)
                {
                    animator.SetBool("isRunning", true);
                    if(soundPlaying == false)
                    {
                        Debug.Log("Sound Started");
                        soundPlaying = true;
                        footstepSource.Play();
                    }
                }
                else
                {
                    animator.SetBool("isRunning", false);
                    if (soundPlaying == true)
                    {
                        soundPlaying = false;
                        footstepSource.Stop();
                    } 
                }   
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        animator.SetBool("isRunning", false);
        if (soundPlaying == true)
        {
            soundPlaying = false;
            footstepSource.Stop();
        }
    }
}
