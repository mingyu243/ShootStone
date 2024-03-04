using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    AdmobBanner _admobBanner = new AdmobBanner();

    private void Start()
    {
        _admobBanner.Init();
        _admobBanner.LoadAd();

        _admobBanner.Show();
    }

    public void Show()
    {
        _admobBanner.Show();
    }

    public void Hide()
    {
        _admobBanner.Hide();
    }
}
