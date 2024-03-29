using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] Button  soundButton,closeButton,batuButton,titleButton;
    [SerializeField] GameObject optionPanel;
    [SerializeField] Slider BGMSlider;
    [SerializeField] FadeSample fade;
    [SerializeField] StageSelector stageSelector;
    

    GameManager.GAMESTATE beforState;
    bool isPlaing;
    void Start()
    {
        isPlaing = GameObject.Find("GameManager");
        
        soundButton.onClick.AddListener(_viewPanel);
        closeButton.onClick.AddListener(_closePanel);
        titleButton.onClick.AddListener(_toTitle);
        batuButton.onClick.AddListener(_closePanel);
        BGMSlider.value = SoundManager.I.bgmVolume;
        BGMSlider.onValueChanged.AddListener((value) => {
            SoundManager.I.bgmVolume = value;
            SoundManager.I.ChangeBGMVolumes();
            SoundManager.I.seVolume = value;
            SoundManager.I.ChangeSEVolumes();
        });
    }

    private void _toTitle()
    {
        
        StartCoroutine(_title());
        
    }

    private void _viewPanel()
    {
        

        SoundManager.I.PlaySE(SESoundData.SE.MENU_IN);

        StartCoroutine(showPanelCO());
    }

    //パネルの表示に時間差(ボタン音の後に）
    IEnumerator showPanelCO()
    {
        
        yield return new WaitForSeconds(0.35f);
        if (isPlaing)
        {
            Time.timeScale = 0;
            beforState = GameManager.I.gameState;
            GameManager.I.gameState = GameManager.GAMESTATE.WAIT;
        }
        stageSelector.UpdateCurrentStage();
        optionPanel.SetActive(true);
        
    }


    private void _closePanel()
    {
 
        
        if (isPlaing)
        {
            if (GameManager.I.currentCup != SceneController.I.selectStageNum)
            {
                
                StartCoroutine(_reLoad());
            }
            else
            {
                
                GameManager.I.gameState = beforState;
            }
            Time.timeScale = 1;

        }
        SoundManager.I.PlaySE(SESoundData.SE.MENU_OUT);
        optionPanel.SetActive(false);

    }

    IEnumerator _title()
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.3f);
        fade.ShowEndFade("Title");
    }

    IEnumerator _reLoad()
    {
        yield return new WaitForSeconds(0.3f);
        fade.ShowEndFade("TanisuScene");
    }
}
