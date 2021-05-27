using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitApp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitApp()
    {
        ActiveButtons.yellow = false;
        ActiveButtons.blue = false;
        ActiveButtons.red = false;

        ActiveButtons.goButton = false;
        ActiveButtons.resetButton = false;
        ActiveButtons.advancebutton = false;
        Application.Quit();
    }
}
