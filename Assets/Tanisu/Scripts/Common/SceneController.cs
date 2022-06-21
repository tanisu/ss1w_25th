using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{
    
    public static SceneController I { get; private set; }
    public int selectStageNum;

    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }   
    }


    public string GetCurrentScnene()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void StartGame()
    {
        SoundManager.I.PlaySE(SESoundData.SE.START);
        SoundManager.I.FadeOutBGM();
        SceneManager.sceneLoaded += _gameSceneLoaded;
        SceneManager.LoadScene("TanisuScene");
        
        
    }

    private void _gameSceneLoaded(Scene next ,LoadSceneMode mode)
    {
        GameManager gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        int currentMax = selectStageNum;
        
        gameManager.currentCup = currentMax;
        SceneManager.sceneLoaded -= _gameSceneLoaded;
    }

    public void ToTitle()
    {

        Time.timeScale = 1;
        SoundManager.I.FadeOutBGM();
        SceneManager.LoadScene("Title");

    }

    public void ToEnding()
    {
        SoundManager.I.FadeOutBGM();
        SceneManager.LoadScene("momo_Clear");
    }

    public string GetCurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }
}
