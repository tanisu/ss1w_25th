using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Lean.Localization;

public class Config : MonoBehaviour
{
    public static Config I;
    public string[] stages,stagesJP;
    [SerializeField] PlayerData[] playerDatas;
    public PlayerData selectedPlayerData;
    public GameObject leanObj;
    public StageSelector stageSelector;
    public bool startUp;
    public int isRepeat;
    public string[] unlockedPlayers;
    

    LeanLocalization lean;
    public enum CONTROLLER
    {
        SWIPE,
        BUTTON
    }

    public enum LANG
    {
        JP,
        EN
    }

    public CONTROLLER controller;
    public LANG lang;
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


        unlockedPlayers = PlayerPrefs.GetString("UnlockedPlayer").Split(",");
        
        isRepeat = PlayerPrefs.GetInt("Repeat");
        
        if(PlayerPrefs.GetString("SelectedPlayer") != null)
        {
            
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

    private void Start()
    {
        lean = leanObj.GetComponent<LeanLocalization>();
        switch (lean.CurrentLanguage)
        {
            case "Japanese":
                lang = LANG.JP;
                break;
            case "English":
                lang = LANG.EN;
                break;
        }
        
    }

    public void SwitchController(int _controller)
    {
        controller = (CONTROLLER)Enum.ToObject(typeof(CONTROLLER), _controller);
        if(SceneController.I.GetCurrentScene() == "TanisuScene")
        {
            GameManager.I.SwitchController();
        }
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

    public void SwitchLang(string _lang)
    {
        switch (_lang)
        {
            case "Japanese":
                lang = LANG.JP;
                break;
            case "English":
                lang = LANG.EN;
                break;
        }
        stageSelector.ChangeLangView();
    }

    public void SetUnlockPlayer(string _name)
    {
        if(PlayerPrefs.GetString("UnlockedPlayer") == "")
        {
            PlayerPrefs.SetString("UnlockedPlayer", _name);
        }
        else
        {
            PlayerPrefs.SetString("UnlockedPlayer", PlayerPrefs.GetString("UnlockedPlayer") + "," + _name);
        }
        unlockedPlayers = PlayerPrefs.GetString("UnlockedPlayer").Split(",");
        
    }
}
