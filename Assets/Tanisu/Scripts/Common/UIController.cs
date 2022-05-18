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

    bool isPlaing;
    void Start()
    {
        isPlaing = GameObject.Find("GameManager");
        
        soundButton.onClick.AddListener(_viewPanel);
        closeButton.onClick.AddListener(_closePanel);
        BGMSlider.value = SoundManager.I.bgmVolume;
        BGMSlider.onValueChanged.AddListener((value) => {
            SoundManager.I.bgmVolume = value;
            SoundManager.I.ChangeBGMVolumes();
            SoundManager.I.seVolume = value;
            SoundManager.I.ChangeSEVolumes();
        });
    }


    private void _viewPanel()
    {
        if (isPlaing)
        {
            Time.timeScale = 0;
        }

        slidePanel.SetActive(true);
    }

    private void _closePanel()
    {
        if (isPlaing)
        {
            Time.timeScale = 1;   
        }
        slidePanel.SetActive(false);
    }

    
}
