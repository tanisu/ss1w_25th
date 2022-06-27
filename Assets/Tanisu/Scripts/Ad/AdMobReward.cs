using UnityEngine;
using GoogleMobileAds.Api;
using System;


public class AdMobReward : MonoBehaviour
{
    [SerializeField] PlayerSelector playerSelector;
    bool rewardeFlag = false;
    RewardedAd rewardedAd;
    string adUnitId,lockedName;


    private void Start()
    {
#if UNITY_ANDROID
        adUnitId = "ca-app-pub-4751206041539571/9962469904";
#else
        adUnitId = "";
#endif
        CreateAndLoadRewardedAd();

    }


    void Update()
    {
        if (rewardeFlag)
        {
            rewardeFlag = false;
            
            playerSelector.UnLockedPlayer(lockedName);
            CreateAndLoadRewardedAd();
        }
    }

    public void CreateAndLoadRewardedAd()
    {
        rewardedAd = new RewardedAd(adUnitId);
        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;

        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
        
    }

    public void ShowAdmobReward(string _name)
    {
        if (rewardedAd.IsLoaded())
        {
            
            lockedName = _name;
            
            rewardedAd.Show();
        }
        else
        {
           // Debug.Log("not loaded");
        }
    }

    public void HandleRewardedAdLoaded(object sender,EventArgs args)
    {
       // Debug.Log("reward loaded");

    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
       // Debug.Log("Failed : " + args.LoadAdError);
    }

    public void HandleRewardedAdClosed(object sender,EventArgs args)
    {
      //  Debug.Log("Cancel");
    }

    public void HandleUserEarnedReward(object sender,Reward args)
    {

        rewardeFlag = true;
    }
}
