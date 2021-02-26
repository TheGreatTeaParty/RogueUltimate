using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Potions/Effect Disolver")]
public class EffectDisolver : UsableItem
{
    [SerializeField] EffectType effectType;

    public override void ModifyStats()
    {
        base.ModifyStats();
        CharacterManager.Instance.Stats.EffectController.RemoveEffectsOfType(effectType);
        AudioManager.Instance.Play("Bottle");
    }
}
