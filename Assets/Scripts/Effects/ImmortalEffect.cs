﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/ImmortalEffect")]
public class ImmortalEffect : Effect
{
    private bool _isApplied = false;
    private float _lastHealthBeforeApply;

    public override void ApplyEffect()
    {
        base.ApplyEffect();



        if (!_isApplied)
        {
            _stat.PhysicalProtection.AddModifier(new StatModifier(10000f, StatModifierType.Flat));
            _lastHealthBeforeApply = PlayerOnScene.Instance.stats.CurrentHealth;
            _isApplied = true;
        }
    }

    public override void RemoveEffect()
    {
        PlayerOnScene.Instance.stats.CurrentHealth = _lastHealthBeforeApply;
        _stat.PhysicalProtection.RemoveLast();
        base.RemoveEffect();
    }
}
