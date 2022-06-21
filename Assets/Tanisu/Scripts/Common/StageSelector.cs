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
    
    void Start()
    {
        increment.onClick.AddListener(() => _incrementStage());
        decrement.onClick.AddListener(() => _decrementStage());
        maxStageNum = PlayerPrefs.GetInt("maxCup");
        currentStageNum = maxStageNum;
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
        if(Config.I.lang == Config.LANG.JP)
        {
            stageText.text = $"{currentStageNum + 1}\n{Config.I.stagesJP[currentStageNum]}";
        }
        else
        {
            stageText.text = $"{currentStageNum + 1}\n{Config.I.stages[currentStageNum]}";
        }
        
    }
    

}
