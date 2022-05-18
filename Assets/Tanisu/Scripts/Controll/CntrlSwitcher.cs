using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CntrlSwitcher : MonoBehaviour
{
    Slider slider;
    
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(val => Config.I.SwitchController((int)val));
    }

    
}
