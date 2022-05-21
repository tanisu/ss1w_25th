using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Board : MonoBehaviour
{

    [SerializeField] float timeLimit,roteLimit,roteMax,beforePosX,thrust;
    [SerializeField] Surfer surfer;
    [SerializeField] GameObject namiL, namiR;
    [SerializeField] string frontLayer, backLayer;
    float time;
    Rigidbody2D rgbd2d;
    //BoxCollider2D bc2d;
    PolygonCollider2D pc2d;
    bool isWater;
    Tween tween;
    SpriteRenderer sp;
    private enum DIRECTION
    {
        NONE,
        LEFT,
        RIGHT
    }

    DIRECTION direction;

    void Start()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
        pc2d = GetComponent<PolygonCollider2D>();
        
        sp = GetComponent<SpriteRenderer>();
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

    public void PushButton(int dir)
    {
        tween = transform.DORotate(new Vector3(0, 0, roteMax * dir), 0.5f);
        direction = dir < 0 ? DIRECTION.LEFT : DIRECTION.RIGHT ;
        transform.Translate(new Vector3(dir, 0) * thrust * Time.deltaTime);
    }

    public void ReleaseButton()
    {
        tween.Kill();
        transform.DORotate(new Vector3(0, 0, 0), 0.5f);
    }

    float _updateTimer()
    {
        time += Time.deltaTime;
        float timer = time / timeLimit;
        return timer;
    }

    private void FixedUpdate()
    {
        if (!isWater) return;
        //if (Input.GetKey(KeyCode.A))
        //{
        //    rgbd2d.AddForce(new Vector2(1f, 0));

        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    rgbd2d.AddForce(new Vector2(-1f, 0));

        //}
        //switch (direction)
        //{
        //    case DIRECTION.LEFT:
        //        rgbd2d.AddForce(new Vector2(thrust, 0));
        //        break;
        //    case DIRECTION.RIGHT:
        //        rgbd2d.AddForce(new Vector2(-thrust, 0));
        //        break;
        //}

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            isWater = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
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
            if(collision.transform.position.y < transform.position.y && GameManager.I.gameState == GameManager.GAMESTATE.PLAY)
            {
                GameManager.I.CupClear();
                collision.gameObject.SetActive(false);
            }
        }
    }

    public void OffPhysics()
    {
        sp.sortingLayerName = frontLayer;
        surfer.SetSortingLayerName(frontLayer);
        rgbd2d.bodyType = RigidbodyType2D.Kinematic;
        rgbd2d.simulated = false;
        pc2d.enabled = false;
    }

    public void OnPhysics()
    {
        sp.sortingLayerName = backLayer;
        surfer.SetSortingLayerName(backLayer);
        rgbd2d.velocity = Vector3.zero;
        rgbd2d.angularVelocity = 0f;
        rgbd2d.bodyType = RigidbodyType2D.Dynamic;
        rgbd2d.simulated = true;
        pc2d.enabled = true;
    }

    public void SetStartPos(Vector2 _startPos)
    {
        
        transform.DOLocalMove(_startPos, GameManager.I.cupChangeTime);
        transform.DORotate(Vector3.zero, GameManager.I.cupChangeTime);
    }

    public void SetBeforePos(Vector2 _startPos)
    {
        transform.position = new Vector3(beforePosX, _startPos.y);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
