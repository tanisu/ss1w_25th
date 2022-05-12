using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField] Fade fade;
    void Start()
    {
        fade.FadeOut(1f);
        SoundManager.I.FadeInBGM();
        SoundManager.I.PlayBGM(BGMSoundData.BGM.ENDING);
    }


}