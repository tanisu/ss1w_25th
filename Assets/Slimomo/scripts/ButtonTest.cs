using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonTest : MonoBehaviour
{

    [SerializeField] RectTransform re;


    public void onClick()
    {
        re.localScale = Vector3.one;

        re.DOPunchScale(Vector3.one * 0.1f,0.2f,1)
            .SetEase(Ease.OutExpo)
            .SetLink(gameObject);

    }



}
