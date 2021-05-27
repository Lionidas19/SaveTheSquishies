using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscToTitle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            ActiveButtons.yellow = false;
            ActiveButtons.blue = false;
            ActiveButtons.red = false;

            ActiveButtons.goButton = false;
            ActiveButtons.resetButton = false;
            ActiveButtons.advancebutton = false;
            SceneManager.LoadScene("TitleScreen");
        }
    }
}

