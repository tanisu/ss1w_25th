using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGenerator : MonoBehaviour
{
    [SerializeField] ObjectPool waterPool;
    Coroutine coroutine;
    
    void Start()
    {
        coroutine = StartCoroutine(_launchWater());
    }

    
    void Update()
    {
        if(GameManager.I.gameState == GameManager.GAMESTATE.CLEAR)
        {
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
        for (int i = 0; i < waterPool.maxCount ; i++)
        {
            PoolContent obj = waterPool.Launch(transform.position);
            obj.transform.parent = transform.parent;
            
            yield return new WaitForSeconds(0.03f);
        }
    }

}
