using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    
    private void Awake()
    {

    }
    void Start()
    {

        SoundManager.I.PlayBGM(BGMSoundData.BGM.TITLE);
        //foreach(string stage in Config.stages)
        //{
        //    //Debug.Log(stage);
        //}
    }

    
    void Update()
    {
        
    }
}
