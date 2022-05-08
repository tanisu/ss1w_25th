using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Board : MonoBehaviour
{
    Rigidbody2D rgbd2d;
    bool isWater;
    void Start()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.DORotate(new Vector3(0, 0, 50f), 0.5f);

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.DORotate(new Vector3(0, 0, -50f), 0.5f);
        }

        //if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        //{
        //    transform.DORotate(Vector3.zero, 0.1f);
        //}
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
}
