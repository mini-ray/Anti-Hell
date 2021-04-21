using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 1f;

    Animator myAnimator;
    Collider2D myBodyCollider;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<Collider2D>();
        
    }

    private void Update()
    {
        Open();
        Continue();
    }

    public void Open()
    {
        
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Player")) /*&& Input.GetButtonDown("Submit")*/)
        {
            
            myAnimator.SetBool("Open", true);
            
        }
        else if(!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            myAnimator.SetBool("Open", false);
        }
            
    }

    public void Continue()
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Player")) && Input.GetButtonDown("Submit"))
        {
            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(LevelLoadDelay);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 2);
        Debug.Log("WIP");
    }
}
