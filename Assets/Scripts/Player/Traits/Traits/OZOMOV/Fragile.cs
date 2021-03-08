﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traits/Fragile")]
public class Fragile : Trait
{
    public override void ApplyTrait()
    {
        CharacterManager.Instance.Stats.Strength.MaxHealth.AddModifier(new StatModifier(-0.5f, StatModifierType.PercentAdd, this));
    }
    public override void DeleteTrait()
    {
        CharacterManager.Instance.Stats.Strength.MaxHealth.RemoveAllModifiersFromSource(this);
    }
}
