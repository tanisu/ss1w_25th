using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Surfer : MonoBehaviour
{
    [SerializeField] Transform board;
    [SerializeField] Sprite fall,onBoard;
    Rigidbody2D rgbd2d;
    SpriteRenderer sp;
    Vector3 defaultScale;
    bool isTween;
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        rgbd2d = GetComponent<Rigidbody2D>();
        defaultScale = transform.localScale;
    }

    public void LeaveBoard()
    {
        sp.sprite = fall;
        transform.parent = transform.parent;
        rgbd2d.bodyType = RigidbodyType2D.Dynamic;
       // rgbd2d.simulated = true;
    }

    public void SetOnBoard()
    {
        if (isTween)
        {
            _resetSurfer();
            isTween = false;
        }
        sp.sprite = onBoard;
        transform.parent = board;
        transform.localPosition = new Vector3(0, 0.38f, 0);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        rgbd2d.velocity = Vector3.zero;
        rgbd2d.bodyType = RigidbodyType2D.Kinematic;

       // rgbd2d.simulated = false;
    }

    public void SetSortingLayerName(string _layerName)
    {
        sp.sortingLayerName = _layerName;
    }

    public void SetConfigSprites()
    {
        onBoard = Config.I.selectedPlayerData.surfer;
        fall = Config.I.selectedPlayerData.fall;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BlackHole"))
        {
            
            rgbd2d.bodyType = RigidbodyType2D.Static;
            transform.DOScale(0f, 0.5f);
            transform.DOMove(collision.transform.position, 0.5f).OnComplete(() =>
            {
                //_resetSurfer();
                isTween = true;
                GameManager.I.GameOver();
            });
        }
    }

    private void _resetSurfer()
    {
        rgbd2d.bodyType = RigidbodyType2D.Dynamic;
        transform.localScale = defaultScale;
        
    }

}
