using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/PoisonEffect")]
public class PoisonEffect : Effect
{
    public override void ApplyEffect()
    {
        base.ApplyEffect();
        _stat.TakeEffectDamage(_intensity);
        //Here can be added aditional options:
    }
}
