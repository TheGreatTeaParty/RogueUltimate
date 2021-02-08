using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPRegenEffect : Effect
{
    public SPRegenEffect(float intensity, int ticks) : base(intensity, ticks)
    {
        _effectType = EffectType.Natural;
    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        CharacterManager.Instance.Stats.ModifyStamina(_intensity);
    }
}
