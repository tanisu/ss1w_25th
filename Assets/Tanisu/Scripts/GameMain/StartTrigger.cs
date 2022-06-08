using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrigger : MonoBehaviour
{
    [SerializeField] PolygonCollider2D plcd2d;
    public bool isStart;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Board"))
        {
            plcd2d.enabled = true;
            isStart = true;
        }
    }

    public void OffCollider()
    {
        plcd2d.enabled = false;
        isStart = false;
    }
}
