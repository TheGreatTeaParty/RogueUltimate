using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class MobAdsSimple : MonoBehaviour
{
    private InterstitialAd interstitialAd;

    private const string interstitialAdId = "ca-app-pub-3940256099942544/8691691433";

    private void OnEnable()
    {
        interstitialAd = new InterstitialAd(interstitialAdId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(adRequest);
    }
    public void ShowAd()
    {
        StartCoroutine(Show());
    }
    private IEnumerator Show()
    {
        yield return new WaitForSeconds(1f);
        if (interstitialAd.IsLoaded())
            interstitialAd.Show();
    }
}
