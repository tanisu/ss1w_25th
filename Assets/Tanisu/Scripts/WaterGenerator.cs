using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGenerator : MonoBehaviour
{
    [SerializeField] ObjectPool waterPool;
    
    void Start()
    {
        
        StartCoroutine(_launchWater());
    }

    
    void Update()
    {

    }

    IEnumerator _launchWater()
    {
 

        for (int i = 0; i < waterPool.maxCount ; i++)
        {
            waterPool.Launch(transform.position);
            yield return new WaitForSeconds(0.01f);
        }
            


        
        //yield return null;
    }

}
