using UnityEngine;

[CreateAssetMenu(menuName = "Items/HealthPotion")]﻿
public class HealthPotion : UsableItem
{
    [SerializeField] private int value;
    
    public override void ModifyStats()
    {
        base.ModifyStats();
        PlayerStat.Instance.CurrentHealth += value;
    }
    
}
