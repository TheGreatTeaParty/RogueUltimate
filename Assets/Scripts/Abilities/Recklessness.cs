﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Recklessness")]
public class Recklessness : ActiveAbility
{
    [SerializeField]
    private Effect _recklessnessEffect;

    public override void Activate()
    {
        base.Activate();

        PlayerOnScene.Instance.stats.EffectController.
            AddEffect(Instantiate(_recklessnessEffect), PlayerOnScene.Instance.stats);
    }

    protected override void Update()
    {
        base.Update();
    }
}
