﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatigueEffect : Effect
{
    protected float _slowDown;
    bool _isApplied;
    EnemyStat _enemy;
    AI enemeyAI;
    public FatigueEffect(EnemyStat enemy,float slowDown,float intensity, int ticks) : base(intensity, ticks)
    {
        _effectType = EffectType.Elemental;
        _slowDown = slowDown;
        _isApplied = false;
        _enemy = enemy;
        enemeyAI = _enemy.GetComponent<AI>();
    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        _enemy.TakeDamage(_intensity);

        //Here can be added aditional options:
        if (!_isApplied)
        {
            enemeyAI.attackCoolDown -= _slowDown;
        }
    }

    public override void RemoveEffect()
    {
        enemeyAI.movementSpeed += _slowDown;
    }
}
