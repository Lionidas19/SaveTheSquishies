using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateMovement : MonoBehaviour
{

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

    private string currentState;

    const string PIRATE_IDLE = "Pirate1_Idle";
    const string PIRATE_RUN = "Pirate1_Running";
    const string PIRATE_FALL = "Pirate1_Falling";
    const string PIRATE_FALL_DEATH = "Pirate1_FallDeath";
    const string PIRATE_WALL_DEATH = "Pirate1_WallDeath";
    const string PIRATE_RFF_DEATH = "Pirate1_RedfieldDeath";
    const string PIRATE_ELECTRIC_DEATH = "Pirate1_ElectricDeath";
    const string PIRATE_ATTACK = "Pirate1_Attack";
    const string PIRATE_BOUNCE = "Pirate1_Bouncing";
    const string PIRATE_CEILING_DEATH = "Pirate1_CeilingDeath";

    // Start is called before the first frame update
    void Start()
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
        previousFrameVelocity = currentFrameVelocity;
        currentFrameVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;

        if (previousFrameVelocity.y < -20 && -0.5 < currentFrameVelocity.y && currentFrameVelocity.y < 0.5 && ableToMove != false)
        {
            Debug.Log("y negative to 0");
            ableToMove = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            gameObject.GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            ChangeAnimationState(PIRATE_FALL_DEATH);
        }
        else if (previousFrameVelocity.y > 20 && -0.5 < currentFrameVelocity.y && currentFrameVelocity.y < 0.5 && ableToMove != false)
        {
            ableToMove = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            Destroy(gameObject.GetComponent<Collider2D>());
            ChangeAnimationState(PIRATE_CEILING_DEATH);
        }
        else if (previousFrameVelocity.x < -20 && -0.5 < currentFrameVelocity.x && currentFrameVelocity.x < 0.5 && ableToMove != false)
        { 
            Debug.Log("x negative to 0");
            ableToMove = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            //CHRIS
            //gameObject.GetComponent<Collider2D>().enabled = false;
            //CHRIS commented out so it does not hang in mid air
            //Destroy(gameObject.GetComponent<Rigidbody2D>());
            ChangeAnimationState(PIRATE_WALL_DEATH);
        }
        else if (previousFrameVelocity.x > 20 && -0.5 < currentFrameVelocity.x && currentFrameVelocity.x < 0.5 && ableToMove != false)
        {
            Debug.Log("x positive to 0");
            ableToMove = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            //CHRIS
            //gameObject.GetComponent<Collider2D>().enabled = false;
            //CHRIS commented out so it does not hang in mid air
            //Destroy(gameObject.GetComponent<Rigidbody2D>());
            ChangeAnimationState(PIRATE_WALL_DEATH);
        }
//CHRIS added abletomove != false so it does not trigger when already dead
        if (gameObject.GetComponent<Rigidbody2D>().velocity.y <= -2 && ableToMove != false)
        {
            ChangeAnimationState(PIRATE_FALL);
        }
        /*else
        {
            animator.SetBool("isFalling", false);
        }*/

        if (gameObject.GetComponent<Rigidbody2D>().velocity.y >= 3)
        {
            ChangeAnimationState(PIRATE_BOUNCE);
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
          //CHRIS commented out so it does not hang in mid air
          //Destroy(gameObject.GetComponent<Rigidbody2D>());
          //CHRIS
          //gameObject.GetComponent<Collider2D>().enabled = false;
            ChangeAnimationState(PIRATE_RFF_DEATH);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "ElecPlat" && ableToMove != false)
        {
            ableToMove = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            //CHRIS commented out so dead squishy doesn't hang in mid air and fall through platforms
            //Destroy(gameObject.GetComponent<Rigidbody2D>());
            //gameObject.GetComponent<Collider2D>().enabled = false;
            ChangeAnimationState(PIRATE_ELECTRIC_DEATH);
        }
        if (collision.collider.tag == "Avatar")
        {
            ableToMove = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (ActiveButtons.goButton == true && ableToMove == true)
        {
            if (gameObject.GetComponent<Rigidbody2D>().velocity.x < 5)
                gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * -17);
            else
                gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * 0);
            if (gameObject.GetComponent<Rigidbody2D>().velocity.x > 0.1)
            {
                if (soundPlaying == false)
                {
                    Debug.Log("Sound Started");
                    soundPlaying = true;
                    footstepSource.Play();
                }
            }
            else
            {
                ChangeAnimationState(PIRATE_RUN);
                if (soundPlaying == true)
                {
                    soundPlaying = false;
                    footstepSource.Stop();
                }
            }
        }
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
