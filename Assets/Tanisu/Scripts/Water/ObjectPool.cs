using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] PoolContent content = default;
    public int maxCount = 300;
    Queue<PoolContent> objQueue;

    private void Awake()
    {
        objQueue = new Queue<PoolContent>(maxCount);
        for (int i = 0; i < maxCount; i++)
        {
            PoolContent tmpObj = Instantiate(content);
            tmpObj.transform.parent = transform;
            tmpObj.transform.localPosition = new Vector3(100, 100, 0);
            objQueue.Enqueue(tmpObj);
        }
    }

    public void Collect(PoolContent _obj)
    {
        _obj.gameObject.SetActive(false);
        _obj.transform.parent = transform;
        objQueue.Enqueue(_obj);
    }

    public PoolContent Launch(Vector3 _pos)
    {
        if (objQueue.Count <= 0) return null;
        PoolContent tmpObj = objQueue.Dequeue();
        tmpObj.gameObject.SetActive(true);
        tmpObj.ShowInStage(_pos);
        return tmpObj;
    }
    public void ResetAll()
    {
        BroadcastMessage("HideFromStage", SendMessageOptions.DontRequireReceiver);
    }
}
