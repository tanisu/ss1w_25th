using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectablePlayer : MonoBehaviour
{

    [SerializeField] bool isLocked;
    Button btn;
    AdMobReward reward;
    private void Start()
    {
        btn = GetComponent<Button>();
        reward = GameObject.FindWithTag("Admob").GetComponent<AdMobReward>() ;
        btn.onClick.AddListener(() => {
            if(isLocked == true)
            {
                
                reward.ShowAdmobReward(gameObject.name);
            }
            
        });
    }

    public bool IsLocked()
    {
        return isLocked;
    }

    public void UnLock()
    {
        isLocked = false;
    }
    
}
