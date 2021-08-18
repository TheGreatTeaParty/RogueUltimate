using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/FuryEffect")]
public class FuryEffect : Effect
{
    private bool _isApplied = false;
    private float _lastHealthBeforeApply;

    public override void ApplyEffect()
    {
        base.ApplyEffect();

        if (!_isApplied)
        {
            _stat.PhysicalProtection.AddModifier(new StatModifier(10000f, StatModifierType.Flat,this));
            _lastHealthBeforeApply = PlayerOnScene.Instance.stats.CurrentHealth;
            _isApplied = true;
        }
    }

    public override void RemoveEffect()
    {
        PlayerOnScene.Instance.stats.CurrentHealth = _lastHealthBeforeApply;
        _stat.PhysicalProtection.RemoveAllModifiersFromSource(this);
        base.RemoveEffect();
    }
}
