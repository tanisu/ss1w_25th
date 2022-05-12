using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    [SerializeField] Fade fade;
    [SerializeField] GameObject stage,clearEffect;
    [SerializeField] Player player;
    [SerializeField] float stageX = 6f;
    [SerializeField] public float cupChangeTime;
    [SerializeField] MeshRenderer QuadRenderer;
    int currentCup = 0;
    Cup[] cups;
    bool cupClear,gameOver;
    public enum GAMESTATE
    {
        WAIT,
        PLAY,
        REPLAY,
        CLEAR,
        

    }

    public GAMESTATE gameState;
    public GAMESTATE beforePause;

    public static GameManager I { get; private set; }

    private void Awake()
    {
        if (I == null)
        {
            I = this;
            //DontDestroyOnLoad(gameObject);
        }
        /*else
        {
            Destroy(gameObject);
        }*/
    }


    void Start()
    {
        StartCoroutine(_start());


    }
    IEnumerator _start()
    {
        fade.FadeOut(1f);
        cups = stage.GetComponentsInChildren<Cup>();
        gameState = GAMESTATE.WAIT;
        cups[currentCup].ChangeColor();
        player.ToStartPos();
        SoundManager.I.FadeInBGM();
        SoundManager.I.PlayBGM(cups[currentCup].BGMTitle);

        yield return new WaitForSeconds(1f);
        cups[currentCup].showWaterGenerator();
        
    }


    void Update()
    {
        //if( !isPause)
        //{

        //    isPause = true;
        //}

        //if(isPause)
        //{

        //    isPause = false;
        //}
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    SceneController.I.ToEnding();
        //}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameOver();
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneController.I.ToTitle();
        }
        if ( (Input.GetKeyDown(KeyCode.Space) && gameState == GAMESTATE.PLAY) || (gameState == GAMESTATE.PLAY && cupClear))
        {
            
            StartCoroutine(_moveNext());
        }

        if(gameState == GAMESTATE.PLAY && !cupClear && gameOver)
        {
            SoundManager.I.PlaySE(SESoundData.SE.OBORERU);
            gameState = GAMESTATE.REPLAY;
            gameOver = false;
            cups[currentCup].StopWaters();
            cups[currentCup].hideWaterGenerator();
            player.SetRetry();
        }

    }

    public void CupClear()
    {
        cupClear = true;
    }

    public void GameOver()
    {
        gameOver = true;
    }


    private IEnumerator _moveNext()
    {

        gameState = GAMESTATE.CLEAR;
        SoundManager.I.PlaySE(SESoundData.SE.CLEAR);
        SoundManager.I.PlaySE(SESoundData.SE.CHEERS1);
        Instantiate(clearEffect,new Vector3(1.5f,3f),Quaternion.identity,transform.parent);
        Instantiate(clearEffect, new Vector3(-1.5f, 3f), Quaternion.identity, transform.parent);

        cupClear = false;
        cups[currentCup].StopWaters();
        cups[currentCup].hideWaterGenerator();
        SoundManager.I.StopBGM();
        player.switchRgbd();
        yield return new WaitForSeconds(2f);

        if(currentCup < cups.Length - 1)
        {
            currentCup++;
            player.SetPlayerPos();

            stage.transform.DOMoveX(stage.transform.position.x - stageX, cupChangeTime).OnComplete(() => {
                cups[currentCup].ChangeColor();
                QuadRenderer.gameObject.SetActive(false);
                QuadRenderer.gameObject.SetActive(true);
                cups[currentCup].showWaterGenerator();
                player.switchRgbd();
                SoundManager.I.PlayBGM(cups[currentCup].BGMTitle);
                gameState = GAMESTATE.PLAY;
            });
        }
        else
        {
            SceneController.I.ToEnding();
        }
        
    }
    

}
