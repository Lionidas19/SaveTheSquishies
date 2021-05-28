using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      //  if (Input.GetKey("b") && Input.GetKey("u") && Input.GetKey("r") && Input.GetKey("n"))
        if (Input.GetKey("space") && Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            SceneManager.LoadScene("TitleScreen End");
        }
    }
}
