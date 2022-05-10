using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public static SoundManager I { get; private set; }


    bool isPlayBGM;


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
            PlayBGM(BGMSoundData.BGM.CALYPSO);
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
        CALYPSO,
        CHINA,
        DARK,
        REGGAETON1,
        JAPANESE,
        ONSEN

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
        CHEERS2
    }

    public SE se;
    public AudioClip audioClip;
    [Range(0, 1)]
    public float volume = 1;
}