using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundManager : MonoBehaviour
{

    [SerializeField] AudioSource bgmAudioSource;
    [SerializeField] AudioSource seAudioSource;

    [SerializeField] List<BGMSoundData> bGMSoundDatas;
    [SerializeField] List<SESoundData> SESoundDatas;

    public float mastarVolume = 1;
    public float bgmVolume = 1;
    public float seVolume = 1;
    public bool isSibuki;
    float beforeVolume = 1f;
    public static SoundManager I { get; private set; }


    public bool isPlayBGM;


    private void Awake()
    {
        if (I == null)
        {
            I = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (!isPlayBGM)
        {
            
            isPlayBGM = true;
        }
    }
    public void PlayBGM(BGMSoundData.BGM bgm)
    {
        BGMSoundData data = bGMSoundDatas.Find(data => data.bgm == bgm);
        bgmAudioSource.clip = data.audioClip;
        bgmAudioSource.volume = data.volume * bgmVolume * mastarVolume;

        bgmAudioSource.Play();
    }

    public void StopBGM()
    {

        bgmAudioSource.Stop();
    }

    public void LoopSwitch()
    {
        bgmAudioSource.loop = !bgmAudioSource.loop;
    }

    public void ChangeBGMVolumes()
    {
        bgmAudioSource.volume = bgmVolume;
    }

    public void ChangeSEVolumes()
    {
        seAudioSource.volume = seVolume;
    }

    public void FadeOutBGM()
    {
        beforeVolume = bgmAudioSource.volume;
        bgmAudioSource.DOFade(0f, 0.8f);
    }

    public void FadeInBGM()
    {
        bgmAudioSource.DOFade(beforeVolume, 0.8f);
    }


    public void PlaySE(SESoundData.SE se)
    {
        SESoundData data = SESoundDatas.Find(data => data.se == se);
        seAudioSource.volume = data.volume * seVolume * mastarVolume;
        seAudioSource.PlayOneShot(data.audioClip);
    }

    public void SibukiChu()
    {
        StartCoroutine(_sibukichu());
    }

    IEnumerator _sibukichu()
    {
        isSibuki = true;
        yield return new WaitForSeconds(1f);
        isSibuki = false;
    }


}
[System.Serializable]
public class BGMSoundData
{
    public enum BGM
    {
        TITLE,
        REGGAETON,
        REGGAETON1,
        CALYPSO,
        RAMEN,
        VOLCANO,
        JAPANESE,
        ONSEN,
        GUITAR1,
        GUITAR2,
        STEELPAN,
        APPLE,
        CLOCK,
        JAZZ,
        MALLET,
        RYTHM,
        SPACE,
        STOMACH,
        UKIYOE,
        ENDING,
        CALCULATOR,
        IKURA,
        DOGFOOD,
        GOOD,
        MERLION,
        MOJI,
        TOILET,
        ANT

    }

    public BGM bgm;
    public AudioClip audioClip;
    [Range(0, 1)]
    public float volume = 1;
}

[System.Serializable]
public class SESoundData
{
    public enum SE
    {
        WAVE1,
        WAVE2,
        WAVE3,
        OBORERU,
        START,
        CLEAR,
        SHIBUKI,
        CHEERS1,
        CHEERS2,
        MENU_IN,
        MENU_OUT,
        RETRY,
        TAP_CURSOR

    }

    public SE se;
    public AudioClip audioClip;
    [Range(0, 1)]
    public float volume = 1;
}