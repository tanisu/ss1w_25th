using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrigger : MonoBehaviour
{
    [SerializeField] PolygonCollider2D plcd2d;
    [SerializeField] GameObject waterGenerateor;
    public bool reStart;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Board"))
        {
            plcd2d.enabled = true;
            
            waterGenerateor.SetActive(true);
            if (reStart)
            {
                waterGenerateor.GetComponent<WaterGenerator>().ReStart();
            }
            reStart = false;
        }
    }

    public void OffCollider()
    {
        waterGenerateor.SetActive(false);
        plcd2d.enabled = false;
        reStart = true;
    }
}
