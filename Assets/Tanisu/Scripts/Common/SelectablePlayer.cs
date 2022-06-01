using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectablePlayer : MonoBehaviour
{

    [SerializeField] bool isLocked;

    public bool IsLocked()
    {
        return isLocked;
    }

    
}
