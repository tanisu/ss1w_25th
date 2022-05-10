using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Water : MonoBehaviour
{
    
    [SerializeField] float force;
    [SerializeField] ParticleSystem sibuki;
    //int x = 1;
    //float waveInterval, interval;
    Rigidbody2D rgbd2d;
    public WaterGenerator waterGenerator;   

    void Start()
    {
        //waveInterval = 2.02f;
        rgbd2d = GetComponent<Rigidbody2D>();
        //interval = waveInterval;
    }


    //private void FixedUpdate()
    //{
    //    if (GameManager.I.gameState != GameManager.GAMESTATE.PLAY) return;
    //    interval -= Time.fixedDeltaTime;
    //    if(interval <= 0)
    //    {
    //        rgbd2d.AddForce(new Vector2(force * x, 0));
    //        interval = waveInterval;
    //        x *= -1;
    //    }
    //}

    public void Wave(float _force,int _vec)
    {
        rgbd2d.AddForce(new Vector2(_force * _vec, 0));
    }



    public void StopMove()
    {
        rgbd2d.velocity = Vector3.zero;
    }

    public void EntryStage()
    {
        rgbd2d.velocity = Vector3.zero;
        rgbd2d.angularVelocity = 0f;
        int x = Random.Range(-1, 2);
        rgbd2d.AddForce(new Vector2(x * force, x));
    }

    public void EixtStage()
    {
        waterGenerator.RemoveWater(this);
        rgbd2d.velocity = Vector3.zero;
        rgbd2d.angularVelocity = 0f;
        
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Surfer"))
        {
            
            if(collision.transform.position.y < transform.position.y)
            {
                if (!SoundManager.I.isSibuki)
                {
                    SoundManager.I.PlaySE(SESoundData.SE.SHIBUKI);
                    SoundManager.I.SibukiChu();
                }
                
                
                Instantiate(sibuki,new Vector3(transform.position.x,transform.position.y),transform.rotation);
                GetComponent<PoolContent>().HideFromStage();
            }
            
        }
    }


}
