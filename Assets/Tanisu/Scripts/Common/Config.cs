using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Config : MonoBehaviour
{
    public static Config I;
    public static string[] stages;
    [SerializeField] PlayerData[] playerDatas;
    public PlayerData selectedPlayerData;
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
        stages = new string[] { "Beach","Soda","Nabe","Garden","ColorBall","Volcano",
            "Beaker","Pool","Ramen","Onsen","Good Button","Calculator","Feet","Surf","Apple","Ikura",
            "DogFood","Merlion","Ukiyoe","Clock","Toilet","Ant",
            "Space"
        };

        if(PlayerPrefs.GetString("SelectedPlayer") != null)
        {
            //Debug.Log(PlayerPrefs.GetString("SelectedPlayer"));
            foreach(PlayerData p in playerDatas)
            {
                if(PlayerPrefs.GetString("SelectedPlayer") == p.name)
                {
                    selectedPlayerData = p;
                    break;
                }
            }
        }
        else
        {
            selectedPlayerData = playerDatas[0];
        }
    }

    public void SwitchController(int _controller)
    {
        controller = (CONTROLLER)Enum.ToObject(typeof(CONTROLLER), _controller);
        
    }

    public void SelectPlayer(string _playerName)
    {
        foreach(PlayerData p in playerDatas)
        {
            
            if(_playerName == p.name)
            {
                selectedPlayerData = p;
                PlayerPrefs.SetString("SelectedPlayer",p.name);
                break;
            }
        }
        
    }
}
