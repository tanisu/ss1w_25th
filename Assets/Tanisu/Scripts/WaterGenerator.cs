using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGenerator : MonoBehaviour
{
    [SerializeField] GameObject waterPrefab;
    [SerializeField] int setCount;
    bool setted,loopStart;
    
    void Start()
    {
        StartCoroutine(_createWater());
    }

    
    void Update()
    {
        //if (setted && !loopStart)
        //{
        //    StartCoroutine(_reCreateWater());
        //}
    }

    IEnumerator _createWater()
    {
        for (int i = 0; i < setCount; i++)
        {
            GameObject water = Instantiate(waterPrefab, transform.position, transform.rotation,transform.parent.transform);
            float moveX = Random.Range(-180f,180f);
            Vector2 force = new Vector2(moveX, 0);
            water.GetComponent<Rigidbody2D>().AddForce(force);
            yield return null;
        }
        StartCoroutine(_reCreateWater());
    }
    IEnumerator _reCreateWater()
    {

        yield return new WaitForSeconds(2f);

        for (int i = 0; i < 1000; i++)
        {
            GameObject water = Instantiate(waterPrefab, transform.position, transform.rotation, transform.parent.transform);
            float moveX = Random.Range(-180f, 180f);
            Vector2 force = new Vector2(moveX, 0);
            water.GetComponent<Rigidbody2D>().AddForce(force);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
