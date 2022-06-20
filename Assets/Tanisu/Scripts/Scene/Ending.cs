using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    
    void Start()
    {
        
        SoundManager.I.FadeInBGM();
        SoundManager.I.PlayBGM(BGMSoundData.BGM.ENDING);
    }


}
