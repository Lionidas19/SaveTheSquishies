using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarMovement2D : MonoBehaviour
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
    const string SQUISHY_CEILING_DEATH = "Squishy1_Death_Ceiling";

    private void Start()
    {
        BounceStrength = 3;
        AccelerationStrength = 1.5f;
        DecelerationStrength = 2;
        MaximumSpeed = 10;

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
        /*Debug.Log("Avatar's horizontal speed" + gameObject.GetComponent<Rigidbody2D>().velocity.x);
        Debug.Log("Avatar's vertical speed" + gameObject.GetComponent<Rigidbody2D>().velocity.y);*/
        previousFrameVelocity = currentFrameVelocity;
        currentFrameVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        
        //If the squishy is falling with a vertical velocity exceeding -10 and they hit the ground they die
        if (previousFrameVelocity.y < -10 && -0.5 < currentFrameVelocity.y && currentFrameVelocity.y < 0.5 && ableToMove != false) 
        {
            ableToMove = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            ChangeAnimationState(SQUISHY_FALL_DEATH);
        }
        else if (previousFrameVelocity.y > 10 && -0.5 < currentFrameVelocity.y && currentFrameVelocity.y < 0.5 && ableToMove != false)
        {
            ableToMove = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            Destroy(gameObject.GetComponent<Collider2D>());
            ChangeAnimationState(SQUISHY_CEILING_DEATH);
        }
        else if (previousFrameVelocity.x < -10 && -0.5 < currentFrameVelocity.x && currentFrameVelocity.x < 0.5 && ableToMove != false)
        {
            Debug.Log("x negative to 0");
        }
        else if (previousFrameVelocity.x > 10 && -0.5 < currentFrameVelocity.x && currentFrameVelocity.x < 0.5 && ableToMove != false)
        {
            Debug.Log("x positive to 0");
            ableToMove = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            ChangeAnimationState(SQUISHY_WALL_DEATH);
        }

        if (gameObject.GetComponent<Rigidbody2D>().velocity.y <= -2 && ableToMove == true)
        {
            ChangeAnimationState(SQUISHY_FALL);
        }

        if (gameObject.GetComponent<Rigidbody2D>().velocity.y >= 3)
        {
            ChangeAnimationState(SQUISHY_BOUNCE);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Entered collider " + other.tag);
        //Bounce platform
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
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x * AccelerationStrength, gameObject.GetComponent<Rigidbody2D>().velocity.y * AccelerationStrength);

            /*Vector2 velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
            float x = gameObject.GetComponent<Rigidbody2D>().velocity.x;
            float y = gameObject.GetComponent<Rigidbody2D>().velocity.y;


            if (Mathf.Sqrt((x * x) + (y * y)) * AccelerationStrength < MaximumSpeed){
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sqrt((x * x)) * AccelerationStrength, gameObject.GetComponent<Rigidbody2D>().velocity.y * AccelerationStrength);
            }*/
            /*if(Mathf.Abs( gameObject.GetComponent<Rigidbody2D>().velocity.x ) * AccelerationStrength <= MaximumSpeed && Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.y ) * AccelerationStrength <= MaximumSpeed)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x * AccelerationStrength, gameObject.GetComponent<Rigidbody2D>().velocity.y * AccelerationStrength);
            }
            else
            {
                float xSpeed;
                float ySpeed;

                if(Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x) * AccelerationStrength > MaximumSpeed)
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
*/
            //            }          
        }
        else if (other.tag == "OrangePlat")
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x / DecelerationStrength, gameObject.GetComponent<Rigidbody2D>().velocity.y / DecelerationStrength);
        }
        else if (other.tag == "RedPlat")
        {
            ableToMove = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            //CHRIS commented out so dead squishy doesn't hang in mid air
            //CHRIS Destroy(gameObject.GetComponent<Rigidbody2D>());
            //CHRIS gameObject.GetComponent<Collider2D>().enabled = false;
            ChangeAnimationState(SQUISHY_RFF_DEATH);
            /*animator.PlayInFixedTime("Squishy1_Death_RedFF", 1, 1f);*/
            /*Destroy(gameObject);*/
        }
        else if (other.tag == "PurplePlat") 
        {
            if (inContactWithFirstPurplePlatform == false && teleported == false)
            {

                gameObject.transform.position = other.gameObject.GetComponent<PurpleTeleport>().getTeleportPosition();
                inContactWithFirstPurplePlatform = true;
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
            ChangeAnimationState(SQUISHY_IDLE);
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
        if(collision.collider.tag == "ElecPlat" && ableToMove != false)
        {
            ableToMove = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            //CHRIS commented out so dead squishy doesn't hang in mid air
            //Destroy(gameObject.GetComponent<Rigidbody2D>());
            //CHRIS
            //gameObject.GetComponent<Collider2D>().enabled = false;
            ChangeAnimationState(SQUISHY_ELECTRIC_DEATH);
        }

        if (collision.collider.tag == "Pirate" && ableToMove != false)
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
 //                   if (soundPlaying == false)
 //                   {
 //                       Debug.Log("Sound Started");
 //                       soundPlaying = true;
 //                       footstepSource.Play();
 //                   }
                }
                else
                {
                ChangeAnimationState(SQUISHY_IDLE);
 //                   if (soundPlaying == true)
 //                   {
 //                       soundPlaying = false;
 //                       footstepSource.Stop();
 //                   }
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
 //       if (soundPlaying == true)
 //       {
 //           soundPlaying = false;
 //           footstepSource.Stop();
 //       }
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}
