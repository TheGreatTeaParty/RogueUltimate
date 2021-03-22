﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traits/FiveHead")]
public class FiveHead : Trait
{
    public override void ApplyTrait()
    {
        CharacterManager.Instance.Stats.Intelligence.MaxMana.AddModifier(new StatModifier(0.5f, StatModifierType.PercentAdd, this));
    }
    public override void DeleteTrait()
    {
        CharacterManager.Instance.Stats.Intelligence.MaxMana.RemoveAllModifiersFromSource(this);
    }
}