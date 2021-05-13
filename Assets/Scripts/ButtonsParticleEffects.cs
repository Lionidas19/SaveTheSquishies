using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsParticleEffects : MonoBehaviour
{
    private bool active = false;

    // Update is called once per frame
    void Update()
    {
        if(gameObject.name == "BlueButton")
        {
            if(ActiveButtons.blue == true)
            {
                if(active == true)
                {
                    gameObject.transform.Find("Particles").GetComponent<ParticleSystem>().Play();
                    active = false;
                }    
            }
            else
            {
                if (active == false)
                {
                    gameObject.transform.Find("Particles").GetComponent<ParticleSystem>().Stop();
                    active = true;
                }
            }
        }

        if (gameObject.name == "YellowButton")
        {
            if (ActiveButtons.yellow == true)
            {
                if (active == true)
                {
                    gameObject.transform.Find("Particles").GetComponent<ParticleSystem>().Play();
                    active = false;
                }
            }
            else
            {
                if (active == false)
                {
                    gameObject.transform.Find("Particles").GetComponent<ParticleSystem>().Stop();
                    active = true;
                }
            }
        }

        if (gameObject.name == "RedButton")
        {
            if (ActiveButtons.red == true)
            {
                if (active == true)
                {
                    gameObject.transform.Find("Particles").GetComponent<ParticleSystem>().Play();
                    active = false;
                }
            }
            else
            {
                if (active == false)
                {
                    gameObject.transform.Find("Particles").GetComponent<ParticleSystem>().Stop();
                    active = true;
                }
            }
        }
    }
}
