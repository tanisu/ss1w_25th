using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ButtonTest2 : MonoBehaviour
{
    [SerializeField] RectTransform re;


    //完了するのがわからないー。。

    public void PonintEnter()
    {
        Debug.Log("Enter!!!");
        re.localScale = Vector3.one;

        re.DOPunchScale(Vector3.one * 0.1f, 0.2f, 1)
           .SetLoops(-1, LoopType.Restart)
           .SetEase(Ease.OutExpo)
           .SetLink(gameObject);
    }

    public void PonintClick()
    {

        re.localScale = Vector3.one * 0.5f;
        re.DOScale(1f, 0.6f)
          .SetLink(gameObject)
          .SetEase(Ease.OutBack, 5f);


    }

    public void PonintExit()
    {

        Debug.Log("Out!!!");
        re.localScale = Vector3.one;
        re.DOPause();
    }
}
