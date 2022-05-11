using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Color[] colors;
    SpriteRenderer sp;

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        int idx = Random.Range(0, colors.Length);
        sp.color = colors[idx];
        //StartCoroutine(_setColor());

    }

    IEnumerator _setColor()
    {
        yield return new WaitForSeconds(0.1f);
        int idx = Random.Range(0, colors.Length);
        sp.color = colors[idx];
    }
}
