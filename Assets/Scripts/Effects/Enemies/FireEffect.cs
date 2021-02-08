using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : Effect
{
    EnemyStat _enemy;

    public FireEffect(EnemyStat enemy, float intensity, int ticks) : base(intensity, ticks)
    {
        _effectType = EffectType.Elemental;
        _enemy = enemy;
    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        _enemy.TakeDamage(_intensity);
    }
}
