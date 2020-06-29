using UnityEngine;

[CreateAssetMenu(menuName = "Items/Potions/Health potion")]﻿
public class HealthPotion : UsableItem
{
    [SerializeField] private int healthBonus;
    
    
    public override void ModifyStats()
    {
        base.ModifyStats();
        PlayerStat.Instance.ModifyHealth(healthBonus);
    }
    
}
