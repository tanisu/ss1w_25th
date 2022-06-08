using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Water : MonoBehaviour
{
    
    [SerializeField] float force;
    [SerializeField] ParticleSystem sibuki;
    
    Rigidbody2D rgbd2d;
    public WaterGenerator waterGenerator;
   // bool isWater;
    void Start()
    {
        //isWater = gameObject.name == "Water";

        rgbd2d = GetComponent<Rigidbody2D>();
        
    }


    public void Wave(float _force,int _vec)
    {
        rgbd2d.AddForce(new Vector2(_force * _vec, 0));
    }



    public void StopMove()
    {
        rgbd2d.velocity = Vector3.zero;
        rgbd2d.angularVelocity = 0f;
    }

    public void EntryStage(bool isSlow = false)
    {
        rgbd2d.velocity = Vector3.zero;
        rgbd2d.angularVelocity = 0f;
        float x = 0;
        if (isSlow)
        {
            x = 0.5f;
            force = 30;
        }
        else
        {
            x = Random.Range(-1, 2);
        }
         
        
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
        if (collision.CompareTag("Surfer") /*&& isWater*/)
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
