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
        base.ModifyStats();
        CharacterManager.Instance.Stats.EffectController.AddEffect(Instantiate(effect), CharacterManager.Instance.Stats);
        AudioManager.Instance.Play("Bottle");
    }
}

