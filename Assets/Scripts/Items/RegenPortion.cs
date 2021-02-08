using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Potions/Regeneration potion")]
public class RegenPortion : UsableItem
{
    [SerializeField] private int EffectIntensity;
    [SerializeField] private int Ticks;

    public override void ModifyStats()
    {
        base.ModifyStats();
        CharacterManager.Instance.Stats.EffectController.AddEffect(new HPRegenEffect(EffectIntensity, Ticks));
        AudioManager.Instance.Play("Bottle");
    }
}

