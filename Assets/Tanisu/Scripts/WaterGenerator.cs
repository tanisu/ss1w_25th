using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGenerator : MonoBehaviour
{
    [SerializeField] GameObject waterPrefab;
    [SerializeField] int setCount;
    bool setted, startOnce;
    
    void Start()
    {
        StartCoroutine(_createWater());
    }

    
    void Update()
    {
        if (setted & !startOnce)
        {
            StartCoroutine(_createWater());
        }
    }

    IEnumerator _createWater()
    {
        for (int i = 0; i < setCount; i++)
        {
            GameObject water = Instantiate(waterPrefab, transform.position, transform.rotation,transform.parent.transform);
            float moveX = Random.Range(-180f,180f);
            Vector2 force = new Vector2(moveX, 0);
            water.GetComponent<Rigidbody2D>().AddForce(force);
            yield return new WaitForFixedUpdate();
        }
        setted = true;
        startOnce = setted == true ? true : false;

    }
}
