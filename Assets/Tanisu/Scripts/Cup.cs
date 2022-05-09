using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    public BGMSoundData.BGM BGMTitle;
    public Material metaBallRenderer;
    [SerializeField] GameObject waterGenerator;
    [SerializeField] Color color, strokeColor;

    private void Start()
    {
        
        //metaBallRenderer.SetColor("_Color",color);
        //metaBallRenderer.SetColor("_StrokeColor", strokeColor);
    }

    public void StopWaters()
    {
        PoolContent[] waters = GetComponentsInChildren<PoolContent>();      
        StartCoroutine(_collectWaters(waters));
    }

    public void ChangeColor()
    {
        metaBallRenderer.SetColor("_Color", color);
        metaBallRenderer.SetColor("_StrokeColor", strokeColor);
    }

    public void hideWaterGenerator()
    {
        waterGenerator.SetActive(false);
    }

    public void showWaterGenerator()
    {
        waterGenerator.SetActive(true);
    }

    public void Restart()
    {
        waterGenerator.SetActive(true);
        waterGenerator.GetComponent<WaterGenerator>().ReStart();
    }

    

    IEnumerator _collectWaters(PoolContent[] _waters)
    {
        foreach(PoolContent water in _waters)
        {
            water.GetComponent<Water>().StopMove();
        }
        yield return new WaitForSeconds(2f);
        foreach (PoolContent water in _waters)
        {
            water.HideFromStage();
        }
        if(GameManager.I.gameState == GameManager.GAMESTATE.REPLAY)
        {
            Debug.Log("replay");
            yield return new WaitForSeconds(2f);
            Restart();
        }
        GameManager.I.gameState = GameManager.GAMESTATE.PLAY;
    }
}
