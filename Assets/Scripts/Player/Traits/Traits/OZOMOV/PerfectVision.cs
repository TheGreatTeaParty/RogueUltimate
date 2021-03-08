using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traits/PerfectVision")]
public class PerfectVision : Trait
{
    public override void ApplyTrait()
    {
        CharacterManager.Instance.Stats.Agility.MaxStamina.AddModifier(new StatModifier(10f, StatModifierType.Flat, this));
    }
    public override void DeleteTrait()
    {
        CharacterManager.Instance.Stats.Agility.MaxStamina.RemoveAllModifiersFromSource(this);
    }
}
