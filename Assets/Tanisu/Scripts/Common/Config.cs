using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Config : MonoBehaviour
{
    public static Config I;
    public enum CONTROLLER
    {
        SWIPE,
        BUTTON
    }

    public CONTROLLER controller;

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

    public void SwitchController(int _controller)
    {
        controller = (CONTROLLER)Enum.ToObject(typeof(CONTROLLER), _controller);
        Debug.Log(controller);
    }
}
