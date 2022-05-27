using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectablePlayer : MonoBehaviour
{
    Button choiceButton;
    [SerializeField] bool isLocked;
    PlayerSelector playerSelector;

    void Start()
    {
        playerSelector = GameObject.FindWithTag("PlayerSelector").GetComponent<PlayerSelector>();
        choiceButton = GetComponent<Button>();
        if (isLocked)
        {
            choiceButton.interactable = !isLocked;
        }
        choiceButton.onClick.AddListener(() => { 
            Config.I.SelectPlayer(gameObject.name);
            playerSelector.ChangePlayerSprite();
        });
    }

    public bool IsLocked()
    {
        return isLocked;
    }

    
}
