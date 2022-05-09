using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surfer : MonoBehaviour
{
    [SerializeField] Transform board;
    Rigidbody2D rgbd2d;

    void Start()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
    }

    public void LeaveBoard()
    {
        transform.parent = transform.parent;
        rgbd2d.bodyType = RigidbodyType2D.Dynamic;
        rgbd2d.simulated = true;
    }

    public void SetOnBoard()
    {
        transform.parent = board;
        transform.localPosition = new Vector3(0, 0.38f, 0);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        rgbd2d.velocity = Vector3.zero;
        rgbd2d.bodyType = RigidbodyType2D.Kinematic;
        
        rgbd2d.simulated = false;
    }
}
