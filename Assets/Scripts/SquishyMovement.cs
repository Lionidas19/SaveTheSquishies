using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquishyMovement : MonoBehaviour
{
    public float BounceStrength;
    public float AccelerationStrength;
    public float DecelerationStrength;
    public float MaximumSpeed;

    private bool inContactWithFirstPurplePlatform;
    private bool teleported;

    private bool soundPlaying;

    private bool ableToMove;

    private Animator animator;

    public AudioSource footstepSource;

    //Variables to store the squishy's velocity each currnet and previous frame 
    private Vector2 previousFrameVelocity;
    private Vector2 currentFrameVelocity;

    private string currentState;

    //Animation names for the Squishy
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

    // Start is called before the first frame update
    void Start()
    {
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

        if (previousFrameVelocity.y < -10 && -0.5 < currentFrameVelocity.y && currentFrameVelocity.y < 0.5 && ableToMove == true)
        {
            Debug.Log("y negative to 0");
            ableToMove = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            ChangeAnimationState(SQUISHY_FALL_DEATH);
        }
        else if (previousFrameVelocity.y > 10 && -0.5 < currentFrameVelocity.y && currentFrameVelocity.y < 0.5)
        {
            Destroy(gameObject);
        }
        else if (previousFrameVelocity.x < -10 && -0.5 < currentFrameVelocity.x && currentFrameVelocity.x < 0.5)
        {
            Debug.Log("x negative to 0");
        }
        else if (previousFrameVelocity.x > 10 && -0.5 < currentFrameVelocity.x && currentFrameVelocity.x < 0.5)
        {
            Debug.Log("x positive to 0");
            ableToMove = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            ChangeAnimationState(SQUISHY_WALL_DEATH);
        }
        if (gameObject.GetComponent<Rigidbody2D>().velocity.y <= -2 /*&& standingOnSomething == false*/ && ableToMove == true)
        {
            ChangeAnimationState(SQUISHY_FALL);
        }

        if (gameObject.GetComponent<Rigidbody2D>().velocity.y >= 3 /*&& standingOnSomething == false*/)
        {
            ChangeAnimationState(SQUISHY_BOUNCE);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered collider " + other.tag);
        if (other.tag == "YellowPlat")
        {
            if (gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, -gameObject.GetComponent<Rigidbody2D>().velocity.y + BounceStrength);
            }
        }
        //Double the squishy's directional velocities unless they exceed the Maximum Velocity allowed, in which case the directional velocity that exceeds it gets set to the same amount
        else if (other.tag == "GreenPlat")
        {
            if (Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x) * AccelerationStrength <= MaximumSpeed && Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.y) * AccelerationStrength <= MaximumSpeed)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x * AccelerationStrength, gameObject.GetComponent<Rigidbody2D>().velocity.y * AccelerationStrength);
            }
            else
            {
                float xSpeed;
                float ySpeed;

                if (Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x) * AccelerationStrength > MaximumSpeed)
                {
                    if (gameObject.GetComponent<Rigidbody2D>().velocity.x >= 0)
                    {
                        xSpeed = MaximumSpeed;
                    }
                    else
                    {
                        xSpeed = -MaximumSpeed;
                    }
                }
                else
                {
                    xSpeed = gameObject.GetComponent<Rigidbody2D>().velocity.x * AccelerationStrength;
                }

                if (Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.y) * AccelerationStrength > MaximumSpeed)
                {
                    if (gameObject.GetComponent<Rigidbody2D>().velocity.y >= 0)
                    {
                        ySpeed = MaximumSpeed;
                    }
                    else
                    {
                        ySpeed = -MaximumSpeed;
                    }
                }
                else
                {
                    ySpeed = gameObject.GetComponent<Rigidbody2D>().velocity.x * AccelerationStrength;
                }

                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed, ySpeed);

            }
        }
        else if (other.tag == "OrangePlat")
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x / DecelerationStrength, gameObject.GetComponent<Rigidbody2D>().velocity.y / DecelerationStrength);
        }
        else if (other.tag == "RedPlat")
        {
            ableToMove = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            ChangeAnimationState(SQUISHY_RFF_DEATH);
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

        if (other.tag == "Door")
        {
            ActiveButtons.advancebutton = true;
            ableToMove = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            Animator doorAnim = other.gameObject.GetComponent<Animator>();
            doorAnim.Play("Exit_Hatch_close1");
        }
    }

    public void AvatarPirateAttackDeath()
    {
        ChangeAnimationState(SQUISHY_PIRATE_DEATH);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "ElecPlat")
        {
            ableToMove = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            ChangeAnimationState(SQUISHY_ELECTRIC_DEATH);
        }

        if (collision.collider.tag == "Pirate")
        {
            ableToMove = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            Animator PirateAttack = collision.gameObject.GetComponent<Animator>();
            PirateAttack.Play("Pirate1_Attack");
            Invoke("AvatarPirateAttackDeath", PirateAttack.GetCurrentAnimatorStateInfo(0).length / 2);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Obstacle" || collision.collider.tag == "BluePlat")
        {
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
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
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
