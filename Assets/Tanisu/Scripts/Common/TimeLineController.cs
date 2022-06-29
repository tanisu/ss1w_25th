using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineController : MonoBehaviour
{
    [SerializeField] GameObject[] timelineObjs;

    private void Start()
    {
        if (Config.I.isRepeat == 0)
        {

            timelineObjs[(int)Config.I.controller].SetActive(true);


        PlayerPrefs.SetInt("Repeat", 1);
        Config.I.isRepeat = 1;
    }
}
}
