using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarMovement2D : MonoBehaviour
{
    /*public float movementSpeed;*/

    public float BounceStrength;
    public float AccelerationStrength;
    public float DecelerationStrength;

    private bool inContactWithFirstPurplePlatform;
    private bool teleported;

    private bool soundPlaying;

    private bool ableToMove;

    private Animator animator;

    public AudioSource footstepSource;

    private Vector2 previousFrameVelocity;
    private Vector2 currentFrameVelocity;

    private void Start()
    {
        BounceStrength = 3;
        AccelerationStrength = 2;
        DecelerationStrength = 2;

        inContactWithFirstPurplePlatform = false;
        teleported = false;

        soundPlaying = false;

        ableToMove = true;

        animator = GetComponent<Animator>();
        previousFrameVelocity = Vector2.zero;
        currentFrameVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("Go active? " + ActiveButtons.goButton);
        /*Debug.Log("Avatar's horizontal speed" + gameObject.GetComponent<Rigidbody2D>().velocity.x);*/
        /*Debug.Log("Avatar's vertical speed" + gameObject.GetComponent<Rigidbody2D>().velocity.y);*/
        /*if (ActiveColors.goButton == true)
            transform.Translate(Vector2.right * Time.deltaTime * movementSpeed);*/
        previousFrameVelocity = currentFrameVelocity;
        currentFrameVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        /*Debug.Log("Avatar's current speed is " + gameObject.GetComponent<Rigidbody2D>().velocity.y);*/
        
        if (previousFrameVelocity.y < -10 && -0.5 < currentFrameVelocity.y && currentFrameVelocity.y < 0.5)
        {
            ableToMove = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            gameObject.GetComponent<Collider2D>().enabled = false;
            animator.SetBool("IsSplattering", true);
        }
        else if (previousFrameVelocity.y > 10 && -0.5 < currentFrameVelocity.y && currentFrameVelocity.y < 0.5)
        {
            Destroy(gameObject);
        }
        else if (previousFrameVelocity.x < -10 && -0.5 < currentFrameVelocity.x && currentFrameVelocity.x < 0.5)
        {
            Destroy(gameObject);
        }
        else if (previousFrameVelocity.x > 10 && -0.5 < currentFrameVelocity.x && currentFrameVelocity.x < 0.5)
        {
            Destroy(gameObject);
        }

        if (gameObject.GetComponent<Rigidbody2D>().velocity.y <= -2)
        {
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isFalling", false);
        }

        if (gameObject.GetComponent<Rigidbody2D>().velocity.y >= 2)
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
        Debug.Log("IDK " + other.gameObject.name);
        if (other.tag == "YellowPlat")
        {
            if (gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, -gameObject.GetComponent<Rigidbody2D>().velocity.y + BounceStrength);
            }
        }
        else if (other.tag == "GreenPlat")
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x * AccelerationStrength, gameObject.GetComponent<Rigidbody2D>().velocity.y * AccelerationStrength);
        }
        else if (other.tag == "OrangePlat")
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x / DecelerationStrength, gameObject.GetComponent<Rigidbody2D>().velocity.y / DecelerationStrength);
        }
        else if (other.tag == "RedPlat")
        {
            ableToMove = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            gameObject.GetComponent<Collider2D>().enabled = false;
            animator.SetBool("IsDisintegrating", true);
            /*animator.PlayInFixedTime("Squishy1_Death_RedFF", 1, 1f);*/
            /*Destroy(gameObject);*/
        }
        else if (other.tag == "PurplePlat")
        {
            if (inContactWithFirstPurplePlatform == false && teleported == false)
            {
                inContactWithFirstPurplePlatform = true;
                gameObject.transform.position = other.gameObject.GetComponent<PurpleTeleport>().getTeleportPosition();
            }
            else if (inContactWithFirstPurplePlatform == true && teleported == false)
            {
                teleported = true;
            }
            else if (inContactWithFirstPurplePlatform == true && teleported == true)
            {
                inContactWithFirstPurplePlatform = false;
            }
            else if (inContactWithFirstPurplePlatform == false && teleported == true)
            {
                teleported = false;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("In contact with somwthind");
        /*Debug.Log("Collided with " + collision.gameObject.name);*/
        /*if (collision.collider.tag == "Obstacle" || collision.collider.tag == "BluePlat")
        {*/
        if (ActiveButtons.goButton == true && ableToMove == true)
            {
                if (gameObject.GetComponent<Rigidbody2D>().velocity.x < 5)
                    gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * 10);
                else
                    gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * 0);
                if (gameObject.GetComponent<Rigidbody2D>().velocity.x > 0.1)
                {
                    animator.SetBool("isRunning", true);
                    if (soundPlaying == false)
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
        //}
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Exited collision");
        animator.SetBool("isRunning", false);
        if (soundPlaying == true)
        {
            soundPlaying = false;
            footstepSource.Stop();
        }
    }
}
