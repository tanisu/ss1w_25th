using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.U2D.Animation;

public class Ibukuro : MonoBehaviour
{
    [SerializeField] Cup cup;
    Rigidbody2D[] rgbd2ds;
    
    bool initIbukuro;
    private void Start()
    {
        rgbd2ds = GetComponentsInChildren<Rigidbody2D>();
        
        
    }
    // Update is called once per frame
    void Update()
    {
        if (cup.GetCurrentCup() && !initIbukuro)
        {
            foreach(Rigidbody2D rgbd2d in rgbd2ds)
            {
                rgbd2d.simulated = true;
            }
            initIbukuro = true;
            
        }
    }
}
