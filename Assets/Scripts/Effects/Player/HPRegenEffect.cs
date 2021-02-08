using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPRegenEffect : Effect
{
    public HPRegenEffect(float intensity, int ticks):base(intensity,ticks)
    {
        _effectType = EffectType.Natural;
    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        CharacterManager.Instance.Stats.ModifyHealth(_intensity);
    }
}
