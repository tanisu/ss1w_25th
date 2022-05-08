using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Water : MonoBehaviour
{
    
    [SerializeField] float force = 50f;
    int x = 1;
    float waveInterval, interval;
    Rigidbody2D rgbd2d;
    

    void Start()
    {
        waveInterval = 2.02f;
        rgbd2d = GetComponent<Rigidbody2D>();
        interval = waveInterval;
    }


    private void FixedUpdate()
    {
        if (GameManager.I.gameState != GameManager.GAMESTATE.PLAY) return;
        interval -= Time.fixedDeltaTime;
        if(interval <= 0)
        {
            rgbd2d.AddForce(new Vector2(force * x, 0));
            interval = waveInterval;
            x *= -1;
        }
    }



    public void StopMove()
    {
        
        rgbd2d.velocity = Vector3.zero;
    }

    public void EntryStage()
    {
        rgbd2d.AddForce(new Vector2(force, 0));
    }




}
