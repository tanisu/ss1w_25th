using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    [SerializeField] Board board;
    Vector2 firstPos;
    Vector2 secondPos;
    Vector2 currentPos;
    public float detectionSensitivBottom = -0.8f;
    public float detectionSensitivUp = 0.8f;


    void Update()
    {
        _swipeWithMouse();
    }

    void _swipeWithMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButton(0))
        {
            secondPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            currentPos = new Vector2(secondPos.x - firstPos.x, secondPos.y - firstPos.y);
            currentPos.Normalize();
            
            if (currentPos.x < 0 && currentPos.y > detectionSensitivBottom && currentPos.y < detectionSensitivUp)
            {
                board.PushButton(1);
            }
            if (currentPos.x > 0 && currentPos.y > detectionSensitivBottom && currentPos.y < detectionSensitivUp)
            {
                board.PushButton(-1);
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            board.ReleaseButton();
        }
    }
}
