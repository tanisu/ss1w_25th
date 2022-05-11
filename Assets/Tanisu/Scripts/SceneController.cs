using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] Fade fade;
    public static SceneController I { get; private set; }

    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }
   
    }


    public void StartGame()
    {
        fade.FadeIn(1f, () =>  SceneManager.LoadScene("TanisuScene"));
        
    }

    public void ToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
