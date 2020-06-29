using UnityEngine;

[CreateAssetMenu(menuName = "Items/Potions/Mana potion")]﻿
public class ManaPotion : UsableItem
{
    [SerializeField] private int manaBonus;
    
    
    public override void ModifyStats()
    {
        base.ModifyStats();
        PlayerStat.Instance.ModifyMana(manaBonus);
    }
    
} 