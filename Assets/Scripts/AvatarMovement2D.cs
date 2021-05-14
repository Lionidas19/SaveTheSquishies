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

    /*private bool standingOnSomething;*/

    private Animator animator;

    public AudioSource footstepSource;

    private Vector2 previousFrameVelocity;
    private Vector2 currentFrameVelocity;

    private string currentState;

    const string SQUISHY_IDLE = "Squishy1_Idle";
    const string SQUISHY_RUN = "Squishy1_Running";
    const string SQUISHY_FALL = "Squishy1_Falling";
    const string SQUISHY_BOUNCE = "Squishy1_Bouncing";
    const string SQUISHY_FALL_DEATH = "Squishy1_Death_Fall";
    const string SQUISHY_WALL_DEATH = "Squishy1_Death_wall";
    const string SQUISHY_RFF_DEATH = "Squishy1_Death_RedFF";
    const string SQUISHY_PIRATE_DEATH = "Squishy1_Death_pirate";
    const string SQUISHY_ELECTRIC_DEATH = "Squishy1_Death_Electricity";
    const string SQUISHY_SHOT = "Squishy1_Death_Shot";

    private void Start()
    {
        BounceStrength = 3;
        AccelerationStrength = 2;
        DecelerationStrength = 2;

        inContactWithFirstPurplePlatform = false;
        teleported = false;

        soundPlaying = false;

        ableToMove = true;

        /*standingOnSomething = false;*/

        animator = GetComponent<Animator>();
        previousFrameVelocity = Vector2.zero;
        currentFrameVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Go active? " + ActiveButtons.goButton);
        /*Debug.Log("Avatar's horizontal speed" + gameObject.GetComponent<Rigidbody2D>().velocity.x);*/
        /*Debug.Log("Avatar's vertical speed" + gameObject.GetComponent<Rigidbody2D>().velocity.y);*/
        /*if (ActiveColors.goButton == true)
            transform.Translate(Vector2.right * Time.deltaTime * movementSpeed);*/
        previousFrameVelocity = currentFrameVelocity;
        currentFrameVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        /*Debug.Log("Avatar's current speed is " + gameObject.GetComponent<Rigidbody2D>().velocity.y);*/
        
        if (previousFrameVelocity.y < -10 && -0.5 < currentFrameVelocity.y && currentFrameVelocity.y < 0.5)
        {
            Debug.Log("y negative to 0");
            ableToMove = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            gameObject.GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            ChangeAnimationState(SQUISHY_FALL_DEATH);
        }
        else if (previousFrameVelocity.y > 10 && -0.5 < currentFrameVelocity.y && currentFrameVelocity.y < 0.5)
        {
            Destroy(gameObject);
        }
        else if (previousFrameVelocity.x < -10 && -0.5 < currentFrameVelocity.x && currentFrameVelocity.x < 0.5)
        {
            /*ableToMove = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            gameObject.GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            animator.SetBool("IsSmashing", true);*/
            Debug.Log("x negative to 0");
        }
        else if (previousFrameVelocity.x > 10 && -0.5 < currentFrameVelocity.x && currentFrameVelocity.x < 0.5)
        {
            Debug.Log("x positive to 0");
            ableToMove = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            gameObject.GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            ChangeAnimationState(SQUISHY_WALL_DEATH);
        }

        if (gameObject.GetComponent<Rigidbody2D>().velocity.y <= -2 /*&& standingOnSomething == false*/)
        {
            ChangeAnimationState(SQUISHY_FALL);
        }
        /*else
        {
            animator.SetBool("isFalling", false);
        }*/

        if (gameObject.GetComponent<Rigidbody2D>().velocity.y >= 3 /*&& standingOnSomething == false*/)
        {
            ChangeAnimationState(SQUISHY_BOUNCE);
        }
        /*else
        {
            animator.SetBool("isBouncing", false);
        }*/
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("IDK " + other.gameObject.name);
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
            ChangeAnimationState(SQUISHY_RFF_DEATH);
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

        if(other.tag == "Door")
        {
            ActiveButtons.advancebutton = true;
            ableToMove = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            Animator doorAnim = other.gameObject.GetComponent<Animator>();
            doorAnim.Play("Exit_Hatch_close1");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
       // Debug.Log("In contact with somwthind");
        //standingOnSomething = true;
        /*Debug.Log("Collided with " + collision.gameObject.name);*/
        /*if (collision.collider.tag == "Obstacle" || collision.collider.tag == "BluePlat")
        {*/
        if (ActiveButtons.goButton == true && ableToMove == true)
            {
                if (gameObject.GetComponent<Rigidbody2D>().velocity.x < 5)
                    gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * 17);
                else
                    gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * 0);
                if (gameObject.GetComponent<Rigidbody2D>().velocity.x > 0.1)
                {
                    ChangeAnimationState(SQUISHY_RUN);
                    if (soundPlaying == false)
                    {
                        Debug.Log("Sound Started");
                        soundPlaying = true;
                        footstepSource.Play();
                    }
                }
                else
                {
                ChangeAnimationState(SQUISHY_IDLE);
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
        //standingOnSomething = false;
        //Debug.Log("Exited collision");
        /*animator.SetBool("isRunning", false);*/
        if (soundPlaying == true)
        {
            soundPlaying = false;
            footstepSource.Stop();
        }
    }
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}
