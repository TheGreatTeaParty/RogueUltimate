using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/BleedEffect")]
public class BleedEffect : Effect
{
    public override void ApplyEffect()
    {
        base.ApplyEffect();
        _stat.TakeEffectDamage(_intensity);
        //Here can be added aditional options:
    }
}
