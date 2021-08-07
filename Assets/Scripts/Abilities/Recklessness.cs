using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Recklessness")]
public class Recklessness : ActiveAbility
{
    [SerializeField]
    private Effect recklessnessEffect;

    public override void Activate()
    {
        base.Activate();

        PlayerOnScene.Instance.stats.EffectController.
            AddEffect(Instantiate(recklessnessEffect), PlayerOnScene.Instance.stats);
    }

    protected override void Update()
    {
        base.Update();
    }
}
