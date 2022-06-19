using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    [SerializeField] Tongue tongue;

    public void SwitchEating()
    {
        tongue.Eating();
    }
}
