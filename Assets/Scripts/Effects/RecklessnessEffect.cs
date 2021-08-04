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
                (20 * player.Strength.GetBaseValue()) / 100, StatModifierType.PercentAdd));
            _stat.PhysicalProtection.AddModifier(new StatModifier(
                (20 * player.Strength.GetBaseValue()) - 5 * player.Strength.GetBaseValue() / 100, StatModifierType.PercentAdd));
        }
    }
}
