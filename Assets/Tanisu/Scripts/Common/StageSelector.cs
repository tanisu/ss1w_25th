using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageSelector : MonoBehaviour
{
    [SerializeField]
    Button increment, decrement;
    [SerializeField]
    TextMeshProUGUI stageText;
    int currentStageNum,maxStageNum;
    string[] stagesText;
    void Start()
    {
        Config.I.stageSelector = this;
        increment.onClick.AddListener(() => _incrementStage());
        decrement.onClick.AddListener(() => _decrementStage());
        maxStageNum = PlayerPrefs.GetInt("maxCup");
        if(SceneController.I.GetCurrentScene() == "TanisuScene")
        {
            currentStageNum = GameManager.I.currentCup;
        }
        else
        {
            currentStageNum = maxStageNum;
        }
        
        ChangeLangView();
        _updateStageText();
    }

    private void _incrementStage()
    {
        if (currentStageNum >= Config.I.stages.Length - 1 || currentStageNum >= maxStageNum) return;
        currentStageNum++;
        _updateStageText();
    }

    private void _decrementStage()
    {
        if (currentStageNum <= 0) return;
        currentStageNum--;
        
        _updateStageText();
    }

    private void _updateStageText()
    {
        SoundManager.I.PlaySE(SESoundData.SE.TAP_CURSOR);
        SceneController.I.selectStageNum = currentStageNum;
        UpdateStageText();

    }

    public void UpdateCurrentStage()
    {
        if (SceneController.I.GetCurrentScene() == "TanisuScene")
        {
            currentStageNum = GameManager.I.currentCup;
            
        }
        else
        {
            currentStageNum = maxStageNum;
        }
        ChangeLangView();
        _updateStageText();
    }

    public void ChangeLangView()
    {
        if (Config.I.lang == Config.LANG.JP)
        {
            stagesText = Config.I.stagesJP;
        }
        else
        {
            stagesText = Config.I.stages;
        }
        UpdateStageText();

    }

    public void UpdateStageText()
    {
        
        stageText.text = $"{currentStageNum + 1}\n{stagesText[currentStageNum]}";
    }
    

}
