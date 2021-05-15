using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorParticleEffects : MonoBehaviour
{
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // 10 meters in front of the camera:
        transform.position = ray.origin;

        //BLACK
        if(ActiveButtons.blue == false && ActiveButtons.yellow == false && ActiveButtons.red == false)
        {
            gameObject.transform.Find("Particles").GetComponent<ParticleSystem>().startColor = new Color(0, 0, 0);
        }
        //BLUE
        else if (ActiveButtons.blue == true && ActiveButtons.yellow == false && ActiveButtons.red == false)
        {
            gameObject.transform.Find("Particles").GetComponent<ParticleSystem>().startColor = new Color(0.1f, 0f, 1f);
        }
        //YELLOW
        else if (ActiveButtons.blue == false && ActiveButtons.yellow == true && ActiveButtons.red == false)
        {
            gameObject.transform.Find("Particles").GetComponent<ParticleSystem>().startColor = new Color(1f, 0.95f, 0f);
        }
        //RED
        else if (ActiveButtons.blue == false && ActiveButtons.yellow == false && ActiveButtons.red == true)
        {
            gameObject.transform.Find("Particles").GetComponent<ParticleSystem>().startColor = new Color(1f, 0f, 0.13f);
        }
        //GREEN
        else if (ActiveButtons.blue == true && ActiveButtons.yellow == true && ActiveButtons.red == false)
        {
            gameObject.transform.Find("Particles").GetComponent<ParticleSystem>().startColor = new Color(0.1f, 0.7f, 0.1f);
        }
        //PURPLE
        else if (ActiveButtons.blue == true && ActiveButtons.yellow == false && ActiveButtons.red == true)
        {
            gameObject.transform.Find("Particles").GetComponent<ParticleSystem>().startColor = new Color(0.75f, 0f, 1f);
        }
        //ORANGE
        else if (ActiveButtons.blue == false && ActiveButtons.yellow == true && ActiveButtons.red == true)
        {
            gameObject.transform.Find("Particles").GetComponent<ParticleSystem>().startColor = new Color(1f, 0.3f, 0f);
        }
        //WHITE
        else if (ActiveButtons.blue == true && ActiveButtons.yellow == true && ActiveButtons.red == true)
        {
            gameObject.transform.Find("Particles").GetComponent<ParticleSystem>().startColor = new Color(1f, 1f, 1f);
        }
    }
}
