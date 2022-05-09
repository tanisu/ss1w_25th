using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject board, surfer;
    [SerializeField] float startPosX = -1.5f;
    bool ready;

    
    public void switchRgbd()
    {
        if (!ready)
        {
            board.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            board.GetComponent<BoxCollider2D>().enabled = false;
            ready = true;
        }
        else
        {
            board.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            board.GetComponent<BoxCollider2D>().enabled = true;
            ready = false;
        }
    }

    public void SetPlayerPos()
    {
        board.transform.DOLocalMoveX(startPosX, GameManager.I.cupChangeTime);
    }

    public void SetRetry()
    {
        surfer.transform.parent = board.transform;
        //board.transform.position = 
    }
}
