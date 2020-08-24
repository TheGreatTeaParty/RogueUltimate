using UnityEngine;

[CreateAssetMenu(menuName = "Items/Potions/Health potion")]﻿
public class HealthPotion : UsableItem
{
    [SerializeField] private int healthBonus;
    [SerializeField] private Transform healFX;
    
    
    public override void ModifyStats()
    {
        base.ModifyStats();
        PlayerStat.Instance.ModifyHealth(healthBonus);
        KeepOnScene.instance.GetComponent<PlayerFX>().SpawnEffect(healFX);
    }
    
}
