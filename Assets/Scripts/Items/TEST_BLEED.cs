using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Potions/BLEED potion")]
public class TEST_BLEED : UsableItem
{
    [SerializeField] private int EffectIntensity;
    [SerializeField] private int Ticks;


    public override void ModifyStats()
    {
        base.ModifyStats();
        CharacterManager.Instance.Stats.EffectController.AddEffect(new BleedEffect(EffectIntensity, Ticks));
        AudioManager.Instance.Play("Bottle");
    }
}