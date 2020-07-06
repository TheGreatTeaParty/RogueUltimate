using System;
using UnityEngine;


public class HealthBar : Bar 
{
    private void Start()
    {
        SetMaxValue(PlayerStat.Instance.maxHealth);
    }

    private void Update()
    {
        SetCurrentValue(PlayerStat.Instance.currentHealth);
    }
    
}