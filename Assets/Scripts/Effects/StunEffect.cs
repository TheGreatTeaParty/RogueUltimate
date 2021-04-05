using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/StunEffect")]
public class StunEffect : Effect
{
    bool _isApplied = false;

    public override void ApplyEffect()
    {
        base.ApplyEffect();

        //Here can be added aditional options:
        if (!_isApplied)
        {
            _stat.AllowControll = false;
        }
    }

    public override void RemoveEffect()
    {
        _stat.AllowControll = true;
        base.RemoveEffect();
    }
}
