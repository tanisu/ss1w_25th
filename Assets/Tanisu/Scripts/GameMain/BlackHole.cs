using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlackHole : MonoBehaviour
{
    [SerializeField] Transform corePosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.CompareTag("Water"))
        //{
        //    Vector3 defaultScale = collision.transform.localScale;
        //    collision.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        //    collision.transform.DOScale(0f, 0.7f);
        //    collision.transform.DOMove(corePosition.position,0.7f).OnComplete(()=> {
        //        collision.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        //        collision.transform.localScale = defaultScale;
        //        collision.GetComponent<PoolContent>().HideFromStage();
        //    });

        //}
    }
}
