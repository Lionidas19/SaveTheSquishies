using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionDoorAnimator : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ActiveButtons.resetButton == true)
        {
            ActiveButtons.resetButton = false;
            animator.Play("LevelClose");
            Invoke("ResetScene", animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }

    void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
