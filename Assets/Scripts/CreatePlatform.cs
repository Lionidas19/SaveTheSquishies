using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreatePlatform : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private Vector2 objectStart;
    private Vector2 objectEnd;
    private bool startedPlatform = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag != "Obstacle" || hit.collider.tag != "Avatar")
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
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
            GameObject platform = GameObject.CreatePrimitive(PrimitiveType.Cube);
            platform.transform.position = new Vector2((objectStart.x + objectEnd.x) / 2, (objectStart.y + objectEnd.y) / 2);
            platform.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan((objectEnd.y - objectStart.y) / (objectEnd.x - objectStart.x)));
            platform.transform.localScale = new Vector3(Vector3.Distance(objectStart, objectEnd), 0.1f, 10f);
            platform.AddComponent(typeof(UnityEngine.AI.NavMeshObstacle));
            Renderer platformRenderer = platform.GetComponent<Renderer>();
            if(ActiveColors.blue == true && ActiveColors.red == false && ActiveColors.yellow == false)
            {
                platformRenderer.material.SetColor("_Color", Color.blue);
            }
            else if (ActiveColors.blue == false && ActiveColors.red == true && ActiveColors.yellow == false)
            {
                platformRenderer.material.SetColor("_Color", Color.red);
            }
            else if (ActiveColors.blue == false && ActiveColors.red == false && ActiveColors.yellow == true)
            {
                platformRenderer.material.SetColor("_Color", Color.yellow);
            }
            else if (ActiveColors.blue == true && ActiveColors.red == true && ActiveColors.yellow == false)
            {
                platformRenderer.material.SetColor("_Color", Color.magenta);
            }
            else if (ActiveColors.blue == true && ActiveColors.red == false && ActiveColors.yellow == true)
            {
                platformRenderer.material.SetColor("_Color", Color.green);
            }
            else if (ActiveColors.blue == false && ActiveColors.red == true && ActiveColors.yellow == true)
            {
                platformRenderer.material.SetColor("_Color", Color.cyan);
            }
        }
    }
}
