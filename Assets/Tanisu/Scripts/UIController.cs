using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] Button  soundButton,closeButton;
    [SerializeField] GameObject slidePanel;
    [SerializeField] Slider BGMSlider;
    void Start()
    {
        
        //startButton.onClick.AddListener(_startGame);
        soundButton.onClick.AddListener(_viewPanel);
        closeButton.onClick.AddListener(_closePanel);
        BGMSlider.value = SoundManager.I.bgmVolume;
        BGMSlider.onValueChanged.AddListener((value) => {
            SoundManager.I.bgmVolume = value;
            SoundManager.I.ChangeBGMVolumes();
        });
    }


    private void _viewPanel()
    {
        slidePanel.SetActive(true);
    }

    private void _closePanel()
    {
        slidePanel.SetActive(false);
    }

    
}
