using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject stage;
    [SerializeField] Player player;
    [SerializeField] float stageX = 6f;
    [SerializeField] public float cupChangeTime;
    [SerializeField] MeshRenderer QuadRenderer;
    int currentCup = 0;
    Cup[] cups;
    bool cupClear;
    public enum GAMESTATE
    {
        WAIT,
        PLAY,
        REPLAY,
        CLEAR
    }

    public GAMESTATE gameState;

    public static GameManager I { get; private set; }

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


    void Start()
    {
        cups = stage.GetComponentsInChildren<Cup>();
        gameState = GAMESTATE.PLAY;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneController.I.ToTitle();
        }
        if (Input.GetKeyDown(KeyCode.Space) || (gameState == GAMESTATE.PLAY && cupClear))
        {
            
            gameState = GAMESTATE.CLEAR;
            cupClear = false;
            cups[currentCup].StopWaters();
            cups[currentCup].hideWaterGenerator();
            currentCup++;
            //QuadRenderer.materials[0] = cups[currentCup].metaBallRenderer;
            //QuadRenderer.gameObject.SetActive(false);
            //QuadRenderer.gameObject.SetActive(true);
            SoundManager.I.StopBGM();
            player.switchRgbd();
            player.SetPlayerPos();
            stage.transform.DOMoveX(stage.transform.position.x - stageX, cupChangeTime).OnComplete(()=> {
                cups[currentCup].showWaterGenerator();
                player.switchRgbd();
                gameState = GAMESTATE.PLAY;
                SoundManager.I.PlayBGM(cups[currentCup].BGMTitle);
            });
        }
    }

    public void CupClear()
    {
        cupClear = true;
    }

    

}
