using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class MobAdsSimple : MonoBehaviour
{
    private InterstitialAd interstitialAd;

    private const string interstitialAdId = "ca-app-pub-3581850693025727/4172125900";

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
        {
            interstitialAd.Show();
           // FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventScreenView, new Parameter(FirebaseAnalytics.ParameterScreenName, "simple_ad"));
        }
    }
}
