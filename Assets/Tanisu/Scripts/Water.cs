using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] float force = 200f;
    int x = 1;
    float waveInterval, interval;
    Rigidbody2D rgbd2d;

    void Start()
    {
        waveInterval = 2.5f;
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
    
}
