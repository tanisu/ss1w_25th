using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSample : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    //シーンがスタートして
    //フェードスタートのアニメーションをよぶ
    void Start()
    {
        anim.SetTrigger("Start");
        StartCoroutine(FadeStartCO());
    }

    //１秒アニメーション待ったあと
    //フェードパネルを非表示
    IEnumerator FadeStartCO()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }


    //フェードパネルを表示
    //フェードエンドのアニメーションをよぶ
    //１秒アニメーションを待ったあと
    //シーンの切り替え前に入れる
    public void ShowEndFade()
    {
        gameObject.SetActive(true);
        anim.SetTrigger("End");
        StartCoroutine(FadeEndCO());
    }

    IEnumerator FadeEndCO()
    {
        yield return new WaitForSeconds(1f);
        //このあとにシーン移動をさせたいTODO
        Debug.Log("シーン移動");
    }
}
