using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController I { get; private set; }

    private void Awake()
    {
        if (I == null)
        {
            I = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    

    public void StartGame()
    {
        SceneManager.LoadScene("TanisuScene");
    }

    public void ToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
