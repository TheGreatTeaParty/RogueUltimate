using System;
using UnityEngine;


public class HealthBar : Bar
{
    private void Update()
    {
        SetCurrentValue(PlayerStat.Instance.GetCurrentHealth());
        SetMaxValue(PlayerStat.Instance.GetMaxHealth());
    }
    
}