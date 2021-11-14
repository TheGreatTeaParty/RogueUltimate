using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/RecklessnessEffect")]
public class RecklessnessEffect : Effect
{
    private bool _isApplied = false;

    public override void ApplyEffect()
    {
        base.ApplyEffect();

        var player = PlayerOnScene.Instance.stats;

        if(!_isApplied)
        {
            _stat.PhysicalDamage.AddModifier(new StatModifier(
               (int)(0.015* player.Strength.GetBaseValue()/ (1 + 0.015 * player.Strength.GetBaseValue()) * 100),
               StatModifierType.Flat, this));
            _stat.PhysicalProtection.AddModifier(new StatModifier(
                -(30)/ 100, StatModifierType.PercentAdd,this));
            _isApplied = true;
        }
    }
    public override void RemoveEffect()
    {
        _stat.PhysicalDamage.RemoveAllModifiersFromSource(this);
        _stat.PhysicalProtection.RemoveAllModifiersFromSource(this);
        base.RemoveEffect();
    }
}
