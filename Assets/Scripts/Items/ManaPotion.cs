using UnityEngine;

[CreateAssetMenu(menuName = "Items/Potions/Mana potion")]﻿
public class ManaPotion : UsableItem
{
    [SerializeField] private int manaBonus;
    [SerializeField] private Transform effectFX;


    public override void ModifyStats()
    {
        base.ModifyStats();
        PlayerStat.Instance.ModifyMana(manaBonus);
        PlayerOnScene.Instance.playerFX.SpawnEffect(effectFX);
        //Sound
        AudioManager.Instance.Play("Bottle");
    }
    
} 