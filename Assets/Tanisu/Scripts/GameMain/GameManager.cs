using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    
    [SerializeField] GameObject stage,clearEffect,swipe,buttons;
    [SerializeField] Player player;
    [SerializeField] float stageX = 6f;
    [SerializeField] public float cupChangeTime;
    [SerializeField] MeshRenderer QuadRenderer;
    [SerializeField] ObjectPool[] objectPools;
    [SerializeField] int interstitialCount;
    [SerializeField] AdMobInterstitial interstitial;
    [SerializeField] FadeSample fade;
    
    int clearCount = 0;
    public int currentCup = 0;
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

        }
 
    }


    void Start()
    {

        SwitchController();
        cups = stage.GetComponentsInChildren<Cup>();
        stage.transform.position =  new Vector3(-stageX * currentCup,0) ;
        for (int i = 0; i < cups.Length; i++)
        {
            if(currentCup != i)
            {
                cups[i].gameObject.SetActive(false);
            }

        }
        gameState = GAMESTATE.WAIT;
        
        player.ToStartPos();
        SoundManager.I.FadeInBGM();
        SoundManager.I.PlayBGM(cups[currentCup].BGMTitle);
        StartCoroutine(_start());
       
        

    }
    IEnumerator _start()
    {

        yield return new WaitForSeconds(1f);
        cups[currentCup].ChangeColor();
        cups[currentCup].showWaterGenerator();
      
        
        
    }

    public void SwitchController()
    {
        if (Config.I.controller == Config.CONTROLLER.SWIPE)
        {
            swipe.SetActive(true);
            buttons.SetActive(false);
        }
        else
        {
            swipe.SetActive(false);
            buttons.SetActive(true);
        }
    }

    void Update()
    {

        /* For debug
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneController.I.ToEnding();
        }
        */
        //if (Input.GetKeyDown(KeyCode.Backspace))
        //{

        //    SceneController.I.ToTitle();
        //}
        //if ((Input.GetKeyDown(KeyCode.Space) && gameState == GAMESTATE.PLAY))
        //{
        //    StartCoroutine(_moveNext());
        //}

        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    GameOver();
        //}

        if (gameState == GAMESTATE.PLAY && cupClear)
        {

            StartCoroutine(_moveNext());
        }



        if (gameState == GAMESTATE.PLAY && !cupClear && gameOver)
        {
            SoundManager.I.PlaySE(SESoundData.SE.OBORERU);
            gameState = GAMESTATE.REPLAY;
            gameOver = false;
            _currentCupReset();
            player.SetRetry();
        }
       

    }



    public void CupClear()
    {
        cupClear = true;
    }

    public void ChangePlayerSprite()
    {
        player.ChangeSprite();
    }

    public void GameOver()
    {
        SoundManager.I.PlaySE(SESoundData.SE.RETRY);
        gameOver = true;
    }

    private void _currentCupReset()
    {
        cups[currentCup].StopWaters();
        cups[currentCup].ResetCup();
        cups[currentCup].hideWaterGenerator();
        
    }

    private void _clearEffect()
    {
        SoundManager.I.PlaySE(SESoundData.SE.CLEAR);
        SoundManager.I.PlaySE(SESoundData.SE.CHEERS1);
        Instantiate(clearEffect, new Vector3(1.5f, 3f), Quaternion.identity, transform.parent);
        Instantiate(clearEffect, new Vector3(-1.5f, 3f), Quaternion.identity, transform.parent);
    }

    private void _initNextCup()
    {
        cups[currentCup].ChangeColor();
        QuadRenderer.gameObject.SetActive(false);
        QuadRenderer.gameObject.SetActive(true);
        cups[currentCup].showWaterGenerator();
    }
       

    private IEnumerator _moveNext()
    {
      
        gameState = GAMESTATE.CLEAR;
        _clearEffect();

        cupClear = false;
        _currentCupReset();
        

        SoundManager.I.StopBGM();
        player.switchRgbd();



        yield return new WaitForSeconds(2f);
      
        if (currentCup < cups.Length - 1)
        {
            currentCup++;
            cups[currentCup].gameObject.SetActive(true);
            
            _checkMaxCup();
            player.SetPlayerPos();
         

            _showAd();

            stage.transform.DOMoveX(stage.transform.position.x - stageX, cupChangeTime).OnComplete(() => {
             
                cups[currentCup - 1].gameObject.SetActive(false);
                _initNextCup();
                player.switchRgbd();
                SoundManager.I.PlayBGM(cups[currentCup].BGMTitle);
                gameState = GAMESTATE.PLAY;
            }).SetLink(gameObject);
        }
        else
        {
            fade.ShowEndFade("Ending");
        }
        
    }

    private void _showAd()
    {
        clearCount++;
        if (clearCount == interstitialCount)
        {
            interstitial.ShowAdMobInterstitial();
            clearCount = 0;
        }
    }

    private void _checkMaxCup()
    {
        int currentMax = PlayerPrefs.GetInt("maxCup");
        if(currentMax < currentCup)
        {
            PlayerPrefs.SetInt("maxCup", currentCup);
            
        }
    }



}
