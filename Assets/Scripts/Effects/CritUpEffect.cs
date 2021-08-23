using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Effects/Backstab")]
public class CritUpEffect : Effect
{
    private bool _isApplied = false;

    public override void ApplyEffect()
    {
        base.ApplyEffect();

        var _stats = PlayerOnScene.Instance.stats;

        if(!_isApplied)
        {
            _stats.Agility.CritChance.AddModifier(new StatModifier(
                (5 * _stats.Agility.GetBaseValue()) / 100, StatModifierType.PercentAdd, this));
            _stats.Strength.CritDamage.AddModifier(new StatModifier(
                (10 * _stats.Agility.GetBaseValue()) / 100, StatModifierType.PercentAdd, this));
        }
    }

    public override void RemoveEffect()
    {
        var _stats = PlayerOnScene.Instance.stats;

        _stats.Agility.CritChance.RemoveAllModifiersFromSource(this);
        _stats.Strength.CritDamage.RemoveAllModifiersFromSource(this);
        base.RemoveEffect();
    }
}
