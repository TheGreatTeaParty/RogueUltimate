using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaknessEffect : Effect
{
    protected float _damageReduction;
    bool _isApplied;
    EnemyStat _enemy;
    StatModifier statModifier;
    public WeaknessEffect(EnemyStat enemy, float damageReduction, float intensity, int ticks) : base(intensity, ticks)
    {
        _effectType = EffectType.Elemental;
        _damageReduction = damageReduction;
        _isApplied = false;
        _enemy = enemy;
        statModifier = new StatModifier(damageReduction, StatModifierType.Flat, this);
    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        //Here can be added aditional options:
        if (!_isApplied)
        {
            _enemy.PhysicalDamage.AddModifier(statModifier);
        }
    }

    public override void RemoveEffect()
    {
        _enemy.PhysicalDamage.RemoveModifier(statModifier);
    }
}
