using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Board : MonoBehaviour
{

    [SerializeField] float timeLimit,roteLimit;
    [SerializeField] GameObject surfer;
    float time;
    Rigidbody2D rgbd2d;
    bool isWater;

    void Start()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {

        if (GameManager.I.gameState != GameManager.GAMESTATE.PLAY) return;
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.DORotate(new Vector3(0, 0, 50f), 0.5f);

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.DORotate(new Vector3(0, 0, -50f), 0.5f);
        }
        
        if(Mathf.Abs(transform.rotation.z) > roteLimit)
        {
            if(_updateTimer() >= 1)
            {
                
                surfer.transform.parent = transform.parent;
                Rigidbody2D surferRgbd2d =  surfer.GetComponent<Rigidbody2D>();
                surferRgbd2d.bodyType = RigidbodyType2D.Dynamic;
                surferRgbd2d.simulated = true;
                
                GameManager.I.GameOver();
            }
        }
        else
        {
            if(time > 0)
            {
                time = 0;
            }
        }

        if(transform.position.y < -5.5f)
        {
            //retry
            GameManager.I.gameState = GameManager.GAMESTATE.REPLAY;
        }
        
    }

    float _updateTimer()
    {
        time += Time.deltaTime;
        float timer = time / timeLimit;
        //Debug.Log(timer);
        return timer;
    }

    private void FixedUpdate()
    {
        if (!isWater) return;
        if (Input.GetKey(KeyCode.A))
        {
            rgbd2d.AddForce(new Vector2(-1f, 0));

        }
        if (Input.GetKey(KeyCode.D))
        {
            rgbd2d.AddForce(new Vector2(1f, 0));

        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            rgbd2d.velocity = Vector3.zero;
            rgbd2d.angularVelocity = 0f;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            isWater = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            isWater = false;
        }
    }
    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ClearLine"))
        {
            if(collision.transform.position.y < transform.position.y)
            {
                
                GameManager.I.CupClear();
            }
        }
    }
}
