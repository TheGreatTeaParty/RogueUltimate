using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traits/BeatenAsAChild")]
public class BeatenAsAChild : Trait
{
    public override void ApplyTrait()
    {
        CharacterManager.Instance.Stats.Strength.MaxHealth.AddModifier(new StatModifier(5, StatModifierType.Flat, this));
    }
    public override void DeleteTrait()
    {
        CharacterManager.Instance.Stats.Strength.MaxHealth.RemoveAllModifiersFromSource(this);
    }
}
