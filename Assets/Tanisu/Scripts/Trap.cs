using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    Vector3 startPos;
    Rigidbody2D rgbd2d = null;
    PolygonCollider2D pc2d = null;

    public void InitTrap()
    {
        startPos = transform.localPosition;
        if (GetComponent<Rigidbody2D>())
        {
            rgbd2d = GetComponent<Rigidbody2D>();
        }
        if (GetComponent<PolygonCollider2D>())
        {
            pc2d = GetComponent<PolygonCollider2D>();
        }
    }
        
    public void TrapActivation()
    {
        if (rgbd2d)
        {
            rgbd2d.bodyType = RigidbodyType2D.Dynamic;
        }
        if (pc2d)
        {
            pc2d.enabled = true;
        }
        
    }

    public void ResetTrap()
    {
        if (rgbd2d)
        {
            rgbd2d.velocity = Vector3.zero;
            rgbd2d.angularVelocity = 0f;
            rgbd2d.bodyType = RigidbodyType2D.Kinematic;
        }
        if (pc2d)
        {
            pc2d.enabled = false;
        }
        transform.localPosition = startPos;
        transform.localRotation = Quaternion.identity;

    }
}
