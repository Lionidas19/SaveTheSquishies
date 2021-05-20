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

    private GameObject purplePlatStart, purplePlatEnd;

    public Slider energySlider;

    private LineRenderer lineRenderer;

    /*public GameObject canvas, StartKnob;

    private GameObject startKnob;*/

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.SetWidth(0.1f, 0.1f);
        lineRenderer.enabled = false;

        energySlider.value = energySlider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if(ActiveButtons.goButton == false)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {
                    if (hit.collider.tag == "BluePlat" || hit.collider.tag == "YellowPlat" || hit.collider.tag == "RedPlat" || hit.collider.tag == "GreenPlat" || hit.collider.tag == "OrangePlat")
                    {
                        energySlider.value += hit.collider.gameObject.transform.localScale.x * 2;
                        Destroy(hit.collider.gameObject);
                    }
                    else if (hit.collider.tag == "PurplePlat")
                    {
                        energySlider.value += hit.collider.gameObject.transform.localScale.y * 4 + Mathf.Abs(Vector2.Distance(hit.collider.gameObject.transform.localPosition, hit.collider.gameObject.GetComponent<PurpleTeleport>().getTeleportPosition()));
                        RaycastHit2D findOtherPurple = Physics2D.Raycast(hit.collider.gameObject.GetComponent<PurpleTeleport>().getTeleportPosition(), Vector2.zero);
                        Destroy(findOtherPurple.collider.gameObject);
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (ActiveButtons.goButton == false) 
            {
                lineRenderer.SetPosition(0, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0));
                lineRenderer.SetVertexCount(1);
                lineRenderer.enabled = true;
            }   
        }
        else if (Input.GetMouseButton(0))
        {
            if (ActiveButtons.goButton == false)
            {
                lineRenderer.SetVertexCount(2);
                lineRenderer.SetPosition(1, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0));
            }                
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (ActiveButtons.goButton == false)
                lineRenderer.enabled = false;
        }
    }

    //First click
    public void OnPointerDown(PointerEventData eventData)
    {
        if (ActiveButtons.goButton == false)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {

                if (ActiveButtons.blue == true || ActiveButtons.red == true || ActiveButtons.yellow == true)
                {
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                    //The starting point of the new forcefield
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
        }   
    }

    //Letting go of the left click
    public void OnPointerUp(PointerEventData eventData)
    {
        if (ActiveButtons.goButton == false)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if ((ActiveButtons.blue == true || ActiveButtons.red == true || ActiveButtons.yellow == true) && startedPlatform == true)
                {
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                    objectEnd = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
                    Debug.Log("Platform end is (" + objectEnd.x + "," + objectEnd.y + ")");
                    /*RaycastHit2D line = Physics2D.Linecast(objectStart, objectEnd);*/
                    /*if (ActiveButtons.blue == true && ActiveButtons.red == true && ActiveButtons.yellow == false)
                    {
                        MakePlatform();
                        energySlider.value -= Mathf.Abs(Vector2.Distance(objectStart, objectEnd));
                    }*/
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
                        if (ActiveButtons.blue == true && ActiveButtons.red == true && ActiveButtons.yellow == false)
                        {
                            if (energySlider.value >= Mathf.Abs(Vector2.Distance(objectStart, objectEnd)) + Mathf.Abs(new Vector2(0.1f, 2f).y * 2 + new Vector2(0.1f, 2f).y * 2))
                            {
                                MakePlatform();
                                energySlider.value -= Mathf.Abs(Vector2.Distance(objectStart, objectEnd));
                            }
                        }
                        else
                        {
                            if (energySlider.value >= Mathf.Abs(Vector2.Distance(objectStart, objectEnd)))
                            {
                                MakePlatform();
                                energySlider.value -= Mathf.Abs(Vector2.Distance(objectStart, objectEnd));
                            }
                        }
                    }
                }
                else
                {
                    if (!(ActiveButtons.blue == true || ActiveButtons.red == true || ActiveButtons.yellow == true))
                        Debug.Log("Still no color");
                    if (startedPlatform == false)
                        Debug.Log("You messed up the starting point");
                }
            }
            objectStart = new Vector2(100, 100);
            objectEnd = new Vector2(100, 100);
        }
    }

    void MakePlatform()
    {
        if(objectStart!= null && objectEnd != null && Vector2.Distance(objectStart, objectEnd) >= 0.3)
        {
            
            if(ActiveButtons.blue == true && ActiveButtons.red == false && ActiveButtons.yellow == false)
            {
                GameObject BluePlatform = Instantiate(bluePlat);
                BluePlatform.name = "BluePlatform";
                BluePlatform.transform.position = new Vector2((objectStart.x + objectEnd.x) / 2, (objectStart.y + objectEnd.y) / 2);
                BluePlatform.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan((objectEnd.y - objectStart.y) / (objectEnd.x - objectStart.x)));
                BluePlatform.transform.localScale = new Vector2(Vector2.Distance(objectStart, objectEnd) / 2, 0.1f);
            }
            else if (ActiveButtons.blue == false && ActiveButtons.red == true && ActiveButtons.yellow == false)
            {
                GameObject RedPlatform = Instantiate(redPlat);
                RedPlatform.name = "RedPlatform";
                RedPlatform.transform.position = new Vector2((objectStart.x + objectEnd.x) / 2, (objectStart.y + objectEnd.y) / 2);
                RedPlatform.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan((objectEnd.y - objectStart.y) / (objectEnd.x - objectStart.x)));
                RedPlatform.transform.localScale = new Vector2(Vector2.Distance(objectStart, objectEnd) / 2, 0.1f);
            }
            else if (ActiveButtons.blue == false && ActiveButtons.red == false && ActiveButtons.yellow == true)
            {
                GameObject YellowPlatform = Instantiate(yellowPlat);
                YellowPlatform.name = "YellowPlatform";
                YellowPlatform.transform.position = new Vector2((objectStart.x + objectEnd.x) / 2, (objectStart.y + objectEnd.y) / 2);
                YellowPlatform.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan((objectEnd.y - objectStart.y) / (objectEnd.x - objectStart.x)));
                YellowPlatform.transform.localScale = new Vector2(Vector2.Distance(objectStart, objectEnd) / 2, 0.1f);
            }
            else if (ActiveButtons.blue == true && ActiveButtons.red == true && ActiveButtons.yellow == false)
            {
                purplePlatStart = Instantiate(purplePlat);
                purplePlatStart.name = "purplePlatStart";
                purplePlatStart.transform.localPosition = objectStart;
                purplePlatStart.transform.localScale = new Vector2(0.1f, 2);

                purplePlatEnd = Instantiate(purplePlat);
                purplePlatEnd.name = "purplePlatEnd";
                purplePlatEnd.transform.localPosition = objectEnd;
                purplePlatEnd.transform.localScale = new Vector2(0.1f, 2);

                energySlider.value -= Mathf.Abs(purplePlatEnd.transform.localScale.y * 2 + purplePlatStart.transform.localScale.y * 2);

                purplePlatStart.GetComponent<PurpleTeleport>().setTeleportPosition(purplePlatEnd.transform.localPosition);
                purplePlatEnd.GetComponent<PurpleTeleport>().setTeleportPosition(purplePlatStart.transform.localPosition);
            }
            else if (ActiveButtons.blue == true && ActiveButtons.red == false && ActiveButtons.yellow == true)
            {
                GameObject GreenPlatform = Instantiate(greenPlat);
                GreenPlatform.name = "GreenPlatform";
                GreenPlatform.transform.position = new Vector2((objectStart.x + objectEnd.x) / 2, (objectStart.y + objectEnd.y) / 2);
                GreenPlatform.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan((objectEnd.y - objectStart.y) / (objectEnd.x - objectStart.x)));
                GreenPlatform.transform.localScale = new Vector2(Vector2.Distance(objectStart, objectEnd) / 2, 0.1f);
            }
            else if (ActiveButtons.blue == false && ActiveButtons.red == true && ActiveButtons.yellow == true)
            {
                GameObject OrangePlatform = Instantiate(orangePlat);
                OrangePlatform.name = "YellowPlatform";
                OrangePlatform.transform.position = new Vector2((objectStart.x + objectEnd.x) / 2, (objectStart.y + objectEnd.y) / 2);
                OrangePlatform.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan((objectEnd.y - objectStart.y) / (objectEnd.x - objectStart.x)));
                OrangePlatform.transform.localScale = new Vector2(Vector2.Distance(objectStart, objectEnd) / 2, 0.1f);
            }
        }
    }
}
