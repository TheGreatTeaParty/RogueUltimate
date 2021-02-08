using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : Effect
{
    public PoisonEffect(float intensity, int ticks) : base(intensity, ticks)
    {
        _effectType = EffectType.Physical;
    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        CharacterManager.Instance.Stats.ModifyHealth(-_intensity);
        //Here can be added aditional options:
    }
}
