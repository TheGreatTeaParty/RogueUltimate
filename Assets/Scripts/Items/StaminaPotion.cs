using UnityEngine;

[CreateAssetMenu(menuName = "Items/Potions/Stamina potion")]﻿
public class StaminaPotion : UsableItem
{
    [SerializeField] private int staminaBonus;
    [SerializeField] private Transform effectFX;

    public override void ModifyStats()
    {
        base.ModifyStats();
        PlayerStat.Instance.ModifyStamina(staminaBonus);
        KeepOnScene.instance.GetComponent<PlayerFX>().SpawnEffect(effectFX);
    }
    
}