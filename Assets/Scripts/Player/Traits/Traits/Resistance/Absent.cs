using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traits/Absent")]
public class Absent : Trait
{
    public override void ApplyTrait()
    {
        CharacterManager.Instance.Stats.Strength.DazeResistance.AddModifier(new StatModifier(-0.5f, StatModifierType.PercentAdd));
    }
}
