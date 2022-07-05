using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerSelector : MonoBehaviour
{
    [SerializeField]
    Button increment, decrement;
    [SerializeField] float slideRange = 100f;
    [SerializeField] Transform playerImages;
    [SerializeField] GameObject locked;
    [SerializeField] float effectTime;
    List<SelectablePlayer> selectablePlayer;
    List<Image> images;
    int currentPlayer = 0;
    bool isMoving;
    

    void Start()
    {
        selectablePlayer = new List<SelectablePlayer>(playerImages.GetComponentsInChildren<SelectablePlayer>());
        images = new List<Image>();
        for(int i = 0;i < selectablePlayer.Count; i++)
        {
            
            if(Array.Exists(Config.I.unlockedPlayers,x => x == selectablePlayer[i].name))
            {
                selectablePlayer[i].UnLock();
            }
            selectablePlayer[i].transform.localPosition = new Vector3(i * -slideRange, selectablePlayer[i].transform.localPosition.y);
            images.Add(selectablePlayer[i].GetComponent<Image>());
            if (Config.I.selectedPlayerData.name == selectablePlayer[i].name)
            {

                currentPlayer = i;
                images[i].color = new Color(1, 1, 1, 1);
                _setDefaultPlayerView();
            }
            else
            {
                images[i].raycastTarget = false;
                images[i].color = new Color(1, 1, 1, 0);
            }
        }
        
        increment.onClick.AddListener(() => _incrementPlayer());
        decrement.onClick.AddListener(() => _decrementPlayer());
        _interactableArrowButton();



    }

    void _incrementPlayer()
    {

        if (currentPlayer >= images.Count - 1 || isMoving) {
            
            return;
        } 
        _slidePlayerView( 1 );
        
        _interactableArrowButton();
    }

    void _decrementPlayer()
    {
        if (currentPlayer <= 0 || isMoving)
        {
            
            return;
        }
            
        _slidePlayerView(-1);
        _interactableArrowButton();

    }
    void _interactableArrowButton()
    {
        if(currentPlayer == 0)
        {
            
            decrement.interactable = false;
            return;
        }
        if(currentPlayer >= images.Count - 1)
        {
            
            increment.interactable = false;
            return;
        }
        decrement.interactable = true;
        increment.interactable = true;
    }

    void _setDefaultPlayerView()
    {
        playerImages.localPosition = new Vector3(slideRange * currentPlayer, -7.7f);
    }

    void _slidePlayerView(int _x)
    {
        isMoving = true;
        images[currentPlayer].raycastTarget = false;
        images[currentPlayer].DOFade(0f, effectTime).SetUpdate(true);
        currentPlayer += _x;
        SoundManager.I.PlaySE(SESoundData.SE.TAP_CURSOR);
        if (selectablePlayer[currentPlayer].IsLocked())
        {
            locked.SetActive(true);
        }
        else
        {
            Config.I.SelectPlayer(selectablePlayer[currentPlayer].gameObject.name);
            if(SceneController.I.GetCurrentScene() == "Title")
            {
                Title.I.ChangePlayerSprite();
            }
            else
            {
                
                GameManager.I.ChangePlayerSprite();
            }
            
            locked.SetActive(false);
        }
        
        images[currentPlayer].DOFade(1f, effectTime).SetLink(images[currentPlayer].gameObject).OnComplete(() => images[currentPlayer].raycastTarget = true).SetUpdate(true); ;
        playerImages.transform.DOLocalMoveX(playerImages.localPosition.x + (slideRange * _x )  , effectTime)
            .OnComplete(()=>isMoving = false).SetUpdate(true).SetLink(playerImages.gameObject);
        
    }

    public void UnLockedPlayer(string _name)
    {
        foreach(SelectablePlayer player in selectablePlayer)
        {
            if(player.name == _name)
            {
                player.UnLock();
                Config.I.SetUnlockPlayer(_name);

                _slidePlayerView(0);
            }
        }
    }
}
