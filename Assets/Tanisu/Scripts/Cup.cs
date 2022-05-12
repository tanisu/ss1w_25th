using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    public BGMSoundData.BGM BGMTitle;
    public Material metaBallRenderer;
    [SerializeField] GameObject waterGenerator;
    [SerializeField] Trap[] traps;
    [SerializeField] Color color, strokeColor,sibukiColor;
    [SerializeField] ParticleSystem sibuki;
    [SerializeField] float trapStartTime;
    float time;
    int currentTrap = 0;
    ParticleSystem.MainModule main;
    bool isCurrentCup;

    private void Start()
    {
        main = sibuki.main;
        foreach (Trap trap in traps)
        {
            trap.InitTrap();
        }
    }

    private void Update()
    {
        if (isCurrentCup && GameManager.I.gameState == GameManager.GAMESTATE.PLAY)
        {

            if(_updateTimer() >= 1 && currentTrap <traps.Length)
            {
                traps[currentTrap].TrapActivation();
                currentTrap++;
                time = 0;
            }
        }
    }


    float _updateTimer()
    {
        time += Time.deltaTime;
        float timer = time / trapStartTime;
        return timer;
    }


    public void StopWaters()
    {
        PoolContent[] waters = GetComponentsInChildren<PoolContent>();
        foreach (PoolContent water in waters)
        {
            water.GetComponent<Water>().StopMove();
        }
        
        //StartCoroutine(_collectWaters(waters));
    }

    public void ResetCup()
    {
        waterGenerator.GetComponent<WaterGenerator>().RemoveAllWaters();
        StartCoroutine(_resetTrap());
        StartCoroutine(_resetAll());
        if (GameManager.I.gameState == GameManager.GAMESTATE.REPLAY)
        {
            StartCoroutine(_restart());
        }
    }

    public void ChangeColor()
    {
        main.startColor = sibukiColor;
        metaBallRenderer.SetColor("_Color", color);
        metaBallRenderer.SetColor("_StrokeColor", strokeColor);
    }

    public void hideWaterGenerator()
    {
        isCurrentCup = false;
        waterGenerator.SetActive(false);
    }

    public void showWaterGenerator()
    {
        isCurrentCup = true;
        waterGenerator.SetActive(true);
    }

    public void Restart()
    {
        showWaterGenerator();
        waterGenerator.GetComponent<WaterGenerator>().ReStart();
    }

    public void ResetAll()
    {
        BroadcastMessage("HideFromStage", SendMessageOptions.DontRequireReceiver);
    }

    IEnumerator _resetAll()
    {
        yield return new WaitForSeconds(2f);
        ResetAll();
    }

    IEnumerator _resetTrap()
    {
        yield return new WaitForSeconds(2f);
        foreach (Trap trap in traps)
        {
            trap.ResetTrap();
        }
        currentTrap = 0;
        time = 0;
    }
    IEnumerator _restart()
    {
        yield return new WaitForSeconds(3.75f);
        Restart();
    }



    /*
    IEnumerator _collectWaters(PoolContent[] _waters)
    {
        
        foreach (PoolContent water in _waters)
        {
            water.GetComponent<Water>().StopMove();
        }
        yield return new WaitForSeconds(2f);
        
        foreach (PoolContent water in _waters)
        {
            water.HideFromStage();
            water.GetComponent<Water>().waterGenerator.RemoveWater(water.GetComponent<Water>());
            
        }
        foreach (Trap trap in traps)
        {
            trap.ResetTrap();
        }
        currentTrap = 0;
        time = 0;
        if (GameManager.I.gameState == GameManager.GAMESTATE.REPLAY)
        {

            yield return new WaitForSeconds(2f);
            Restart();
        }
        
    }*/
}
