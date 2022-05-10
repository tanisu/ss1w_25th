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
        StartCoroutine(_collectWaters(waters));
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

    

    IEnumerator _collectWaters(PoolContent[] _waters)
    {
        
        foreach (PoolContent water in _waters)
        {
            water.GetComponent<Water>().StopMove();
        }
        yield return new WaitForSeconds(2f);
        
        foreach (PoolContent water in _waters)
        {
            water.GetComponent<Water>().waterGenerator.RemoveWater(water.GetComponent<Water>());
            water.HideFromStage();
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
        
    }
}
