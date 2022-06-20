using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class AdMobBanner : MonoBehaviour
{

    private BannerView bannerView;

    void Start()
    {
        _requestBanner();
    }

    private void _requestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-4751206041539571/8042883572";
#elif UNITY_IOS
        string adUnitId = "";
#else
        string adUnitId = "unexpected_platform";
#endif

        if(bannerView != null)
        {
            bannerView.Destroy();
        }

        AdSize adaptiveSize =
            AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Bottom);

        bannerView.OnAdLoaded += HandleAdLoaded;
        bannerView.OnAdFailedToLoad += HandleAdFailedToLoad;

        AdRequest adRequest = new AdRequest.Builder().Build();
        bannerView.LoadAd(adRequest);
            
    }


    #region Banner callback handlers

    public void HandleAdLoaded(object sender ,EventArgs args)
    {
        Debug.Log("success");
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) 
    {
        Debug.Log("Failed : " + args.LoadAdError);
    }

    #endregion


}
