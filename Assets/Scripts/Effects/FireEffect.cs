using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Effects/FireEffect")]
public class FireEffect : Effect
{
    public override void ApplyEffect()
    {
        base.ApplyEffect();
        _stat.TakeEffectDamage(_intensity);
    }
}
