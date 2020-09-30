using System;
using UnityEngine;


public class HealthBar : Bar
{
    private void Update()
    {
        SetCurrentValue(PlayerStat.Instance.CurrentHealth);
        SetMaxValue(PlayerStat.Instance.MaxHealth);
    }
    
}