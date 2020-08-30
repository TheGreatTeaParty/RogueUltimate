﻿using UnityEngine;

[CreateAssetMenu(menuName = "Items/Potions/Mana potion")]﻿
public class ManaPotion : UsableItem
{
    [SerializeField] private int manaBonus;
    [SerializeField] private Transform effectFX;


    public override void ModifyStats()
    {
        base.ModifyStats();
        PlayerStat.Instance.ModifyMana(manaBonus);
        KeepOnScene.instance.GetComponent<PlayerFX>().SpawnEffect(effectFX);
        //Sound
        AudioManager.Instance.Play("Bottle");
    }
    
} 