using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    #region Public Variables
    #endregion

    #region Private Variables
    private BannerView bview;
    //private string appID = "ca-app-pub-6818619256470654~7001355373"; //My AppID.
    private string appID = "ca-app-pub-3940256099942544~3347511713"; //Test AppID.

    //private string bannerID = "ca-app-pub-6818619256470654/4071939700"; //My BannerID.
    private string bannerID = "ca-app-pub-3940256099942544/6300978111"; //Test BannerID.
    //private static AdManager instance;
    #endregion

    #region Callbacks
    void Awake()
    {
        //instance = this;
        DontDestroyOnLoad(gameObject);
        MobileAds.Initialize(appID);
    }
    #endregion

    #region My Functions
    public void ShowBanner()
    {
        RequestBanner();
    }
    private void RequestBanner()
    {
        if (bview != null)
        {
            bview.Destroy();
            Debug.LogWarning("Advert Destryoed");

        }

        bview = new BannerView(bannerID, AdSize.Banner, AdPosition.Top);
        Debug.LogWarning("Creating Banner");


        //Called when an ad request has successfully loaded.
        bview.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        bview.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        bview.OnAdOpening += HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        bview.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        bview.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        AdRequest request = new AdRequest.Builder().Build();
        Debug.LogWarning("Requesting Advert");

        bview.LoadAd(request);
        Debug.LogWarning("Advert Loaded");
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        Debug.LogWarning("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.LogWarning("Banner failed to load: " + args.Message);
        // Handle the ad failed to load event.    
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        Debug.LogWarning("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        Debug.LogWarning("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        Debug.LogWarning("HandleAdLeavingApplication event received");
    }
    #endregion
}
