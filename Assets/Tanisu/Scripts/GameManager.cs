using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject stage;
    [SerializeField] Player player;
    [SerializeField] float stageX = 6f;
    int currentCup = 0;
    Cup[] cups;
    void Start()
    {
        cups = stage.GetComponentsInChildren<Cup>();
        Debug.Log(cups.Length);
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneController.I.ToTitle();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentCup++;
            SoundManager.I.StopBGM();
            player.switchRgbd();
            stage.transform.DOMoveX(stage.transform.position.x - stageX, 3f).OnComplete(()=> {
                player.switchRgbd();
                SoundManager.I.PlayBGM(cups[currentCup].BGMTitle);
            });
        }
    }
}
