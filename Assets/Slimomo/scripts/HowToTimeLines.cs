using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class HowToTimeLines : MonoBehaviour
{
    [SerializeField] PlayableDirector playableDirector;
    void Start()
    {
        //同じゲームオブジェクトにあるPlayableDirectorを取得する
        playableDirector = GetComponent<PlayableDirector>();
        //PlayTimeline();
    }

    //再生する
    public void PlayTimeline()
    {
        playableDirector.Play();
    }

    //一時停止する
    void PauseTimeline()
    {
        playableDirector.Pause();
    }

    //一時停止を再開する
    void ResumeTimeline()
    {
        playableDirector.Resume();
    }

    //停止する
    void StopTimeline()
    {
        playableDirector.Stop();
    }

    //タイムラインのトラックが終わったら、ゲームオブジェクト非表示してタイムラインを終了
    public void OffTimelineObj()
    {
        this.gameObject.SetActive(false);

    }

}
