using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField] SpriteRenderer board, player;

    public static Title I;
    private void Awake()
    {
        if(I == null)
        {
            I = this;
        }
    }

    void Start()
    {
        SoundManager.I.PlayBGM(BGMSoundData.BGM.TITLE);
        SceneController.I.selectStageNum = PlayerPrefs.GetInt("maxCup");
        ChangePlayerSprite();
    }


    public void ChangePlayerSprite()
    {

        board.sprite = Config.I.selectedPlayerData.board;
        player.sprite = Config.I.selectedPlayerData.surfer;
    }
}
