using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Config : MonoBehaviour
{
    public static Config I;
    public static string[] stages;
    
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
        stages = new string[] { "Beach","Soda","Nabe","Garden","ColorBall","Volcano","Beaker","Pool","Ramen","Onsen" };
    }

    public void SwitchController(int _controller)
    {
        controller = (CONTROLLER)Enum.ToObject(typeof(CONTROLLER), _controller);
        
    }
}
