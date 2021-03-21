using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarMovement : MonoBehaviour
{
    public float movementSpeed;

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Avatar's horizontal speed" + gameObject.GetComponent<Rigidbody>().velocity.x);
        /*Debug.Log("Avatar's vertical speed" + gameObject.GetComponent<Rigidbody>().velocity.y);*/
        /*if (ActiveColors.goButton == true)
            transform.Translate(Vector2.right * Time.deltaTime * movementSpeed);*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "YellowPlat")
        {
            if (gameObject.GetComponent<Rigidbody>().velocity.y <= 0)
            {
                gameObject.GetComponent<Rigidbody>().velocity = new Vector2(gameObject.GetComponent<Rigidbody>().velocity.x, -gameObject.GetComponent<Rigidbody>().velocity.y + 1);
            }
            else
            {
                gameObject.GetComponent<Rigidbody>().velocity = new Vector2(gameObject.GetComponent<Rigidbody>().velocity.x, gameObject.GetComponent<Rigidbody>().velocity.y + 5);
            }
            
        }
        else if(other.tag == "GreenPlat")
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector2(gameObject.GetComponent<Rigidbody>().velocity.x * 2, gameObject.GetComponent<Rigidbody>().velocity.y * 2);
            Debug.Log("Passed through green");
        }
        else if (other.tag == "OrangePlat")
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector2(0, 0);
            Debug.Log("Passed through Red");
        }
        else if(other.tag == "RedPlat")
        {
            Destroy(gameObject);
        }

        if(other.tag == "Door")
        {
            Destroy(gameObject);
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        
    }*/

    private void OnCollisionStay(Collision collision)
    {
        if(collision.collider.tag != "Untagged")
        {
            if (ActiveColors.goButton == true)
            {
                if(gameObject.GetComponent<Rigidbody>().velocity.x < 5)
                    gameObject.GetComponent<Rigidbody>().AddForce(transform.right * 10);
                else
                    gameObject.GetComponent<Rigidbody>().AddForce(transform.right * 0);
            }
        }
        
            /*transform.Translate(Vector2.right * Time.deltaTime * movementSpeed);*/

    }
}
