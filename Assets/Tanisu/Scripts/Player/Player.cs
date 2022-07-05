using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] Board board;
    [SerializeField] Surfer surfer;
    [SerializeField] Vector2 startPos;
    bool ready;

    private void Start()
    {
        board.SetConfigSprites();
        surfer.SetConfigSprites();

    }

    public void switchRgbd()
    {
        if (!ready)
        {

            board.OffPhysics();
            ready = true;
        }
        else
        {
            board.OnPhysics();
            ready = false;
        }
    }

    public void SetPlayerPos()
    {
        board.SetStartPos(startPos);
    }

    public void SetRetry()
    {
        StartCoroutine(_setStartPos());
    }

    public void ChangeSprite()
    {
        board.SetConfigSprites();
        surfer.SetConfigSprites();
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    IEnumerator _setStartPos()
    {
        yield return new WaitForSeconds(2.5f);
        board.SetBeforePos(startPos);
        surfer.SetOnBoard();
        switchRgbd();
        board.transform.DOMove(startPos, 1f).OnComplete(()=> { 
            switchRgbd();
            GameManager.I.gameState = GameManager.GAMESTATE.PLAY;
        }).SetLink(board.gameObject);
    }

    public void ToStartPos()
    {
        board.transform.DOMove(startPos, 1f).OnComplete(() => {
            GameManager.I.gameState = GameManager.GAMESTATE.PLAY;
        }).SetLink(board.gameObject);
    }
}
