using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarMovement2D : MonoBehaviour
{
    /*public float movementSpeed;*/

    public float BounceStrength;
    public float AccelerationStrength;
    public float DecelerationStrength;

    private void Start()
    {
        BounceStrength = 3;
        AccelerationStrength = 2;
        DecelerationStrength = 2;
    }
    // Update is called once per frame
    void Update()
    {
        /*Debug.Log("Avatar's horizontal speed" + gameObject.GetComponent<Rigidbody2D>().velocity.x);*/
        /*Debug.Log("Avatar's vertical speed" + gameObject.GetComponent<Rigidbody2D>().velocity.y);*/
        /*if (ActiveColors.goButton == true)
            transform.Translate(Vector2.right * Time.deltaTime * movementSpeed);*/
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
            Debug.Log("Passed through green");
        }
        else if (other.tag == "OrangePlat")
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x / DecelerationStrength, gameObject.GetComponent<Rigidbody2D>().velocity.y / DecelerationStrength);
            Debug.Log("Passed through Red");
        }
        else if (other.tag == "RedPlat")
        {
            Destroy(gameObject);
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
            }
        }
    }
}
