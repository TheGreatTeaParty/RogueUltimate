using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traits/CoagulationDisorder")]
public class CoagulationDisorder : Trait
{
    public override void ApplyTrait()
    {
        CharacterManager.Instance.Stats.Strength.BleedResistance.AddModifier(new StatModifier(-0.5f, StatModifierType.PercentAdd));
    }
}
