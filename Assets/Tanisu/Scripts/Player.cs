using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject board, surfer;
    bool ready;
    void Start()
    {
            
    }


    public void switchRgbd()
    {
        if (!ready)
        {
            board.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            board.GetComponent<BoxCollider2D>().enabled = false;
            ready = true;
        }
        else
        {
            board.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            board.GetComponent<BoxCollider2D>().enabled = true;
            ready = false;
        }
        
    }
}
