using UnityEngine;

[CreateAssetMenu(menuName = "Items/Potions/Mana potion")]﻿
public class ManaPotion : UsableItem
{
    [SerializeField] private int manaBonus;
    [SerializeField] private Transform effectFX;


    public override void ModifyStats()
    {
        base.ModifyStats();
        CharacterManager.Instance.Stats.ModifyMana(manaBonus);
        PlayerOnScene.Instance.playerFX.SpawnEffect(effectFX);
        AudioManager.Instance.Play("Bottle");
    }
    
} 