using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class MobAdsReward : MonoBehaviour
{
    private RewardedAd rewardedAd;

    private const string rewardUnitId = "ca-app-pub-3940256099942544/5354046379";

    private void OnEnable()
    {
        rewardedAd = new RewardedAd(rewardUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(adRequest);

        rewardedAd.OnUserEarnedReward += HandleUserEearnReward;
    }
    private void OnDisable()
    {
        rewardedAd.OnUserEarnedReward -= HandleUserEearnReward;
    }

    public void ShowRewardAd()
    {
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
            Debug.Log("Displaying");
        }
        else
        {
            Debug.Log("Ad does not load");
        }
    }


    public void HandleUserEearnReward(object sender, Reward reward)
    {
        AccountManager.Instance.Renown += 150;
    }
}