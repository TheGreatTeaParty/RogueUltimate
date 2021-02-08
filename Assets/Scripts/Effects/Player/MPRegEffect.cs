using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPRegEffect : Effect
{
    public MPRegEffect(float intensity, int ticks) : base(intensity, ticks)
    {
        _effectType = EffectType.Natural;
    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        CharacterManager.Instance.Stats.ModifyMana(_intensity);
    }
}
