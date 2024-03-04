using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdmobBanner
{
    public void Init()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
        });
    }

    string _adUnitId = "ca-app-pub-3940256099942544/6300978111"; // Sample ID
    // string _adUnitId = "ca-app-pub-5716955240192450~2215979773"; // Real ID

    BannerView _bannerView;

    public void Show()
    {
        _bannerView.Show();
    }

    public void Hide()
    {
        _bannerView.Hide();
    }

    public void LoadAd()
    {
        if (_bannerView == null)
        {
            CreateBannerView();
        }
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        _bannerView.LoadAd(adRequest);
    }

    void CreateBannerView()
    {
        Debug.Log("Creating banner view");

        // If we already have a banner, destroy the old one.
        if (_bannerView != null)
        {
            DestroyAd();
        }

        // Create a 320x50 banner at top of the screen
        _bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Bottom);
    }

    void DestroyAd()
    {
        if (_bannerView != null)
        {
            _bannerView.Destroy();
            _bannerView = null;
        }
    }

}
