using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI timeText,recordTimeText;
    DateTime dateTime = new DateTime();
    TimeSpan timeSpan, totalTimeSpan, showTimeSpan;
    bool timerIsActive = false;
    
    
    public void InitTimer()
    {
        dateTime = DateTime.Now;
        timeSpan = new TimeSpan(0, 0, 0, 0);
        totalTimeSpan = new TimeSpan(0, 0, 0, 0);
        timerIsActive = true;
    }
    public void TimerReset()
    {
        InitTimer();
        _timerTMP();
    }

    public void TimerUpdate()
    {
        if (!timerIsActive) return;
        timeSpan = DateTime.Now - dateTime;
        _timerTMP();
    }

    public void TimerStop()
    {
        if (timerIsActive)
        {
            totalTimeSpan += timeSpan;
            timerIsActive = false;
        }
    }

    public void ScoreTime(int _current)
    {
        
        string currentScore = totalTimeSpan.ToString();
        string beforeScore = PlayerPrefs.GetString(_current.ToString());        
        if(beforeScore == "" || TimeSpan.Parse(currentScore) < TimeSpan.Parse(beforeScore))
        {
            PlayerPrefs.SetString(_current.ToString(), currentScore);
        }

    }

    public void GetRecordTime(int _current)
    {
        string recordTime = PlayerPrefs.GetString(_current.ToString());
        _recordTMP(recordTime);
 
    }

    private void _recordTMP(string _recordTime)
    {
        if (_recordTime == "")
        {
            _resetTMP(recordTimeText);
            return;
        }
        TimeSpan recordTimeSpan = TimeSpan.Parse(_recordTime);
        _renderTMP(recordTimeText, recordTimeSpan);

    }
    

    private void _timerTMP()
    {
        showTimeSpan = totalTimeSpan + timeSpan;
        _renderTMP(timeText, showTimeSpan);

    }
    private void _renderTMP(TextMeshProUGUI _tmp,TimeSpan _timeSpan)
    {
        _tmp.SetText(
            "{0:00}:{1:00}:{2:000}",
            _timeSpan.Minutes,
            _timeSpan.Seconds,
            _timeSpan.Milliseconds
        );
    }

    private void _resetTMP(TextMeshProUGUI _tmp)
    {
        _tmp.text = "00:00:000";
    }
    


}
