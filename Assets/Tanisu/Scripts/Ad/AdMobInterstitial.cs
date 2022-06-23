using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdMobInterstitial : MonoBehaviour
{
    private InterstitialAd interstitial;

    void Start()
    {
        _requestInterstitial();
        Debug.Log("load start");
    }

    public void ShowAdMobInterstitial()
    {
        if(interstitial.IsLoaded() == true)
        {
            interstitial.Show();
        }
        else
        {
            Debug.Log("not ready");
        }
    }

    void _requestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-4751206041539571/8753330011";

#else
        string adUnitId = "unexpected_platform";
#endif
        interstitial = new InterstitialAd(adUnitId);

        interstitial.OnAdLoaded += HandleOnAdLoaded;
        interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        interstitial.OnAdClosed += HandleOnAdClose;

        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }

    public void HandleOnAdLoaded(object sender,EventArgs args)
    {
        Debug.Log("Loaded");
    }

    public void HandleOnAdFailedToLoad(object sender ,AdFailedToLoadEventArgs args)
    {
        Debug.Log("Failed : " + args.LoadAdError);
    }

    public void HandleOnAdClose(object sender,EventArgs args)
    {
        Debug.Log("Ad Close");
        interstitial.Destroy();
        _requestInterstitial();
        Debug.Log("Ad ReLoad");
    }
}
