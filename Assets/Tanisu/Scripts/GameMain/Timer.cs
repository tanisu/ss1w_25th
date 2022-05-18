using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI timeText;
    DateTime dateTime = new DateTime();
    TimeSpan timeSpan, totalTimeSpan, showTimeSpan;
    bool timerIsActive = false;
    void Start()
    {
        timeText = GetComponent<TextMeshProUGUI>();
        
    }


    //private void Update() => TimerUpdate();
    
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
        TimerTMP();
    }

    public void TimerUpdate()
    {
        if (!timerIsActive) return;
        timeSpan = DateTime.Now - dateTime;
        TimerTMP();
    }

    public void TimerStop()
    {
        if (timerIsActive)
        {
            totalTimeSpan += timeSpan;
            timerIsActive = false;
        }
    }

    public void TimerTMP()
    {
        showTimeSpan = totalTimeSpan + timeSpan;
        timeText.SetText(
            "{0:00}:{1:00}:{2:000}",
            showTimeSpan.Minutes,
            showTimeSpan.Seconds,
            showTimeSpan.Milliseconds
            );
    }
}
