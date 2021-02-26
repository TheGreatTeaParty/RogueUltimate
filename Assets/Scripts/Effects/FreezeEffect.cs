using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/FreezeEffect")]
public class FreezeEffect : Effect
{
    [Range(0f, 1f)]
    [SerializeField] protected float _slowDown;
    bool _isApplied = false;

    public override void ApplyEffect()
    {
        base.ApplyEffect();

        //Here can be added aditional options:
        if (!_isApplied)
        {
            _stat.ModifyMovementSpeed(_slowDown);
        }
    }

    public override void RemoveEffect()
    {
        _stat.ModifyMovementSpeed(0f);
        base.RemoveEffect();
    }
}
