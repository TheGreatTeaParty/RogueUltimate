using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/PoisonEffect")]
public class PoisonEffect : Effect
{
    bool _isApplied = false;
    public override void ApplyEffect()
    {
        base.ApplyEffect();
        _stat.TakeEffectDamage(_intensity);
        if (!_isApplied)
        {
            _stat.ModifyMovementSpeed(0.4f);
            _isApplied = true;
        }
        //Here can be added aditional options:
    }
    public override void RemoveEffect()
    {
        _stat.ModifyMovementSpeed(-0.4f);
        base.RemoveEffect();
    }
}
