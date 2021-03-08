using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traits/Restless")]
public class Restless : Trait
{
    public override void ApplyTrait()
    {
        CharacterManager.Instance.Stats.Agility.dodgeChance.AddModifier(new StatModifier(-0.5f, StatModifierType.PercentAdd, this));
    }
}
