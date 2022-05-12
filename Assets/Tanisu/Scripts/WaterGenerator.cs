using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGenerator : MonoBehaviour
{
    [SerializeField] ObjectPool waterPool;
    [SerializeField] float waveInterval,force;
    [SerializeField] float launcInterval;
    [SerializeField] int waveSoundTime = 1;

    float interval;
    int vec = 1;
    int waru = 3;
    Coroutine coroutine;
    List<Water> waters;
    
    void Start()
    {
        waters = new List<Water>();
        coroutine = StartCoroutine(_launchWater());
        interval = waveInterval;
    }

    
    void Update()
    {
        if(GameManager.I.gameState == GameManager.GAMESTATE.PLAY)
        {
            interval -= Time.deltaTime;
            if(interval <= 0)
            {
                if(waveSoundTime % waru == 0)
                {
                    SoundManager.I.PlaySE(SESoundData.SE.WAVE1);
                }
                
                foreach(Water water in waters)
                {
                    water.Wave(force,vec);
                }
                interval = waveInterval;
                vec *= -1;
                waveSoundTime++;
            }
        }
        if(GameManager.I.gameState == GameManager.GAMESTATE.CLEAR)
        {
            interval = 0;
            vec = 1;
            StopCoroutine(coroutine);
        }
    }

    public void ReStart()
    {
        StartCoroutine(_launchWater());
    }

    IEnumerator _launchWater()
    {
        
        yield return new WaitForSeconds(0.001f);

        while (true)
        {
            PoolContent obj = waterPool.Launch(transform.position);
            if(obj != null)
            {
                obj.transform.parent = transform.parent;
                Water water = obj.GetComponent<Water>();
                water.waterGenerator = this;
                waters.Add(water);

            }
            
            yield return new WaitForSeconds(launcInterval);
        }
    }

    public void RemoveWater(Water _water)
    {
        
        waters.Remove(_water);
    }

}
