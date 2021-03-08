using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traits/ExpandedMind")]
public class ExpandedMind : Trait
{
    public override void ApplyTrait()
    {
        CharacterManager.Instance.Stats.Intelligence.MaxMana.AddModifier(new StatModifier(10f, StatModifierType.Flat, this));
    }
    public override void DeleteTrait()
    {
        CharacterManager.Instance.Stats.Intelligence.MaxMana.RemoveAllModifiersFromSource(this);
    }
}
