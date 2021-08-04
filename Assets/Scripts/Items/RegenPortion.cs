using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Potions/Regeneration potion")]
public class RegenPortion : UsableItem
{
    [SerializeField] private int EffectIntensity;
    [SerializeField] private int Ticks;
    [SerializeField] Effect effect;

    public override void ModifyStats()
    {
        var newEffect = Instantiate(effect);
        newEffect.Ticks = Ticks;
        newEffect.Intensity = EffectIntensity;
        newEffect.Icon = sprite;
        CharacterManager.Instance.Stats.EffectController.AddEffect(newEffect, CharacterManager.Instance.Stats);
        AudioManager.Instance.Play("Bottle");
    }
}

