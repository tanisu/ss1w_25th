using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeSample : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] Button[] buttons;
    string nextScene;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Start()
    {
        if (Config.I.startUp || SceneController.I.GetCurrentScene() == "TanisuScene")
        {
            StartCoroutine(FadeStartCO());
        }
        
    }



    IEnumerator FadeStartCO()
    {

        anim.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }



    public void ShowEndFade(string _next)
    {
        foreach (Button btn in buttons)
        {
            btn.enabled = false;
        }
        nextScene = _next;
        gameObject.SetActive(true);
        anim.SetTrigger("End");        
    }



    public void SceneChange()
    {
        Config.I.startUp = true;
        switch (nextScene)
        {
            case "TanisuScene":
                SceneController.I.StartGame();
                break;
            case "Title":
                SceneController.I.ToTitle();
                break;
            case "Ending":
                SceneController.I.ToEnding();
                break;
        }
        
        
    }
    
}
