using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Traits/Immunodeficiency")]
public class Immunodeficiency : Trait
{
    public override void ApplyTrait()
    {
        CharacterManager.Instance.Stats.Strength.PoisonResistance.AddModifier(new StatModifier(-0.5f, StatModifierType.PercentAdd));
    }
}
