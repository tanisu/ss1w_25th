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
        SoundManager.I.FadeOutBGM();
        fade.FadeIn(1f, () =>  SceneManager.LoadScene("TanisuScene"));
        
    }

    public void ToTitle()
    {
        SoundManager.I.FadeOutBGM();
        fade.FadeIn(1f,()=> SceneManager.LoadScene("Title"));
    }

    public void ToEnding()
    {
        SoundManager.I.FadeOutBGM();
        fade.FadeIn(1f, () => SceneManager.LoadScene("momo_Clear"));
    }
}
