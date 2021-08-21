using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Abilities/CritUp")]
public class CritUpAbility : ActiveAbility
{
    [SerializeField]
    private Effect _backstabEffect;
    public override void Activate()
    {
        base.Activate();

        PlayerOnScene.Instance.stats.EffectController.
            AddEffect(Instantiate(_backstabEffect), PlayerOnScene.Instance.stats);
    }

    protected override void Update()
    {
        base.Update();
    }
}
