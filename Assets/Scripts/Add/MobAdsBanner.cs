using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class MobAdsBanner : MonoBehaviour
{
    private BannerView bannerView;
    private const string bannerId = "ca-app-pub-3940256099942544/6300978111";

    private void OnEnable()
    {
        bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.BottomLeft);
        AdRequest adRequest = new AdRequest.Builder().Build();
        bannerView.LoadAd(adRequest);
        bannerView.Show();
    }
    private void OnDisable()
    {
        bannerView.Hide();
    }
}