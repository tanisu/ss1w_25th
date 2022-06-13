using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject particle;

    [SerializeField]
    private GameObject player;

    public void ShowEffect()
    {
        Instantiate(particle, player.transform.position, Quaternion.identity); //パーティクル用ゲームオブジェクト生成
    }

}
