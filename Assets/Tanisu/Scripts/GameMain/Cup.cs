using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    public BGMSoundData.BGM BGMTitle;
    public Material metaBallRenderer;
    [SerializeField] GameObject[] waterGenerators;
    [SerializeField] Trap[] traps;
    [SerializeField] Color color, strokeColor,sibukiColor;
    [SerializeField] ParticleSystem sibuki;
    [SerializeField] float trapStartTime;
    [SerializeField] StartTrigger st;
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
        foreach(GameObject waterGenerator in waterGenerators)
        {
            waterGenerator.GetComponent<WaterGenerator>().RemoveAllWaters();
        }
        
        StartCoroutine(_resetTrap());
        StartCoroutine(_resetAll());
        if (GameManager.I.gameState == GameManager.GAMESTATE.REPLAY)
        {
            StartCoroutine(_restart());
        }
        if(st != null)
        {
            st.OffCollider();
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
        foreach(GameObject waterGenerator in waterGenerators)
        {
            waterGenerator.SetActive(false);
        }
        
    }

    public void showWaterGenerator()
    {
        isCurrentCup = true;
        if (st != null) return;
        foreach (GameObject waterGenerator in waterGenerators)
        {
            
            
            
            waterGenerator.SetActive(true);
            
                
            
        }
        
    }

    public bool GetCurrentCup()
    {
        return isCurrentCup;
    }
    public void Restart()
    {
        showWaterGenerator();
        if (st != null) return;
        foreach(GameObject waterGenerator in waterGenerators)
        {
            waterGenerator.GetComponent<WaterGenerator>().ReStart();
        }
        
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



}
