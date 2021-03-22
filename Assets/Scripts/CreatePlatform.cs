using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreatePlatform : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private Vector2 objectStart;
    private Vector2 objectEnd;
    private bool startedPlatform = false;
    public GameObject redPlat, bluePlat, yellowPlat, orangePlat, purplePlat, greenPlat;
    /*public GameObject canvas, StartKnob;

    private GameObject startKnob;*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            /*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag != "Obstacle" || hit.collider.tag != "Avatar")
                {
                    Destroy(hit.collider.gameObject);
                }
            }*/
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit.collider != null) {
                if (hit.collider.tag == "BluePlat" || hit.collider.tag == "YellowPlat" || hit.collider.tag == "RedPlat" || hit.collider.tag == "GreenPlat" || hit.collider.tag == "OrangePlat" || hit.collider.tag == "PurplePlat")
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if (ActiveColors.blue == true || ActiveColors.red == true || ActiveColors.yellow == true)
            {
                /*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                objectStart = ray.origin;
                startedPlatform = true;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null)
                    {
                        Debug.Log("No no no no no no no");
                        startedPlatform = false;
                    }
                }
                else
                {
                    Debug.Log("Clicked");
                    *//*startKnob = Instantiate(StartKnob, canvas.transform);
                    startKnob.transform.localPosition = objectStart;*//*
                }*/

                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                objectStart = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
                Debug.Log("Platform start is (" + objectStart.x + "," + objectStart.y + ")");
                startedPlatform = true;
                if (hit.collider != null)
                {
                    Debug.Log("No no no no no no no");
                    startedPlatform = false;
                }
            }
            else
            {
                Debug.Log("Select a color dumbass");
            }
        }
        /*else if (eventData.button == PointerEventData.InputButton.Right)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag != "Obstacle")
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }*/
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if ((ActiveColors.blue == true || ActiveColors.red == true || ActiveColors.yellow == true) && startedPlatform == true)
            {
                /*Destroy(startKnob);*/
                /*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                objectEnd = ray.origin;
                bool endPlatform = true;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null)
                    {
                        endPlatform = false;
                    }
                    else
                    {
                        Debug.Log("What did I say before?");
                    }
                }
                else if (Physics.Linecast(objectStart, objectEnd, out hit))
                {
                    if (hit.collider != null)
                    {
                        Debug.Log("What are you doing?");
                        endPlatform = false;
                    }
                }
                else
                {
                    Debug.Log("Click Lifted");
                    MakePlatform();
                }*/
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                objectEnd = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
                Debug.Log("Platform end is (" + objectEnd.x + "," + objectEnd.y + ")");
                /*RaycastHit2D line = Physics2D.Linecast(objectStart, objectEnd);*/
                if (Physics2D.Linecast(objectStart, objectEnd))
                {
                    if (hit.collider != null)
                    {
                        Debug.Log("What are you doing?");
                    }
                }
                else
                {
                    Debug.Log("Click Lifted");
                    MakePlatform();
                }
            }
            else
            {
                if (!(ActiveColors.blue == true || ActiveColors.red == true || ActiveColors.yellow == true))
                    Debug.Log("Still no color");
                if (startedPlatform == false)
                    Debug.Log("You messed up the starting point");
            }
        }
    }

    void MakePlatform()
    {
        if(objectStart!= null && objectEnd != null)
        {
            
            if(ActiveColors.blue == true && ActiveColors.red == false && ActiveColors.yellow == false)
            {
                GameObject BluePlatform = Instantiate(bluePlat);
                BluePlatform.name = "BluePlatform";
                BluePlatform.transform.position = new Vector2((objectStart.x + objectEnd.x) / 2, (objectStart.y + objectEnd.y) / 2);
                BluePlatform.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan((objectEnd.y - objectStart.y) / (objectEnd.x - objectStart.x)));
                BluePlatform.transform.localScale = new Vector2(Vector2.Distance(objectStart, objectEnd) / 2, 0.1f);
                BluePlatform.AddComponent(typeof(UnityEngine.AI.NavMeshObstacle));
                /*Renderer platformRenderer = BluePlatform.GetComponent<Renderer>();
                platformRenderer.material.SetColor("_Color", Color.blue);*/

            }
            else if (ActiveColors.blue == false && ActiveColors.red == true && ActiveColors.yellow == false)
            {
                GameObject RedPlatform = Instantiate(redPlat);
                RedPlatform.name = "RedPlatform";
                RedPlatform.transform.position = new Vector2((objectStart.x + objectEnd.x) / 2, (objectStart.y + objectEnd.y) / 2);
                RedPlatform.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan((objectEnd.y - objectStart.y) / (objectEnd.x - objectStart.x)));
                RedPlatform.transform.localScale = new Vector2(Vector2.Distance(objectStart, objectEnd) / 2, 0.1f);
                RedPlatform.AddComponent(typeof(UnityEngine.AI.NavMeshObstacle));
                /*platformRenderer.material.SetColor("_Color", Color.red);*/
            }
            else if (ActiveColors.blue == false && ActiveColors.red == false && ActiveColors.yellow == true)
            {
                GameObject YellowPlatform = Instantiate(yellowPlat);
                YellowPlatform.name = "YellowPlatform";
                YellowPlatform.transform.position = new Vector2((objectStart.x + objectEnd.x) / 2, (objectStart.y + objectEnd.y) / 2);
                YellowPlatform.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan((objectEnd.y - objectStart.y) / (objectEnd.x - objectStart.x)));
                YellowPlatform.transform.localScale = new Vector2(Vector2.Distance(objectStart, objectEnd) / 2, 0.1f);
                /*YellowPlatform.AddComponent(typeof(UnityEngine.AI.NavMeshObstacle));*/
                /*platformRenderer.material.SetColor("_Color", Color.yellow);*/
            }
            else if (ActiveColors.blue == true && ActiveColors.red == true && ActiveColors.yellow == false)
            {
                GameObject PurplePlatform = Instantiate(purplePlat);
                PurplePlatform.name = "PurplePlatform";
                PurplePlatform.transform.position = new Vector2((objectStart.x + objectEnd.x) / 2, (objectStart.y + objectEnd.y) / 2);
                PurplePlatform.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan((objectEnd.y - objectStart.y) / (objectEnd.x - objectStart.x)));
                PurplePlatform.transform.localScale = new Vector2(Vector2.Distance(objectStart, objectEnd) / 2, 0.1f);
                PurplePlatform.AddComponent(typeof(UnityEngine.AI.NavMeshObstacle));
                /*platformRenderer.material.SetColor("_Color", Color.magenta);*/
            }
            else if (ActiveColors.blue == true && ActiveColors.red == false && ActiveColors.yellow == true)
            {
                GameObject GreenPlatform = Instantiate(greenPlat);
                GreenPlatform.name = "GreenPlatform";
                GreenPlatform.transform.position = new Vector2((objectStart.x + objectEnd.x) / 2, (objectStart.y + objectEnd.y) / 2);
                GreenPlatform.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan((objectEnd.y - objectStart.y) / (objectEnd.x - objectStart.x)));
                GreenPlatform.transform.localScale = new Vector2(Vector2.Distance(objectStart, objectEnd) / 2, 0.1f);
                GreenPlatform.AddComponent(typeof(UnityEngine.AI.NavMeshObstacle));
                /*platformRenderer.material.SetColor("_Color", Color.green);*/
            }
            else if (ActiveColors.blue == false && ActiveColors.red == true && ActiveColors.yellow == true)
            {
                GameObject OrangePlatform = Instantiate(orangePlat);
                OrangePlatform.name = "YellowPlatform";
                OrangePlatform.transform.position = new Vector2((objectStart.x + objectEnd.x) / 2, (objectStart.y + objectEnd.y) / 2);
                OrangePlatform.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan((objectEnd.y - objectStart.y) / (objectEnd.x - objectStart.x)));
                OrangePlatform.transform.localScale = new Vector2(Vector2.Distance(objectStart, objectEnd) / 2, 0.1f);
                OrangePlatform.AddComponent(typeof(UnityEngine.AI.NavMeshObstacle));
                /*platformRenderer.material.SetColor("_Color", Color.cyan);*/
            }
        }
    }
}
