using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    public BGMSoundData.BGM BGMTitle;
    public Material metaBallRenderer;
    [SerializeField] GameObject waterGenerator;
    
    public void StopWaters()
    {
        PoolContent[] waters = GetComponentsInChildren<PoolContent>();      
        StartCoroutine(_collectWaters(waters));
    }
    public void hideWaterGenerator()
    {
        waterGenerator.SetActive(false);
    }

    public void showWaterGenerator()
    {
        waterGenerator.SetActive(true);
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
    }
}
