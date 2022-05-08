using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolContent : MonoBehaviour
{
    ObjectPool pool;
    Water water;

    
    void Start()
    {
        pool = transform.parent.GetComponent<ObjectPool>();
        water = GetComponent<Water>();
        gameObject.SetActive(false);
    }

    public void ShowInStage(Vector3 _pos)
    {
        transform.position = _pos;
        water.EntryStage();

    }

        public void HideFromStage()
    {
        pool.Collect(this);
    }
}
