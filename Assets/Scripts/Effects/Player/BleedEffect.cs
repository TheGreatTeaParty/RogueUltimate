using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedEffect : Effect
{
    
    public BleedEffect(float intensity, int ticks) : base(intensity, ticks)
    {
        _effectType = EffectType.Physical;
    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        CharacterManager.Instance.Stats.TakeDamage(_intensity);
        //Here can be added aditional options:
    }
}
