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

    private void Update()
    {
        if(transform.position.y < -6f || transform.localPosition.x > 3.2f || transform.localPosition.x < -3.2f )
        {
            water.EixtStage();
            HideFromStage();
        }
    }

    public void ShowInStage(Vector3 _pos,bool isSlow = false)
    {
        transform.position = _pos;
        water.EntryStage(isSlow);

    }

    public void HideFromStage()
    {
        pool.Collect(this);
    }
}
