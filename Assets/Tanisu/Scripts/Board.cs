using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Board : MonoBehaviour
{

    [SerializeField] float timeLimit,roteLimit,roteMax;
    [SerializeField] Surfer surfer;
    float time;
    Rigidbody2D rgbd2d;
    BoxCollider2D bc2d;
    //bool isWater;
    Tween tween;

    void Start()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
    }

    
    void Update()
    {

        if (GameManager.I.gameState != GameManager.GAMESTATE.PLAY) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            tween = transform.DORotate(new Vector3(0, 0, roteMax), 0.5f);
            

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            tween = transform.DORotate(new Vector3(0, 0, -roteMax), 0.5f);
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            tween.Kill();
            //transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f);
        }


        if (Mathf.Abs(transform.rotation.z) > roteLimit)
        {
            if(_updateTimer() >= 1)
            {
                surfer.LeaveBoard();
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
            GameManager.I.GameOver();
        }
    }



    float _updateTimer()
    {
        time += Time.deltaTime;
        float timer = time / timeLimit;
        return timer;
    }

    private void FixedUpdate()
    {
        //if (!isWater) return;
        //if (Input.GetKey(KeyCode.A))
        //{
        //    rgbd2d.AddForce(new Vector2(-1f, 0));

        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    rgbd2d.AddForce(new Vector2(1f, 0));

        //}

    }
    
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Water"))
    //    {
    //        isWater = true;
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Water"))
    //    {
    //        isWater = false;
    //    }
    //}
    

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

    public void OffPhysics()
    {
        rgbd2d.bodyType = RigidbodyType2D.Kinematic;
        rgbd2d.simulated = false;
        bc2d.enabled = false;
    }

    public void OnPhysics()
    {
        rgbd2d.velocity = Vector3.zero;
        rgbd2d.angularVelocity = 0f;
        rgbd2d.bodyType = RigidbodyType2D.Dynamic;
        rgbd2d.simulated = true;
        bc2d.enabled = true;
    }

    public void SetStartPos(Vector2 _startPos)
    {
        transform.DOLocalMove(_startPos, GameManager.I.cupChangeTime);
        transform.DORotate(Vector3.zero, GameManager.I.cupChangeTime);
    }

    public void SetBeforePos(Vector2 _startPos)
    {
        transform.position = new Vector3(-4.5f, _startPos.y);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
