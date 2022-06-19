using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    public bool eating;

    public void Eating()
    {
        eating = !eating;
    }
}
