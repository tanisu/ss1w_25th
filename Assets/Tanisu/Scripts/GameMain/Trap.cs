using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Trap : MonoBehaviour
{
    
    [SerializeField] Vector3 movePos;
    [SerializeField] float deg,loopX,loopY,size,time;
    [SerializeField] bool isStartPc2d;
    Vector3 startPos;
    Rigidbody2D rgbd2d = null;
    PolygonCollider2D pc2d = null;
    Animator animator;
    
    Tween tween;
    enum TRAPTYPE
    {
        FALL,
        MOVE,
        ROTA,
        SIZE,
        SEQ,
        ANIM
    }

    [SerializeField] TRAPTYPE trapType;

    public void InitTrap()
    {
        startPos = transform.localPosition;
        if (GetComponent<Rigidbody2D>())
        {
            rgbd2d = GetComponent<Rigidbody2D>();
        }
        if (GetComponent<PolygonCollider2D>())
        {
            pc2d = GetComponent<PolygonCollider2D>();
        }
        if (GetComponent<Animator>())
        {
            animator = GetComponent<Animator>();
            
        }
    }
        
    public void TrapActivation()
    {
        switch (trapType)
        {
            case TRAPTYPE.FALL:
                _fallTrap();
                break;
            case TRAPTYPE.MOVE:
                _moveTrap();
                break;
            case TRAPTYPE.ROTA:
                _rotaTrap();
                break;
            case TRAPTYPE.SIZE:
                _sizeTrap();
                break;
            case TRAPTYPE.SEQ:
                _seqTrap();
                break;
            case TRAPTYPE.ANIM:
                _animTrap();
                break;
            
        }
    }

    private void Update()
    {
        if(transform.position.y < -10)
        {
            if (rgbd2d)
            {
                rgbd2d.velocity = Vector3.zero;
                rgbd2d.angularVelocity = 0f;
                rgbd2d.bodyType = RigidbodyType2D.Kinematic;
            }
        }
    }

    private void _animTrap()
    {
        if (pc2d)
        {
            pc2d.enabled = true;
        }
        animator.SetBool("trap", true);
        
        //anim.enabled = true;
        
    }

    private void _seqTrap()
    {
        if (pc2d)
        {
            pc2d.enabled = true;
        }
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMoveY(movePos.y, 1f))
            .Append(transform.DOLocalRotate(new Vector3(0,0,deg), 1f))
            .Append(transform.DOLocalMoveX(movePos.x,1f)).AppendCallback(()=> {
                if (loopX != 0)
                {
                    tween = transform.DOLocalMoveX(loopX, 1f).SetLoops(-1, LoopType.Yoyo).SetLink(gameObject);
                }
                if(loopY != 0)
                {
                    tween = transform.DOLocalMoveY(loopY, 1f).SetLink(gameObject);
                }
            }).SetLink(gameObject);
    }

    private void _sizeTrap()
    {
        if (rgbd2d)
        {
            rgbd2d.bodyType = RigidbodyType2D.Dynamic;
        }
        if (pc2d)
        {
            pc2d.enabled = true;
        }
        tween = transform.DOScale(new Vector3(size, size), time).SetLink(gameObject);
    }

    private void _rotaTrap()
    {
        if (pc2d)
        {
            pc2d.enabled = true;
        }
        tween = transform.DOLocalRotate(new Vector3(0,0,deg),2f).SetLoops(-1, LoopType.Yoyo).SetLink(gameObject);
    }

    private void _moveTrap()
    {
        transform.localPosition = movePos;
    }

    private void _fallTrap()
    {
        if (rgbd2d)
        {
            rgbd2d.bodyType = RigidbodyType2D.Dynamic;
        }
        if (pc2d)
        {
            pc2d.enabled = true;
        }
    }

    public void ResetTrap()
    {
        if (rgbd2d)
        {
            rgbd2d.velocity = Vector3.zero;
            rgbd2d.angularVelocity = 0f;
            rgbd2d.bodyType = RigidbodyType2D.Kinematic;
        }
        if (pc2d && !isStartPc2d)
        {
            pc2d.enabled = false;
        }
        if (animator)
        {

            animator.SetBool("trap", false);
            
        }

        tween.Kill();
        transform.localScale = Vector3.one;
        transform.localPosition = startPos;
        transform.localRotation = Quaternion.identity;

    }
}
