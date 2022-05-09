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

    IEnumerator _setStartPos()
    {
        yield return new WaitForSeconds(2.5f);
        board.SetBeforePos(startPos);
        surfer.SetOnBoard();
        switchRgbd();
        board.transform.DOMove(startPos, 1f).OnComplete(()=> { switchRgbd(); });
    }
}
