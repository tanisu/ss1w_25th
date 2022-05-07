using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Water : MonoBehaviour
{
    [SerializeField] float force = 250f;
    int x = 1;
    float waveInterval, interval;
    Rigidbody2D rgbd2d;
    bool leave;

    void Start()
    {
        waveInterval = 2.02f;
        rgbd2d = GetComponent<Rigidbody2D>();
        interval = waveInterval;
    }


    private void FixedUpdate()
    {
        interval -= Time.fixedDeltaTime;
        if(interval <= 0)
        {
            
            rgbd2d.AddForce(new Vector2(force * x, 0));
            interval = waveInterval;
            x *= -1;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

            
            transform.DOScale(new Vector3(0.3f, 0.3f, 1), 0.51f).OnComplete(()=> {
                transform.DOScale(Vector3.one, 0.11f);
            });

    }


    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Water"))
    //    {

    //        transform.localScale = Vector3.one;
    //    }
    //}

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Water"))
    //    {

    //        transform.localScale = Vector3.one;
    //    }
    //}


}
