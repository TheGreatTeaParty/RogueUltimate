﻿using UnityEngine;

[CreateAssetMenu(menuName = "Items/Potions/Stamina potion")]﻿
public class StaminaPotion : UsableItem
{
    [SerializeField] private int staminaBonus;
    [SerializeField] private Transform effectFX;

    public override void ModifyStats()
    {
        base.ModifyStats();
        CharacterManager.Instance.Stats.ModifyStamina(staminaBonus);
        PlayerOnScene.Instance.GetComponent<PlayerFX>().SpawnEffect(effectFX);
        AudioManager.Instance.Play("Bottle");
    }
    
}