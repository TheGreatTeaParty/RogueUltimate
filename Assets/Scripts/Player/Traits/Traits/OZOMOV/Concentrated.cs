﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traits/Concentrated")]
public class Concentrated : Trait
{
    public override void ApplyTrait()
    {
        CharacterManager.Instance.Stats.Agility.MaxStamina.AddModifier(new StatModifier(0.5f, StatModifierType.PercentAdd, this));
    }
    public override void DeleteTrait()
    {
        CharacterManager.Instance.Stats.Agility.MaxStamina.RemoveAllModifiersFromSource(this);
    }
}
