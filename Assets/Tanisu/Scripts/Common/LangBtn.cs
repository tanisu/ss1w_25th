using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LangBtn : MonoBehaviour
{
    [SerializeField] Color selectedColor, nomalColor;
    [SerializeField] Button jpBtn, enBtn;
    Image jpImg, enImg;
    void Start()
    {
        jpImg = jpBtn.GetComponent<Image>();
        enImg = enBtn.GetComponent<Image>();
        switch (Config.I.lang)
        {
            case Config.LANG.EN:
                enImg.color = selectedColor;
                jpImg.color = nomalColor;
                break;
            case Config.LANG.JP:
                enImg.color = nomalColor;
                jpImg.color = selectedColor;
                break;
        }

        jpBtn.onClick.AddListener(() => {
            enImg.color = nomalColor;
            jpImg.color = selectedColor;
        });
        enBtn.onClick.AddListener(() => {
            enImg.color = selectedColor;
            jpImg.color = nomalColor;
        });
    }

    

}
